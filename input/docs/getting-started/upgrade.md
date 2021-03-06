Order: 60
Title: Upgrade instructions
Description: Upgrade instructions between different versions of Cake
---

# Cake 1.0.0 to Cake 1.1.0

## Cake.DotNetTool.Module

Starting with this release, `Cake.DotNetTool.Module` will always be released together with Cake and will only be compatible with the current release.

If you use `Cake.DotNetTool.Module` already on your builds remove the `Cake.DotNetTool.Module` from your build script as it's no longer needed.

# Cake 0.38.x to Cake 1.0.0

Cake 1.0 is a major version containing breaking changes.

## Replace obsolete members

Members marked as obsolete in previous versions have been removed in Cake 1.0.
Update to the member suggested in the obsolete message.

## Cake Frosting

### Removal of CakeHostBuilder

`CakeHostBuilder` has been removed.
With [Cake Frosting] 1.0 `CakeHost` can be used directly to create the `CakeHost` object.

With [Cake Frosting] 0.38.x:

```csharp
// Create the host.
var host =
    new CakeHostBuilder()
       .WithArguments(args)
       .UseStartup<Program>()
       .Build();

// Run the host.
return host.Run();
```

With [Cake Frosting] 1.0:

```csharp
// Create and run the host.
return
    new CakeHost()
        .UseContext<BuildContext>()
        .Run(args);
```

### Removal of ICakeServices

`ICakeServices` has been removed.
With [Cake Frosting] 1.0 you no longer need to implement the `IFrostingStartup` interface in the `Program` class.
Configuration can be done directly on the `CakeHost` object instead.

With [Cake Frosting] 0.38.x:

```csharp
public class Program : IFrostingStartup
{
    public static int Main(string[] args)
    {
        // Create the host.
        var host =
            new CakeHostBuilder()
                .WithArguments(args)
                .UseStartup<Program>()
                .Build();

        // Run the host.
        return host.Run();
    }

    public void Configure(ICakeServices services)
    {
        services.UseContext<BuildContext>();
        services.UseLifetime<Lifetime>();
        services.UseWorkingDirectory("..");
    }
}
```

With [Cake Frosting] 1.0:

```csharp
public class Program : IFrostingStartup
{
    public static int Main(string[] args)
    {
        // Create and run the host.
        return
            new CakeHost()
                .UseContext<BuildContext>()
                .UseLifetime<Lifetime>()
                .UseWorkingDirectory("..")
                .Run(args);
    }
}
```

### Tool installation improvements

It is no longer required to manually register the `nuget` module with [Cake Frosting].
The method to install tools in [Cake Frosting] has also been renamed from `UseTool` to `InstallTool`:

With [Cake Frosting] 0.38.x:

```csharp
public class Program : IFrostingStartup
{
    public static int Main(string[] args)
    {
        // Create the host.
        var host =
            new CakeHostBuilder()
                .WithArguments(args)
                .UseStartup<Program>()
                .Build();

        // Run the host.
        return host.Run();
    }

    public void Configure(ICakeServices services)
    {
        // Register the NuGet module.
        var module = new NuGetModule(new CakeConfiguration(new Dictionary<string, string>()));
        module.Register(services);

        // Register tools.
        services.UseTool(new Uri("nuget:?package=NUnit.ConsoleRunner&version=3.11.1"));
    }
}
```

With [Cake Frosting] 1.0:

```csharp
public class Program : IFrostingStartup
{
    public static int Main(string[] args)
    {
        // Create and run the host.
        return
            new CakeHost()
                .InstallTool(new Uri("nuget:?package=NUnit.ConsoleRunner&version=3.11.1"));
                .Run(args);
    }
}
```

## Cake CLI updates

As part of the rewrite of the  CLI of Cake for Cake 1.0 parsing of switches is now stricter.

### Argument syntax

With Cake 1.0 arguments should always be called with multi-dash syntax (e.g. `--target=Foo`).
When using single-dash syntax (e.g. `-target=Foo`) an error message similar to the following will be shown:

```
Error: Unknown command 'Foo'.
       build.cake -target=Foo
                          ^^^^^^ No such command
```

### Passing empty arguments

With previous versions of Cake it was possible to define an empty argument (e.g. `--foo=`) or pass an empty value (e.g. `--foo=""`).

With Cake 1.0 an error message similar to the following will be shown:

```
Error: Expected an option value.
```

One key difference with Cake 1.0 is that beyond key/value arguments (`--key=value`), it supports flags (`--flag`),
and multiple arguments with the same name (`--key=value1 --key=value2`), which allows for much more flexibility than before.

In Cake 1.0 use a space instead of `=` if the value can be empty or null:

```
--foo ""
```

If you use this syntax for passing variables from a CI system you can use a space as separator between argument and value:

```
--foo %myvariable%
```

## Azure DevOps Build Task Extension

Make sure to use at least version 2.1 of [Azure DevOps Build Task Extension](/docs/integrations/build-systems/azure-pipelines/azure-devops-build-task-extension)
and version `2.*` of the task with Cake 1.0.

[Cake Frosting]: /docs/running-builds/runners/cake-frosting
