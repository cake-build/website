Order: 15
Title: Running tools
Description: Different ways to run external tools in Cake builds
---

Cake provides several ways to run external tools. The best approach depends on whether a typed alias already exists, whether the tool is exposed by an addin, or whether you need full control over process execution.

| Approach | Best for |
|----------|----------|
| Built-in aliases from [Cake.Common] | Common tools with first-class support in Cake |
| Alias from an addin | Tools supported by community addins |
| [Command alias] | Generic command execution with Cake tool resolution |
| [StartProcess alias] | Full low-level process control |

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

### Cake.Sdk

When using Cake.Sdk, reference add-ins with the `#:package` preprocessor directive in file-based apps. The add-in aliases are then available globally.

```csharp
#:sdk Cake.Sdk@6.1.1
#:package Cake.Docker@1.5.0-beta.1

Task("DockerRm")
    .Does(() =>
{
    // one or more container names
    DockerRm("containerName1", "containerName2");
});
```

### Cake .NET Tool

With the Cake .NET Tool, use the `#addin` preprocessor directive in Cake scripts. The add-in aliases are then available globally.
### Cake.Sdk

When using Cake.Sdk, reference add-ins with the `#:package` preprocessor directive in file-based apps. The add-in aliases are then available globally.

```csharp
#:sdk Cake.Sdk@6.1.1
#:package Cake.Docker@1.5.0-beta.1

Task("DockerRm")
    .Does(() => {
        // one or more container names
        DockerRm("containerName1", "containerName2");
    });

```

### Cake .NET Tool

With the Cake .NET Tool, use the `#addin` preprocessor directive in Cake scripts. The add-in aliases are then available globally.

```csharp
#addin nuget:?package=Cake.Docker&version=1.5.0-beta.1&prerelease

Task("DockerRm")
    .Does(() => {
        // one or more container names
        DockerRm("containerName1", "containerName2");
    });
```

### Cake Frosting

Add the add-in as a package reference in your Frosting project file:

```xml
<PackageReference Include="Cake.Docker" Version="1.5.0-beta.1" />
```

Then use it as an extension method on the build context in your tasks:

```csharp
[TaskName("DockerRm")]
public sealed class DockerRmTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DockerRm("containerName1", "containerName2");
    }
}
```

### Cake Frosting

Add the add-in as a package reference in your Frosting project file:

```xml
<PackageReference Include="Cake.Docker" Version="1.5.0-beta.1" />
```

Then use it as an extension method on the build context in your tasks:

```csharp
[TaskName("DockerRm")]
public sealed class DockerRmTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DockerRm("containerName1", "containerName2");
    }
}
```

Why use this approach:

- Gives you a typed, tool-specific API.
- Usually easier than manually constructing command lines.

## Use Command aliases (Cake 2.3.0+)

Use [Command alias] when you want generic command execution, but still want Cake's tool resolution and convenient defaults.

### Cake.Sdk

Call `InstallTool` during setup so the .NET tool package is available to later tasks.

```csharp
#:sdk Cake.Sdk@6.1.1

Setup(context =>
{
    InstallTool("dotnet:https://api.nuget.org/v3/index.json?package=dotnet-ef&version=8.0.0");
});

Task("Ef-Version")
    .Does(() =>
{
    Command(
        new[] { "dotnet", "dotnet.exe" },
        "ef --version"
    );
});
```

### Cake .NET Tool

Use the `#tool` preprocessor directive so Cake downloads the tool into the build's tool cache before the script runs.

```csharp
#tool dotnet:?package=dotnet-ef&version=8.0.0

Task("Ef-Version")
    .Does(() =>
{
    Command(
        new[] { "dotnet", "dotnet.exe" },
        "ef --version"
    );
});
```

### Cake Frosting

Register the tool when building the host with `InstallTool`, then invoke the command from a task via the build context.

```csharp
public sealed class Program : IFrostingStartup
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .InstallTool(new Uri("dotnet:?package=dotnet-ef&version=8.0.0"))
            .Run(args);
    }
}
```

```csharp
[TaskName("Ef-Version")]
public sealed class EfVersionTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.Command(
            new[] { "dotnet", "dotnet.exe" },
            "ef --version"
        );
    }
}
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

[Cake.Common]: https://cakebuild.net/api/Cake.Common/
[Command alias]: https://cakebuild.net/dsl/command/
[StartProcess alias]: https://cakebuild.net/dsl/process/
