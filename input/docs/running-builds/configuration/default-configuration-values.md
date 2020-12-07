Order: 20
Description: Available configuration options
RedirectFrom:
  - docs/running-builds/default-configuration-values
  - docs/fundamentals/default-configuration-values
---

The following shows all of the configuration options currently available within Cake, as well as their default values.

Refer to [set configuration values](/docs/running-builds/configuration/set-configuration-values) for instructions on using a config file.

# Addin NuGet Dependencies

:::{.alert .alert-info}
Available since Cake `0.22.0`.
:::

When using In-Process NuGet installation you can also opt in to automatically installing and referencing NuGet package dependencies.

_Default value:_ `false`

_Valid values:_ `true`, `false`

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#env1">Environment variable name</a></li>
    <li><a data-toggle="tab" href="#config1">Config file contents</a></li>
    <li><a data-toggle="tab" href="#arg1">Argument name</a></li>
</ul>

<div class="tab-content">
    <div id="env1" class="tab-pane fade in active">
        <p>
            <pre><code class="language-sh hljs">CAKE_NUGET_LOADDEPENDENCIES</code></pre>
        </p>
    </div>
    <div id="config1" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">[NuGet]
LoadDependencies=true</code></pre>
        </p>
    </div>
    <div id="arg1" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">--nuget_loaddependencies=true</code></pre>
        </p>
    </div>
</div>

# Addins Path

This allows the configuration of the addins folder which is used by Cake when restoring addins.

_Default value:_ `./tools/Addins`

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#env2">Environment variable name</a></li>
    <li><a data-toggle="tab" href="#config2">Config file contents</a></li>
    <li><a data-toggle="tab" href="#arg2">Argument name</a></li>
</ul>

<div class="tab-content">
    <div id="env2" class="tab-pane fade in active">
        <p>
            <pre><code class="language-sh hljs">CAKE_PATHS_ADDINS</code></pre>
        </p>
    </div>
    <div id="config2" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">[Paths]
Addins=./tools/Addins</code></pre>
        </p>
    </div>
    <div id="arg2" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">--paths_addins=./tools/Addins</code></pre>
        </p>
    </div>
</div>

# In-Process NuGet installation

:::{.alert .alert-info}
Available since Cake `0.25.0`.
:::

Since Cake `0.25.0` the default is to use the In-Process NuGet Client.
If you require to use `NuGet.exe` instead, you can opt-out by setting this value to false.

_Default value:_ `true`

_Valid values:_ `true`, `false`

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#env3">Environment variable name</a></li>
    <li><a data-toggle="tab" href="#config3">Config file contents</a></li>
    <li><a data-toggle="tab" href="#arg3">Argument name</a></li>
</ul>

<div class="tab-content">
    <div id="env3" class="tab-pane fade in active">
        <p>
            <pre><code class="language-sh hljs">CAKE_NUGET_USEINPROCESSCLIENT</code></pre>
        </p>
    </div>
    <div id="config3" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">[NuGet]
UseInProcessClient=false</code></pre>
        </p>
    </div>
    <div id="arg3" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">--nuget_useinprocessclient=false</code></pre>
        </p>
    </div>
</div>

# Modules Path

This allows the configuration of the Modules folder which is used by Cake when loading custom Modules.

_Default value:_ `./tools/Modules`

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#env4">Environment variable name</a></li>
    <li><a data-toggle="tab" href="#config4">Config file contents</a></li>
    <li><a data-toggle="tab" href="#arg4">Argument name</a></li>
</ul>

<div class="tab-content">
    <div id="env4" class="tab-pane fade in active">
        <p>
            <pre><code class="language-sh hljs">CAKE_PATHS_MODULES</code></pre>
        </p>
    </div>
    <div id="config4" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">[Paths]
Modules=./tools/Modules</code></pre>
        </p>
    </div>
    <div id="arg4" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">--paths_modules=./tools/Modules</code></pre>
        </p>
    </div>
</div>

# NuGet Configuration File Path

This allows the configuration of the path to NuGet config file for overriding the default file.

_Default value:_ If NuGet config not specified, will use NuGet default mechanism for resolving it.

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#env5">Environment variable name</a></li>
    <li><a data-toggle="tab" href="#config5">Config file contents</a></li>
    <li><a data-toggle="tab" href="#arg5">Argument name</a></li>
</ul>

<div class="tab-content">
    <div id="env5" class="tab-pane fade in active">
        <p>
            <pre><code class="language-sh hljs">CAKE_NUGET_CONFIGFILE</code></pre>
        </p>
    </div>
    <div id="config5" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">[NuGet]
ConfigFile=./NuGet.config</code></pre>
        </p>
    </div>
    <div id="arg5" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">--nuget_configfile=./NuGet.config</code></pre>
        </p>
    </div>
</div>

# NuGet Download Url

This allows the control of where Cake downloads NuGet packages from when using the addin and tool preprocessor directives.
This can be useful when it is necessary to work in an offline mode, where direct access to nuget.org is not available.

:::{.alert .alert-info}
Multiple sources can be passed separated by a semicolon.
:::

_Default value:_ `https://packages.nuget.org/api/v2`

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#env6">Environment variable name</a></li>
    <li><a data-toggle="tab" href="#config6">Config file contents</a></li>
    <li><a data-toggle="tab" href="#arg6">Argument name</a></li>
</ul>

<div class="tab-content">
    <div id="env6" class="tab-pane fade in active">
        <p>
            <pre><code class="language-sh hljs">CAKE_NUGET_SOURCE</code></pre>
        </p>
    </div>
    <div id="config6" class="tab-pane fade">
        <p>
            With a single source:
        </p>
        <p>
            <pre><code class="language-sh hljs">[Nuget]
Source=http://myfeed/nuget/</code></pre>
        </p>
        <p>
            With multiple sources:
        </p>
        <p>
            <pre><code class="language-sh hljs">[Nuget]
Source=http://myfeed/nuget/;http://myotherfeed/nuget</code></pre>
        </p>
    </div>
    <div id="arg6" class="tab-pane fade">
        <p>
            With a single source:
        </p>
        <p>
            <pre><code class="language-sh hljs">--nuget_source=http://myfeed/nuget/</code></pre>
        </p>
        <p>
            With multiple sources:
        </p>
        <p>
            <pre><code class="language-sh hljs">--nuget_source=http://myfeed/nuget/;http://myotherfeed/nuget</code></pre>
        </p>
    </div>
</div>

# Show Process Command Line

When Cake runs a tool the default behavior is to log the file name and the arguments only when the `Diagnostic` log verbosity is used.
This setting will instruct Cake to always log the file name and arguments regardless of the log verbosity setting.

_Default value:_ `false`

_Valid values:_ `true`, `false`

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#env7">Environment variable name</a></li>
    <li><a data-toggle="tab" href="#config7">Config file contents</a></li>
    <li><a data-toggle="tab" href="#arg7">Argument name</a></li>
</ul>

<div class="tab-content">
    <div id="env7" class="tab-pane fade in active">
        <p>
            <pre><code class="language-sh hljs">CAKE_SETTINGS_SHOWPROCESSCOMMANDLINE</code></pre>
        </p>
    </div>
    <div id="config7" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">[Settings]
ShowProcessCommandLine=true</code></pre>
        </p>
    </div>
    <div id="arg7" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">--settings_showprocesscommandline=true</code></pre>
        </p>
    </div>
</div>

# Skip Verification

If any breaking changes are introduced to Cake, we'll set the minimum supported version of the `Cake.Core` assembly.
Any addin/assembly that references an older version of `Cake.Core` will generate an exception and ultimately abort the execution of the script.
This is to avoid runtime errors during execution of the script.

But if you really need to use an addin/assembly and have verified that the breaking change introduced doesn't concern the addin/assembly directly, i.e. it doesn't use a removed property or changed interface, for this scenario we'll allow you to temporarily opt-out of the assembly verification until addin/assembly author has released a version of the assembly targeting the minimum required version of `Cake.Core`.

:::{.alert .alert-warning}
This setting is global for all addins/assemblies your script references and should therefore be considered a temporary quick fix.
:::

_Default value:_ `false`

_Valid values:_ `true`, `false`

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#env8">Environment variable name</a></li>
    <li><a data-toggle="tab" href="#config8">Config file contents</a></li>
    <li><a data-toggle="tab" href="#arg8">Argument name</a></li>
</ul>

<div class="tab-content">
    <div id="env8" class="tab-pane fade in active">
        <p>
            <pre><code class="language-sh hljs">CAKE_SETTINGS_SKIPVERIFICATION</code></pre>
        </p>
    </div>
    <div id="config8" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">[Settings]
SkipVerification=true</code></pre>
        </p>
    </div>
    <div id="arg8" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">--settings_skipverification=true</code></pre>
        </p>
    </div>
</div>

# Tools Path

This allows the configuration of the tools folder which is used by Cake when restoring tools.

_Default value:_ `./tools`

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#env9">Environment variable name</a></li>
    <li><a data-toggle="tab" href="#config9">Config file contents</a></li>
    <li><a data-toggle="tab" href="#arg9">Argument name</a></li>
</ul>

<div class="tab-content">
    <div id="env9" class="tab-pane fade in active">
        <p>
            <pre><code class="language-sh hljs">CAKE_PATHS_TOOLS</code></pre>
        </p>
    </div>
    <div id="config9" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">[Paths]
Tools=./tools</code></pre>
        </p>
    </div>
    <div id="arg9" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">--paths_tools=./tools</code></pre>
        </p>
    </div>
</div>
