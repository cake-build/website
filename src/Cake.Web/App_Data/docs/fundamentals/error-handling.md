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

### Aborting the build

If something has gone wrong that you cannot recover from, you should throw an exception to indicate it. The Cake script runner will then log the error (using the Error method) and return exit code 1 to indicate that something went wrong.

```csharp
Task("Check-ReleaseNotes")
    .Does(() =>
{
    var releaseNotes = ParseReleaseNotes("./ReleaseNotes.md");
    if(releaseNotes.Version.ToString() != nugetVersion)
    {
        throw new Exception(String.Format("Release notes are missing an entry for v{0}. Latest release notes are for v{1}", nugetVersion, releaseNotes.Version));   
    }
});
```
