---
content-type: markdown
---

Tasks represent a unit of work in Cake, and you use them to perform specific work in a specific order. A task can, for example, also have [dependencies](/fundamentals/tasks/dependencies), [criterias](/fundamentals/tasks/criterias) and [error handling](/fundamentals/tasks/error-handling) associated to it. 

To define a new task, use the Task-method.

```csharp
Task("A")
    .Does(() =>
{
});
```
