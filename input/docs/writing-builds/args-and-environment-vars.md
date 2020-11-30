Order: 40
Title: Arguments And Environment Variables
Description: How to work with arguments and environment variables
RedirectFrom: docs/fundamentals/args-and-environment-vars
---

How you can pass settings to build.cake.

# Passing And Reading Arguments

Call the [Argument alias](/dsl/arguments/) in your Cake file to read arguments from the command line.

## Example

Build script:

```csharp
Argument<bool>("showstuff", false);
```

Execution:

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
                dotnet cake --showstuff=true
            </code>
        </p>
    </div>
    <div id="frosting2" class="tab-pane fade">
        <p>
            <code class="language-powershell hljs">
                dotnet Cake.Frosting.dll --showstuff=true
            </code>
        </p>
    </div>
    <div id="netfx2" class="tab-pane fade">
        <p>
            <code class="language-powershell hljs">
                Cake.exe --showstuff=true
            </code>
        </p>
    </div>
    <div id="core2" class="tab-pane fade">
        <p>
            <code class="language-powershell hljs">
                Cake.exe --showstuff=true
            </code>
        </p>
    </div>
</div>

:::{.alert .alert-info}
The conversion uses [type converters](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.typeconverter) under the hood to convert the string value to the desired type.
:::

# Reading Environment Variables


Call the [Environment variables alias](/dsl/environment/#Environment-Variables) alias in your Cake file to get the environment variable.

The following example will read the value of the `BUILD_NUMBER` environment variable or return `42` if the environment variable is not defined:

```csharp
Information(EnvironmentVariable<int>("BUILD_NUMBER", 42));
```
