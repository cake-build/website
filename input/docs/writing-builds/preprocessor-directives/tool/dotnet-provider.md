Order: 10
Title: .NET Tool Provider
Description: Package installer for installing tools using the dotnet CLI
---

Package installer for installing tools using the dotnet CLI.

The following URI parameters are supported by the `dotnet` package installer.

# Source

This is not a named parameter, but it is permitted as per the URI definition.
By default, the dotnet CLI will attempt to install tools from nuget.org.
If the package is hosted on another feed the installation source can be overridden.

## Example

```csharp
#tool dotnet:https://www.myget.org/F/cake-build/api/v2?package=Octopus.DotNet.Cli
```

# Package

The name of the .NET Tool that should be installed.

## Example

```csharp
#tool dotnet:?package=Octopus.DotNet.Cli
```

# Version

The specific version of the .NET Tool that should be installed.
If not provided, the dotnet CLI will install the latest package version that is available.

## Example

```csharp
#tool dotnet:?package=Octopus.DotNet.Cli&version=4.41.0
```

# Global

By default, a tool will be installed to the configured Cake Tools folder.
If the tool should be installed globally on the machine, the `global` parameter need to be passed.

## Example

```csharp
#tool dotnet:?package=Octopus.DotNet.Cli&version=4.41.0&global
```

# Config File

Allows to specify a NuGet config to use for example to authenticate to a particular feed.

## Example

```csharp
#tool dotnet:?package=Octopus.DotNet.Cli&version=4.41.0&configfile="../../NuGet.config"
```

# Ignore failed sources

Ignores failed NuGet sources as long as the package could be restored.

## Example

```csharp
#tool dotnet:?package=Octopus.DotNet.Cli&version=4.41.0&ignore-failed-sources"
```

# Framework

Specifies the target framework to install the tool for.
By default, the .NET Core SDK tries to choose the most appropriate target framework.

## Example

```csharp
#tool dotnet:?package=Octopus.DotNet.Cli&version=4.41.0&framework="net472"
```

[target framework]: https://docs.microsoft.com/en-us/dotnet/standard/frameworks

# Verbosity

Logging verbosity of the underlying `dotnet` command can be altered through the verbosity of the Cake execution.

## Example

```bash
./build.sh --verbosity=diagnostic
```
