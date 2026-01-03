Order: 20
Title: Installing and using tools
Description: How to install and use external tools with Cake.Sdk
---

Cake.Sdk provides the `InstallTool` method to download and install tool executables available as NuGet packages as part of a build.

# Installing tools with InstallTool

[Cake.Sdk] provides an `InstallTool` method to download a tool and install it:

:::{.alert .alert-info}
Out of the box NuGet and .NET Tools (since Cake 1.1) are supported as provider.
More providers are available through [Modules](/extensions/).
:::

The following example downloads the [GitVersion.Tool package](https://www.nuget.org/packages/GitVersion.Tool)
as part of executing your build script:

```csharp
#:sdk Cake.Sdk

var target = Argument("target", "Pack");

Setup(context =>
{
    InstallTool("dotnet:https://api.nuget.org/v3/index.json?package=GitVersion.Tool&version=6.3.0");
    var version = GitVersion();

    Information("Building Version: {0}", version.FullSemVer);
});
```

## Installing from NuGet

You can install tools from NuGet using the `nuget:` provider:

```csharp
Setup(context =>
{
    InstallTool("nuget:?package=xunit.runner.console&version=2.4.1");
});
```

## Installing .NET Tools

You can install .NET Tools using the `dotnet:` provider:

```csharp
Setup(context =>
{
    InstallTool("dotnet:?package=GitVersion.Tool&version=6.3.0");
});
```

You can also specify a custom NuGet source:

```csharp
Setup(context =>
{
    InstallTool("dotnet:https://api.nuget.org/v3/index.json?package=GitVersion.Tool&version=6.3.0");
});
```

:::{.alert .alert-info}
For more information about supported URI string parameters see [#tool pre-processor directive] for [Cake .NET Tool].
:::

# Installing tools from other providers

Out of the box NuGet and .NET Tools (since Cake 1.1) is supported as provider for `InstallTool`.
More providers are available through [Modules](/extensions/).


[Cake.Sdk]: /docs/running-builds/runners/cake-sdk
[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[#tool pre-processor directive]: /docs/writing-builds/preprocessor-directives/tool/
