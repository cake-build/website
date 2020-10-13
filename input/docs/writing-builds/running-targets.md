Order: 30
Description: How to run targets and tasks
RedirectFrom: docs/fundamentals/running-targets
---

To run a target, use the `RunTarget` method. The `RunTarget` method should be placed at the end of the script.

```csharp
Task("Default")
    .Does(() =>
{
    Information("Hello World!");
});

RunTarget("Default");
```

# Passing a target to the script

All arguments passed to Cake will also be accessible from the Cake script. You can access the arguments by using the [argument DSL](/dsl/#arguments).

```csharp
var target = Argument("target", "Build");

Task("Build")
    .Does(() =>
{
});

Task("Publish")
    .IsDependentOn("Build")
    .Does(() =>
{
});

RunTarget(target);
```

With this Cake script, you can run a specific target by passing the `--target` argument to Cake. Thus, we can run the `"Publish"` target by calling:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool1">.NET Core Tool</a></li>
    <li><a data-toggle="tab" href="#frosting1">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx1">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core1">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool1" class="tab-pane fade in active">
        <p>
            <code class="language-powershell hljs">
                dotnet cake --target=Publish
            </code>
        </p>
    </div>
    <div id="frosting1" class="tab-pane fade">
        <p>
            On Windows:<br/>
            <code class="language-powershell hljs">
                ./build.ps1 --target=Publish
            </code>
        </p>
        <p>
            On macOS & Linux:<br/>
            <code class="language-bash hljs">
                build.sh --target=Publish
            </code>
        </p>
    </div>
    <div id="netfx1" class="tab-pane fade">
        <p>
            On Windows:<br/>
            <code class="language-powershell hljs">
                ./build.ps1 --target=Publish
            </code>
        </p>
        <p>
            On macOS & Linux using Mono:<br/>
            <code class="language-bash hljs">
                build.sh --target=Publish
            </code>
        </p>
    </div>
    <div id="core1" class="tab-pane fade">
        <p>
            On Windows:<br/>
            <code class="language-powershell hljs">
                ./build.ps1 --target=Publish
            </code>
        </p>
        <p>
            On macOS & Linux:<br/>
            <code class="language-bash hljs">
                build.sh --target=Publish
            </code>
        </p>
    </div>
</div>

The `--exclusive` parameter causes `RunTarget` to run only the specified target and no dependencies.
This command runs the `Publish` target without running the `Build` target:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool2">.NET Core Tool</a></li>
    <li><a data-toggle="tab" href="#frosting2">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx2">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core2">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool2" class="tab-pane fade in active">
        <p>
            <code class="language-powershell hljs">
                dotnet cake --target=Publish --exclusive
            </code>
        </p>
    </div>
    <div id="frosting2" class="tab-pane fade">
        <p>
            On Windows:<br/>
            <code class="language-powershell hljs">
                ./build.ps1 --target=Publish --exclusive
            </code>
        </p>
        <p>
            On macOS & Linux:<br/>
            <code class="language-bash hljs">
                build.sh --target=Publish --exclusive
            </code>
        </p>
    </div>
    <div id="netfx2" class="tab-pane fade">
        <p>
            On Windows:<br/>
            <code class="language-powershell hljs">
                ./build.ps1 --target=Publish --exclusive
            </code>
        </p>
        <p>
            On macOS & Linux using Mono:<br/>
            <code class="language-powershell hljs">
                ./build.ps1 --target=Publish --exclusive
            </code>
        </p>
    </div>
    <div id="core2" class="tab-pane fade">
        <p>
            On Windows:<br/>
            <code class="language-powershell hljs">
                ./build.ps1 --target=Publish --exclusive
            </code>
        </p>
        <p>
            On macOS & Linux:<br/>
            <code class="language-bash hljs">
                build.sh --target=Publish --exclusive
            </code>
        </p>
    </div>
</div>
