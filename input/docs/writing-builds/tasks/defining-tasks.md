Order: 10
Excerpt: How to define tasks
---

# Defining tasks in Cake .NET Tool

To define a task in a script for [Cake .NET Tool] use the `Task` method:

```csharp
Task("A")
    .Does(() =>
{
});
```

# Defining tasks in Cake Frosting

To define a task in [Cake Frosting] create a class inheriting from [FrostingTask]:

```csharp
[TaskName("A")]
public class TaskA : FrostingTask
{
}
```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting
[FrostingTask]: /api/Cake.Frosting/FrostingTask/