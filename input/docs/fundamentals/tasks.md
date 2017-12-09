Order: 10
---

Tasks represent a unit of work in Cake, and you use them to perform specific work in a specific order. A task can, for example, also have [dependencies](/docs/fundamentals/dependencies), [criteria](/docs/fundamentals/criteria) and [error handling](/docs/fundamentals/error-handling) associated to it. 

To define a new task, use the Task-method.

```csharp
Task("A")
    .Does(() =>
{
});
```

# DoesForEach

`DoesForEach` allows a collection of items, or a delegate that returns a collection of items, to be added as actions to a given task. In the case of the delegate returning a collection, the delegate is only executed when the task is executed. This allows the task to look up items that don't exist when the script is first run, such as build artifacts.

To define a `DoesForEach` use the Task-method.

```csharp
Task("A")
    .Does(() => 
{ 
})
.DoesForEach(GetFiles("**/*.txt"), (file) => 
{ 
    // Take action on the file. 
});
```