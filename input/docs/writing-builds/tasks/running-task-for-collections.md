Order: 40
Description: How to run code for a collection of items
---

`DoesForEach` allows a collection of items, or a delegate that returns a collection of items, to be added as actions to a given task. In the case of the delegate returning a collection, the delegate is only executed when the task is executed. This allows the task to look up items that don't exist when the script is first run, such as build artifacts.

To define a `DoesForEach` use the Task-method.

```csharp
Task("A")
    .DoesForEach(GetFiles("**/*.txt"), (file) =>
{
    // Take action on the file.
});

```

## Cake Frosting

In Cake Frosting, tasks are plain C# classes, so there's no `DoesForEach` fluent method. Instead, simply iterate over the collection using a standard `foreach` loop inside the task's `Run` method.

```csharp
[TaskName("A")]
public sealed class TaskA : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (var file in context.GetFiles("**/*.txt"))
        {
            // Take action on the file.
        }
    }
}
```