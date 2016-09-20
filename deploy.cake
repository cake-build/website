#tool "nuget:https://www.nuget.org/api/v2/?package=KuduSync.NET"
#addin "nuget:https://www.nuget.org/api/v2/?package=Cake.Kudu"
///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////
var target          = Argument<string>("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
FilePath        solutionPath        = MakeAbsolute(File("./src/Cake.Web.sln"));
FilePath        addinsXmlPath       = MakeAbsolute(File("./src/Cake.Web/App_Data/addins.xml"));
DirectoryPath   websitePublishPath  = MakeAbsolute(Directory("./src/Cake.Web"));
DirectoryPath   addinPath           = MakeAbsolute(Directory("./src/Cake.Web/App_Data/libs"));
DirectoryPath   deploymentPath;


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
    Information("Running tasks...");
});

Teardown(ctx =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
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

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() =>
{
    // Build target web & tests.
    Information("Building web {0}...", solutionPath);
    MSBuild(solutionPath, settings =>
        settings.SetPlatformTarget(PlatformTarget.MSIL)
            .WithProperty("TreatWarningsAsErrors","true")
            .WithTarget("Build")
            .SetConfiguration(configuration));
});

Task("Prefetch-Addins")
    .IsDependentOn("Build")
    .Does(() =>
{
    Information("Parsing {0}...", addinsXmlPath);
    var addinIds = (
                    from addins in System.Xml.Linq.XDocument.Load(addinsXmlPath.FullPath).Elements("Addins")
                    from addin in addins.Elements("Addin")
                    from nuget in addin.Elements("NuGet")
                    from id in nuget.Attributes("Id")
                    select id.Value
                ).ToArray();
    Information("Found {0} addins.", addinIds.Length);

    foreach(var addinId in addinIds)
    {
        try
        {
            Information("Installing addin {0}...", addinId);
            NuGetInstall(addinId, new NuGetInstallSettings {
                ExcludeVersion  = true,
                OutputDirectory = addinPath,
                Source          = new [] { "https://api.nuget.org/v3/index.json" },
                Verbosity       = NuGetVerbosity.Quiet
            });
        }
        catch
        {
            Information("Failed to install addin {0}.", addinId);
        }
     }
});

Task("Publish")
    .IsDependentOn("Prefetch-Addins")
    .Does(() =>
{
    DeleteDirectory("./src/Cake.Web/obj", recursive:true);

    Information("Deploying web from {0} to {1}...", websitePublishPath, deploymentPath);
    Kudu.Sync(websitePublishPath);
});


Task("Default")
    .IsDependentOn("Publish");


///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);