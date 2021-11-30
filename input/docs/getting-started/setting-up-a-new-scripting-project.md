Order: 20
Title: Setting Up A New Cake .NET Tool Project
Description: Guide on how to get started with writing a build pipeline in a Cake script using Cake .NET Tool
RedirectFrom:
  - docs/tutorials/getting-started
  - docs/tutorials/setting-up-a-new-project
  - docs/getting-started/setting-up-a-new-project
---

This is a guide to get started with Cake using the Cake .NET Tool and to show you how Cake works.

# Choose your runner

Choose the runner suitable for your scenario.
See [Runners](../running-builds/runners) for other possibilities how to run Cake builds.

For this tutorial we use the [Cake .NET Tool](../running-builds/runners/dotnet-tool) on .NET Core 3.1 and newer.

Make sure to have a tool manifest available in your repository or create one using the following command:

```powershell
dotnet new tool-manifest
```

Install Cake as a local tool using the `dotnet tool` command (you can replace `<?! Meta CakeLatestReleaseName /?>` with a different version of Cake you want to use):

```powershell
dotnet tool install Cake.Tool --version <?! Meta CakeLatestReleaseName /?>
```

:::{.alert .alert-info}
See [Bootstrapping Cake .NET Tool](../running-builds/runners/dotnet-tool#bootstrapping-for.net-tool) for details about the bootstrapping process.
:::

# Create initial build script

Create a file called `build.cake` with the following content:

```csharp
var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory($"./src/Example/bin/{configuration}");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreBuild("./src/Example.sln", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest("./src/Example.sln", new DotNetCoreTestSettings
    {
        Configuration = configuration,
        NoBuild = true,
    });
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
```

# Run build script

Run the build script using the .NET CLI:

```powershell
dotnet cake
```
