Order: 60
RedirectFrom: docs/fundamentals/finally-block
---

If you want to execute some arbritrary piece of code regardless of how a task exited, you can use the `Finally` task extension.

```csharp
Task("A")
    .Does(() =>
{
})
.Finally(() =>
{
    // Do magic.
});
```