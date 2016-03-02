---
content-type: markdown
---

Tasks represent a unit of work in Cake, and you use them to perform specific work in a specific order. A task can, for example, also have [dependencies](/docs/fundamentals/dependencies), [criteria](/docs/fundamentals/criteria) and [error handling](/docs/fundamentals/error-handling) associated to it. 

To define a new task, use the Task-method.

```csharp
Task("A")
    .Does(() =>
{
});
```
