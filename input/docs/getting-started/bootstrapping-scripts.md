Order: 30
RedirectFrom:
  - docs/tutorials/extending-the-bootstrapper
  - docs/tutorials/powershell-security
---

Bootstrapping scripts ensure you have Cake and other required dependencies installed.
The bootstrapper scripts are also responsible for invoking Cake.

:::{.alert .alert-info}
If you have [.NET Core Tool](running-cake-scripts#net-core-tool) or any other Cake Runner already available in your
environment you won't need a bootstrapping script.
:::

# Bootstrapping for .NET Core Tool

:::{.alert .alert-info}
The following instructions require .NET Core 3.0 or newer.
See [How to manage .NET Core tools](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) for details and other options.
:::

## Setup

There's a one-time setup required for configuring a repository to use Cake .NET Core tool.

Make sure to have a tool manifest available in your repository or create one using the following command:

```shell
dotnet new tool-manifest
```

Install Cake as a local tool using the `dotnet tool` command:

```shell
dotnet tool install Cake.Tool --version x.y.z
```

## Running build script

Make sure tools are restored:

```shell
dotnet tool restore
```

Once installed, you can launch Cake using the .NET CLI:

```shell
dotnet cake
```

:::{.alert .alert-info}
By convention this will execute the build script named `build.cake`.
You can override this behavior by additionally passing the name of the build script.
:::

# Bootstrapping for Cake Frosting

When creating a new [Cake Frosting](https://github.com/cake-build/frosting) project from the template default bootstrapping scripts for Windows, macOS and Linux are created.

:::{.alert .alert-info}
The following instructions require .NET Core 3.1.301 or newer.
You can find the SDK at https://dotnet.microsoft.com/download
:::

## Setup

To create a new [Cake Frosting](https://github.com/cake-build/frosting) project you need to install the Frosting template:

```
dotnet new --install Cake.Frosting.Template
```

Create a new Frosting project:

```
dotnet new cakefrosting
```

This will create the Cake Frosting build script and bootstrapping scripts.

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

# Bootstrapping for Cake runner for .NET Framework

## Getting the bootstrapper

Bootstrapper for [Cake runner for .NET Framework](running-cake-scripts/#cake-runner-for-net-framework) is available in the
[Cake Resources repository](https://github.com/cake-build/resources) and can be installed using the command for your
operating system from below:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#windows">Windows</a></li>
    <li><a data-toggle="tab" href="#linux">Linux</a></li>
    <li><a data-toggle="tab" href="#macos">MacOS</a></li>
</ul>

<div class="tab-content">
    <div id="windows" class="tab-pane fade in active">
        <p>
            Open a new PowerShell window and run the following command:
        </p>
        <p>
            <code class="language-powershell hljs">
                Invoke-WebRequest https://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1
            </code>
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
                    <code class="language-powershell hljs">
                        Unblock-File path\to\build.ps1
                    </code>
                </p>
                <p>
                    See <a href="https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/unblock-file">Unblock-File</a> for details.
                </p>
            </div>
        </p>
    </div>
    <div id="linux" class="tab-pane fade">
        <p>
            Open a new shell and run the following command:
        </p>
        <p>
            <code class="language-bash hljs">
                curl -Lsfo build.sh https://cakebuild.net/download/bootstrapper/linux
            </code>
        </p>
        <p>
            <div class="alert alert-info" role="alert">
                <p>
                    If you are downloading the <code>build.sh</code> file on a Windows machine you can give the script permission to execute using the following command:
                </p>
                <p>
                    <code class="language-bash hljs">
                        git update-index --add --chmod=+x build.sh
                    </code>
                </p>
                <p>
                    This assumes that you have the Git command line installed.
                </p>
            </div>
        </p>
    </div>
    <div id="macos" class="tab-pane fade">
        <p>
            Open a new shell and run the following command:
        </p>
        <p>
            <code class="language-bash hljs">
                curl -Lsfo build.sh https://cakebuild.net/download/bootstrapper/osx
            </code>
        </p>
        <p>
            <div class="alert alert-info" role="alert">
                <p>
                    If you are downloading the <code>build.sh</code> file on a Windows machine you can give the script permission to execute using the following command:
                </p>
                <p>
                    <code class="language-bash hljs">
                        git update-index --add --chmod=+x build.sh
                    </code>
                </p>
                <p>
                    This assumes that you have the Git command line installed.
                </p>
            </div>
        </p>
    </div>
</div>

## Extending the bootstrapper

The bootstrapper that you can get directly from [cakebuild.net](https://cakebuild.net) is intended as a starting point for what can be done.
It is the developer's discretion to extend the bootstrapper to solve for your own requirements.
