Order: 30
Title: Setting Up A New Cake Frosting Project
Description: Guide on how to get started with writing a build pipeline in a console application using Cake Frosting
---

This is a guide to get started with [Cake Frosting] and to show you how [Cake Frosting] works.

# Installation and scaffolding

This tutorial uses [Cake Frosting], which allows you to write builds as standard console applications as part of your solution.
See [Runners](../running-builds/runners) for other possibilities of how to run Cake builds.

:::{.alert .alert-info}
The following instructions require Cake Frosting 1.0.0 or newer running on .NET Core 3.1.301 or newer.
You can find the .NET SDK at https://dotnet.microsoft.com/download
:::

To create a new [Cake Frosting] project you need to install the Frosting template:

```powershell
dotnet new install Cake.Frosting.Template
```

Create a new Frosting project:

```powershell
dotnet new cakefrosting
```

This will create the Cake Frosting project and bootstrapping scripts.

:::{.alert .alert-info}
See [Bootstrapping for Cake Frosting](/docs/running-builds/runners/cake-frosting#bootstrapping-for-cake-frosting) for details about the bootstrapping process.
:::

# Initial build project

The `Program` class contains code to configure and run the Cake host:

```csharp
public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}
```

The `BuildContext` class can be used to add additional custom properties.
The default template contains an example property `Delay` which can be set through a `--delay` argument.
You can remove this property and customize the properties to your specific needs.

```csharp
public class BuildContext : FrostingContext
{
    public bool Delay { get; set; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        Delay = context.Arguments.HasArgument("delay");
    }
}
```

The file also contains three classes for [tasks](/docs/writing-builds/tasks):

```csharp
[TaskName("Hello")]
public sealed class HelloTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.Log.Information("Hello");
    }
}

[TaskName("World")]
[IsDependentOn(typeof(HelloTask))]
public sealed class WorldTask : AsyncFrostingTask<BuildContext>
{
    // Tasks can be asynchronous
    public override async Task RunAsync(BuildContext context)
    {
        if (context.Delay)
        {
            context.Log.Information("Waiting...");
            await Task.Delay(1500);
        }

        context.Log.Information("World");
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(WorldTask))]
public class DefaultTask : FrostingTask
{
}
```

The `Default` task has a dependency to the `World` task, and the `World` task has a dependency on the `Hello` task.
The `World` task is an [asynchronous task](/docs/writing-builds/tasks/asynchronous-tasks) which waits for
one and a half seconds if the `Delay` property is set.

# Example build pipeline

The following example creates a simple build pipeline consisting of a clean task, a task compiling an MsBuild solution and a task which tests the solution.

:::{.alert .alert-info}
The following example expects a Visual Studio solution `src/Example.sln` in the repository root folder.
You need to adapt the path to your solution.
:::

Add the required using statements:

```csharp
using Cake.Common;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.Test;
```

Remove the `Delay` property from the `BuildContext` class and add a property `MsBuildConfiguration`, which stores the configuration of the solution which should be built:

```csharp
public class BuildContext : FrostingContext
{
    public string MsBuildConfiguration { get; set; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        MsBuildConfiguration = context.Argument("configuration", "Release");
    }
}
```

The `HelloTask` and `WorldTask` class can be deleted.

Create a new class `CleanTask` for cleaning the directory:

```csharp
[TaskName("Clean")]
public sealed class CleanTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.CleanDirectory($"../src/Example/bin/{context.MsBuildConfiguration}");
    }
}
```

Create a new class `BuildTask` for building the solution:

```csharp
[TaskName("Build")]
[IsDependentOn(typeof(CleanTask))]
public sealed class BuildTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetBuild("../src/Example.sln", new DotNetBuildSettings
        {
            Configuration = context.MsBuildConfiguration,
        });
    }
}
```

Create a new class `TestTask` for testing the solution:

```csharp
[TaskName("Test")]
[IsDependentOn(typeof(BuildTask))]
public sealed class TestTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetTest("../src/Example.sln", new DotNetTestSettings
        {
            Configuration = context.MsBuildConfiguration,
            NoBuild = true,
        });
    }
}
```

Update the `DefaultTask` class to call the new tasks:

```csharp
[IsDependentOn(typeof(TestTask))]
public sealed class Default : FrostingTask
{
}
```

# Running build script

Run the build script:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#windows">Windows</a></li>
    <li><a data-toggle="tab" href="#linux">Linux</a></li>
    <li><a data-toggle="tab" href="#macos">MacOS</a></li>
</ul>

<div class="tab-content">
    <div id="windows" class="tab-pane fade in active">
        <p>
            <code class="language-powershell hljs">
               ./build.ps1
            </code>
        </p>
    </div>
    <div id="linux" class="tab-pane fade">
        <p>
            <code class="language-bash hljs">
               build.sh
            </code>
        </p>
    </div>
    <div id="macos" class="tab-pane fade">
        <p>
            <code class="language-bash hljs">
               build.sh
            </code>
        </p>
    </div>
</div>

:::{.alert .alert-info}
See [Cake Frosting] for details how to run Cake Frosting builds.
:::

[Cake Frosting]: /docs/running-builds/runners/cake-frosting
