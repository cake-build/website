Order: 20
---

# Documentation

Cake uses a combination of standard [C# XML Documentation Comments](https://msdn.microsoft.com/en-us/library/b2s063f7.aspx) and attributes to auto generate documentation to this site.

## Classes

Classes purpose are documented with a [summary](https://msdn.microsoft.com/en-us/library/2d6dt3kf.aspx) tag and categorized using [CakeAliasCategoryAttribute](https://cakebuild.net/api/cake.core.annotations/cakealiascategoryattribute/) attribute.

```csharp
/// <summary>
/// Contains functionality for working with GitReleaseManager.
/// </summary>
[CakeAliasCategory("GitReleaseManager")]
public static class GitReleaseManagerAliases
{...}
```

## Methods

Methods purpose are documented with a [summary](https://msdn.microsoft.com/en-us/library/2d6dt3kf.aspx) tag, parameters with the [param](https://msdn.microsoft.com/en-us/library/8cw818w8.aspx) tag, if it returns somethings it's you use the [returns](https://msdn.microsoft.com/en-us/library/4dcfdeds.aspx) tag and categorized using [CakeAliasCategoryAttribute](https://cakebuild.net/api/cake.core.annotations/ffb6caa8) attribute.

Example usage is documented using the [example](https://msdn.microsoft.com/en-us/library/9w4cf933.aspx) tag and [code](https://msdn.microsoft.com/en-us/library/f8hahtxf.aspx) tag for code.

```csharp
/// <summary>
/// Makes the path absolute (if relative) using the current working directory.
/// </summary>
/// <example>
/// <code>
/// var path = MakeAbsolute(Directory("./resources"));
/// </code>
/// </example>
/// <param name="context">The context.</param>
/// <param name="path">The path.</param>
/// <returns>An absolute directory path.</returns>
[CakeMethodAlias]
[CakeAliasCategory("Path")]
public static DirectoryPath MakeAbsolute(this ICakeContext context, DirectoryPath path)
{...}
```

## Properties

Properties purpose are documented with a [summary](https://msdn.microsoft.com/en-us/library/2d6dt3kf.aspx) tag and it's value described with the [value](https://msdn.microsoft.com/en-us/library/azda5z79.aspx) tag.

```csharp
/// <summary>
/// Gets or sets a value indicating whether to allow installation of prerelease packages.
/// This flag is not required when restoring packages by installing from packages.config.
/// </summary>
/// <value>
///   <c>true</c> to allow installation of prerelease packages; otherwise, <c>false</c>.
/// </value>
public bool Prerelease { get; set; }
```

## Namespaces

Namespaces aren't documentable by default in the XML file format. Cake solves this by having an special internal class for each namespace called [NamespaceDoc](https://github.com/cake-build/cake/blob/develop/src/Cake.Common/Properties/Namespaces.cs#L12) which are documented using the [summary](https://msdn.microsoft.com/en-us/library/2d6dt3kf.aspx) tag.

The convention is to place all these classes in common [Namespaces.cs](https://github.com/cake-build/cake/blob/develop/src/Cake.Common/Properties/Namespaces.cs) file per assembly.

```csharp
namespace Cake.Common
{
    /// <summary>
    /// This namespace contain types used for common operations
    /// such as parsing release notes, retrieving arguments and
    /// to read and write environment variables.
    /// </summary>
    [CompilerGenerated]
    internal class NamespaceDoc
    {
    }
}
```
