Order: 20
Title: NuGet Provider
Excerpt: Package installer for installing tools packaged as NuGet packages
---

Package installer for installing tools packaged as NuGet packages.

The following URI parameters are supported by the `nuget` package installer.

# Source

This is not a named parameter, but it is permitted as per the URI definition.
By default, the provider will attempt to install tools from nuget.org.
If the package is hosted on another feed the installation source can be overridden.

## Example

Download from myget.org:

```csharp
#tool nuget:https://myget.org/f/Cake/?package=Cake.Foo
```

Install tool from a local directory:

```csharp
#tool nuget:file://localhost/packages/?package=Cake.Foo
```

# Package

The name of the NuGet package that should be installed.

## Example

```csharp
#tool nuget:?package=Cake.Foo
```

# Version

The specific version of the NuGet package that should be installed.
If not provided, the latest version package that is available will be installed.

## Example

```csharp
#tool nuget:?package=Cake.Foo&version=1.2.3
```

# Prerelease

To install prerelease tools without defining a specific `version` (`#tool nuget:?package=Cake.Foo&version=1.2.3-beta`), the `prerelease` parameter needs to be passed.

## Example

```csharp
#tool nuget:?package=Cake.Foo&prerelease
```

# Include

The `include` parameter allows to define the files which should be included.

:::{.alert .alert-info}
If the tool filename does not end with `.exe` include needs to be passed.
:::

## Example

Include a tool with an extension different than `.exe`:

```csharp
#tool nuget:?package=Cake.Foo&include=path/to/foo.cmd
```

Include only specific files:

```csharp
#tool nuget:?package=Cake.Foo&include=/**/NoFoo.exe
```

# Exclude

The `exclude` parameter allows exclude specific files.

## Example

```csharp
#tool nuget:?package=Cake.Foo&exclude=/**/Foo.exe
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

[out of process NuGet client]: /docs/running-builds/configuration/default-configuration-values#in-process-nuget-installation
[Show Process Command Line configuration value]: /docs/running-builds/configuration/default-configuration-values#show-process-command-line