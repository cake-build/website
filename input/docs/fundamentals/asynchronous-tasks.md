Order: 55
---

# Asynchronous Tasks

Cake supports having asynchronous tasks since version 0.23.0, this will let you use the `async` and `await` C# features within a task.

Tasks will still execute single threaded and in order as before, but this really simplifies asynchronous programming and utilizing asynchronous APIs within your Cake scripts.

```csharp
Task("Copy-To-Memory-Async")
    .Does(async () => 
    {
        IFile file = Context.FileSystem.GetFile("./test.txt");
        using(Stream
            inputStream = testFile.OpenRead(),
            outputStream = new MemoryStream())
        {
            await inputStream.CopyToAsync(outputStream);
            await outputStream.FlushAsync();
            Information("Copied {0} bytes into memory.",
                outputStream.Length
            );
        }
    });   
```

You can also use any parallelization mechanism such as Parallel.ForEach or Parallel.Invoke on your tasks.

```csharp
var projectPaths = new [] {
    "./src/First/First.csproj",
    "./src/Second/Second.csproj",
};

Task("BuildProjectsInParallel")
    .Does(() => 
    {
        BuildInParallel(projectPaths);
    });

Task("Default")
    .IsDependentOn("BuildProjectsInParallel")
    .Does(() => 
    {
    });
    
RunTarget(target);

public void BuildInParallel(
    IEnumerable<string> filePaths,
    int maxDegreeOfParallelism = -1,
    CancellationToken cancellationToken = default(CancellationToken)) 
{
    var actions = new List<Action>();
    foreach (var filePath in filePaths) {
        actions.Add(() =>
            MSBuild(filePath, configurator =>
                configurator.SetConfiguration("Release"))
        );                        
    }

    var options = new ParallelOptions {
        MaxDegreeOfParallelism = maxDegreeOfParallelism,
        CancellationToken = cancellationToken
    };

    Parallel.Invoke(options, actions.ToArray());
}
```
