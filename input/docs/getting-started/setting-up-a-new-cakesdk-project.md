Order: 15
Title: Setting Up A New Cake.Sdk Project
Description: Guide on how to get started with writing a build pipeline using Cake.Sdk with the cakefile template
---
This is a guide to get started with [Cake.Sdk] and to show you how [Cake.Sdk] works.

# Installation and scaffolding

This tutorial uses [Cake.Sdk], which provides a modern way to get the Cake tool scripting experience in regular .NET console applications using the file-based approach.
See [Runners] for other possibilities of how to run Cake builds.

:::{.alert .alert-info}
The following instructions require .NET 10.0 or later for the file-based approach.
You can find the .NET SDK at [get.dot.net]
:::

To create a new [Cake.Sdk] project you need to install the Cake.Template package:

```powershell
dotnet new install Cake.Template
```

Create a new Cake.Sdk file-based project:

```powershell
dotnet new cakefile --name cake
```

This will create a `cake.cs` file with the `#:sdk Cake.Sdk` directive and example code.

:::{.alert .alert-info}
You can optionally include an example project structure with a solution, main project and test project by adding the `--IncludeExampleProject true` parameter:

```powershell
dotnet new cakefile --name cake --IncludeExampleProject true
```
:::

# Initial build script

The generated `cake.cs` file contains the `#:sdk Cake.Sdk` directive and example code to clean, build and test a .NET solution:

```csharp
#:sdk Cake.Sdk

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
    DotNetBuild("./src/Example.sln", new DotNetBuildSettings
    {
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetTest("./src/Example.sln", new DotNetTestSettings
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

The `#:sdk Cake.Sdk` directive tells .NET to use the Cake.Sdk for this file, which automatically configures all necessary dependencies and settings.

# Run build script

Run the build script directly using the .NET CLI:

```powershell
dotnet cake.cs
```

You can also pass arguments to the script:

```powershell
dotnet cake.cs -- --target Test --configuration Debug --rebuild
```

:::{.alert .alert-info}
See [Cake.Sdk] for details about Cake.Sdk and its features.
:::

# Version Control

It's recommended to create a `global.json` file to pin both the .NET SDK and Cake.Sdk versions:

```powershell
dotnet new cakeglobaljson
```

This creates a `global.json` file that pins the versions used in your build.

Alternatively, you can specify the Cake.Sdk version directly in your `cake.cs` file:

```csharp
#:sdk Cake.Sdk@6.0.0
```

[Cake.Sdk]: /docs/running-builds/runners/cake-sdk
[Runners]: ../running-builds/runners
[get.dot.net]: https://get.dot.net
