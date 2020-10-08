Order: 40
Title: Arguments And Environment Variables
RedirectFrom: docs/fundamentals/args-and-environment-vars
---

How you can pass settings to build.cake.

# Arguments

Call the [Argument alias](/dsl/arguments/) in your Cake file to read arguments from the command line.

## Example

Build script:

```csharp
Argument<bool>("showstuff", false);
```

Execution:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool2">.NET Core Tool</a></li>
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
            dotnet Cake.Frosting.dll --showstuff=true
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

# Environment Variables


Call the EnvironmentVariable() alias in your cake file to get the environment variable.


```csharp
var mySetting = EnvironmentVariable("my_setting") ?? "default value";
```

For example:

```csharp
var mySetting = EnvironmentVariable("my_setting") ?? "default value";

Task("Default")
    .Does(() =>
{
    Information("My setting is: " + mySetting);
});

RunTarget("Default");
```

PowerShell:
```powershell
$env:my_setting = "from PowerShell"
.\build.ps1
```

The output is
```
My setting is: from PowerShell
```

Batch File:
```batchfile
SET my_setting=from a batch file
powershell -File build.ps1
```

The output is
```
My setting is: from a batch file
```