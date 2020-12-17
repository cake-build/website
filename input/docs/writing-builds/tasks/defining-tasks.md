Order: 10
Description: How to define tasks
---

# Cake script runner

To define a task in a script for [Cake .NET Tool], [Cake runner for .NET Framework] or [Cake runner for .NET Core],
use the `Task` method:

```csharp
Task("A")
    .Does(() =>
{
});
```

# Cake Frosting

To define a task in [Cake.Frosting] create a class inheriting from [FrostingTask]:

```csharp
[TaskName("A")]
public class TaskA : FrostingTask
{
}
```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake runner for .NET Framework]: /docs/running-builds/runners/cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: /docs/running-builds/runners/cake-runner-for-dotnet-core
[Cake.Frosting]: /docs/running-builds/runners/cake-frosting
[FrostingTask]: /api/Cake.Frosting/FrostingTask/
