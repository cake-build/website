Order: 20
---
# Task dependencies

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


# Reverse task dependencies

Since version 0.23.0, if you prefer define dependencies with a reversed relationship, you can define it using the `IsDependeeOf` method.

The task definition of the previous example will be identical to the following:

```csharp
Task("A")
    .IsDependeeOf("B")
    .Does(() =>
{
});

Task("B")
    .Does(() =>
{
});

RunTarget("B");
```
