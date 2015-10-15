---
content-type: markdown
---

Cake scripts have support for pre-processor line directives, which run before the script is executed.
These can be used to reference other scripts, assemblies, namespaces and more.

### Add-in directive
The add-in directive is used to install and reference assemblies using NuGet.

#### Usage
The directive takes an NuGet package id as first parameter and has NuGet source as an optional second parameter.

```csharp
#addin "package id" ["source"]
```

### Load directive
The load directive is used to reference external Cake scripts. Useful i.e. if you have common utility functions.

#### Usage
The directive has one parameters which is the location to the script.

```csharp
#l "utilities.cake"
or
#load "utilities.cake"
```

### Reference directive
The reference directive is used to reference external assemblies for use in your scripts.

#### Usage
The directive has one parameters which is the path to the dll to load.
```csharp
#r "bin/myassembly.dll"
or
#reference "bin/myassembly.dll"
```

### Tool directive
The tool directive installs external command-line tools using NuGet.

#### Usage
The directive takes an NuGet package id as first parameter and has NuGet source as an optional second parameter.

```csharp
#tool "package id" ["source"]
```

### Shebang directive
Under Unix-like operating systems, when a script with a shebang is run as a program, the program loader parses the rest of the script's initial line as an interpreter directive; the specified interpreter program is run instead, passing to it as an argument the path that was initially used when attempting to run the script.

These are only used for shells to identify how to run the script and are omitted when Cake compiles the script.

#### Usage
```bash
#!path/to/launch/cake
```
