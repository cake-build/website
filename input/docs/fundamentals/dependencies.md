Order: 20
---

To add a dependency on another task, use the `IsDependentOn`-method.

```csharp
Task("A")
    .Does(() =>
{
});

Task("B")
    .IsDependentOn("A")
    .Does(() =>
{
});

RunTarget("B");
```

This will first execute target `A` and then `B` as expected.


Multiple dependencies is also possible.

```csharp
Task("A")
    .Does(() =>
{
});

Task("B")
    .Does(() =>
{
});

Task("C")
    .IsDependentOn("A")
    .IsDependentOn("A")
    .Does(() =>
{
});

RunTarget("C");
```

Running target `C` will execute `A` and then `B`.
