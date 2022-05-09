Order: 60
Title: Upgrade instructions
Excerpt: Upgrade instructions between different versions of Cake
---

To update Cake follow these instructions

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool1">Cake .NET Tool</a></li>
    <li><a data-toggle="tab" href="#frosting1">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx1">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core1">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool1" class="tab-pane fade in active">
        <p>
        When using the <a href="/docs/running-builds/runners/dotnet-tool">.NET tool</a> as a local tool:
        </p>
        <p>
<pre><code class="language-powershell hljs">dotnet tool install --local Cake.Tool</code></pre>
        </p>
        <p>
        When using the <a href="/docs/running-builds/runners/dotnet-tool">.NET tool</a> as a global tool:
        </p>
        <p>
<pre><code class="language-powershell hljs">dotnet tool install --global Cake.Tool</code></pre>
        </p>
    </div>
    <div id="frosting1" class="tab-pane fade">
        <p>
        Update version for the <code>Cake.Frosting</code> NuGet package.
        </p>
    </div>
    <div id="netfx1" class="tab-pane fade">
        <p>
        Update version for <code>Cake</code> package in <code>tools\packages.config</code> file.
        </p>
    </div>
    <div id="core1" class="tab-pane fade">
        <p>
        Update version for <code>Cake.CoreCLR</code> package in <code>tools\packages.config</code> file.
        </p>
    </div>
</div>

# Cake 1.x to Cake 2.0

Cake 2.0 is a major version containing breaking changes.

## Supported runners and required .NET version

To allow Cake to make use of modern platform features, make life easier for extension authors and simplify decision process of users
we have decided to stop shipping [Cake runner for .NET Framework] and the already deprecated [Cake runner for .NET Core] with 2.0.
Additionally we dropped support to run on .NET Core 2.1 and .NET Core 3.0 for [Cake .NET Tool] and [Cake Frosting].

This means that, starting with Cake 2.0, you will need to have the .NET SDK installed on your build machine, at a minimum .NET Core 3.1,
but .NET 6 is recommended, in order to **run** Cake.
In other words, Cake itself will no longer **run** on .NET Framework, Mono and .NET Core 3.0 or older.
Cake will continue to support building of .NET Framework projects as well as projects targeting .NET Core 3.0 or older.

Supported platform matrix for Cake 2.0 will look like this:

| Runner                           | .NET 6 | .NET 5 | .NET Core 3.1 |
| -------------------------------- |:------:|:------:|:-------------:|
| [Cake .NET Tool]                 | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [Cake Frosting]                  | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |

As a consequence of no longer shipping [Cake runner for .NET Framework] we will also stop shipping the [Cake.Portable Chocolatey package]
and [Homebrew Cake formulae].

[Cake runner for .NET Core] has been deprecated since version 1.0 with [Cake .NET Tool] as the suggested replacement.
For users of [Cake runner for .NET Framework] it is also suggested to switch to [Cake .NET Tool] and run builds on .NET Core 3.1 or newer.
For users which rely on an extension, or other dependencies, which require .NET Framework or .NET Core 3.0 or older, suggestion is to stay on Cake 1.x.

See [Sunsetting of .NET Framework and .NET Core runners in Cake 2.0] for details.

## Cake Frosting

The deprecated `ReverseDependencyAttribute` and `DependencyAttribute` have been removed.
`ReverseDependencyAttribute` can be replaced with `IsDependeeOfAttribute` and `DependencyAttribute` with `IsDependentOnAttribute`.

See [#3003](https://github.com/cake-build/cake/issues/3003) and [#3577](https://github.com/cake-build/cake/issues/3577) for details.

## Azure Pipelines build provider updates

### IsRunningOnAzurePipelines and IsRunningOnAzurePipelinesHosted properties

With Cake 1.x `IAzurePipelinesProvider.IsRunningOnAzurePipelines` returned `true` if a build was running on Azure Pipelines **and** on
a self-hosted agent.
To check if a build was running on Azure Pipelines, regardless on which type of agent, two properties needed to be checked:

```csharp
var isRunningOnAzurePipelines =
    BuildSystem.AzurePipelines.IsRunningOnAzurePipelines || BuildSystem.AzurePipelines.IsRunningOnAzurePipelinesHosted;
```

With Cake 2.0 the meaning of the `IAzurePipelinesProvider.IsRunningOnAzurePipelines` and `BuildSystem.IsRunningOnAzurePipelines` properties
have changed to only check if the build is running on Azure Pipelines or not.

To check if a build is running on a Microsoft-hosted or self-hosted agent, the `IsHosted` property can be used:

```csharp
var isMicrosoftHostedAgent =
    BuildSystem.AzurePipelines.Environment.Agent.IsHosted;
```

The `IAzurePipelinesProvider.IsRunningOnAzurePipelinesHosted` and `BuildSystem.IsRunningOnAzurePipelinesHosted` properties have been removed and
need to be replaced with the above code.
Additionally the `BuildProvider.AzurePipelinesHosted` enumeration value has been removed.

See [#3654](https://github.com/cake-build/cake/issues/3654) for details.

### AzurePipelinesBuildInfo properties data type

The data type of `AzurePipelinesBuildInfo.ArtifactStagingDirectory`, `AzurePipelinesBuildInfo.BinariesDirectory`, `AzurePipelinesBuildInfo.SourcesDirectory`,
`AzurePipelinesBuildInfo.StagingDirectory` and `AzurePipelinesBuildInfo.TestResultsDirectory` has been changed from `FilePath` to `DirectoryPath`.

See [#3590](https://github.com/cake-build/cake/issues/3590) for details.

### Removal of TFBuildProvider

Deprecated `TFBuildProvider` has been removed.
It can be replaced with `AzurePipelinesProvider`.

See [#3610](https://github.com/cake-build/cake/issues/3610) for details.

## MsBuild & dotnet support

### New aliases for dotnet

The existing `DotNetCore*` aliases have been made obsolete and should be replaced with new `DotNet*` aliases.

See [#3341](https://github.com/cake-build/cake/issues/3341) for details.

### Xamarin.iOS platform target support

`PlatformTarget.ARMv6`, `PlatformTarget.ARMv7` and `PlatformTarget.ARMv7s` have been added to `MSBuildSettings.PlatformTarget` for Xamarin.iOS support.

See [#3222](https://github.com/cake-build/cake/issues/3222) for details.

## Tool support

### GitVersion

`GitVersionVerbosity` has been updated to match the verbosity in current versions of GitVersion.

See [#3282](https://github.com/cake-build/cake/issues/3282) for details.

### OpenCover

With Cake 1.x `OpenCoverSettings` had a `Register` property of type string, which could be empty for admin-mode, the string `user` for user-mode
or the path to a DLL:

```csharp
OpenCover(x => {
    x.NUnit3(tests);
},
    coverageFile,
    new OpenCoverSettings{
        Register = "some-path-to-dll"
    }
);
```

With Cake 2.0 `OpenCoverSettings.Register` has been changed to type `OpenCoverRegisterOption`.
There are also additional extension methods `OpenCoverSettings`:

```csharp
OpenCover(x => {
    x.NUnit3(tests);
},
    coverageFile,
    new OpenCoverSettings()
        .WithoutRegister() // to omit the register-settings
        .WithRegisterAdmin() // -register
        .WithRegisterUser() // -register:user <-- this is the default
        .WithRegisterDll(someFilePath) // -register:path-to-dll
);
```

See [#2788](https://github.com/cake-build/cake/issues/2788) for details.

[Cake runner for .NET Framework]: /docs/running-builds/runners/deprecated/cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: /docs/running-builds/runners/deprecated/cake-runner-for-dotnet-core
[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting
[Cake.Portable Chocolatey package]: https://community.chocolatey.org/packages/cake.portable
[Homebrew Cake formulae]: https://formulae.brew.sh/formula/cake
[Sunsetting of .NET Framework and .NET Core runners in Cake 2.0]: /blog/2021/10/sunsetting-runners

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