Order: 20
---

This is a guide to get started with Cake and to show you how Cake works.

# Choose your runner

Choose the runner suitable for your scenario.
See [Running Cake Scripts](running-cake-scripts) for other possibilities how to run Cake scripts.

For this tutorial we use the recommended approach using the [.NET Core Tool](running-cake-scripts#net-core-tool)
on .NET Core 3.0 and newer.

Make sure to have a tool manifest available in your repository or create one using the following command:

```shell
dotnet new tool-manifest
```

Install Cake as a local tool using the `dotnet tool` command:

```shell
dotnet tool install Cake.Tool --version x.y.z
```

:::{.alert .alert-info}
See [Bootstrapping .NET Core Tool](bootstrapping-scripts#bootstrapping-for.net-core-tool) for details about the bootstrapping process.
:::

# Create initial build script

Create a file called `build.cake` with the following content:

```csharp
#tool nuget:?package=NUnit.ConsoleRunner&version=3.11.1

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    var buildDir = Directory("./src/Example/bin") + Directory(configuration);
    CleanDirectory(buildDir);
});

Task("Restore-NuGetPackages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./src/Example.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGetPackages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild(
          "./src/Example.sln",
          settings => settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild(
          "./src/Example.sln",
          settings => settings.SetConfiguration(configuration));
    }
});

Task("Run-UnitTests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3(
        "./src/**/bin/" + configuration + "/*.Tests.dll",
        new NUnit3Settings
        {
            NoResults = true
        });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-UnitTests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
```

# Run build script

Run the build script using the .NET CLI:

```shell
dotnet cake
```
