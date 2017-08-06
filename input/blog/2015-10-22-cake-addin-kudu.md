---
title: Cake Kudu - Azure Web Deployment Addin
category: Addins
author: devlead
---

Wouldn't it be great if you could fully control your _Azure web app deployments_, have them _configuration_ and _environment_ driven and all this orchestrated using C#? Well it turns out with Cake Kudu _you_ can!

<!--excerpt-->

### Kudu

[Kudu](https://github.com/projectkudu/kudu) is the engine behind continuous deployments, WebJobs, and many other features in Azure web apps and mobile services.


Out of the box _Kudu_ allows you to build and deploy you web apps directly on _Azure_ triggered by when you push to your code _GitHub_, _Bitbucket_, _Visual Studio Online_, _Dropbox_ and many more services, it can also host it's own GIT repository so can push directly to Kudu.


### Cake Kudu

[Cake Kudu](https://github.com/WCOMAB/Cake.Kudu) is an add-in for the Cake build system which gives you an API to the Kudu deployment environment.

It gives you easy access to

* [Deployment paths](https://cakebuild.net/api/cake.kudu.provider/a5ae8623)
* [App settings](https://cakebuild.net/api/cake.kudu.provider/3d10564b/d3b19dd0)
* [Connection strings](https://cakebuild.net/api/cake.kudu.provider/3d10564b/43c2776d)
* [Source control management](https://cakebuild.net/api/cake.kudu.provider/499464bd)
* [Tool paths](https://cakebuild.net/api/cake.kudu.provider/eb1bc85d)
* [Web site information](https://cakebuild.net/api/cake.kudu.provider/fadcc0d6)
* [Deployment sync file/folder methods](https://cakebuild.net/api/cake.kudu.provider/3d10564b/10367dab)


### Super charging Kudu

So what does [Cake](https://cakebuild.net) and the [Cake Kudu](https://github.com/WCOMAB/Cake.Kudu) add-in bring to the table? It will let you do custom build, test and deploy scripts written in C#, driven by convention, environment and configuration.

This enables advanced scenarios like synchronized deployment of multiple web sites (i.e. frontend, API, backoffice) from one repository, adapting build configuration depending on dev/production/geographical region/etc., basically endless possibilities for continuous deployment automation.

It lets you use any of the available aliases and add-ins for Cake, you could post deployment statuses to [Slack](https://github.com/WCOMAB/Cake.Slack) / [Gitter](https://github.com/cake-contrib/Cake.Gitter), migrate databases using [AliaSql](https://github.com/RichiCoder1/Cake.AliaSql) and much more, all written in a rich, compiled and statically typed language like C# and with the extensive [dsl](https://cakebuild.net/dsl) the Cake builds system provides.

You could even use your Azure web app as a build server posting **not** a website but i.e. a build result report to the web.

### Usage

#### Continuous deployment
First, if you haven't already, you need to enable continuous deployment on your web app, you do that via Settings->Publishing->Continuous Deployment and choose the provider that hosts your source code.

![Continuous deployment](https://cloud.githubusercontent.com/assets/1647294/10564229/cc6e3ed8-75ab-11e5-9a58-bf7de894a673.png)


#### .deployment
To change the default deployment behavior you have to add an `.deployment` file the root of to your repository, this tells Kudu what to run on deployment.

Example config file:
```ini
[config]
command = deploy.cmd
```


#### deploy.cmd
To enable Cake from the Kudu environment we need a little bootstrapper script that sets up folders and fetches Cake binaries from NuGet and then calls Cake with C# deployment script as parameter.

Example deploy bootstrapper:
```dos
@echo off
IF NOT EXIST "Tools" (md "Tools")
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
nuget install Cake -ExcludeVersion -OutputDirectory "Tools"
Tools/Cake/Cake.exe deploy.cake -verbosity=Verbose
```
The above script will create the tools & addins folders, fetch Cake from NuGet(upgrade if newer version exists) and launch the Cake build system with a C# script called `deploy.cake`.


#### deploy.cake
Below is a very basic deployment Cake script.  What it basically does is:
1. Fetches any command line tools needed from NuGet.
2. Fetches and references any Cake add-ins needed from NuGet.
3. Sets up global variables and validates that it's running on the Kudu environment.
4. Cleans any traces of previous builds.
5. Restores any NuGet packages used by the web app.
6. Builds/compiles the web app.
7. If all goes well published the web app.

The process is dependency based so if any step would fail the deployment would be aborted and reported as failed in the Azure portal.

```csharp
#tool "KuduSync.NET" "https://www.nuget.org/api/v2/"
#addin "Cake.Kudu" "https://www.nuget.org/api/v2/"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target          = Argument<string>("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var websitePath     = MakeAbsolute(Directory("./src/TestWebSite"));
var solutionPath    = MakeAbsolute(File("./src/TestWebSite.sln"));

if (!Kudu.IsRunningOnKudu)
{
    throw new Exception("Not running on Kudu");
}

var deploymentPath = Kudu.Deployment.Target;
if (!DirectoryExists(deploymentPath))
{
    throw new DirectoryNotFoundException(
        string.Format(
            "Deployment target directory not found {0}",
            deploymentPath
            )
        );
}

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    //Clean up any binaries
    Information("Cleaning {0}", websitePath);
    CleanDirectories(websitePath + "/bin");
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
    // Build all solutions.
    Information("Building {0}", solutionPath);
    MSBuild(solutionPath, settings =>
        settings.SetPlatformTarget(PlatformTarget.MSIL)
            .WithProperty("TreatWarningsAsErrors","true")
            .WithTarget("Build")
            .SetConfiguration(configuration));
});

Task("Publish")
    .IsDependentOn("Build")
    .Does(() =>
{
    Information("Deploying web from {0} to {1}", websitePath, deploymentPath);
    Kudu.Sync(websitePath);
});

Task("Default")
    .IsDependentOn("Publish");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
```

#### In action

![Azure web app deployment](https://cloud.githubusercontent.com/assets/1647294/10564139/7449fb3c-75a8-11e5-82ef-b06d4da4a13b.png)


### Conclusion

The combination of the Cake build system and Kudu engine proves to be a very powerful and flexible tool for all your deployment needs.
