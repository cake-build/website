Order: 10
Title: Cake .NET Tool
RedirectFrom: docs/running-builds/runners/dotnet-core-tool
---

:::{.alert .alert-success}
This is the recommended way to run Cake Scripts.
:::

# Requirements

The [Cake.Tool](https://www.nuget.org/packages/Cake.Tool) NuGet package, is a .NET Core tool compiled for .NET Core 2.1 or newer.

# Usage

```powershell
dotnet cake [script] [switches]
```

^"../../../Shared/switches.txt"

# Bootstrapping for .NET Tool

Bootstrapping scripts ensure you have Cake and other required dependencies installed.
The bootstrapper scripts are also responsible for invoking Cake.

:::{.alert .alert-info}
The following instructions require .NET Core 3.0 or newer.
See [How to manage .NET Core tools](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) for details and other options.
:::

## Setup

There's a one-time setup required for configuring a repository to use Cake .NET tool.

:::{.alert .alert-info}
If you have .NET Tool already available in your environment you can skip the steps in this chapter.
:::

Make sure to have a tool manifest available in your repository or create one using the following command:

```powershell
dotnet new tool-manifest
```

Install Cake as a local tool using the `dotnet tool` command:

```powershell
dotnet tool install Cake.Tool --version <?! Meta CakeLatestReleaseName /?>
```

You can replace `<?! Meta CakeLatestReleaseName /?>` with a different version of Cake you want to use.

## Getting the bootstrapper

:::{.alert .alert-info}
Use of a bootstrapper is optional.
You can also directly call the .NET CLI if you prefer.
:::

A Bootstrapper for Cake .NET Tool is available in the [Cake Resources repository](https://github.com/cake-build/resources)
and can be installed using the command for your operating system from below:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#windows1">Windows</a></li>
    <li><a data-toggle="tab" href="#linux1">Linux</a></li>
    <li><a data-toggle="tab" href="#macos1">MacOS</a></li>
</ul>

<div class="tab-content">
    <div id="windows1" class="tab-pane fade in active">
        <p>
            Open a new PowerShell window and run the following command:
        </p>
        <p>
<pre><code class="language-powershell hljs">Invoke-WebRequest https://cakebuild.net/download/bootstrapper/dotnet-tool/windows -OutFile build.ps1</code></pre>
        </p>
        <p>
            <div class="alert alert-info" role="alert">
                <p>
                    Sometimes PowerShell might prevent you from running <code>build.ps1</code>.
                    Make sure to have <code>RemoteSigned</code> policy enabled.
                    See <a href="http://go.microsoft.com/fwlink/?LinkID=135170">About Execution Policies</a> for details.
                </p>
                <p>
                    If you have <code>RemoteSigned</code> policy enabled and still an error occurrs it might be because
                    the file was downloaded from the internet and is blocked.
                    The following command will unblock the file:
                </p>
                <p>
<pre><code class="language-powershell hljs">Unblock-File path\to\build.ps1</code></pre>
                </p>
                <p>
                    See <a href="https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/unblock-file">Unblock-File</a> for details.
                </p>
            </div>
        </p>
    </div>
    <div id="linux1" class="tab-pane fade">
        <p>
            Open a new shell and run the following command:
        </p>
        <p>
<pre><code class="language-bash hljs">curl -Lsfo build.sh https://cakebuild.net/download/bootstrapper/dotnet-tool/linux</code></pre>
        </p>
        <p>
            <div class="alert alert-info" role="alert">
                <p>
                    If you are downloading the <code>build.sh</code> file on a Windows machine you can give the script permission to execute using the following command:
                </p>
                <p>
<pre><code class="language-bash hljs">git update-index --add --chmod=+x build.sh</code></pre>
                </p>
                <p>
                    This assumes that you have the Git command line installed.
                </p>
            </div>
        </p>
    </div>
    <div id="macos1" class="tab-pane fade">
        <p>
            Open a new shell and run the following command:
        </p>
        <p>
<pre><code class="language-bash hljs">curl -Lsfo build.sh https://cakebuild.net/download/bootstrapper/dotnet-tool/osx</code></pre>
        </p>
        <p>
            <div class="alert alert-info" role="alert">
                <p>
                    If you are downloading the <code>build.sh</code> file on a Windows machine you can give the script permission to execute using the following command:
                </p>
                <p>
<pre><code class="language-bash hljs">git update-index --add --chmod=+x build.sh</code></pre>
                </p>
                <p>
                    This assumes that you have the Git command line installed.
                </p>
            </div>
        </p>
    </div>
</div>

## Running build script

To launch Cake run the bootstrapper:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#windows2">Windows</a></li>
    <li><a data-toggle="tab" href="#linux2">Linux</a></li>
    <li><a data-toggle="tab" href="#macos2">MacOS</a></li>
</ul>

<div class="tab-content">
    <div id="windows2" class="tab-pane fade in active">
        <p>
            Open a new PowerShell window and run the following command:
        </p>
        <p>
            <pre><code class="language-powershell hljs">./build.ps1</code></pre>
        </p>
    </div>
    <div id="linux2" class="tab-pane fade">
        <p>
            Open a new shell and run the following command:
        </p>
        <p>
            <pre><code class="language-bash hljs">build.sh</code></pre>
        </p>
    </div>
    <div id="macos2" class="tab-pane fade">
        <p>
            Open a new shell and run the following command:
        </p>
        <p>
            <pre><code class="language-bash hljs">build.sh</code></pre>
        </p>
    </div>
</div>

:::{.alert .alert-info}
By convention this will execute the build script named `build.cake`.
You can override this behavior by additionally passing the name of the build script.
:::

## Extending the bootstrapper

The bootstrapper that you can get directly from [cakebuild.net](https://cakebuild.net) is intended as a starting point for what can be done.
It is the developer's discretion to extend the bootstrapper to solve for your own requirements.

# Using pre-release versions

Cake uses [Azure Artifacts](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake.Tool&protocolType=NuGet) as a NuGet feed for testing and pre-release builds.
With these pre-release builds the next version of Cake can be accessed and utilized for getting the latest features or testing addins or build scripts to know if the next release will be safe when you need to upgrade.
