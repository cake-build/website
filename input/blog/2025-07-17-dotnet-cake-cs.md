---
title: "dotnet cake.cs - preview"
category: Announcement
author: devlead
---

We're excited to announce the preview of **Cake.Sdk**, a new way to get the Cake tool scripting experience in regular .NET console applications! This brings you the stellar experience of the new ["dotnet run app.cs"](https://devblogs.microsoft.com/dotnet/announcing-dotnet-run-app/) feature (requires .NET 10), while also working seamlessly with .NET 8 and 9 for regular csproj projects. 🚀 🍰

Here's the minimal example:

```csharp
#:sdk Cake.Sdk

Information("Hello world!");
```

```bash
dotnet cake.cs
```

After weeks of internal testing and development, we're now expanding the beta for public feedback and contribution. While this is still in early stages, we want to gather early feedback from the community and plan to be ready when .NET 10 launches in November.

<!--excerpt-->

## What is Cake.Sdk?

Cake.Sdk is a custom SDK that provides a convenient way to create Cake projects with minimal configuration. It automatically sets up common properties and provides a streamlined development experience for Cake-based build automation projects, whether you're using the new file-based approach or traditional project-based builds.



### Key Features

- **Minimal Project Configuration**: Create Cake projects with just a few lines in your `.csproj` file
- **File-based Build Scripts**: Use the new `#:sdk Cake.Sdk` directive for standalone `.cs` files
- **Optimized Build Settings**: Pre-configured with optimal settings for Cake projects
- **Built-in Source Generation**: Includes Cake.Generator by default for automatic source generation capabilities
- **Full Cake Addin & Module Support**: All existing Cake addins and modules work seamlessly

## Getting Started with Cake.Template

The easiest way to get started will be using the **Cake.Template** package, which provides several templates for different scenarios:

First, install the template package:

```bash
dotnet new install Cake.Template@5.0.25198.49-beta
```

### Version Control with global.json

For all scenarios, it's recommended to create a `global.json` file to pin both the .NET SDK and Cake.Sdk versions:

```bash
dotnet new cakeglobaljson
```

This creates a `global.json` file that looks like:

```json
{
  "sdk": {
    "version": "10.0.100-preview.6.25358.103"
  },
  "msbuild-sdks": {
    "Cake.Sdk": "5.0.25198.49-beta"
  }
}
```

If you already have a `global.json` file, you can add the `msbuild-sdks` section to control the Cake.Sdk version used.

> **Note**: There's currently a bug in .NET 10, but hopefully by preview 7 you'll also be able to specify the Cake.Sdk version in file-based apps like this:
> ```csharp
> #:sdk Cake.Sdk@version
> ```

### File-based Build Script (Requires .NET 10)

For the new file-based approach that works with `dotnet cake.cs`:

```bash
dotnet new cakefile --name cake --IncludeExampleProject true
```

This creates a standalone `cake.cs` file with the `#:sdk Cake.Sdk` directive and example code to clean, build and test a .NET solution. The `--IncludeExampleProject true` flag will create a sample project too (default is false). The resulting file structure will be shown below.

```
cake.cs
src
  │   Example.sln
  │
  ├───Example
  │       Class1.cs
  │       Example.csproj
  │
  └───Example.Tests
          Example.Tests.csproj
          UnitTest1.cs
```

Execute it with:
```bash
dotnet cake.cs
```

### Multi-file File-based Build Script (Requires .NET 10)

For larger projects, you can organize your code into multiple files:

```bash
dotnet new cakemultifile --name cake
```

This creates a structure like:

```
build/
├── ExceptThisFile.cs
├── Models.cs
└── Utilities.cs
cake.cs
```

Where `cake.cs` contains similar project as single file but utilizing MSBuild properties `IncludeAdditionalFiles` and `ExcludeAdditionalFiles` to include/exclude additional files example:
```csharp
#:sdk Cake.Sdk
#:property IncludeAdditionalFiles=build/**/*.cs
#:property ExcludeAdditionalFiles=build/**/Except*.cs

```

### Project-based Build Script

For traditional project-based approach (works with .NET 8, 9, and 10):

```bash
dotnet new cakeproj --Framework net9.0 --name cake
```

This creates a `cake` folder with a `cake.csproj` with minimal configuration:

```xml
<Project Sdk="Cake.Sdk/5.0.25198.49-beta">
  <PropertyGroup>
    <TargetFramework Condition="$(TargetFrameworks) == ''">net9.0</TargetFramework>
    <RunWorkingDirectory>$(MSBuildProjectDirectory)/..</RunWorkingDirectory>
  </PropertyGroup>
</Project>
```

and a `cake.cs` containing minimal example code to clean, build and test and .NET solution.

## Deep Dive: Cake.Sdk Powered by Cake.Generator

Cake.Sdk is built on top of **Cake.Generator**, which provides automatic source generation capabilities. This means you get all the benefits of Cake's powerful aliases and functionality without any additional configuration.

### What's Included

The Cake.Sdk automatically configures the following properties:

- `OutputType`: Exe
- `Nullable`: enable
- `ImplicitUsings`: enable
- `Optimize`: true
- `DebugType`: portable
- `DebugSymbols`: true
- `LangVersion`: latest
- `PublishAot`: false
- `JsonSerializerIsReflectionEnabledByDefault`: true

### Default Package References

The following packages are automatically included:

- [Cake.Generator](https://www.nuget.org/packages/Cake.Generator) - Source generator for Cake aliases
- [Cake.Core](https://www.nuget.org/packages/Cake.Core) - Core Cake functionality
- [Cake.Common](https://www.nuget.org/packages/Cake.Common) - Core Common functionality
- [Cake.Cli](https://www.nuget.org/packages/Cake.Cli) - Command-line interface for Cake
- [Cake.DotNetTool.Module](https://www.nuget.org/packages/Cake.DotNetTool.Module) - .NET Tool support for Cake
- [Cake.NuGet](https://www.nuget.org/packages/Cake.NuGet) - NuGet support for Cake
- [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) - IoC container support

### Cake Addin & Module Support

Just like with Cake tool scripting, code is generated for having methods, properties, and models conveniently available automatically by just adding NuGet package references. The same applies to modules, enabling you to replace Cake core functionality just by adding a reference to a Cake module.

#### Example Cake Addin in File-based Project

```csharp
#:sdk Cake.Sdk
#:package Cake.Twitter@5.0.0

var oAuthConsumerKey        = EnvironmentVariable("TWITTER_CONSUMER_KEY");
var oAuthConsumerSecret     = EnvironmentVariable("TWITTER_CONSUMER_SECRET");
var accessToken             = EnvironmentVariable("TWITTER_ACCESS_TOKEN");
var accessTokenSecret       = EnvironmentVariable("TWITTER_ACCESS_TOKEN_SECRET");

TwitterSendTweet(oAuthConsumerKey, oAuthConsumerSecret, accessToken, accessTokenSecret, "Testing, 1, 2, 3");
```

Any Cake addin can be added as a `PackageReference` and its alias proxies will be generated automatically. The generator will scan the addin assembly and create proxy methods for all discovered Cake method and property aliases, making them available as static methods without requiring explicit `ICakeContext` parameters.

#### Example Module

```csharp
#:sdk Cake.Sdk
#:package Cake.BuildSystems.Module@8.0.0
```

The generator now supports Cake modules with automatic registration. Modules referenced in your project will have their method and property aliases automatically discovered and proxy methods generated, just like regular addins. This includes both NuGet package modules and local module assemblies.

#### Tool Installation Support

Just like [Cake.Tool scripting](https://cakebuild.net/docs/writing-builds/tools/installing-tools), Cake.Sdk supports the `dotnet:` and `nuget:` schemes for installing tools from NuGet. You can install tools using the same familiar syntax:

```csharp
// Install a single tool
InstallTool("dotnet:https://api.nuget.org/v3/index.json?package=GitVersion.Tool&version=5.12.0");

// Install multiple tools
InstallTools(
    "dotnet:https://api.nuget.org/v3/index.json?package=GitVersion.Tool&version=5.12.0",
    "dotnet:https://api.nuget.org/v3/index.json?package=GitReleaseManager.Tool&version=0.20.0"
);

// Using nuget: scheme
InstallTool("nuget:?package=xunit.runner.console&version=2.4.1");
```

This provides the same tool installation capabilities you're familiar with from Cake.Tool scripting, making the transition to Cake.Sdk seamless.

### Advanced Features

#### IoC Container

Register and resolve services using the IoC container:

```csharp
// Register services
static partial void RegisterServices(IServiceCollection services)
{
    services.AddSingleton<IMyService, MyService>();
}

// Resolve services in tasks
Task("MyTask")
    .Does(() => {
        var service = ServiceProvider.GetRequiredService<IMyService>();
        service.DoSomething();
    });
```

## Requirements

- **File-based approach**: .NET 10 Preview 6 or later (available for download at [dotnet.microsoft.com](https://dotnet.microsoft.com/en-us/download/dotnet/10.0))
- **Project-based approach**: .NET 8.0 or later
- Compatible with .NET 8.0, 9.0, and 10.0 target frameworks

## Example Repository

We've created a comprehensive example repository at [github.com/cake-build/cakesdk-example](https://github.com/cake-build/cakesdk-example) that demonstrates:

- File-based build script using `#:sdk Cake.Sdk`
- Project-based build script using Cake.Sdk
- Multi-file file-based build script
- GitHub Actions workflows for all approaches
- Pinned versions via `global.json`

## GitHub Actions Support

The latest unreleased bits of [cake-action](https://github.com/cake-build/cake-action) include support for these new file-based approaches:

```yaml
steps:
  - name: Run a Cake C# file
    uses: cake-build/cake-action@master
    with:
      file-path: path/to/Build.cs
```

## Get Involved

This is a preview release, and we'd love your feedback! You can:

- Try out the templates and SDK
- Report issues on [GitHub](https://github.com/cake-build/generator/issues)
- Join the discussion on our [discussion board](https://github.com/orgs/cake-build/discussions)
- Contribute to the [source code](https://github.com/cake-build/generator/)

## Package References

- [Cake.Sdk](https://www.nuget.org/packages/Cake.Sdk) - The main SDK package
- [Cake.Generator](https://www.nuget.org/packages/Cake.Generator) - Source generator for Cake aliases
- [Cake.Template](https://www.nuget.org/packages/Cake.Template) - Templates for creating Cake projects

We're excited to see what you build with Cake.Sdk! 🍰
