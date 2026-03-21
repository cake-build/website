Order: 15
Title: Running tools
Description: Different ways to run external tools in Cake builds
---

Cake provides several ways to run external tools. The best approach depends on whether a typed alias already exists, whether the tool is exposed by an addin, or whether you need full control over process execution.

| Approach | Best for | Runner support |
|----------|----------|----------------|
| Built-in aliases from [Cake.Common] | Common tools with first-class support in Cake | Cake .NET Tool, Cake Frosting, Cake.Sdk |
| Alias from an addin | Tools supported by community addins | Cake .NET Tool, Cake Frosting |
| [Command alias] | Generic command execution with Cake tool resolution | Cake .NET Tool, Cake Frosting |
| [StartProcess alias] | Full low-level process control | Cake .NET Tool, Cake Frosting, Cake.Sdk |

## Use built-in aliases from Cake.Common

If Cake already has a built-in alias for the tool, this is usually the easiest and safest option.

```csharp
Task("Build")
    .Does(() =>
{
    DotNetBuild("./src/MyApp.sln");
});
```

Why use this approach:

- Strongly-typed settings.
- Better discoverability through IntelliSense.
- Less manual argument handling.

## Use aliases provided by addins

If there is no built-in alias, check whether an addin provides one.

```csharp
#addin nuget:?package=Cake.GitVersion&version=5.12.0
#tool dotnet:?package=GitVersion.Tool&version=6.3.0

Task("Version")
    .Does(() =>
{
    var version = GitVersion();
    Information("Version: {0}", version.FullSemVer);
});
```

Why use this approach:

- Gives you a typed, tool-specific API.
- Usually easier than manually constructing command lines.

## Use Command aliases (Cake 2.3.0+)

Use [Command alias] when you want generic command execution, but still want Cake's tool resolution and convenient defaults.

```csharp
#tool dotnet:?package=dotnet-ef&version=8.0.0

Task("Ef-Version")
    .Does(() =>
{
    Command(
        new [] { "dotnet", "dotnet.exe" },
        "ef --version"
    );
});
```

Why use this approach:

- Uses Cake tool resolution conventions.
- Fails build by default on unexpected exit codes.
- Supports capturing output with overloads when needed.

## Use StartProcess alias

Use [StartProcess alias] when you need full control over process startup and settings.

```csharp
Task("NuGet-Help")
    .Does(() =>
{
    var nugetPath = Context.Tools.Resolve("nuget.exe");

    StartProcess(nugetPath, new ProcessSettings {
        Arguments = new ProcessArgumentBuilder()
            .Append("help")
            .Append("install")
    });
});
```

Why use this approach:

- Maximum flexibility.
- Works well for custom executables and advanced process settings.

## Which one should I choose?

Use this order of preference:

1. Built-in aliases from [Cake.Common].
2. Aliases from addins.
3. [Command alias] for generic command execution.
4. [StartProcess alias] for low-level process control.

[Cake.Common]: /api/Cake.Common/
[Command alias]: /dsl/command/
[StartProcess alias]: /dsl/process/