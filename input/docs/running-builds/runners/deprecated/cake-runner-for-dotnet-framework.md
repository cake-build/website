Order: 10
Title: Cake runner for .NET Framework
RedirectFrom: docs/running-builds/runners/cake-runner-for-dotnet-framework
---

:::{.alert .alert-danger}
The Cake runner for .NET Framework is deprecated and no longer updated since Cake 2.0.
It is suggested to use [Cake .NET Tool] for running Cake scripts.
:::

# Requirements

The [Cake](https://www.nuget.org/packages/Cake) NuGet package was a runner requiring [.NET Framework 4.6.1](https://www.microsoft.com/net/download/dotnet-framework/net461)
or newer on Windows and Mono `5.12.0` or newer on Mac or Linux.

:::{.alert .alert-warning}
It is suggested to use [.NET Framework 4.7.2](https://www.microsoft.com/net/download/dotnet-framework/net472) or newer to run build scripts
which are using addins targeting .NET Standard 2.0 only.
:::

# Usage

```powershell
Cake.exe [script] [switches]
```

^"../../../../Shared/switches-1-0.txt"

# Bootstrapping for Cake runner for .NET Framework

Bootstrapping scripts ensure you have Cake and other required dependencies installed.
The bootstrapper scripts are also responsible for invoking Cake.

## Getting the bootstrapper

Bootstrapper for Cake runner for .NET Framework is available in the [Cake Resources repository](https://github.com/cake-build/resources)
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
    <div id="linux1" class="tab-pane fade">
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
    <div id="macos1" class="tab-pane fade">
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

## Extending the bootstrapper

The bootstrapper that you can get directly from [cakebuild.net](https://cakebuild.net) is intended as a starting point for what can be done.
It is the developer's discretion to extend the bootstrapper to solve for your own requirements.

# Using pre-release versions

Cake uses [Azure Artifacts](https://dev.azure.com/cake-build/Cake/_packaging?_a=package&feed=cake&package=Cake&protocolType=NuGet) as a NuGet feed for testing and pre-release builds.
With these pre-release builds the next version of Cake can be accessed and utilized for getting the latest features or testing addins or build scripts to know if the next release will be safe when you need to upgrade.

:::{.alert .alert-info}
These instructions assume you are using the NuGet CLI as done in the [default bootstrapper for Windows](https://github.com/cake-build/resources/blob/develop/build.ps1)
or [default bootstrapper for macOS & Linux](https://github.com/cake-build/resources/blob/develop/build.sh).
:::

1. Update the bootstrapper

   <ul class="nav nav-tabs">
       <li class="active"><a data-toggle="tab" href="#windows3">Windows</a></li>
       <li><a data-toggle="tab" href="#linux3">Linux</a></li>
       <li><a data-toggle="tab" href="#macos3">MacOS</a></li>
   </ul>

   <div class="tab-content">
       <div id="windows3" class="tab-pane fade in active">
           <p>
               Replace the following line in the bootstrapper:
           </p>
           <p>
               <code class="language-powershell hljs">
                   $NuGetOutput = Invoke-Expression "& $env:NUGET_EXE_INVOCATION install -ExcludeVersion -OutputDirectory `"$TOOLS_DIR`""
               </code>
           </p>
           <p>
               with:
           </p>
           <p>
               <code class="language-powershell hljs">
                   $NuGetOutput = Invoke-Expression "& $env:NUGET_EXE_INVOCATION install -ExcludeVersion -OutputDirectory `"$TOOLS_DIR`" -Source https://pkgs.dev.azure.com/cake-build/Cake/_packaging/cake/nuget/v3/index.json"
               </code>
           </p>
       </div>
       <div id="linux3" class="tab-pane fade">
           <p>
               Replace the following line in the bootstrapper:
           </p>
           <p>
               <code class="language-bash hljs">
                   mono "$NUGET_EXE" install -ExcludeVersion
               </code>
           </p>
           <p>
               with:
           </p>
           <p>
               <code class="language-bash hljs">
                   mono "$NUGET_EXE" install -ExcludeVersion -Source https://pkgs.dev.azure.com/cake-build/Cake/_packaging/cake/nuget/v3/index.json"
               </code>
           </p>
       </div>
       <div id="macos3" class="tab-pane fade">
           <p>
               Replace the following line in the bootstrapper:
           </p>
           <p>
               <code class="language-bash hljs">
                   mono "$NUGET_EXE" install -ExcludeVersion
               </code>
           </p>
           <p>
               with:
           </p>
           <p>
               <code class="language-bash hljs">
                   mono "$NUGET_EXE" install -ExcludeVersion -Source https://pkgs.dev.azure.com/cake-build/Cake/_packaging/cake/nuget/v3/index.json"
               </code>
           </p>
       </div>
   </div>
2. Modify the `packages.config` file to pin the Cake version to one of the pre-releases:

   ```xml
   <?xml version="1.0" encoding="utf-8"?>
   <packages>
       <package id="Cake" version="1.0.0-alpha0079" />
   </packages>
   ```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
