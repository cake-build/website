Order: 20
Description: How to make tasks dependent on each other
RedirectFrom: docs/fundamentals/dependencies
---

# Cake script runner

## Task dependencies

In [Cake .NET Tool], [Cake runner for .NET Framework] or [Cake runner for .NET Core]
a dependency on another task can be defined using the `IsDependentOn` method.

```csharp
Task("A")
    .Does(() =>
{
});

Task("B")
    .IsDependentOn("A")
    .Does(() =>
{
});

RunTarget("B");
```

This will first execute target `A` and then `B` as expected.


## Reverse task dependencies

:::{.alert .alert-success}
Available since Cake 0.23.0.
:::

In [Cake .NET Tool], [Cake runner for .NET Framework] or [Cake runner for .NET Core]
dependencies with a reversed relationship can be defined using the `IsDependeeOf` method.

The task definition of the previous example will be identical to the following:

```csharp
Task("A")
    .IsDependeeOf("B")
    .Does(() =>
{
});

Task("B")
    .Does(() =>
{
});

RunTarget("B");
```

## Multiple dependencies

```csharp
Task("A")
    .Does(() =>
{
});

Task("B")
    .Does(() =>
{
});

Task("C")
    .IsDependentOn("A")
    .IsDependentOn("B")
    .Does(() =>
{
});

RunTarget("C");
```

Running target `C` will execute `A` and then `B`. If a task is referenced multiple times, it will only execute once.

## Referencing dependencies using the task object

This method adds a dependency using the task instead of the name as a string.

```csharp
var taskA = Task("A")
    .Does(() =>
{
});

Task("B")
    .IsDependentOn(taskA)
    .Does(() =>
{
});

RunTarget("B");
```

This will first execute target `A` and then `B` as expected.

# Cake Frosting

## Task dependencies

In [Cake Frosting] a dependency on another task can be defined using the `IsDependentOn` attribute.

```csharp
[TaskName("A")]
public sealed class TaskA : FrostingTask
{
    public override void Run()
    {
    }
}

[TaskName("B")]
[IsDependentOn(typeof(TaskA))]
public sealed class TaskB : FrostingTask
{
    public override void Run()
    {
    }
}
```

When task `B` is executed it will make sure that task `A` has been executed before.

## Reverse task dependencies

In [Cake Frosting] dependencies with a reversed relationship can be defined using the `IsDependeeOf` attribute.

The task definition of the previous example will be identical to the following:

```csharp
[TaskName("A")]
[IsDependeeOf(typeof(TaskB))]
public sealed class TaskA : FrostingTask
{
    public override void Run()
    {
    }
}

[TaskName("B")]
public sealed class TaskB : FrostingTask
{
    public override void Run()
    {
    }
}
```

## Multiple dependencies

```csharp
[TaskName("A")]
public sealed class TaskA : FrostingTask
{
    public override void Run()
    {
    }
}

[TaskName("B")]
public sealed class TaskB : FrostingTask
{
    public override void Run()
    {
    }
}

[TaskName("C")]
[IsDependentOn(typeof(TaskA))]
[IsDependentOn(typeof(TaskB))]
public sealed class TaskC : FrostingTask
{
    public override void Run()
    {
    }
}
```

Running task `C` will execute `A` and then `B`.
If a task is referenced multiple times, it will only execute once.

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake runner for .NET Framework]: /docs/running-builds/runners/cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: /docs/running-builds/runners/cake-runner-for-dotnet-core
[Cake Frosting]: /docs/running-builds/runners/cake-frosting
