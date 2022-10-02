Order: 20
Title: Load directive
Description: Directive to reference external Cake scripts
---

The load directive is used to reference external Cake scripts. Useful i.e. if you have common utility functions.
Starting from 0.18.0 you can also load cake scripts from nuget.

# Usage

The directive has one parameter which is the location to the script which optionally includes a scheme: `local` or `nuget`. The default is `local`.

## Default

```csharp
#l "scripts/utilities.cake"
or
#load "scripts/utilities.cake"
```
Attempts to load `utilities.cake` from `scripts` directory.

## Local scheme:

```csharp
#l "local:?path=scripts/utilities.cake"
or
#load "local:?path=scripts/utilities.cake"
```
Attempts to load `utilities.cake` from `scripts` directory

## NuGet scheme

```csharp
#l "nuget:?package=utilities.cake"
or
#load "nuget:?package=utilities.cake"
```
Attempts to load `utilities.cake` from nuget

## Include and Exclude Options

```csharp
#load nuget:?package=utilities.cake&include=/**/NoFoo.cake
or
#load nuget:?package=utilities.cake&exclude=/**/Foo.cake
```
