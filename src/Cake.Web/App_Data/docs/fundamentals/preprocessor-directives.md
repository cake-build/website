---
content-type: markdown
---

Cake scripts have support for pre-processor line directives, which run before the script is executed.
These can be used to reference other scripts, assemblies, namespaces and more.

### Add-in directive
The add-in directive is used to install and reference assemblies using NuGet.

#### Usage
The directive takes a single package URI parameter
Right now, only NuGet packages are supported.

```csharp
#addin nuget:?package=Cake.Foo
#addin nuget:?package=Cake.Foo&version=1.2.3
#addin nuget:?package=Cake.Foo&prerelease
#addin nuget:https://myget.org/f/Cake/?package=Cake.Foo&prerelease
```

### Load directive
The load directive is used to reference external Cake scripts. Useful i.e. if you have common utility functions.

#### Usage
The directive has one parameter which is the location to the script.

```csharp
#l "utilities.cake"
or
#load "utilities.cake"
```

### Reference directive
The reference directive is used to reference external assemblies for use in your scripts.

#### Usage
The directive has one parameter which is the path to the dll to load.

```csharp
#r "bin/myassembly.dll"
or
#reference "bin/myassembly.dll"
```

### Tool directive
The tool directive installs external command-line tools using NuGet.

#### Usage
The directive takes a single package URI parameter
Right now, only NuGet packages are supported.

```csharp
#tool nuget:?package=Cake.Foo
#tool nuget:?package=Cake.Foo&version=1.2.3
#tool nuget:?package=Cake.Foo&prerelease
#tool nuget:https://myget.org/f/Cake/?package=Cake.Foo&prerelease
```

### Shebang directive
Under Unix-like operating systems, when a script with a shebang is run as a program, the program loader parses the rest of the script's initial line as an interpreter directive; the specified interpreter program is run instead, passing to it as an argument the path that was initially used when attempting to run the script.

These are only used for shells to identify how to run the script and are omitted when Cake compiles the script.

#### Usage
```bash
#!path/to/launch/cake
```
