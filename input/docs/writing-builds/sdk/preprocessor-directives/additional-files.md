Order: 60
Title: Additional files
Description: Organize code across multiple files in file-based Cake apps
---

For larger file-based Cake apps, you can organize your code into multiple files. Use the `IncludeAdditionalFiles` and `ExcludeAdditionalFiles` properties to control which files are included during compilation. This allows you to place models, utility functions, and other code in separate files.

# Usage

Use the `#:property` directive to specify which additional files should be included or excluded during compilation.

## Include additional files

Use `IncludeAdditionalFiles` to specify which files should be included:

```csharp
#:sdk Cake.Sdk
#:property IncludeAdditionalFiles=build/**/*.cs
```

This will include all `.cs` files in the `build` directory and its subdirectories.

## Exclude additional files

Use `ExcludeAdditionalFiles` to specify which files should be excluded:

```csharp
#:sdk Cake.Sdk
#:property IncludeAdditionalFiles=build/**/*.cs
#:property ExcludeAdditionalFiles=build/**/Except*.cs
```

This will include all `.cs` files in the `build` directory, except for files that match the `ExcludeAdditionalFiles` pattern.

# Example

Here's a complete example showing how to organize a multi-file Cake app:

## Main build file (build.cs)

```csharp
#:sdk Cake.Sdk
#:property IncludeAdditionalFiles=build/**/*.cs
#:property ExcludeAdditionalFiles=build/**/Except*.cs

var target = Argument("target", "Default");
var config = new BuildConfiguration 
{ 
    ProjectName = "MyProject",
    Version = "1.0.0" 
};

Task("Default")
    .Does(() =>
{
    BuildUtilities.LogInfo($"Building {config.ProjectName} v{config.Version}");
});

RunTarget(target);
```

## Models file (build/Models.cs)

```csharp
public class BuildConfiguration
{
    public string ProjectName { get; set; } = "";
    public string Version { get; set; } = "";
}
```

## Utilities file (build/Utilities.cs)

```csharp
public static partial class Program
{
    public static void LogInfo(string message)
    {
        Information($"INFO: {message}");
    }
}
```

## Excluded file (build/ExceptThisFile.cs)

```csharp
// This class will not be compiled.
public class UnusedLogic
{
}
```

The `ExceptThisFile.cs` file will not be compiled because it matches the `ExcludeAdditionalFiles` pattern.

# Pattern syntax

Both `IncludeAdditionalFiles` and `ExcludeAdditionalFiles` support glob patterns:

- `**` - Matches any number of directories
- `*` - Matches any number of characters (except path separators)
- `?` - Matches a single character
- `[abc]` - Matches any character in the set

## Examples

Include all C# files in a specific directory:

```csharp
#:property IncludeAdditionalFiles=build/**/*.cs
```

Include files from multiple directories:

```csharp
#:property IncludeAdditionalFiles=build/**/*.cs;scripts/**/*.cs
```

Exclude test files:

```csharp
#:property IncludeAdditionalFiles=build/**/*.cs
#:property ExcludeAdditionalFiles=build/**/*Test*.cs
```

# See also

- [#:property directive](/docs/writing-builds/sdk/preprocessor-directives/property)
- [File-based apps - .NET | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/sdk/file-based-apps)
