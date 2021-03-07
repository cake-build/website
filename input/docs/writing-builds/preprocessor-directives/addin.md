Order: 10
Title: Addin directive
Description: Directive to install and reference assemblies using NuGet
---

The add-in directive is used to install and reference assemblies using NuGet.

# Usage

The directive takes a single package URI parameter
Right now, only NuGet packages are supported.

```csharp
#addin nuget:?package=Cake.Foo
#addin nuget:?package=Cake.Foo&version=1.2.3
#addin nuget:?package=Cake.Foo&prerelease
#addin nuget:https://myget.org/f/Cake/?package=Cake.Foo&prerelease
// Local feed
#addin nuget:file://localhost/packages/?package=Cake.Foo
#addin nuget:file://localhost/packages/?package=Cake.Foo&version=1.2.3
#addin nuget:file://localhost/packages/?package=Cake.Foo&prerelease
```

## Dependencies

:::{.alert .alert-success}
Available since Cake 0.22.0.
:::

There's an option to fetch and load NuGet dependencies

```csharp
#addin nuget:?package=foo&loaddependencies=true
or
#addin nuget:?package=foo.bar&loaddependencies=false
```

:::{.alert .alert-warning}
This feature requires Cake to be [configured to use in-process NuGet client](/docs/running-builds/configuration/default-configuration-values#in-process-nuget-installation)
instead of external `nuget.exe`.
:::

## Include and Exclude Options:

```csharp
#addin nuget:?package=Cake.Foo&include=/**/NoFoo.dll
or
#addin nuget:?package=Cake.Foo&exclude=/**/Foo.dll
```
