Order: 30
---

You can control and influence the flow of the build script execution by providing criteria. This is a predicate that has to be fulfilled for the task to execute. The criteria does not affect however succeeding task will be executed.

```csharp
Task("A")
    .WithCriteria(DateTime.Now.Second % 2 == 0)
    .Does(() =>
    {
    });
    
Task("B")
    .WithCriteria(() => DateTime.Now.Second % 2 == 0)
    .IsDependentOn("A")
    .Does(() => 
    {
    });

RunTarget("B");
```

Task `A`'s criteria will be set when the task is created while Task `B`'s criteria will be evaluated when the task is being executed.

In a real project, we would control the execution of the tasks as a pipeline according to different environmental states, the repository branches, etc. In this case we get two criterias from outside to detect if local build and we are working on master branch. Notice we are using AppVeyor Build System, you can do similar stuff with other Build Systems as well.

```csharp
var isLocalBuild = BuildSystem.IsLocalBuild
var isMasterBranch = StringComparer.OrdinalIgnoreCase.Equals("master",
    BuildSystem.AppVeyor.Environment.Repository.Branch));
    
var deployUrl = HasArgument("DeployUrl") 
    ? Argument<string>("DeployUrl") 
    : EnvironmentVariable("DEPLOY_URL");
    
var target = "Default";

Task("Clean")
    .Does(() =>
    {
    });

Task("Restore")
    .Does(() => 
    {
    });

Task("Build")    
    .Does(() =>
    {
    });

Task("Migrate")    
    .WithCriteria(!isLocalBuild)
    .WithCriteria(isMasterBranch)
    .Does(() => 
    {
    });

Task("Deploy")    
    .WithCriteria(!isLocalBuild)
    .WithCriteria(isMasterBranch)
    .Does(() => 
    {
        // Deploy to some server using content and deployUrl
    });    

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Migrate")
    .IsDependentOn("Deploy")    
    .Does(() => 
    {
    });

RunTarget(target);
```

For criteria with states that might change during the execution of the build script, consider using the lambda alternative.

Using the previous example, we add one local var to pass to recovery mode with some error in the `Migration` task.

```csharp
...
var target = "Default";

var isRecoveryMode = false;    

Task("Migrate")    
    .WithCriteria(!isLocalBuild)
    .WithCriteria(isMasterBranch)
    .Does(() => 
    {
    })
    .OnError(ex => 
    {
        isRecoveryMode = true
    });

Task("Deploy")    
    .WithCriteria(!isLocalBuild)
    .WithCriteria(isMasterBranch)
    .WithCriteria(() => !isRecoveryMode)
    .Does(() => 
    {        
        // Deploy to some server using content and deployUrl
    });    

Task("Recovery")
    .WithCriteria(!isLocalBuild)
    .WithCriteria(isMasterBranch)
    .WithCriteria(() => isRecoveryMode)
    .Does(() =>
    {
        // Restore database
    });

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Migrate")
    .IsDependentOn("Deploy")    
    .IsDependentOn("Recovery")
    .Does(() => 
    {
    });

RunTarget(target);
```
