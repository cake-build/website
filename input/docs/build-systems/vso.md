Order: 10
Title: Visual Studio Team Services
---

The Cake VSTS build task makes it easy to run a Cake script directly, without having to invoke PowerShell or other command line scripts. This makes it easy even for team members that are not familiar with Cake to add or adjust parameters passed to your build scripts.

# 1. Install the VSTS extension

To install the VSTS extension, go to the
[Cake Build Task page](https://marketplace.visualstudio.com/items/cake-build.cake)
on the Visual Studio Marketplace and click "Install".

# 2. Add a build task

Go to the `Build` section in your VSTS tenant  and create a build definition, or
select an existing one. Add a new build step by clicking the
"Add build step..." button.

![Add build step](https://raw.githubusercontent.com/cake-build/cake-vso/develop/Images/addbuildstep.png)

A dialog should appear with different tasks. Select the "Cake" task and click
the "Add" button once. After that, click the "Close" button to close the window.

![Add build step](https://raw.githubusercontent.com/cake-build/cake-vso/develop/Images/addtasks.png)

# 3. Configure the build task

You should now be able to edit the build task information such as which
build script to execute, what build target to run, the amount of logging
(verbosity) and additional arguments sent to the build script.

By default, the Cake build step (when added to a build) will try to run the `build.cake` build script (found in the root of your repository) with the target `Default`. If you wish to run another build script or build target you can change this in the build step settings.

![Add build step](https://raw.githubusercontent.com/cake-build/cake-vso/develop/Images/configurebuildstep.png)

For those who are already using Cake making use of the [Bootstrapper file](http://cakebuild.net/docs/tutorials/setting-up-a-new-project), this is not called when using the VSO task.
To use the bootstrapper file you should add a "PowerShell" or "Shell Script" task instead.

## Notes about Variables

Environment Variables which are marked secret in VSTS, cannot be resolved using `EnvironmentVariable()` in a Cake script. Instead they can be passed as arguments to the build script. You can read more about this limitation in the [Visual Studio documentation under the Secret Variables section](https://www.visualstudio.com/en-us/docs/build/define/variables#SecretVariables).

# Using Cake with a Powershellscript in VSTS
This manual shows how you can use the powershell bootstrapper to build a .Net Standard library (Visual Studio 2017) on a Hosted VS2017 agent.  
This guide also shows how you resolve the following error:
> [error] Failed to load msbuild Toolset  
  Could not load file or assembly 'Microsoft.Build, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' or one of its dependencies. The system cannot find the file specified.
An error occurred when executing task 'RestorePackages'.  
Error: NuGet: Process returned an error (exit code 1).

# 1. Download the Bootstrapperfile

To download the file go to the [Setting up a new project](http://cakebuild.net/docs/tutorials/setting-up-a-new-project) page and download the bootstrapper file for your platform.

# 2. Pin NuGet to at least v4.0.0

To pin the version you have to edit the build.ps1 file. This is necessary because the current Hosted VS2017 agent has several versions installed and without pinning it, the wrong version will be resolved.
Probably with the following Error:


### 2.1 Find the part where the nuget source url is defined

This will look like this:

![NuGet source snippet](/assets/img/vsts_bootstrapper_nugetsource_snippet.PNG)

### 2.2 Pin the NuGet version

Change the URL `https://dist.nuget.org/win-x86-commandline/latest/nuget.exe` to `https://dist.nuget.org/win-x86-commandline/v4.0.0/nuget.exe` to pin NuGet to version 4.0.0.

### 2.3 Change the NuGet resolution

The NuGet resolution part has to be modified like the following (remove the red highlighted text parts):
```diff
-# Try find NuGet.exe in path if not exists
-if (!(Test-Path $NUGET_EXE)) {
-    Write-Verbose -Message "Trying to find nuget.exe in PATH..."
-    $existingPaths = $Env:Path -Split ';' | Where-Object { (![string]::IsNullOrEmpty($_)) -and (Test-Path $_) }
-    $NUGET_EXE_IN_PATH = Get-ChildItem -Path $existingPaths -Filter "nuget.exe" | Select -First 1
-    if ($NUGET_EXE_IN_PATH -ne $null -and (Test-Path $NUGET_EXE_IN_PATH.FullName)) {
-        Write-Verbose -Message "Found in PATH at $($NUGET_EXE_IN_PATH.FullName)."
-        $NUGET_EXE = $NUGET_EXE_IN_PATH.FullName
-    }
-}
-
-# Try download NuGet.exe if not exists
-if (!(Test-Path $NUGET_EXE)) {
    Write-Verbose -Message "Downloading NuGet.exe..."
    try {
        (New-Object System.Net.WebClient).DownloadFile($NUGET_URL, $NUGET_EXE)
    } catch {
        Throw "Could not download NuGet.exe."
    }
-}
```

# 3. Run your build `build.cake`

Now you can run your `build.cake` script on a Hosted VS2017 agent.  
A simple `build.cake` script would be:  

```csharp
#addin "nuget:?package=NuGet.Core"
#addin "Cake.FileHelpers"
#addin "Cake.Incubator"
#addin "Cake.ExtendedNuGet"
#tool "nuget:?package=xunit.runner.console"
#tool "nuget:?package=vswhere"

var target        = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var buildNumber   = Argument("buildnumber", "0");
var buildDir      = Directory("./artifacts");
var solution      = "./<YourSolutionFile>.sln";


Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("RestorePackages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solution);
});

Task("Build")
    .IsDependentOn("RestorePackages")
    .Does(() =>
{
    MSBuild(solution, settings => settings
        .SetConfiguration(configuration)
        .SetVerbosity(Verbosity.Minimal)
        .UseToolVersion(MSBuildToolVersion.VS2017)
    );
});

Task("RunTests")
    .IsDependentOn("Build")
    .Does(() =>
{

	var projects = GetFiles("./src/**/*.Tests.csproj");

	foreach(var project in projects)
	{
		DotNetCoreTest(project.FullPath);
	}
});

Task("Default")
    .IsDependentOn("RunTests");

RunTarget(target);
```
