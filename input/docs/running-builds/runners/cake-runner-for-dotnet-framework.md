Order: 30
Title: Cake runner for .NET Framework
---

This runner is mainly for backwards compatibility where scripts or addins are used which require .NET Framework.
In all other cases it is recommended to use [.NET Core Tool](dotnet-core-tool).

# Requirements

The [Cake](https://www.nuget.org/packages/Cake) NuGet package is a runner requiring [.NET Framework 4.6.1](https://www.microsoft.com/net/download/dotnet-framework/net461)
or newer on Windows and Mono `5.12.0` or newer on Mac or Linux.

:::{.alert .alert-warning}
It is suggested to use [.NET Framework 4.7.2](https://www.microsoft.com/net/download/dotnet-framework/net472) or newer to run build scripts
which are using addins targeting .NET Standard 2.0 only.
:::

# Usage

```powershell
Cake.exe [script] [switches]
```

^"../../../Shared/switches.md"

# Bootstrapping for Cake runner for .NET Framework

Bootstrapping scripts ensure you have Cake and other required dependencies installed.
The bootstrapper scripts are also responsible for invoking Cake.

## Getting the bootstrapper

Bootstrapper for Cake runner for .NET Framework is available in the [Cake Resources repository](https://github.com/cake-build/resources)
and can be installed using the command for your operating system from below:

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
