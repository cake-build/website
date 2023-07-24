Order: 30
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
</ul>

<div class="tab-content">
    <div id="tool" class="tab-pane fade in active">
        <p>
            To use Cake .NET Tool it needs to be installed globally:
        </p>
        <pre><code class="language-cmd hljs">dotnet tool install --global Cake.Tool</code></pre>
        <p>
            This is the default setup in Cake for Rider. Alternatively it is also possible to use a project local installation of the Cake .NET Tool.
        </p>
    </div>
    <div id="frosting" class="tab-pane fade">
        <p>
            There is currently no support for run configurations for Cake Frosting.
        </p>
    </div>
</div>

## Supported runners

| Runner                           | Supported                                             | Remarks                                            |
|----------------------------------|-------------------------------------------------------|----------------------------------------------------|
| [Cake .NET Tool]                 | <i class="fa-solid fa-check" style="color:green"></i> |                                                    |
| [Cake Frosting]                  | <i class="fa-solid fa-xmark" style="color:red"></i>   |                                                    |

[Cake .NET Tool]: dotnet-tool
[Cake Frosting]: cake-frosting

## Settings

There are multiple configuration settings available under **File | Settings | Build, Execution, Deployment | Cake**.

All settings are project - specific and stored in the `.idea` folder. To share settings across developers, make sure to put `CakeRider.xml` under source control.

### Generic settings

* *Task Regex*
  This regular expression is used to parse tasks from the Cake files.
  Default: `Task\s*?\(\s*?"(.*?)"\s*?\)`
* *Verbosity*
  This is the default verbosity to use, when running a task directly from the Cake Tasks window or when creating a new run configuration.
  Default: `normal`

### Search paths

Allows to modify search paths that are used to find all Cake files that are displayed
in the [Cake Tasks window](#running-builds-directly).

* *Search paths*
  One or multiple starting points to search for Cake files.
  The paths are relative to the project root and should use `/` as separator between directories.
  Default: `.` (root of the project directory.)
* *Exclude expressions*
  Regular expressions to exclude search paths.
  Each path that is found during the search is matched against all configured expressions.
  Matches will be excluded.
  Default: `.*/tools/.*`

### Runner settings

Allows to define the runner to use.

The first option, *Use 'dotnet cake' instead of executable*, will use `dotnet cake` as the runner.
The `dotnet` executable must be in available in the path and the Cake .NET Tool must either be globally installed to the machine
or locally installed to the project. If this option is set, all other options on the settings page are ignored.

Alternatively, when *Use 'dotnet cake' instead of executable* is not set,
a path to the Cake executable (either `dotnet-cake.exe`, or `cake.exe`) has to be supplied:

Different runners for different operating systems can be set by defining a regular expression which is matched against the system property `os.name`.

The Cake runner settings are able to process environment variables: Use a specific variable like `${VARIABLE}` and the environment variable will be expanded when the runner is called.

Default value is `~/.dotnet/tools/dotnet-cake` and `${USERPROFILE}\.dotnet\tools\dotnet-cake.exe` for Windows.

# Using run configurations

## Creating run configurations

The configurations can either be created from an existing Cake task,
using the [Cake Tasks window](#running-builds-directly) or created manually using the run [configuration editor](#editing-run-configurations).

## Editing run configurations

An editor for run configurations is available:

![Run configuration editor](/assets/img/cake-rider/docs/runConfiguration-editor.png){.img-responsive}

### Settings in the run configuration

* *Script path*:
  Path to the Cake file. (This is passed as first argument to the runner.)
* *Task*:
  Name of the task to run (This is passed to the runner using `--task=<Task>`.)
* *Verbosity*:
  Verbosity of the Cake output. (This is passed to the runner using `--verbosity=<Verbosity>`.)
* *Arguments*:
  Arbitrary additional arguments to the runner.

### Before launch activity

* *Ensure .NET Tool (Global)*:
  Ensures the Cake .NET tool is installed globally before running Cake.
* *Restore .NET Tools*:
  Restores the .NET tools, effectively calling `dotnet tool restore` before running the build.
  
![Run Actions](/assets/img/cake-rider/docs/beforeRunActions.png){.img-responsive}

## Running builds directly

The Cake Tasks window lists all Cake scripts and their tasks:

![Cake Tasks window](/assets/img/cake-rider/docs/toolWindow.png){.img-responsive}

Using a double click on a task will run that task immediately:

![Cake task run](/assets/img/cake-rider/docs/cake-run.png){.img-responsive}

Alternatively, the buttons at the top of the Cake Tasks window can be used to either run the task immediately,
or create a new [run configuration](#creating-run-configurations).
