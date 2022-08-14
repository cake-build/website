Order: 30
Description: Custom configurations
---
:::{.alert .alert-warning}
Configuration is currently not supported for [Cake Frosting].
:::

Besides the built-in configurations that are available in Cake, it is possible to use custom configurations.

Like the built-in configurations, custom configurations are also separated in sections and keys.

# Accessing a configuration value

Configurations can be accessed by using [IConfiguration.GetValue](https://cakebuild.net/api/Cake.Core.Configuration/ICakeConfiguration/4007C3B8).
The section and the key need to be separated by an underscore (`_`). Access to configurations is case insensitive.

```cs
var value = Context.Configuration.GetValue("MySection_MyKey");
```

# Setting a configuration value

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#environment">Environment variable</a></li>
    <li><a data-toggle="tab" href="#cake-conf">cake.config</a></li>
    <li><a data-toggle="tab" href="#command-line">command line</a></li>
</ul>

<div class="tab-access">
    <div id="environment" class="tab-pane fade in active">
        <p>
            <pre><code class="language-sh hljs"># Set an Environment variable by prefixing 'CAKE_'
# and separating section and key with an underscore.
[Environment]::SetEnvironmentVariable("CAKE_MYSECTION_MYKEY", "MyValue" [EnvironmentVariableTarget]::User)</code></pre>
        </p>
    </div>
    <div id="cake-conf" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs">; Use the ini-format to set a section and provide key-value-pairs.
[MySection]
MyKey=MyValue</code></pre>
        </p>
    </div>
    <div id="command-line" class="tab-pane fade">
        <p>
            <pre><code class="language-sh hljs"># provide the setting as option to the command line
# by separating section and key with an underscore.
dotnet cake build.cake --MySection_MyKey=MyValue</code></pre>
        </p>
    </div>
</div>


[Cake Frosting]: /docs/running-builds/runners/cake-frosting