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

:::{.alert .alert-info}
**Cake Frosting:** Cake Frosting does not have a direct equivalent to `DoesForEach`.
In Frosting, you can achieve similar behavior by iterating over a collection within a task's `Run` method
and calling your logic for each item. For example:

```csharp
[TaskName("ProcessFiles")]
public sealed class ProcessFilesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (var file in context.GlobFiles("**/*.txt"))
        {
            // Take action on the file.
        }
    }
}
```
:::
