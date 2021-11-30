Order: 20
Description: Setup and teardown events
RedirectFrom: docs/fundamentals/setup-and-teardown
---

# Cake .NET Tool

When using [Cake .NET Tool] you can use the following methods to control setup and teardown.

## Global lifetime

If you want to do something before the first or after the last task has been run, you can use `Setup` and `Teardown`. A use case for this might be when you need to start some kind of server and want to make sure it gets torn down properly.

```csharp
Setup(context =>
{
    // Executed BEFORE the first task.
});

Teardown(context =>
{
    // Executed AFTER the last task.
});
```

Setup will only be called if a call to `RunTarget` is made and the dependency graph is correct. If Cake doesn't run any tasks, neither `Setup` or `Teardown` will be called.

## Task lifetime

You can also use `TaskSetup` and `TaskTeardown` if you want to do something before and after each task is run. A use case for this might be when you need to use custom logging for each task executed

```csharp
TaskSetup(setupContext =>
{
    var message = string.Format("Task: {0}", setupContext.Task.Name);
    // custom logging
});

TaskTeardown(teardownContext =>
{
    var message = string.Format("Task: {0}", teardownContext.Task.Name);
    // custom logging
});

```

:::{.alert .alert-warning}
If `RunTarget` is called before the `Setup` or `Teardown` methods are called, they won't be correctly setup and won't work.
:::

# Cake Frosting

When using [Cake Frosting] you can implement the following methods.

## Global lifetime

Implement an `IFrostingLife` to control global lifetime and register it with `UseLifetime` on the Cake host.
There are two base implementation which you can inherit: `FrostingLifetime` and `FrostingLifetime<TContext>`.

The following example shows a lifetime implementation for a script with a build context of type `BuildContext`:

```csharp
public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .UseLifetime<BuildLifetime>()
            .Run(args);
    }
}

public class BuildLifetime : FrostingLifetime<BuildContext>
{
    public override void Setup(BuildContext context)
    {
        // Executed BEFORE the first task.
    }

    public override void Teardown(BuildContext context)
    {
        // Executed AFTER the last task.
    }
}
```

## Task lifetime

Implement an `IFrostingTaskLife` to control task lifetime and register it with `UseTaskLifetime` on the Cake host.
There are two base implementation which you can inherit: `FrostingTaskLifetime` and `FrostingTaskLifetime<TContext>`.

The following example shows a lifetime implementation for a script with a build context of type `BuildContext`:

```csharp
public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .UseTaskLifetime<TaskLifetime>()
            .Run(args);
    }
}

public class TaskLifetime : FrostingTaskLifetime<BuildContext>
{
    public override void Setup(BuildContext context, ITaskSetupContext info)
    {
        // Executed BEFORE every task.
    }

    public override void Teardown(BuildContext context, ITaskTeardownContext info)
    {
        // Executed AFTER every task.
    }
}
```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting