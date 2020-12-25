Order: 20
---

Cake Frosting is a .NET host which allows you to write your build scripts as a console application.

:::{.alert .alert-success}
A console application has the advantage of full IDE support, like IntelliSense, refactoring and debugging.
:::

:::{.alert .alert-info}
See [Setting Up A New Frosting Project](/docs/getting-started/setting-up-a-new-frosting-project) tutorial for how to get started with Cake Frosting.
:::

# Requirements

Cake.Frosting can be used to write console applications targeting `netcoreapp3.1` or `net461`.

# Usage

```powershell
dotnet Cake.Frosting.dll [switches]
```

## Switches

| Switch                            | Description                                                                                        | Available Since |
|-----------------------------------|----------------------------------------------------------------------------------------------------|-----------------|
| --description                     | Shows description about tasks.                                                                     | [1.0.0-rc0002]  |
| --dryrun                          | Performs a dry run.                                                                                | [0.30.0]        |
| -e, --exclusive                   | Execute a single task without any dependencies.                                                    | [1.0.0-rc0002]  |
| -h, --help                        | Prints help information.                                                                           | [0.30.0]        |
| --info                            | Displays additional information about Cake execution.                                              | [1.0.0-rc0002]  |
| -t, --target &lt;TARGET&gt;       | Sets the build target.                                                                             | [0.30.0]        |
| --tree                            | Shows the task dependency tree                                                                     | [1.0.0-rc0002]  |
| -v, --verbosity &lt;VERBOSITY&gt; | Specifies the amount of information to be displayed (quiet, minimal, normal, verbose, diagnostic). | [0.30.0]        |
| --version                         | Displays Cake.Frosting version number.                                                             | [0.30.0]        |
| -w, --working &lt;PATH&gt;        | Sets the working directory.                                                                        | [0.30.0]        |

[0.30.0]: https://github.com/cake-build/cake/releases/tag/v0.30.0
[1.0.0-rc0002]: https://github.com/cake-build/cake/releases/tag/v1.0.0-rc0002

## Custom switches

All switches not recognized by Cake will be added to an argument list that is passed to the build script.
See [Arguments And Environment Variables](../../writing-builds/args-and-environment-vars#arguments) how to read arguments in your script.

# Bootstrapping for Cake Frosting

When creating a new Cake Frosting project from the template default bootstrapping scripts for Windows, macOS and Linux are created.

:::{.alert .alert-info}
The following instructions require .NET Core 3.1.301 or newer.
You can find the SDK at https://dotnet.microsoft.com/download
:::

## Setup

To create a new Cake Frosting project you need to install the Frosting template:

```powershell
dotnet new --install Cake.Frosting.Template
```

Create a new Frosting project:

```powershell
dotnet new cakefrosting
```

This will create the Cake Frosting project and bootstrapping scripts.

## Running build script


Run the build script:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#windows">Windows</a></li>
    <li><a data-toggle="tab" href="#linux">Linux</a></li>
    <li><a data-toggle="tab" href="#macos">MacOS</a></li>
</ul>

<div class="tab-content">
    <div id="windows" class="tab-pane fade in active">
        <p>
            <code class="language-powershell hljs">
               ./build.ps1
            </code>
        </p>
    </div>
    <div id="linux" class="tab-pane fade">
        <p>
            <code class="language-bash hljs">
               build.sh
            </code>
        </p>
    </div>
    <div id="macos" class="tab-pane fade">
        <p>
            <code class="language-bash hljs">
               build.sh
            </code>
        </p>
    </div>
</div>

# Using pre-release versions

Cake uses [Azure Artifacts](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake.Frosting&protocolType=NuGet) as a NuGet feed for testing and pre-release builds.
With these pre-release builds the next version of Cake can be accessed and utilized for getting the latest features or testing addins or build scripts to know if the next release will be safe when you need to upgrade.
