---
content-type: markdown
---

If you want to do something before the first or after the last task has been run, you can use `Setup` and `Teardown`. A use case for this might be when you need to start some kind of server and want to make sure it gets teared down properly.

```csharp
Setup(() =>
{
    // Executed BEFORE the first task.
});

Teardown(() =>
{
    // Executed AFTER the last task.
});
```

Setup will only be called if a call to `RunTarget` is made and the dependency graph is correct. If Cake doesn't run any tasks, neither `Setup` or `Teardown` will be called.