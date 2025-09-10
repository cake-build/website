---
title: "Cake.Sdk 5.0.25253.70-beta released"
category: Announcement
author: devlead
---

A new preview version of **Cake.Sdk** has been released! Version **5.0.25253.70-beta** brings compatibility with .NET 10 RC1 and introduces powerful new features for modular build script organization. 🚀 🍰

### What's new in this release

- **Compiled with/against .NET 10 RC1** - Full compatibility with the latest .NET 10 release candidate
- **Multiple Main Entry Points** - Support for multiple `Main_*` methods for better script organization
- **Script Host IoC Integration** - Enhanced dependency injection with script host action execution
- **Performance Optimizations** - Significant improvements in build time and memory usage
- **New minimal template** - Easily scaffold new Cake projects with the streamlined minimal template

<!--excerpt-->

### .NET 10 RC1 Compatibility

This release is fully compiled against .NET 10 RC1, ensuring compatibility with the latest .NET 10 features and improvements. The SDK now takes full advantage of the latest framework capabilities while maintaining backward compatibility with .NET 8 and 9.

### Multiple Main Entry Points

One of the most exciting new features is support for multiple main entry points through `Main_*` methods. This allows you to organize your build scripts into logical modules while maintaining a single entry point.

```csharp
#:sdk Cake.Sdk@5.0.25253.70-beta

Task("Default")
    .IsDependentOn("Three");

RunTarget("Default");


// Additional main methods for different scenarios will be automatically invoked
static partial void Main_One()
{
    Task("One")
        .Does(() => Information("Building component one"));
}

static partial void Main_Two()
{
    Task("Two")
        .IsDependentOn("One")
        .Does(() => Information("Building component two"));
}
```

The generator automatically scans for `Main_*` methods and registers them as available entry points, making it easy to create modular build scripts that can be executed independently or as part of a larger workflow. This feature works in both file-based and project-based approaches, allowing you to organize your build logic across multiple files while maintaining a single entry point. This simplifies migrating from Cake script files that have Tasks registered in multiple files. When placing a partial main method in an external file, it must be inside a partial Program class:


```csharp
public static partial class Program
{
    static void Main_Three()
    {
        Task("Build-Three")
            .IsDependentOn("Build-Two")
            .Does(() => Information("Building component three"));
    }
}
```

### Script Host IoC Integration

Enhanced dependency injection capabilities now allow for deeper integration between the script host and IoC container. This enables dynamic task creation and more flexible service registration patterns, including the ability to add Cake Tasks via external Cake modules.

```csharp
#:sdk Cake.Sdk@5.0.25253.70-beta

var target = Argument("target", "Test");

Task("Build")
    .Does(() => {
       // Build
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        // Test
    });

RunTarget(target);

// Register services with script host integration
static partial void RegisterServices(IServiceCollection services)
{
    // Injects IOC-Task as an dependency of Build task
    services.AddSingleton(new Action<IScriptHost>(
                        host => host.Task("IOC-Task")
                                    .IsDependeeOf("Build")
                                    .Does(() => Information("Hello from IOC-Task"))));
}
```

This integration provides seamless access to registered services throughout your build script, enabling more sophisticated build automation patterns.

### Performance Optimizations

This release includes significant performance improvements that result in:

- Reduction in incremental build times
- Reduction in peak memory usage
- Reduction in CPU usage for large projects
- Better scalability with large numbers of Cake aliases


### Getting Started

To try out the latest preview, update your `global.json` file:

```json
{
  "sdk": {
    "version": "10.0.100-rc.1.25451.107"
  },
  "msbuild-sdks": {
    "Cake.Sdk": "5.0.25253.70-beta"
  }
}
```

Or install the latest [template package](https://www.nuget.org/packages/Cake.Template#readme-body-tab):

```bash
dotnet new install Cake.Template@5.0.25253.70-beta
```

Create a new Cake file-based project with sample project to build:

```bash
dotnet new cakefile --name cake --IncludeExampleProject true
```

And run it with:

```bash
dotnet cake.cs
```

### Minimal Template

For the simplest possible setup, you can use the new minimal template:

```bash
dotnet new cakeminimal --name cake
```

This creates a minimal `cake.cs` file with just the essential SDK directive and Information log statement. Perfect for getting started quickly or when you want to build your script from scratch.

### Multi-file Organization

For larger projects, you can organize your code across multiple files:

```bash
dotnet new cakemultifile --name cake
```

This creates a structure that supports both single-file and multi-file approaches, i.e., for use with the new `Main_*` methods providing flexible entry points for different build scenarios. This allows you to organize your build logic across multiple files while maintaining a single entry point.


### Requirements

- **File-based approach**: .NET 10 RC1 or later
- **Project-based approach**: .NET 8.0 or later
- Compatible with .NET 8.0, 9.0, and 10.0 target frameworks

### Example Repository

We've updated our comprehensive example repository at [github.com/cake-build/cakesdk-example](https://github.com/cake-build/cakesdk-example) to demonstrate:

- Multiple main entry points with `Main_*` methods
- Script host IoC integration patterns
- Performance-optimized build scripts
- Multi-file organization strategies

### GitHub Actions Support

The latest unreleased bits of [cake-action](https://github.com/cake-build/cake-action) include support for the new features:

```yaml
steps:
  - name: Run Cake with multiple main entry points
    uses: cake-build/cake-action@master
    with:
      file-path: path/to/Build.cs
      arguments: --target="Main_One"
```

### Feedback Welcome

This is still a preview release, and we'd love your feedback! You can:

- Try out the new multiple main entry points and IoC features
- Report issues on [GitHub](https://github.com/cake-build/generator/issues)
- Join the discussion on our [discussion board](https://github.com/orgs/cake-build/discussions)
- Contribute to the [source code](https://github.com/cake-build/generator/)

### Package References

- [Cake.Sdk](https://www.nuget.org/packages/Cake.Sdk) - The main SDK package
- [Cake.Generator](https://www.nuget.org/packages/Cake.Generator) - Source generator for Cake aliases
- [Cake.Template](https://www.nuget.org/packages/Cake.Template) - Templates for creating Cake projects

We're excited to see what you build with the enhanced Cake.Sdk capabilities! 🍰