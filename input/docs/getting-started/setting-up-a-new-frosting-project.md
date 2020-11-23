Order: 30
Description: Guide on how to get started with writing a build pipeline in a console application using Cake Frosting
---

This is a guide to get started with [Cake Frosting] and to show you how [Cake Frosting] works.

# Choose your runner

This tutorial uses [Cake Frosting], which allows you to write builds as standard console applications as part of your solution.
See [Runners](../running-builds/runners) for other possibilities of how to run Cake scripts.

:::{.alert .alert-info}
The following instructions require .NET Core 3.1.301 or newer.
You can find the SDK at https://dotnet.microsoft.com/download
:::

To create a new [Cake Frosting] project you need to install the Frosting template:

```powershell
dotnet new --install Cake.Frosting.Template
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

`Program.cs` contains code to configure and run the Cake host:

```csharp
using Cake.Core;
using Cake.Frosting;

public class Program : IFrostingStartup
{
    public static int Main(string[] args)
    {
        // Create the host.
        var host = new CakeHostBuilder()
            .WithArguments(args)
            .UseStartup<Program>()
            .Build();

        // Run the host.
        return host.Run();
    }

    public void Configure(ICakeServices services)
    {
        services.UseContext<Context>();
        services.UseLifetime<Lifetime>();
        services.UseWorkingDirectory("..");
    }
}
```

`Context.cs` can be used to enhance the context class with custom properties:

```csharp
using Cake.Core;
using Cake.Frosting;

public class Context : FrostingContext
{
    public Context(ICakeContext context)
        : base(context)
    {
    }
}
```

`Lifetime.cs` can be used to define setup and teardown methods which are run when build or tasks are started and stopped:

```csharp
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;

public sealed class Lifetime : FrostingLifetime<Context>
{
    public override void Setup(Context context)
    {
        context.Information("Setting things up...");
    }

    public override void Teardown(Context context, ITeardownContext info)
    {
        context.Information("Tearing things down...");
    }
}
```

`/Tasks/Default.cs` contains the code for the default task which has a dependency on the `Hello` task:

```csharp
using Cake.Frosting;

[Dependency(typeof(Hello))]
public sealed class Default : FrostingTask<Context>
{
}
```

`/Tasks/Hello.cs` contains the code for the `Hello` task:

```csharp
using Cake.Common.Diagnostics;
using Cake.Frosting;

[TaskName("Hello")]
public sealed class Hello : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.Information("Hello World");
    }
}
```

# Example build pipeline

The following example creates a simple build pipeline consisting of a clean task, a task compiling MsBuild solution and a task testing the solution.

:::{.alert .alert-info}
The following example expects a Visual Studio solution `/src/Example.sln`.
You need to adapt the path to your solution.
:::

First create an additional property `MsBuildConfiguration` to the context in the `Context.cs` file, which stores the configuration of the solution which should be built:

```csharp
using Cake.Core;
using Cake.Frosting;

public class Context : FrostingContext
{
    public string MsBuildConfiguration { get; set; }

    public Context(ICakeContext context)
        : base(context)
    {
    }
}
```

In the `Setup` method in the `Lifetime.cs` file set the configuration based on the arguments passed to the build:

```csharp
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;

public sealed class Lifetime : FrostingLifetime<Context>
{
    public override void Setup(Context context)
    {
        context.Information("Setting things up...");

        context.MsBuildConfiguration = context.Argument("configuration", "Release");
    }

    public override void Teardown(Context context, ITeardownContext info)
    {
        context.Information("Tearing things down...");
    }
}
```

The `/Tasks/Hello.cs` file from the template can be deleted.

Create a new file `/Tasks/Clean.cs` for the task for cleaning the directory with the following content:

```csharp
using Cake.Common.IO;
using Cake.Frosting;

public sealed class Clean : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.CleanDirectory($"./src/Example/bin/{context.MsBuildConfiguration}");
    }
}
```

Create a new file `/Tasks/Build.cs` for building the solution with the following content:

```csharp
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Frosting;

[Dependency(typeof(Clean))]
public sealed class Build : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetCoreBuild("./src/Example.sln", new DotNetCoreBuildSettings
        {
            Configuration = context.MsBuildConfiguration,
        });
    }
}
```

Create a new file `/Tasks/Test.cs` for testing the solution with the following content:

```csharp
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Frosting;

[Dependency(typeof(Build))]
public sealed class Test : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetCoreTest("./src/Example.sln", new DotNetCoreTestSettings
        {
            Configuration = context.MsBuildConfiguration,
            NoBuild = true,
        });
    }
}
```

Finally update the file `/Tasks/Default.cs` to call the new tasks:

```csharp
using Cake.Frosting;

[Dependency(typeof(Test))]
public sealed class Default : FrostingTask<Context>
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
