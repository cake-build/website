Order: 10
RedirectFrom: docs/tools/installing-tools
---

This guide will demonstrate how to install tool executables to make sure
they are discovered by your build script.

# Installing Tools From NuGet

## Via Script

:::{.alert .alert-success}
This is the recommended way to install tools.
:::

Cake extends the C# language with custom pre-processor directives, and we've added one
to automatically download a tool from NuGet and install it in the `tools` folder.

To download the [xunit.runner.console package](https://www.nuget.org/packages/xunit.runner.console)
as part of executing your build script, simply use the `#tool` directive.

```csharp
#tool "xunit.runner.console"
```

For more information see [preprocessor directives](../preprocessor-directives)

## Via Bootstrapper

:::{.alert .alert-warning}
This option is only available when using [Cake runner for .NET Framework](/docs/running-builds/runners/cake-runner-for-dotnet-framework) or
[Cake runner for .NET Core](/docs/running-builds/runners/cake-runner-for-dotnet-core).
:::

The [default bootstrapper for Cake runner for .NET Framework](/docs/running-builds/runners/cake-runner-for-dotnet-framework#bootstrapping-for-cake-runner-for.net-framework)
supports restoring of tools listed in the `tools\packages.config` file before running the Cake script.

# Installing Tools From Disk

If you want to install a tool that's not available via NuGet or if you prefer to store
the tool locally, you want to take a look at the [tool resolution conventions](tool-resolution).
