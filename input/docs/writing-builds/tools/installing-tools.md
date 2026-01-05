Order: 10
Title: Installing and using tools
Description: How to install and use external tools
RedirectFrom: docs/tools/installing-tools
---

# Installing tools from NuGet

Cake provides different ways to install tool executables available as NuGet packages as part of a build.

| Installation method              | Cake .NET Tool | Cake Frosting | Cake.Sdk |
|----------------------------------|----------------|---------------|----------|
| [Pre-processor directive]        | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-xmark" style="color:red"></i>   | <i class="fa-solid fa-xmark" style="color:red"></i>   |
| [InstallTool]                    | <i class="fa-solid fa-xmark" style="color:red"></i>   | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> |

## Installing tools via pre-processor directive

The [#tool pre-processor directive] for [Cake .NET Tool] can be used to automatically download a tool and install it in the `tools` folder.

:::{.alert .alert-info}
Out of the box NuGet and .NET Tools (since Cake 1.1) are supported as provider.
More providers are available through [Modules](/extensions/).
:::

The following example downloads the [xunit.runner.console package](https://www.nuget.org/packages/xunit.runner.console)
as part of executing your build script:

```csharp
#tool "nuget:?package=xunit.runner.console&version=2.4.1"
```

:::{.alert .alert-info}
For more information see [#tool pre-processor directive].
:::

## Installing tools with InstallTool

[Cake Frosting] and [Cake.Sdk] provide an `InstallTool` method to download a tool and install it:

:::{.alert .alert-info}
Out of the box NuGet and .NET Tools (since Cake 1.1) are supported as provider.
More providers are available through [Modules](/extensions/).
:::

### Cake Frosting

The following example downloads the [xunit.runner.console package](https://www.nuget.org/packages/xunit.runner.console)
as part of executing your build script:

```csharp
public class Program : IFrostingStartup
{
    public static int Main(string[] args)
    {
        // Create and run the host.
        return
            new CakeHost()
                .InstallTool(new Uri("nuget:?package=xunit.runner.console&version=2.4.1"))
                .Run(args);
    }
}
```

### Cake.Sdk

The following example downloads the [GitVersion.Tool package](https://www.nuget.org/packages/GitVersion.Tool)
as part of executing your build script:

```csharp
#:sdk Cake.Sdk

Setup(context =>
{
    InstallTool("dotnet:https://api.nuget.org/v3/index.json?package=GitVersion.Tool&version=6.3.0");
    var version = GitVersion();
    Information("Building Version: {0}", version.FullSemVer);
});
```

:::{.alert .alert-info}
For more information about using tools with Cake.Sdk see [Installing and using tools with Cake.Sdk](/docs/writing-builds/sdk/tools).
:::

:::{.alert .alert-info}
For more information about supported URI string parameters see [#tool pre-processor directive].
:::

# Installing tools from other providers

Out of the box NuGet and .NET Tools (since Cake 1.1) is supported as provider for [Pre-processor directive] and [InstallTool].
More providers are available through [Modules](/extensions/).

# Using tools from disk

To use a tool that's not available via NuGet or if you prefer to store the tool locally,
Cakes [tool resolution conventions](tool-resolution) can be used to resolve the path to the tool and call it through a [process alias](/dsl/process/):

```csharp
Task("Install-XUnit")
    .Does(()=> {
    FilePath nugetPath = Context.Tools.Resolve("nuget.exe");
    StartProcess(nugetPath, new ProcessSettings {
        Arguments = new ProcessArgumentBuilder()
            .Append("install")
            .Append("xunit.runner.console")
        });
});
```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting
[Cake.Sdk]: /docs/running-builds/runners/cake-sdk
[Pre-processor directive]: #installing-tools-via-pre-processor-directive
[InstallTool]: #installing-tools-with-installtool
[Bootstrapper]: #installing-tools-via-bootstrapper
[#tool pre-processor directive]: /docs/writing-builds/preprocessor-directives/tool/
