Order: 20
---

:::{.alert .alert-success}
A console application has the advantage of full IDE support, like IntelliSense, refactoring and debugging.
:::

# Requirements

[Cake.Frosting](https://github.com/cake-build/frosting) is a .NET host which allows you to write your build scripts as a console application
(`netcoreapp3.1` or `net461`).

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
