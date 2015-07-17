---
content-type: markdown
---

In order to act on an error thrown from an individual task, you can use the `OnError` task extension. This will give you the opportunity to act on the error. If you want the error to propagate as normal, you can rethrow the exception.

```csharp
Task("A")
    .Does(() =>
{
})
.OnError(exception =>
{
    // Handle the error here.
});
```

### Ignoring errors

To automatically swallow errors that occur in a task, you can use the `ContinueOnError` task extension. The `ContinueOnError` extension cannot be combined with `OnError`.

```csharp
Task("A")
    .ContinueOnError()
    .Does(() =>
{

});
```

### Reporting errors

If you want to report an error without affecting it's propagation or resulting stack trace, you can use the `ReportError` task extension. Any exceptions thrown in the scope of `ReportError` will be swallowed.

```csharp
Task("A")
    .Does(() =>
{
})
.ReportError(exception =>
{  
    // Report the error.
});
```
