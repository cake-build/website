Order: 30
Title: Cake.Sdk
---
Cake.Sdk is a custom SDK that provides a modern way to get the Cake tool scripting experience in regular .NET console applications. This brings you the stellar experience of the new ["dotnet run app.cs"](https://devblogs.microsoft.com/dotnet/announcing-dotnet-run-app/) feature (requires .NET 10), while also working seamlessly with .NET 8 and 9 for regular csproj projects.

# Requirements

The [Cake.Sdk](https://www.nuget.org/packages/Cake.Sdk) NuGet package is a custom MSBuild SDK that requires:

- **File-based approach**: .NET 10 or later
- **Project-based approach**: .NET 8.0 or later

# Usage

## File-based approach (requires .NET 10)

For file-based builds, create a `.cs` file with the `#:sdk Cake.Sdk` directive:

```csharp
#:sdk Cake.Sdk

var target = Argument("target", "Default");

Task("Default")
    .Does(() =>
{
    Information("Hello from Cake.Sdk!");
});

RunTarget(target);
```

Run the script directly:

```bash
dotnet cake.cs
```

## Project-based approach (works with .NET 8, 9, and 10)

For project-based builds, create a `.csproj` file using the Cake.Sdk:

```xml
<Project Sdk="Cake.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <RunWorkingDirectory>$(MSBuildProjectDirectory)/..</RunWorkingDirectory>
  </PropertyGroup>
</Project>
```

and a `Program.cs`:

```csharp
var target = Argument("target", "Default");

Task("Default")
    .Does(() =>
{
    Information("Hello from Cake.Sdk!");
});

RunTarget(target);
```

Run the project:

```bash
dotnet run --project cake.csproj
```

# Getting Started with Cake.Template

The easiest way to get started with Cake.Sdk is using the **Cake.Template** package, which provides several templates for different scenarios.

## Install the template

First, install the template package:

```bash
dotnet new install Cake.Template
```

## File-based templates

### Minimal template

For the simplest possible setup:

```bash
dotnet new cakeminimalfile --name cake
```

This creates a minimal `cake.cs` file with just the essential SDK directive and Information log statement.

### File template

Create a new Cake file-based project with example code:

```bash
dotnet new cakefile --name cake --IncludeExampleProject true
```

This creates a standalone `cake.cs` file with the `#:sdk Cake.Sdk` directive and example code to clean, build and test a .NET solution.

### Multi-file template

For larger projects, organize your code across multiple files:

```bash
dotnet new cakemultifile --name cake
```

This creates a structure that supports both single-file and multi-file approaches, including support for `Main_*` methods providing flexible entry points for different build scenarios.

## Project-based template

For traditional project-based approach:

```bash
dotnet new cakeproj --Framework net10.0 --name cake
```

This creates a `cake` folder with a `cake.csproj` using Cake.Sdk and a `cake.cs` containing minimal example code.

# Key Features

Cake.Sdk provides a streamlined development experience with the following features:

- **Minimal Project Configuration**: Create Cake projects with just a few lines in your `.csproj` file or a single directive in `.cs` files
- **File-based Build Scripts**: Use the new `#:sdk Cake.Sdk` directive for standalone `.cs` files (requires .NET 10)
- **Optimized Build Settings**: Pre-configured with optimal settings for Cake projects
- **Built-in Source Generation**: Includes Cake.Generator by default for automatic source generation capabilities
- **Full Cake Addin & Module Support**: All existing Cake addins and modules work seamlessly
- **Native .NET CLI Publish Support**: Create self-contained precompiled binaries and containers
- **Multiple Main Entry Points**: Support for multiple `Main_*` methods for better script organization
- **Script Host IoC Integration**: Enhanced dependency injection with script host action execution

# Publish Support

Cake.Sdk supports native .NET CLI publish operations, allowing you to create self-contained executables and containers.

## Creating Self-Contained Binaries

You can publish your Cake scripts as self-contained executables:

```bash
dotnet publish cake.cs --output cake.sdk
```

This will result in a self-contained `cake.sdk` binary in the output folder, eliminating the need for the .NET runtime to be installed on the target machine.

## Container Support

Building containers is straightforward with Cake.Sdk:

```bash
dotnet publish cake.cs \
    --output cake.sdk \
    --target:PublishContainer \
    -p ContainerBaseImage='mcr.microsoft.com/dotnet/runtime-deps:10.0-noble-chiseled' \
    -p ContainerArchiveOutputPath=Cake.Sdk.tar.gz
```

Once built, you can import and run the container:

```bash
# Import the container
podman load -i Cake.Sdk.tar.gz

# Run the container
podman run -it --rm localhost/cake-sdk:latest
```

You can also publish directly to a container image registry. This enables powerful scenarios, for example, you can have your build pipeline pre-compiled and cached on build agents for even faster execution.

# Version Control

For all scenarios, it's recommended to create a `global.json` file to pin both the .NET SDK and Cake.Sdk versions:

```bash
dotnet new cakeglobaljson
```

This creates a `global.json` file that looks like:

```json
{
  "sdk": {
    "version": "10.0.100"
  },
  "msbuild-sdks": {
    "Cake.Sdk": "6.0.0"
  }
}
```

If you already have a `global.json` file, you can add the `msbuild-sdks` section to control the Cake.Sdk version used.

For file-based apps, you can also specify the Cake.Sdk version directly in your `.cs` file:

```csharp
#:sdk Cake.Sdk@6.0.0
```

# Using pre-release versions

Cake uses [Azure Artifacts](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake.Sdk&protocolType=NuGet) as a NuGet feed for testing and pre-release builds.
With these pre-release builds the next version of Cake.Sdk can be accessed and utilized for getting the latest features or testing addins or build scripts to know if the next release will be safe when you need to upgrade.

