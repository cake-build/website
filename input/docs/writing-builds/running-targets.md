Order: 30
Description: How to run targets and tasks
RedirectFrom: docs/fundamentals/running-targets
---

# Cake script runner

When using [Cake .NET Tool], [Cake runner for .NET Framework] or [Cake runner for .NET Core] you can use the `RunTarget` method to run a target.
The `RunTarget` method should be placed at the end of the script.

```csharp
Task("Default")
    .Does(() =>
{
    Information("Hello World!");
});

RunTarget("Default");
```

## Passing a target to the script

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

With this Cake script, you can run a specific target by passing the `--target` argument to Cake.
Thus, we can run the `"Publish"` target by calling Cake with the following argument:

```powershell
--target=Publish
```

The `--exclusive` parameter causes `RunTarget` to run only the specified target and no dependencies.
The following arguments will run the `Publish` target without running the `Build` target:

```powershell
--target=Publish --exclusive
```

# Cake Frosting

[Cake Frosting] by default will call a task named `Default`.

:::{.alert .alert-info}
In [Cake Frosting] it is not possible to define a task other than `Default`, which is run by default in your build script.
:::

## Passing a target to the script

To override the task which should be run [Cake Frosting] comes with a `--target` switch:

```powershell
--target=Publish
```

The `--exclusive` parameter causes [Cake Frosting] to run only the specified target and no dependencies.
The following arguments will run the `Publish` target without running the `Build` target:

```powershell
--target=Publish --exclusive
```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake runner for .NET Framework]: /docs/running-builds/runners/cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: /docs/running-builds/runners/cake-runner-for-dotnet-core
[Cake Frosting]: /docs/running-builds/runners/cake-frosting
