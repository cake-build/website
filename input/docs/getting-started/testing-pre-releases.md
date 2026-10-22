Order: 55
Title: Testing pre-releases
Description: How to test pre-release versions of Cake before they ship
---

Pre-releases of Cake are published to the Cake feed ahead of each major or minor release.
Trying them out and reporting what you find is one of the most helpful ways to contribute.

The examples below use placeholders such as `<version>`. Find the current pre-release versions by browsing the
[Cake feed](https://pkgs.dev.azure.com/cake-build/Cake/_packaging/cake/nuget/v3/index.json) or the
[Cake repository releases](https://github.com/cake-build/cake/releases).

| Package        | Latest Released | Latest Pre-release (Cake feed) |
|----------------|-----------------|--------------------------------|
| Cake.Tool      | [![NuGet](https://img.shields.io/nuget/v/Cake.Tool.svg)](https://www.nuget.org/packages/Cake.Tool) | [![Azure Artifacts](https://azpkgsshield.azurevoodoo.net/cake-build/Cake/cake/cake.tool)](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake.Tool&protocolType=NuGet) |
| Cake.Frosting  | [![NuGet](https://img.shields.io/nuget/v/Cake.Frosting.svg)](https://www.nuget.org/packages/Cake.Frosting) | [![Azure Artifacts](https://azpkgsshield.azurevoodoo.net/cake-build/Cake/cake/Cake.Frosting)](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake.Frosting&protocolType=NuGet) |
| Cake.Core      | [![NuGet](https://img.shields.io/nuget/v/Cake.Core.svg)](https://www.nuget.org/packages/Cake.Core) | [![Azure Artifacts](https://azpkgsshield.azurevoodoo.net/cake-build/Cake/cake/Cake.Core)](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake.Core&protocolType=NuGet) |
| Cake.Common    | [![NuGet](https://img.shields.io/nuget/v/Cake.Common.svg)](https://www.nuget.org/packages/Cake.Common) | [![Azure Artifacts](https://azpkgsshield.azurevoodoo.net/cake-build/Cake/cake/Cake.Common)](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake.Common&protocolType=NuGet) |
| Cake.Sdk       | [![NuGet](https://img.shields.io/nuget/v/Cake.Sdk.svg)](https://www.nuget.org/packages/Cake.Sdk) | [![Azure Artifacts](https://azpkgsshield.azurevoodoo.net/cake-build/Cake/cake/Cake.Sdk)](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake.Sdk&protocolType=NuGet) |
| Cake.Generator | [![NuGet](https://img.shields.io/nuget/v/Cake.Generator.svg)](https://www.nuget.org/packages/Cake.Generator) | [![Azure Artifacts](https://azpkgsshield.azurevoodoo.net/cake-build/Cake/cake/cake.generator)](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake.Generator&protocolType=NuGet) |
| Cake.Template  | [![NuGet](https://img.shields.io/nuget/v/Cake.Template.svg)](https://www.nuget.org/packages/Cake.Template) | [![Azure Artifacts](https://azpkgsshield.azurevoodoo.net/cake-build/Cake/cake/cake.template)](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake.Template&protocolType=NuGet) |

:::{.alert .alert-info}
Different package groups are versioned independently.
`Cake.Tool`, `Cake.Frosting`, `Cake.Core` and `Cake.Common` share one version,
while `Cake.Sdk`, `Cake.Generator` and `Cake.Template` share another.
:::

# 1. Prerequisites

* The .NET SDK required by the pre-release. Check the release notes for the target frameworks and
  whether a preview/RC SDK is needed. Install additional SDKs if you want to test more than one target framework.
* A `nuget.config` in your repository root (next to `cake.cs`/`build.cake`/your `.csproj`) or in a parent folder:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
    <add key="cake" value="https://pkgs.dev.azure.com/cake-build/Cake/_packaging/cake/nuget/v3/index.json" />
  </packageSources>
  <packageSourceMapping>
    <packageSource key="cake">
      <package pattern="Cake.Cli" />
      <package pattern="Cake.Common" />
      <package pattern="Cake.Core" />
      <package pattern="Cake.DotNetTool.Module" />
      <package pattern="Cake.Frosting" />
      <package pattern="Cake.Generator" />
      <package pattern="Cake.NuGet" />
      <package pattern="Cake.Sdk" />
      <package pattern="Cake.Template" />
      <package pattern="Cake.Tool" />
    </packageSource>
    <packageSource key="nuget">
      <package pattern="*" />
    </packageSource>
  </packageSourceMapping>
</configuration>
```

If your repository already has a `nuget.config` with source mapping, merge the `cake` source and its patterns into it.

# 2. Point your build at the pre-release

## Start from a template

Run these in a folder that has the `nuget.config` above:

```
dotnet new install Cake.Template::<sdk-version>
```

| Template          | Creates                                                                                                      |
|-------------------|--------------------------------------------------------------------------------------------------------------|
| `cakefile`        | File-based `cake.cs` (add `--IncludeExampleProject true` for a sample solution in `src/`)                    |
| `cakeminimalfile` | Trivial file-based `cake.cs`                                                                                 |
| `cakemultifile`   | File-based script split over several files                                                                   |
| `cakeproj`        | Project-based `build/build.csproj` using `Cake.Sdk` (`--Framework` selects the target framework)             |
| `cakeglobaljson`  | `global.json` pinning the .NET SDK and `Cake.Sdk`, and enabling Microsoft.Testing.Platform for `dotnet test` |
| `cakeci`          | CI pipeline for running a Cake file-based build script                                                       |

File-based:

```
dotnet new cakefile -n cake --IncludeExampleProject true
dotnet new cakeglobaljson
dotnet run cake.cs
```

Project-based:

```
dotnet new cakeproj -n build
dotnet new cakeglobaljson
dotnet run --project build
```

The example project's tests use Microsoft.Testing.Platform, so add `cakeglobaljson` or `dotnet test` will fail.
`cakeglobaljson` pins an exact .NET SDK version; if you use a different SDK build, edit the generated `global.json`
or pass `--SdkVersion`.

Generate a CI pipeline with `cakeci` by selecting a provider:

```
dotnet new cakeci --Provider github
```

The template supports several CI providers. See the [Cake SDK CI Pipeline template documentation](https://github.com/cake-build/generator/blob/develop/src/Cake.Template/README.md#cake-sdk-ci-pipeline)
for the provider list and options such as selecting a build script or runner OS.

## Existing Cake.Tool build (`build.cake`)

```
dotnet tool update Cake.Tool --version <tool-version>
dotnet cake
```

Add `-g` for a global tool.

## Existing Cake.Sdk build (file-based `cake.cs`)

```csharp
#:sdk Cake.Sdk@<sdk-version>
```

```
dotnet run cake.cs
```

Run it from a file, piping code through stdin doesn't pick up `nuget.config`.

## Cake.Frosting

```xml
<PackageReference Include="Cake.Frosting" Version="<version>" />
```

## Addin and module authors

```xml
<PackageReference Include="Cake.Core" Version="<version>" PrivateAssets="All" />
<PackageReference Include="Cake.Common" Version="<version>" PrivateAssets="All" />
```

With central package management, set these versions on `PackageVersion` instead.

# 3. Report what you find

Start a question in the [cake-build Q&A discussions](https://github.com/orgs/cake-build/discussions/categories/q-a).
These organization-level discussions are a place to share feedback about both Cake and Generator.
Include the package versions, operating system and target framework.
