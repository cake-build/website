Order: 60
RedirectFrom: docs/fundamentals/sharing-build-state
---

Sharing variables and state across your builds can currently be achieved in a handful of different ways, depending on what state you want to share and across what scope.

## Typed Context

:::{.alert .alert-success}
Available since Cake 0.28.0.
:::

You can use a typed context to easily share complex state across tasks *without* using global variables or static members. Using typed context in your script is done in 3 parts: the context itself, the `Setup` method, and individual `Task` methods.

:::{.alert .alert-info}
For more background on typed context, check out [the release blog post](https://cakebuild.net/blog/2018/05/cake-v0.28.0-released#typed-context)
:::

### Creating your typed context

First, you will need a class to act as the typed context. This can be any standard C#, and doesn't need to include any Cake-specific code. For this example, we'll use the following simple class:

```csharp
public class BuildData
{
    public string Configuration { get; }
    public bool BuildPackages { get; }
    public List<string> Frameworks {get; set;}
    // you can use read-only or mutable properties

    public BuildData(
        string configuration,
        bool buildPackages)
    {
        Configuration = configuration;
        BuildPackages = buildPackages;
    }
}
```

### Returning your context from `Setup`

To have your script use a typed context, you need to return an instance of your setup class from your (correctly-typed) `Setup` method. You can use this method to change how your tasks will run later. Using the example above:

```csharp
Setup<BuildData>(setupContext => {
    return new BuildData(
        configuration: Argument("configuration", "Release"),
        buildPackages: !BuildSystem.IsLocalBuild
    ) {
        Frameworks = IsRunningOnUnix()
            ? new List<string> { "netcoreapp2.1" }
            : new List<string> { "net472", "netcoreapp2.1" ;}
    }
});
```

### Using your context in a `Task`

Finally, you can access your typed context from any task, by supplying a type parameter to the `Does`, `DoesForEach` or `WithCriteria` method of your task declaration:

```csharp
Task("Build-Packages")
    .WithCriteria<BuildData>((context, data) => data.BuildPackages) //Your typed context is the second argument
    .Does<BuildData>(data => //make sure you use the right type parameter here
    {
        Information("Packages were {0}", data.BuildPackages ? "built" : "not built");
    });
```

You can also use your typed context in the `Teardown` method:

```csharp
Teardown<BuildData>((context, data) => // make sure you use the type parameter here
{
    Information($"Completed build for {(string.Join(", ", data.Frameworks))}");
});
```

## Global Variables

The simplest approach to sharing variables or state across your build is using global variables in your build script.

To do this, simply declare any variables outside the scope of your `Task` methods. The convention for these variables is to place them at the start of the script.

```csharp
///////////////////////////////////////////////////////////////////////////////
// VARIABLES
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var artifacts = "./dist/";
var testResultsPath = MakeAbsolute(Directory(artifacts + "./test-results"));
```

You can then access these variables in any of your `Task` methods.

```csharp
Setup(setupContext =>
{
    Information($"Using {configuration} build configuration");
});

Task("Clean")
    .Does(() =>
{
    // Clean artifacts directory
    CleanDirectory(artifacts);
});
```