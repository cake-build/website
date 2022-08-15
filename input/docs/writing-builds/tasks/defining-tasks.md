Order: 10
Description: How to define tasks
---

# Defining tasks in Cake .NET Tool

To define a task in a script for [Cake .NET Tool] use the `Task` method:

```csharp
Task("A")
    .Does(() =>
{
});
```

In script for [Cake .NET Tool], each task can have one or more actions to be executed when the task is executed.
Those actions are defined using the `Does` (see above) and `DoesForEach` (see [tasks for collections](./running-task-for-collections)) methods.
Both methods can be chained to define more than one action per task. As an example:

```csharp
Task("A")
.Does(() => 
{
   Information("This action runs first.");
}).DoesForEach(GetFiles("./**/*"), f => 
{
   Information("Found file: "+f);
}).Does(() => {
   Information("This action runs last.");
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

To define the action of a task in [Cake Frosting], override the `Run` method:

```csharp
[TaskName("A")]
public class TaskA : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.Information("This task runs...");
    }
}
```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting
[FrostingTask]: /api/Cake.Frosting/FrostingTask/
