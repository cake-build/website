Order: 40
Title: Tool directive
Description: Directive to install external command-line tools
---

The tool directive installs external command-line tools using NuGet.

# Usage

The directive takes a single package URI parameter
Right now, only NuGet packages are supported.
Specify the `include` parameter if the executable does not end with .exe

```csharp
#tool nuget:?package=Cake.Foo
#tool nuget:?package=Cake.Foo&version=1.2.3
#tool nuget:?package=Cake.Foo&prerelease
#tool nuget:https://myget.org/f/Cake/?package=Cake.Foo&prerelease
#tool nuget:?package=Cake.Foo&include=path/to/foo.cmd
// Local feed
#tool nuget:file://localhost/packages/?package=Cake.Foo
#tool nuget:file://localhost/packages/?package=Cake.Foo&version=1.2.3
#tool nuget:file://localhost/packages/?package=Cake.Foo&prerelease
```

## Include and Exclude Options:

```csharp
#tool nuget:?package=Cake.Foo&include=/**/NoFoo.exe
or
#tool nuget:?package=Cake.Foo&exclude=/**/Foo.exe
```
