---
content-type: markdown
---

Tasks represent a unit of work in Cake, and you use them to perform specific work in a specific order. A task can, for example, also have [dependencies](/how-does-it-work/tasks/dependencies), [criterias](/how-does-it-work/tasks/criterias) and [error handling](/how-does-it-work/tasks/error-handling) associated to it. 

To define a new task, use the Task-method.

```csharp
Task("A")
    .Does(() =>
{
});
```