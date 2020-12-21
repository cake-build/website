Order: 60
Title: Visual Studio task runner
Description: Visual Studio task runner integration
---

# Configuring task runner

Visual Studio task runner integration is provided by the [Cake extension for Visual Studio](https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio).

## Supported runners

| Runner                           | Supported                                       | Remarks                                            |
|----------------------------------|-------------------------------------------------|----------------------------------------------------|
| [Cake .NET Tool]                 | <i class="fa fa-times" style="color:red"></i>   |                                                    |
| [Cake Frosting]                  | <i class="fa fa-times" style="color:red"></i>   |                                                    |
| [Cake runner for .NET Framework] | <i class="fa fa-check" style="color:green"></i> | `build.cake` needs to be included in the solution. |
| [Cake runner for .NET Core]      | <i class="fa fa-times" style="color:red"></i>   |                                                    |

[Cake .NET Tool]: dotnet-tool
[Cake Frosting]: cake-frosting
[Cake runner for .NET Framework]: cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: cake-runner-for-dotnet-core

# Using task runner

You can open the Task Runner Explorer window by right-clicking on the file from your Solution Explorer and choosing `Task Runner Explorer`.

Individual tasks will be listed on the left, and each task can be executed by double-clicking the task.

![Visual Studio task runner explorer](/assets/img/cake-for-vs/task-runner-explorer.png)

## Bindings

Task bindings make it possible to associate individual tasks with Visual Studio events such as `Project Open`, `Build` or `Clean`.
These bindings are saved in your `cake.config` file.

![Visual Studio task runner explorer bindings](/assets/img/cake-for-vs/trx.png)
