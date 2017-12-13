Order: 60
---

# Script

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

# Task

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

**NOTE:** If `RunTarget` is called before the `Setup` or `Teardown` methods are called, they won't be correctly setup and won't work.
