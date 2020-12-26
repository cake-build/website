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
