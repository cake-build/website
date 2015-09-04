#load "utilities.cake"

using Cake.Core.IO.Arguments;

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");

var local = BuildSystem.IsLocalBuild;
var isRunningOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var branch = AppVeyor.Environment.Repository.Branch;
var isBuildingMaster = branch != null && branch == "master";

// Parse release notes.
var releaseNotes = ParseReleaseNotes("./ReleaseNotes.md");

// Get version.
var buildNumber = AppVeyor.Environment.Build.Number;
var version = releaseNotes.Version.ToString();
var semVersion = local ? version : (version + string.Concat("-build-", buildNumber));

var appDataLibPath = Directory("./src/Cake.Web/App_Data/libs");
var objPath = Directory("./src/Cake.Web/obj");
var publishPath = Directory("./src/Cake.Web/obj") + Directory(configuration) + Directory("Package/PackageTmp");

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(objPath);
    CleanDirectory(appDataLibPath);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./src/Cake.Web.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild("./src/Cake.Web.sln", settings =>
        settings.SetConfiguration(configuration)
            .WithProperty("DeployOnBuild","true")
            .UseToolVersion(MSBuildToolVersion.NET46)
            .SetVerbosity(Verbosity.Minimal)
            .SetNodeReuse(false));
});

Task("Update-AppVeyor-Build-Number")
    .WithCriteria(() => isRunningOnAppVeyor)
    .Does(() =>
{
    AppVeyor.UpdateBuildVersion(semVersion);
});

Task("Deploy")
    .IsDependentOn("Build")
    .WithCriteria(() => isRunningOnAppVeyor && isBuildingMaster)
    .Does(context =>
{
    // Get the MSDeploy path.
    var programFiles = Directory(context.Environment.GetSpecialPath(SpecialPath.ProgramFilesX86).FullPath);
    var msDeploy = programFiles + File("IIS/Microsoft Web Deploy V3/msdeploy.exe");
    if(!FileExists(msDeploy))
    {
        Warning("Path: {0}", msDeploy.Path.FullPath);
        throw new InvalidOperationException("MSDeploy.exe is not installed.");
    }

    // Get the username and password.
    var username = EnvironmentVariable("AZURE_USERNAME");
    var password = EnvironmentVariable("AZURE_PASSWORD");
    if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
    {
        throw new InvalidOperationException("Could not resolve Azure username or password.");
    }

    // Adjust the username since Powershell doesn't like
    // the $ sign which Azure use to prefix usernames.
    username = string.Concat("$", username);

    // Build the argumens to MSDeploy.
    var builder = new ProcessArgumentBuilder();
    builder.Append("-verb:sync");
    builder.Append(new KeyValueArgument("-source:contentPath", new SingleQuotedArgument(MakeAbsolute(publishPath).FullPath)));
    builder.Append(new ChainedArgument()
        .Append(new KeyValueArgument("-dest:contentPath", new SingleQuotedArgument("D:/home/site/wwwroot")))
        .Append(new KeyValueArgument("ComputerName", new SingleQuotedArgument("https://cakebuild.scm.azurewebsites.net:443/msdeploy.axd?site=cakebuild")))
        .Append(new KeyValueArgument("UserName", new SecretArgument(new SingleQuotedArgument(username))))
        .Append(new KeyValueArgument("Password", new SecretArgument(new SingleQuotedArgument(password))))
        .Append(new KeyValueArgument("AuthType", new SingleQuotedArgument("Basic"))));

    // Run MSDeploy.
    StartProcess(msDeploy, new ProcessSettings { Arguments = builder });
});

///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

Task("AppVeyor")
    .IsDependentOn("Update-AppVeyor-Build-Number")
    .IsDependentOn("Deploy");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
