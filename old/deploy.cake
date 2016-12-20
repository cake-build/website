#tool "nuget:https://www.nuget.org/api/v2/?package=KuduSync.NET"
#addin "nuget:https://www.nuget.org/api/v2/?package=Cake.Kudu"
#addin "nuget:https://www.nuget.org/api/v2/?package=Cake.Git"
using System.Xml.Linq;

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////
var target          = Argument<string>("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
DirectoryPath   outputPath          = MakeAbsolute(Directory("./output"));
DirectoryPath   websitePublishPath  = outputPath.Combine("_PublishedWebsites/Cake.Web");
DirectoryPath   packagesPath        = outputPath.Combine("packages");
DirectoryPath   addinPath           = websitePublishPath.Combine("App_Data/libs");
DirectoryPath   deploymentPath;

FilePath        solutionPath        = MakeAbsolute(File("./src/Cake.Web.sln"));
FilePath        projectPath         = MakeAbsolute(File("./src/Cake.Web/Cake.Web.csproj"));
FilePath        addinsXmlPath       = addinPath.CombineWithFilePath("../addins.xml").Collapse();
FilePath        packagesConfigPath  = addinPath.CombineWithFilePath("package.config").Collapse();
FilePath        solutionInfoPath    = MakeAbsolute(File("./src/SolutionInfo.cs"));
FilePath        webConfigPath       = websitePublishPath.CombineWithFilePath("web.config");

DateTime        utcNow              = DateTime.UtcNow;
string          version             = string.Format(
                                            "{0}.{1}.{2}.{3}{4:00}",
                                            utcNow.Year,
                                            utcNow.Month,
                                            utcNow.Day,
                                            utcNow.Hour,
                                            utcNow.Minute
                                        );
string          semVersion          = string.Format(
                                            "{0}+{1}.{2}.{3}",
                                            version,
                                            GitBranchCurrent("./").FriendlyName,
                                            GitLogTip("./").Sha,
                                            System.Environment.MachineName
                                        );

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
    if (!Kudu.IsRunningOnKudu)
    {
        throw new Exception("Not running on Kudu");
    }

    deploymentPath = Kudu.Deployment.Target;

    if (!DirectoryExists(deploymentPath))
    {
        throw new DirectoryNotFoundException(
            string.Format(
                "Deployment target directory not found {0}.",
                deploymentPath
                )
            );
    }

    // Executed BEFORE the first task.
    Information("Building version {0}...", semVersion);
});

Teardown(ctx =>
{
    // Executed AFTER the last task.
    Information("Done building version {0}.", semVersion);
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    //Clean up any artifacts from previous builds
    CleanDirectories("./src/**/" + configuration + "/bin");
    CleanDirectories("./src/**/" + configuration + "/obj");
    CleanDirectories(new [] {
        outputPath,
        websitePublishPath,
        packagesPath,
        addinPath,
        "./src/Cake.Web/bin",
        "./src/Cake.Web/obj"
        });
});

Task("Restore")
    .Does(() =>
{
    // Restore all NuGet packages.
    Information("Restoring {0}...", solutionPath);
    NuGetRestore(solutionPath);
});

Task("Create-Solution-Info")
    .Does(() =>
{
    var assemblyInfoSettings  =  new AssemblyInfoSettings {
            Version                 = version,
            FileVersion             = version,
            InformationalVersion    = semVersion,
            Copyright               = "Copyright (c) .NET Foundation and Contributors"
        };
    CreateAssemblyInfo(solutionInfoPath, assemblyInfoSettings);
});


Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Create-Solution-Info")
    .Does(() =>
{
    // Build target web & tests.
    Information("Building web {0}...", projectPath);

    MSBuild(projectPath, settings =>
        settings.SetPlatformTarget(PlatformTarget.MSIL)
            .WithProperty("TreatWarningsAsErrors","true")
            .WithProperty("OutDir", outputPath.FullPath + "/")
            .WithTarget("Build")
            .WithTarget("ResolveReferences")
            .WithTarget("_CopyWebApplication")
            .SetConfiguration(configuration)
            .SetVerbosity(Verbosity.Minimal));
});

Task("Transform-Warmup")
    .IsDependentOn("Build")
    .Does(() =>
{
    XmlPoke(webConfigPath, "/configuration/system.webServer/applicationInitialization/add[@initializationPage = '/']/@hostName", Kudu.WebSite.HostName);
});

Task("Prefetch-Addins")
    .IsDependentOn("Transform-Warmup")
    .Does(() =>
{
    Information("Parsing {0}...", addinsXmlPath);
    var addinIds = new []{
            "Cake",
            "Cake.Common",
            "Cake.Core",
            "Cake.Testing"
        }.Concat(
                    from addins in XDocument.Load(addinsXmlPath.FullPath).Elements("Addins")
                    from addin in addins.Elements("Addin")
                    from nuget in addin.Elements("NuGet")
                    from id in nuget.Attributes("Id")
                    where !string.IsNullOrEmpty(id.Value)
                    select id.Value
                ).ToArray();

    Information("Found {0} addins.", addinIds.Length);

    System.Threading.Tasks.Parallel.ForEach(
        addinIds,
        new ParallelOptions { MaxDegreeOfParallelism = 20 },
        addinId => {
            var addInVersionUrl = string.Format("https://www.nuget.org/api/v2/Packages()?$filter=Id%20eq%20'{0}'%20and%20IsAbsoluteLatestVersion%20eq%20true",
                                    Uri.EscapeDataString(addinId));
            string addinVersion;
            try
            {
                addinVersion = (
                    from feed in XDocument.Load(addInVersionUrl).Elements()
                    from entry in feed.Elements().Where(element => element.Name.LocalName == "entry")
                    from properties in entry.Elements().Where(element => element.Name.LocalName == "properties")
                    from version in properties.Elements().Where(element => element.Name.LocalName == "Version")
                    select version.Value
                ).FirstOrDefault();
            }
            catch(Exception ex)
            {
                addinVersion = null;
                Information("Failed to fetch version for addin {0} ({1})...", addinId, ex.Message);
            }

            try
            {
                Information("Installing addin {0} ({1})...", addinId, addinVersion ?? "*null*");
                NuGetInstall(addinId, new NuGetInstallSettings {
                            Version                 = addinVersion,
                            ExcludeVersion          = true,
                            NoCache                 = true,
                            OutputDirectory         = addinPath,
                            Source                  = new [] { "https://api.nuget.org/v3/index.json" },
                            Verbosity               = NuGetVerbosity.Quiet,
                            Prerelease              = true,
                            EnvironmentVariables    = new Dictionary<string, string>{
                                {"EnableNuGetPackageRestore", "true"},
                                {"NUGET_XMLDOC_MODE", "None"},
                                {"NUGET_PACKAGES", packagesPath.FullPath},
                                {"NUGET_EXE",  Context.Tools.Resolve("nuget.exe").FullPath }
                    }});
            }
            catch(Exception ex)
            {
                Information("Failed to install addin {0} ({1}, {2})...", addinId, addinVersion, ex.Message);
            }
        });
});

Task("Publish")
    .IsDependentOn("Prefetch-Addins")
    .Does(() =>
{
    Information("Deploying web from {0} to {1}...", websitePublishPath, deploymentPath);
    Kudu.Sync(websitePublishPath);
});


Task("Default")
    .IsDependentOn("Publish");


///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);