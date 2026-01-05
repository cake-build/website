Order: 40
Title: Multiple Entry Points
Description: Define multiple entry points that execute before top-level main
---

Cake.Sdk supports multiple entry points using methods prefixed with `Main_*`. These methods are automatically discovered and executed before the top-level main code, allowing you to organize tasks across multiple files and create more modular build scripts.

# Overview

Multiple entry points provide a way to define tasks and setup logic in separate methods that are automatically discovered and executed. This is particularly useful for:

- Organizing tasks by feature or module
- Creating conditional task registration
- Building dynamic task structures based on configuration
- Splitting large build scripts into manageable pieces

# Basic Usage

Define entry points by creating methods prefixed with `Main_` in a `partial class Program`:

```csharp
#:sdk Cake.Sdk

var target = Argument("target", "Default");

Task("Default")
    .Does(() =>
    {
        Information("Hello from top-level main!");
    });

RunTarget(target);

public static partial class Program
{
    private static void Main_One()
    {
        Task(nameof(Main_One))
            .IsDependeeOf("Clean")
            .Does(() => Information("Hello from Main_One"));
    }

    private static void Main_Two()
    {
        Task(nameof(Main_Two))
            .IsDependeeOf("Clean")
            .Does(() => Information("Hello from Main_Two"));
    }
}
```

## Naming Convention

- Methods must be prefixed with `Main_` to be automatically discovered
- The method name after `Main_` can be any valid C# identifier
- Methods must be `static` and can be `private` or `public`
- Methods must be part of a `partial class Program`

# Execution Order

`Main_*` methods are executed in the following order:

1. All `Main_*` methods are discovered and executed first
2. Top-level main code (outside of methods) is executed after all `Main_*` methods
3. `RunTarget` is called at the end

This ensures that tasks defined in `Main_*` methods are available when the top-level code runs.

# Examples

## Basic Multiple Entry Points

Here's a simple example with two entry points:

```csharp
#:sdk Cake.Sdk

var target = Argument("target", "Default");

Task("Clean")
    .Does(() => Information("Cleaning..."));

Task("Build")
    .IsDependentOn("Clean")
    .Does(() => Information("Building..."));

RunTarget(target);

public static partial class Program
{
    private static void Main_PreClean()
    {
        Task("PreClean")
            .IsDependeeOf("Clean")
            .Does(() => Information("Preparing for clean operation"));
    }

    private static void Main_PreBuild()
    {
        Task("PreBuild")
            .IsDependeeOf("Build")
            .Does(() => Information("Preparing for build operation"));
    }
}
```

## Entry Points with Criteria

You can use criteria to conditionally register tasks:

```csharp
#:sdk Cake.Sdk

var target = Argument("target", "Default");
var rebuild = HasArgument("rebuild");
var buildData = new BuildData(rebuild);

Task("Clean")
    .Does(() => Information("Cleaning..."));

RunTarget(target);

public record BuildData(bool Rebuild);

public static partial class Program
{
    static void Main_PreClean()
    {
        Task("PreClean")
            .IsDependeeOf("Clean")
            .WithCriteria<BuildData>(c => c.Rebuild, nameof(BuildData.Rebuild))
            .Does(() => Information("Preparing for clean operation"));
    }
}
```

## Entry Points with Task Dependencies

Entry points can define tasks with dependencies:

```csharp
#:sdk Cake.Sdk

var target = Argument("target", "Default");

Task("Build")
    .IsDependentOn("Validate")
    .Does(() => Information("Building..."));

RunTarget(target);

public static partial class Program
{
    private static void Main_Setup()
    {
        Task("Setup")
            .Does(() =>
            {
                Information("Setting up build environment...");
            });
    }

    private static void Main_Validate()
    {
        Task("Validate")
            .IsDependentOn("Setup")
            .Does(() =>
            {
                Information("Validating configuration...");
            });
    }
}
```

# Use Cases

## Organizing Tasks by Feature

Split your build script into logical modules:

```csharp
public static partial class Program
{
    private static void Main_Testing()
    {
        Task("UnitTest")
            .Does(() => Information("Running unit tests..."));

        Task("IntegrationTest")
            .IsDependentOn("UnitTest")
            .Does(() => Information("Running integration tests..."));
    }

    private static void Main_Packaging()
    {
        Task("Pack")
            .Does(() => Information("Packing artifacts..."));

        Task("Publish")
            .IsDependentOn("Pack")
            .Does(() => Information("Publishing artifacts..."));
    }
}
```

## Conditional Task Registration

Register tasks based on configuration or arguments:

```csharp
public static partial class Program
{
    private static void Main_ConditionalTasks()
    {
        if (HasArgument("include-tests"))
        {
            Task("RunTests")
                .IsDependeeOf("Build")
                .Does(() => Information("Running tests..."));
        }
    }
}
```

## Dynamic Task Creation

Create tasks dynamically based on configuration:

```csharp
public static partial class Program
{
    private static void Main_DynamicTasks()
    {
        var environments = new[] { "Dev", "Staging", "Production" };
        
        foreach (var env in environments)
        {
            Task($"Deploy-{env}")
                .Does(() => Information($"Deploying to {env}..."));
        }
    }
}
```

# Multi-file Structure

When using a multi-file structure, you can organize `Main_*` methods across multiple files:

## Main file (build.cs)

```csharp
#:sdk Cake.Sdk
#:property IncludeAdditionalFiles=build/**/*.cs

var target = Argument("target", "Default");

Task("Default")
    .Does(() => Information("Hello from main file!"));

RunTarget(target);
```

## Task definitions file (build/Task.cs)

```csharp
public static partial class Program
{
    static void Main_PreClean()
    {
        Task("PreClean")
            .IsDependeeOf("Clean")
            .WithCriteria<BuildData>(c => c.Rebuild, nameof(BuildData.Rebuild))
            .Does(() => Information("Preparing for clean operation"));
    }
}
```

## Additional task file (build/Testing.cs)

```csharp
public static partial class Program
{
    private static void Main_Testing()
    {
        Task("UnitTest")
            .Does(() => Information("Running unit tests..."));

        Task("IntegrationTest")
            .IsDependentOn("UnitTest")
            .Does(() => Information("Running integration tests..."));
    }
}
```

All `Main_*` methods across all included files will be automatically discovered and executed before the top-level main code.

# See also

- [Additional files](/docs/writing-builds/sdk/preprocessor-directives/additional-files)
- [IoC Container](/docs/writing-builds/sdk/ioc-container)
