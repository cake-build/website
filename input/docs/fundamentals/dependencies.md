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
    .IsDependentOn("B")
    .Does(() =>
{
});

RunTarget("C");
```
Running target `C` will execute `A` and then `B`.

# Reverse task dependencies

Since version 0.23.0, if you prefer to define dependencies with a reversed relationship, you can define them using the `IsDependeeOf` method.

The task definition of the first example above will be identical to the following:

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

=======
RunTarget("B");
```
