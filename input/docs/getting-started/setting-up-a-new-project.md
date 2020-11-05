Order: 20
Description: Guide on how to get started with Cake
RedirectFrom:
  - docs/tutorials/getting-started
  - docs/tutorials/setting-up-a-new-project
---

This is a guide to get started with Cake and to show you how Cake works.

# Choose your runner

Choose the runner suitable for your scenario.
See [Runners](../running-builds/runners) for other possibilities how to run Cake scripts.

For this tutorial we use the recommended approach using the [.NET Tool](../running-builds/runners/dotnet-tool)
on .NET Core 3.0 and newer.

Make sure to have a tool manifest available in your repository or create one using the following command:

```powershell
dotnet new tool-manifest
```

Install Cake as a local tool using the `dotnet tool` command (replace `x.y.z` with the version of Cake you want to use, e.g. 0.38.5):

```powershell
dotnet tool install Cake.Tool --version x.y.z
```

:::{.alert .alert-info}
See [Bootstrapping .NET Tool](../running-builds/runners/dotnet-tool#bootstrapping-for.net-tool) for details about the bootstrapping process.
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
