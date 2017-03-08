Order: 10
---

This guide will demonstrate how to install tool executables to make sure 
they are discovered by your build script.

# From NuGet

## Via bootstrapper

The recommended way of using Cake is by using the bootstrapper script which you can
read more about in the [Setting up a new project](/docs/tutorials/setting-up-a-new-project).  

When using the bootstrapper script you can simply add the NuGet package to the 
`packages.config` file in the `tools` directory.

## Via script

Cake extends the C# language with custom pre-processor directives, and we've added one 
to automatically download a tool from NuGet and install it in the `tools` folder.

To download the [xunit.runner.console package](https://www.nuget.org/packages/xunit.runner.console) 
as part of executing your build script, simply use the `#tool` directive.

```csharp
#tool "xunit.runner.console"
```

For more information see [preprocessor directives](/docs/fundamentals/preprocessor-directives)

# From disk

If you want to install a tool that's not available via NuGet or if you prefer to store 
the tool locally, you want to take a look at the 
[tool resolution conventions](/docs/tools/tool-resolution).
