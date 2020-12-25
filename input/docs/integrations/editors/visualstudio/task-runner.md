Order: 60
Title: Visual Studio task runner
Description: Visual Studio task runner integration
---

# Configuring task runner

Visual Studio task runner integration is provided by the [Cake extension for Visual Studio](https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio).

## Install Cake

In order to use the task runner you must have Cake installed on your machine or in your solution.

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool">Cake .NET Tool</a></li>
    <li><a data-toggle="tab" href="#frosting">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool" class="tab-pane fade in active">
        <p>
            To use Cake .NET Tool it needs to be installed globally:
        </p>
        <pre><code class="language-cmd hljs">dotnet tool install --global Cake.Tool</code></pre>
    </div>
    <div id="frosting" class="tab-pane fade">
        <p>
            There is currently no support for task runner for Cake Frosting.
        </p>
    </div>
    <div id="netfx" class="tab-pane fade">
        <p>
            Install Cake using any of the following options:
        </p>
        <ul>
            <li>
                <p>
                    On Windows using <a href="https://chocolatey.org/">Chocolatey to install it globally</a>:
                </p>
                <pre><code class="language-cmd hljs">choco install cake.portable</code></pre>
            </li>
            <li>
                <p>
                    Run bootstrapper.
                </p>
                <p>
                    The task runner will automatically use a project-local copy of Cake if it is already present in the current directory or one of the default paths.
                    However, at this time, it will not automatically download Cake for you.
                </p>
            </li>
        </ul>
    </div>
    <div id="core" class="tab-pane fade">
        <p>
            There is currently no support for task runner for Cake runner for .NET Core.
        </p>
    </div>
</div>

## Supported runners

| Runner                           | Supported                                       | Remarks                                            |
|----------------------------------|-------------------------------------------------|----------------------------------------------------|
| [Cake .NET Tool]                 | <i class="fa fa-check" style="color:green"></i> | Only for global installations.                     |
| [Cake Frosting]                  | <i class="fa fa-times" style="color:red"></i>   |                                                    |
| [Cake runner for .NET Framework] | <i class="fa fa-check" style="color:green"></i> | `build.cake` needs to be included in the solution. |
| [Cake runner for .NET Core]      | <i class="fa fa-times" style="color:red"></i>   |                                                    |

[Cake .NET Tool]: dotnet-tool
[Cake Frosting]: cake-frosting
[Cake runner for .NET Framework]: cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: cake-runner-for-dotnet-core

# Using task runner

You can open the Task Runner Explorer window by right-clicking on the file from your Solution Explorer and choosing `Task Runner Explorer`:

![Open Visual Studio task runner explorer](/assets/img/cake-for-vs/task-runner-explorer-open.png)

Individual tasks will be listed on the left:

![Task list](/assets/img/cake-for-vs/task-runner-explorer-task-list.png)

Each task can be executed by double-clicking the task:

![Visual Studio task runner explorer](/assets/img/cake-for-vs/task-runner-explorer.png)

## Bindings

Task bindings make it possible to associate individual tasks with Visual Studio events such as `Project Open`, `Build` or `Clean`.
These bindings are stored in your `cake.config` file.

![Visual Studio task runner explorer bindings](/assets/img/cake-for-vs/trx.png)
