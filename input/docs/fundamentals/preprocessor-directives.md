Order: 90
---

Cake scripts have support for pre-processor line directives, which run before the script is executed.
These can be used to reference other scripts, assemblies, namespaces and more.

# Add-in directive
The add-in directive is used to install and reference assemblies using NuGet.

## Usage
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

### Dependencies

From Cake version 0.22.0 there's an option to fetch and load NuGet dependencies

```csharp
#addin nuget:?package=foo&loaddependencies=true
or
#addin nuget:?package=foo.bar&loaddependencies=false
```
This feature requires Cake to be [configured](/docs/fundamentals/configuration) to not use `nuget.exe` but instead let Cake handle NuGet installation in-process.

### Include and Exclude Options:

```csharp
#addin nuget:?package=Cake.Foo&include=/**/NoFoo.dll
or
#addin nuget:?package=Cake.Foo&exclude=/**/Foo.dll
```

# Load directive
The load directive is used to reference external Cake scripts. Useful i.e. if you have common utility functions.
Starting from 0.18.0 you can also load cake scripts from nuget.

## Usage
The directive has one parameter which is the location to the script which optionally includes a scheme: `local` or `nuget`. The default is `local`.

### Default:
```csharp
#l "scripts/utilities.cake"
or
#load "scripts/utilities.cake"
```
Attempts to load `utilities.cake` from `scripts` directory.

### Local scheme:
```csharp
#l "local:?path=scripts/utilities.cake"
or
#load "local:?path=scripts/utilities.cake"
```
Attempts to load `utilities.cake` from `scripts` directory

### Nuget scheme:
```csharp
#l "nuget:?package=utilities.cake"
or
#load "nuget:?package=utilities.cake"
```
Attempts to load `utilities.cake` from nuget

### Include and Exclude Options:

```csharp
#load nuget:?package=utilities.cake&include=/**/NoFoo.cake
or
#load nuget:?package=utilities.cake&=exclude/**/Foo.cake
```

# Reference directive
The reference directive is used to reference external assemblies for use in your scripts.

## Usage
The directive has one parameter which is the path to the dll to load.

```csharp
#r "bin/myassembly.dll"
or
#reference "bin/myassembly.dll"
```

# Tool directive
The tool directive installs external command-line tools using NuGet.

## Usage
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

### Include and Exclude Options:

```csharp
#tool nuget:?package=Cake.Foo&include=/**/NoFoo.exe
or
#tool nuget:?package=Cake.Foo&=exclude/**/Foo.exe
```

# Shebang directive
Under Unix-like operating systems, when a script with a shebang is run as a program, the program loader parses the rest of the script's initial line as an interpreter directive; the specified interpreter program is run instead, passing to it as an argument the path that was initially used when attempting to run the script.

These are only used for shells to identify how to run the script and are omitted when Cake compiles the script.

## Usage
```bash
#!path/to/launch/cake
```

# Break directive
The break directive, when placed on any line within your Cake file, will cause `System.Diagnostics.Debugger.Break();` to be emitted at runtime.  As a result, when used in conjunction with the `-debug` parameter (which can be passed into the Cake.exe), the debugger will automatically stop on these lines.

**NOTE:** If the debugger is not currently attached, then the `#break` directive is simply ignored.

## Usage

```bash
#break
```

# Using static directive
The using static directive allows referencing a type's static members without needed to specify the type name.

## Usage
```csharp
using static System.Math;

Information(Round(1.1));
```

# Module directive

The module directive lets you boostrap Cake modules by downloading them from a NuGet source.

## Usage

```cake
#module nuget:?package=Cake.UrlLoadDirective.Module&version=1.0.2
```
As modules can change and extend the internals of Cake, this bootstrapping needs to be done before Cake executes. This is solved by bootrapping being it's own step by invoking Cake with a `--bootstrap` argument before you execute the script normally.

```bash
./cake.exe build.cake --bootstrap
./cake.exe build.cake
```
