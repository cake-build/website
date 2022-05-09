Order: 10
Title: Addin directive
Excerpt: Directive to install and reference addins using NuGet
---

Supported in: **[Cake .NET Tool] | [Cake runner for .NET Framework] | [Cake runner for .NET Core]**

The addin directive is used to install and reference addins using NuGet:

```csharp
#addin nuget:?package=Cake.Foo&version=1.0.0
```

The addin directive supports also optional quotes:

```csharp
#addin "nuget:?package=Cake.Foo&version=1.0.0"
```

:::{.alert .alert-info}
The `#addin` directive is not available when running with [Cake Frosting].
Instead addins can be added as NuGet packages to the [Cake Frosting] project.
:::

The following URI parameters are supported by the addin directive.

# Source

This is not a named parameter, but it is permitted as per the URI definition.
By default, the provider will attempt to install addins from nuget.org.
If the package is hosted on another feed the installation source can be overridden.

## Example

Download from myget.org:

```csharp
#addin nuget:https://myget.org/f/Cake/?package=Cake.Foo
```

Install addin from a local directory:

```csharp
#addin nuget:file://C:/MyPackages/?package=Cake.Foo
```

# Package

The name of the NuGet package that should be installed.

## Example

```csharp
#addin nuget:?package=Cake.Foo
```

# Version

The specific version of the NuGet package that should be installed.
If not provided, the latest version package that is available will be installed.

## Example

```csharp
#addin nuget:?package=Cake.Foo&version=1.2.3
```

# Prerelease

To install prerelease tools without defining a specific `version` (`#addin nuget:?package=Cake.Foo&version=1.2.3-beta`), the `prerelease` parameter needs to be passed.

## Example

```csharp
#addin nuget:?package=Cake.Foo&prerelease
```

# Dependencies

:::{.alert .alert-success}
Available since Cake 0.22.0.
:::

By default dependencies defined on the NuGet package are not loaded.
The `loaddependencies` parameter allows to fetch and load dependent NuGet packages.

:::{.alert .alert-warning}
This feature requires Cake to be [configured to use in-process NuGet client] instead of external `nuget.exe`.
:::

## Example

```csharp
#addin nuget:?package=Cake.Foo&loaddependencies=true
```

# Include

The `include` parameter allows to define the files which should be included.

## Example

```csharp
#addin nuget:?package=Cake.Foo&include=/**/NoFoo.dll
```

# Exclude

The `exclude` parameter allows exclude specific files.

## Example

```csharp
#tool nuget:?package=Cake.Foo&exclude=/**/Foo.dll
```

# Diagnostic

Logging verbosity can be altered through the verbosity of the Cake execution.

:::{.alert .alert-info}
When using the [out of process NuGet client], the [Show Process Command Line configuration value]
can be set to show the executed command, even without diagnostic verbosity.
:::

## Example

```bash
./build.sh --verbosity=diagnostic
```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting
[Cake runner for .NET Framework]: /docs/running-builds/runners/deprecated/cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: /docs/running-builds/runners/deprecated/cake-runner-for-dotnet-core
[configured to use in-process NuGet client]: /docs/running-builds/configuration/default-configuration-values#in-process-nuget-installation
[out of process NuGet client]: /docs/running-builds/configuration/default-configuration-values#in-process-nuget-installation
[Show Process Command Line configuration value]: /docs/running-builds/configuration/default-configuration-values#show-process-command-line