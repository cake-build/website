Order: 20
Title: Rider run configurations
Description: Run configurations for Cake tasks
---

It is possible to have Cake tasks as run configurations:

![Run configurations](/assets/img/cake-rider/docs/runConfigurations.png){.img-responsive}

Currently only run configurations are supported. Debug configurations are not supported.

# Configuring run configurations

In order to use the run configurations you must have Cake installed on your machine or in your solution.

## Install Cake

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
        <p>
            This is the default setup in Cake for Rider.
        </p>
    </div>
    <div id="frosting" class="tab-pane fade">
        <p>
            There is currently no support for run configurations for Cake Frosting.
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
                    Use a <a href="/docs/running-builds/runners/cake-runner-for-dotnet-framework#bootstrapping-for-cake-runner-for.net-framework">bootstrapper</a>.
                </p>
                <p>
                    This requires to setup the bootstrapper(s) under <a href="#runner-settings">settings</a>
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
| [Cake .NET Tool]                 | <i class="fa fa-check" style="color:green"></i> |                                                    |
| [Cake Frosting]                  | <i class="fa fa-times" style="color:red"></i>   |                                                    |
| [Cake runner for .NET Framework] | <i class="fa fa-check" style="color:green"></i> |                                                    |
| [Cake runner for .NET Core]      | <i class="fa fa-times" style="color:red"></i>   |                                                    |

[Cake .NET Tool]: dotnet-tool
[Cake Frosting]: cake-frosting
[Cake runner for .NET Framework]: cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: cake-runner-for-dotnet-core

## Settings

There are multiple configuration settings available under File -> Settings -> Build, Execution, Deployment -> Cake.

All settings are project - specific and stored in the `.idea` folder. To share settings across developers, make sure to put `CakeRider.xml` under source control.

### Generic settings

* *Cake file extension*  
  This setting is used to find all Cake files and display them in the tool window.  
  Default: `cake`
* *Task Regex*  
  This regular expression is used to parse tasks from the Cake files.  
  Default: `Task\s*?\(\s*?"(.*?)"\s*?\)`
* *Verbosity*  
  This is the default verbosity to use, when running a task directly from the tool window or when creating a new run configuration.  
  Default: `normal`

### Runner settings

Allows to define the runner to use. 
Different runners for different operating systems can be set by defining a regular expression which is matched against the system property `os.name`.

Default value is `dotnet-cake` and `dotnet-cake.exe` for Windows.

# Using run configurations

## Creating run configurations

The configurations can either be created from an existing Cake task, 
using the [tool window](#cake-tasks-tool-window) or created manually using the run [configuration editor](#editing-run-configurations).

## Editing run configurations

An editor for run configurations is available:

![Run configuration editor](/assets/img/cake-rider/docs/runConfiguration-editor.png){.img-responsive}

## Running builds

The Cake tasks tool window lists all Cake scripts and their tasks:

![Cake Tasks tool window](/assets/img/cake-rider/docs/toolWindow.png){.img-responsive}

Using a double click on a task will run that task immediately:

![Cake Tasks tool window](/assets/img/cake-rider/docs/cake-run.png){.img-responsive}

Alternatively, the buttons at the top of the tool window can be used to either run the task immediately,
or create a new [run configuration](#creating-run-configurations).

