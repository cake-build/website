#module nuget:?package=Cake.DotNetTool.Module&version=0.4.0
#tool "dotnet:https://api.nuget.org/v3/index.json?package=Wyam.Tool&version=2.2.9"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Wyam&version=2.2.9"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Yaml&version=3.1.1"
#addin "nuget:https://api.nuget.org/v3/index.json?package=YamlDotNet&version=6.1.2"

#load "nuget.cake"
#load "github.cake"

using Octokit;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define variables
var isRunningOnAppVeyor  = AppVeyor.IsRunningOnAppVeyor;
var isPullRequest        = AppVeyor.Environment.PullRequest.IsPullRequest;
var deployRemote         = EnvironmentVariable("git_deploy_remote");
var zipFileName          = "output.zip";
var deployCakeFileName   = "deploy.cake";

// Define directories.
var releaseDir           = Directory("./release");
var cakeSourceDir        = releaseDir + Directory("cake-repo");
var cakeContribSourceDir = releaseDir + Directory("cake-contrib-repo");
var extensionDir         = releaseDir + Directory("extensions");
var outputPath           = MakeAbsolute(Directory("./output"));
var rootPublishFolder    = MakeAbsolute(Directory("publish"));

// Definitions
class MaintainerSpec
{
    public string Name { get; set; }
    public string GitHubUserName { get; set; }
    public string GitHubID { get; set; }
    public string Website { get; set; }
    public string Twitter { get; set; }
    public string LinkedIn { get; set; }
}

class ExtensionSpec
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string NuGet { get; set; }
    public List<string> Assemblies { get; set; }
    public string Repository { get; set; }
    public string ProjectUrl { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public List<string> Categories { get; set; }
    public string AnalyzedPackageVersion { get; set; }
    public string AnalyzedPackagePublishDate { get; set; }
    public bool AnalyzedPackageIsPrerelease { get; set; }
    public string TargetCakeVersion { get; set; }
    public List<string> TargetFrameworks { get; set; }
    public int ComplianceScore { get; set; }
    public List<string> ComplianceNotes { get; set; }
}

// Variables
List<MaintainerSpec> maintainerSpecs = new List<MaintainerSpec>();
List<ExtensionSpec> extensionSpecs = new List<ExtensionSpec>();
List<string> assemblies = new List<string>();

//////////////////////////////////////////////////////////////////////
// SETUP
//////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
    // Executed BEFORE the first task.
    Information("Building site...");
});


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("CleanSource")
    .Does(() =>
{
    if(DirectoryExists(cakeSourceDir))
    {
        CleanDirectory(cakeSourceDir);
        DeleteDirectory(cakeSourceDir, new DeleteDirectorySettings {
            Recursive = true,
            Force = true
        });
    }

    if(DirectoryExists(cakeContribSourceDir))
    {
        CleanDirectory(cakeContribSourceDir);
        DeleteDirectory(cakeContribSourceDir, new DeleteDirectorySettings {
            Recursive = true,
            Force = true
        });
    }

    foreach(var cakeDir in GetDirectories(releaseDir.Path.FullPath + "/cake*"))
    {
        DeleteDirectory(cakeDir, new DeleteDirectorySettings {
            Recursive = true,
            Force = true
        });
    }
});

Task("GetSource")
    .IsDependentOn("CleanSource")
    .Does(context =>
    {
        var releaseInfo = context.GetCakeGitHubReleaseInfo(releaseDir);

        FilePath releaseZip = DownloadFile(releaseInfo.LatestReleaseZipUrl);
        Unzip(releaseZip, releaseDir);

        // Need to rename the container directory in the zip file to something consistent
        var containerDir = GetDirectories(releaseDir.Path.FullPath + "/*").First(x => x.GetDirectoryName().StartsWith("cake"));
        MoveDirectory(containerDir, cakeSourceDir);

        // Download cake-contrib Home repository
        FilePath cakeContribReleaseZip = DownloadFile("https://github.com/cake-contrib/Home/archive/master.zip");
        Unzip(cakeContribReleaseZip, releaseDir);

        // Need to rename the container directory in the zip file to something consistent
        var cakeContribContainerDir = GetDirectories(releaseDir.Path.FullPath + "/*").First(x => x.GetDirectoryName().StartsWith("Home-master"));
        MoveDirectory(cakeContribContainerDir, cakeContribSourceDir);
    });

Task("CleanExtensionPackages")
    .Does(() =>
{
    CleanDirectory(extensionDir);
});

Task("GetMaintainerSpecs")
    .Does(() =>
{
    var maintainerSpecFiles = GetFiles("./maintainers/*.yml");
    maintainerSpecs
        .AddRange(maintainerSpecFiles
            .Select(x =>
            {
                Verbose("Deserializing maintainer YAML from " + x);
                return DeserializeYamlFromFile<MaintainerSpec>(x);
            })
        );
});

Task("GetExtensionSpecs")
    .Does(() =>
{
    var extensionSpecFiles = GetFiles("./extensions/*.yml");
    extensionSpecs
        .AddRange(extensionSpecFiles
            .Select(x =>
            {
                Verbose("Deserializing extension YAML from " + x);
                return DeserializeYamlFromFile<ExtensionSpec>(x);
            })
        );

    assemblies
        .AddRange(
            extensionSpecs
                .Where(x => x.Assemblies != null)
                .SelectMany(
                    extensionSpec =>
                        extensionSpec
                            .Assemblies
                            .Select(
                                assembly =>
                                    $"../release/extensions/{extensionSpec.NuGet.ToLower()}.{extensionSpec.AnalyzedPackageVersion}{assembly}")));
    Verbose("Extension assemblies:");
    foreach (var assembly in assemblies)
    {
        Verbose($"  {assembly}");
    }
});

Task("GetExtensionPackages")
    .IsDependentOn("CleanExtensionPackages")
    .IsDependentOn("GetExtensionSpecs")
    .Does(context =>
    {
        context.DownloadPackages(extensionDir,
            extensionSpecs
                .Where(x => !string.IsNullOrEmpty(x.NuGet))
                .ToDictionary(e => e.NuGet, e => e.AnalyzedPackageVersion));

        context.CalcSupportedCakeVersions(extensionDir,
            extensionSpecs
                .ToDictionary(e => e.NuGet, e => e.TargetCakeVersion));
    });

Task("Build")
    .IsDependentOn("GetArtifacts")
    .Does(context =>
    {
        var releaseInfo = context.GetCakeGitHubReleaseInfo(releaseDir);

        Wyam(new WyamSettings
        {
            Recipe = "Docs",
            Theme = "Samson",
            UpdatePackages = true,
            Settings = new Dictionary<string, object>
            {
                { "AssemblyFiles",  assemblies },
                { "CakeLatestReleaseName", releaseInfo.LatestReleaseName },
                { "CakeLatestReleaseUrl", releaseInfo.LatestReleaseUrl },
            },
            EnvironmentVariables = new Dictionary<string, string>
            {
                { "DOTNET_ROLL_FORWARD", "Major" },
            },
        });
    });

// Does not download artifacts (run Build or GetArtifacts target first)
Task("Preview")
    .IsDependentOn("GetMaintainerSpecs")
    .IsDependentOn("GetExtensionSpecs")
    .Does(context =>
    {
        var releaseInfo = context.GetCakeGitHubReleaseInfo(releaseDir);

        Wyam(new WyamSettings
        {
            Recipe = "Docs",
            Theme = "Samson",
            UpdatePackages = true,
            Preview = true,
            Watch = true,
            Settings = new Dictionary<string, object>
            {
                { "AssemblyFiles",  assemblies },
                { "CakeLatestReleaseName", releaseInfo.LatestReleaseName },
                { "CakeLatestReleaseUrl", releaseInfo.LatestReleaseUrl },
            },
            EnvironmentVariables = new Dictionary<string, string>
            {
                { "DOTNET_ROLL_FORWARD", "Major" },
            },
        });
    });

// Assumes Wyam source is local and at ../Wyam
Task("Debug")
    .Does(() =>
    {
        StartProcess("../Wyam/src/clients/Wyam/bin/Debug/net462/wyam.exe",
            "-a \"../Wyam/tests/integration/Wyam.Examples.Tests/bin/Debug/net462/**/*.dll\" -r \"docs -i\" -t \"../Wyam/themes/Docs/Samson\" -p --attach");
    });

// Does not download artifacts (run Build or GetArtifacts target first)
Task("Debug-Extensions")
    .IsDependentOn("GetExtensionSpecs")
    .Does(() =>
    {
        StartProcess("../Wyam/src/clients/Wyam/bin/Debug/net462/wyam.exe",
            "-a \"../Wyam/tests/integration/Wyam.Examples.Tests/bin/Debug/net462/**/*.dll\" -r \"docs -i\" -t \"../Wyam/themes/Docs/Samson\" -p --attach"
            + " --setting \"AssemblyFiles=["
            + String.Join(",", assemblies)
            + "]\"");
    });

Task("Copy-Bootstrapper-Download")
    .Does(()=>
    {
        CopyDirectory("./download", outputPath.Combine("download"));
        CopyDirectory("./download/bootstrapper", outputPath.Combine("bootstrapper"));
    });

Task("Copy-Support-Redirect")
    .Does(()=>
    {
        CopyDirectory("./support/nuget", outputPath.Combine("support/nuget"));
    });

Task("ZipArtifacts")
    .IsDependentOn("Build")
    .IsDependentOn("Copy-Bootstrapper-Download")
    .IsDependentOn("Copy-Support-Redirect")
    .Does(() =>
{
    Zip(outputPath, zipFileName);
});

Task("UploadArtifacts")
    .IsDependentOn("ZipArtifacts")
    .WithCriteria(BuildSystem.IsRunningOnAzurePipelinesHosted)
    .Does(() =>
{
    AzurePipelines.Commands.UploadArtifact("website", zipFileName, "website");
    AzurePipelines.Commands.UploadArtifact("website", deployCakeFileName, "website");
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

Task("GetArtifacts")
    .IsDependentOn("GetSource")
    .IsDependentOn("GetMaintainerSpecs")
    .IsDependentOn("GetExtensionPackages");

Task("AppVeyor")
    .IsDependentOn("Build");

Task("AzureDevOps")
    .IsDependentOn("UploadArtifacts");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

if (!StringComparer.OrdinalIgnoreCase.Equals(target, "Deploy"))
{
    RunTarget(target);
}
