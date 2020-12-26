Order: 10
Description: Ways to set configuration values
RedirectFrom:
  - docs/fundamentals/configuration
---

Cake supports the concept of external configuration, to allow the internals of how Cake operates to be controlled, based on a specified priority, from either:

* Environment variables
* A configuration file
* Arguments passed into Cake

:::{.alert .alert-info}
These configuration options are prioritised in the order shown above.
This means the configuration file overrides any environment variables, and arguments passed directly into Cake override both environment variables and configuration file settings.
:::

As an example of where this overridable configuration can be applied, let's look at how Cake determines where to download NuGet packages from. By default, it does this by downloading the NuGet packages user configured sources.  However, it may be necessary (for example, when running in an offline/local environment) to download these NuGet packages from an alternative source.  This is where the Cake Configuration comes into play.

:::{.alert .alert-info}
Refer to the [default configuration values](default-configuration-values) for a list of all available configuration options.
:::

# Environment Variable

By creating an Environment variable with the name of `CAKE_NUGET_SOURCE` and setting the value to the URL that is required, Cake will use this alternative download source, rather than the defaults.

# Configuration File

Alternatively, you can create a `cake.config` file with the following content:

```sh
; The configuration file for Cake.

[Nuget]
Source=https://mycustomurl
```

Specifying a configuration value within a configuration file will override the same configuration value stored within an equivalent environment variable.

:::{.alert .alert-info}
The configuration file should be located in the same directory as your Cake script.
:::

# Command Line

Finally, you can specify an input parameter directly to Cake, in the following format:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool1">Cake .NET Tool</a></li>
    <li><a data-toggle="tab" href="#frosting1">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx1">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core1">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool1" class="tab-pane fade in active">
        <p>
            <code class="language-powershell hljs">
                dotnet cake --nuget_source=http://mycustomurl
            </code>
        </p>
    </div>
    <div id="frosting1" class="tab-pane fade">
        <p>
            <code class="language-powershell hljs">
                ./build.ps1 --nuget_source=http://mycustomurl
            </code>
        </p>
    </div>
    <div id="netfx1" class="tab-pane fade">
        <p>
            <code class="language-powershell hljs">
                cake.exe --nuget_source=http://mycustomurl
            </code>
        </p>
    </div>
    <div id="core1" class="tab-pane fade">
        <p>
            <code class="language-powershell hljs">
                cake.exe --nuget_source=http://mycustomurl
            </code>
        </p>
    </div>
</div>

Passing a configuration value directly to Cake will override the same configuration value stored within an environment variable and also any stored in a local configuration file.

When configuring NuGet sources in both `cake.config`, and via the command line, multiple sources can be supplied by joining them with a semicolon:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool2">Cake .NET Tool</a></li>
    <li><a data-toggle="tab" href="#frosting2">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx2">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core2">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool2" class="tab-pane fade in active">
        <p>
            <code class="language-powershell hljs">
                dotnet cake --nuget_source=http://mycustomurl;http://myothercustomurl
            </code>
        </p>
    </div>
    <div id="frosting2" class="tab-pane fade">
        <p>
            <code class="language-powershell hljs">
                ./build.ps1 --nuget_source=http://mycustomurl;http://myothercustomurl
            </code>
        </p>
    </div>
    <div id="netfx2" class="tab-pane fade">
        <p>
            <code class="language-powershell hljs">
                cake.exe --nuget_source=http://mycustomurl;http://myothercustomurl
            </code>
        </p>
    </div>
    <div id="core2" class="tab-pane fade">
        <p>
            <code class="language-powershell hljs">
                cake.exe --nuget_source=http://mycustomurl;http://myothercustomurl
            </code>
        </p>
    </div>
</div>
