Order: 25
Title: Available Constants
Description: Built-in constants available in Cake.Sdk
---

Cake.Sdk automatically generates several constants that are available throughout your build script. These constants provide information about the Cake.Generator version and when the aliases were generated.

# Available Constants

The following constants are automatically generated and available in your Cake script:

| Constant                               | Description                                                                            |
|----------------------------------------|----------------------------------------------------------------------------------------|
| `CakeGeneratorDate`                    | The UTC date and time when the aliases were generated (format: `yyyy-MM-dd HH:mm:ssZ`) |
| `CakeGeneratorVersion`                 | The version of Cake.Generator.Core used to generate the aliases                        |
| `CakeGeneratorInformationalVersion`    | The full informational version of Cake.Generator.Core, including build metadata        |
| `CakeGeneratorNuGetVersion`            | The NuGet package version of Cake.Generator.Core                                       |

# Usage

These constants are useful for version tracking and debugging purposes in your Cake scripts. You can use them to display version information or include them in build artifacts.

## Displaying Version Information

Here's an example of how to use these constants in your Cake script:

```csharp
#:sdk Cake.Sdk

var target = Argument("target", "Default");

Task("Version-Info")
    .Does(() =>
    {
        Information("Generated with Cake.Generator.Core version: {0}", CakeGeneratorVersion);
        Information("Generation date: {0}", CakeGeneratorDate);
        Information("Generation informational version: {0}", CakeGeneratorInformationalVersion);
        Information("Generation NuGet version: {0}", CakeGeneratorNuGetVersion);
    });

Task("Default")
    .IsDependentOn("Version-Info");

RunTarget(target);
```

When you run this task, you'll see output similar to:

```
════════════════════════════════════════════════════════════════════════════════════════════════════
 Version-Info
════════════════════════════════════════════════════════════════════════════════════════════════════
Generated with Cake.Generator.Core version: 6.0.0.0
Generation date: 2026-01-03 22:59:28Z
Generation informational version: 6.0.0+53e8acdf86db6bd1653f495fcb5bb450af9cfd30
Generation NuGet version: 6.0.0
```

# Constant Details

## CakeGeneratorDate

The `CakeGeneratorDate` constant contains the UTC date and time when the Cake aliases were generated during the build process. The format is `yyyy-MM-dd HH:mm:ssZ`.

**Example value**: `2026-01-03 22:59:28Z`

## CakeGeneratorVersion

The `CakeGeneratorVersion` constant contains the version number of Cake.Generator.Core that was used to generate the aliases. This is typically a semantic version number.

**Example value**: `6.0.0.0`

## CakeGeneratorInformationalVersion

The `CakeGeneratorInformationalVersion` constant contains the full informational version string, which may include additional build metadata such as commit hash, build date, or other version information.

**Example value**: `6.0.0+53e8acdf86db6bd1653f495fcb5bb450af9cfd30`

## CakeGeneratorNuGetVersion

The `CakeGeneratorNuGetVersion` constant contains the NuGet package version of Cake.Generator.Core. This matches the version that would be used when referencing the package from NuGet.

**Example value**: `6.0.0`

# See also

- [Cake.Generator README](https://github.com/cake-build/generator/blob/main/README.md)
