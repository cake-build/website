Order: 10
Title: Script Aliases
Description: Convenience methods for Cake scripts
---

Cake supports something called script aliases. Script aliases are convenience methods that are easily accessible directly from a Cake script. Every single [DSL method](/dsl) in Cake is implemented like an alias method.

:::{.alert .alert-info}
See [Reference](/dsl) for a list of available aliases.
:::

# Calling aliases

Aliases are extension methods of `ICakeContext`.

## Calling aliases in a Cake script

When using a Cake script with [Cake .NET Tool](/docs/running-builds/runners/dotnet-tool),
[Cake runner for .NET Framework](/docs/running-builds/runners/cake-runner-for-dotnet-framework) or
[Cake runner for .NET Core](/docs/running-builds/runners/cake-runner-for-dotnet-core)
aliases can be called directly inside a task, without explicitly passing the context:

```csharp
Task("Clean")
    .Does(() =>
{
    // Delete a file.
    DeleteFile("./file.txt");

    // Clean a directory.
    CleanDirectory("./temp");
});
```

## Calling aliases in Frosting

Inside a [Cake Frosting](/docs/running-builds/runners/cake-frosting) project aliases can be
called as extension methods of the context passed to the `Run` method of the task:

```csharp
public sealed class Clean : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        // Delete a file.
        context.DeleteFile("./file.txt");

        // Clean a directory.
        context.CleanDirectory("./temp");
    }
}

```

# Custom aliases

Additional aliases can be defined in [addins](../extending/addins).
