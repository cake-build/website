Order: 10
Title: Installing and using tools
Description: How to install and use external tools
RedirectFrom: docs/tools/installing-tools
---

# Installing tools from NuGet

Cake provides different ways to install tool executables available as NuGet packages as part of a build.

| Installation method              | Cake .NET Tool | Cake Frosting | Cake runner for .NET Framework | Cake runner for .NET Core |
|----------------------------------|----------------|---------------|--------------------------------|---------------------------|
| [Pre-processor directive]        | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-times" style="color:red"></i>   | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [InstallTool]                    | <i class="fa fa-times" style="color:red"></i>   | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-times" style="color:red"></i>   | <i class="fa fa-times" style="color:red"></i>   |
| [Bootstrapper]                   | <i class="fa fa-times" style="color:red"></i>   | <i class="fa fa-times" style="color:red"></i>   | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |

## Installing tools via pre-processor directive

The [#tool pre-processor directive] can be used to automatically download a tool and install it in the `tools` folder.

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

[Cake Frosting] provides a `InstallTool` method to download a tool and install it:

:::{.alert .alert-info}
Out of the box NuGet and .NET Tools (since Cake 1.1) are supported as provider.
More providers are available through [Modules](/extensions/).
:::

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

:::{.alert .alert-info}
For more information about supported URI string parameters see [#tool pre-processor directive].
:::

## Installing tools via bootstrapper

The [default bootstrapper for Cake runner for .NET Framework](/docs/running-builds/runners/cake-runner-for-dotnet-framework#bootstrapping-for-cake-runner-for.net-framework)
supports restoring of tools which are available as NuGet packages.
The tools need to be listed in the `tools\packages.config` file and are restored before running the Cake script.

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

[Cake .NET Tool]: dotnet-tool
[Cake Frosting]: cake-frosting
[Cake runner for .NET Framework]: cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: cake-runner-for-dotnet-core
[Pre-processor directive]: #installing-tools-via-pre-processor-directive
[InstallTool]: #installing-tools-with-installtool
[Bootstrapper]: #installing-tools-via-bootstrapper
[#tool pre-processor directive]: /docs/writing-builds/preprocessor-directives/tool/
