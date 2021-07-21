Order: 30
Description: How to define criterias for tasks
RedirectFrom: docs/fundamentals/criteria
---

# Cake script runner

When using [Cake .NET Tool], [Cake runner for .NET Framework] or [Cake runner for .NET Core] you can control and influence the flow of the build script execution by providing criteria. This is a predicate that has to be fulfilled for the task to execute. The criteria does not affect however succeeding task will be executed.

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

### Print message from Criteria

It's possible to pass a message to a criteria which will be shown in the output when a criteria
is not satisfied and a task is skipped.

Example:

```csharp
Task("A")
    .WithCriteria(() => DateTime.Now.Second % 2 == 0, "Need even seconds.")
    .Does(() => {

    });

RunTarget("A");
```
Output:

```powershell
========================================
A
========================================
Skipping task: Need even seconds.
````

:::{.alert .alert-info}
For the message to be shown `Verbosity` needs be set to `Verbose` or `Diagnostic`.
:::

# Cake Frosting

To implement conditional execution of tasks in [Cake Frosting] the `ShouldRun` method in the task can be overriden:

```csharp
public sealed class MyTask : FrostingTask<Context>
{
    public override bool ShouldRun(Context context)
    {
        // Criteria here
        return true;
    }

    public override void Run(Context context)
    {
    }
}
```

:::{.alert .alert-info}
See [Cake.Frosting.Example](https://github.com/cake-build/cake/tree/develop/src/Cake.Frosting.Example) for an example project.
:::

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake runner for .NET Framework]: /docs/running-builds/runners/cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: /docs/running-builds/runners/cake-runner-for-dotnet-core
[Cake Frosting]: /docs/running-builds/runners/cake-frosting