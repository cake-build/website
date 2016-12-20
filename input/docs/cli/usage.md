Order: 10
Title: Usage Information
---

# Usage

    Cake.exe [script] [-switches]

# Switches

<table class="table table-bordered table-striped">
    <tbody>
        <tr>
          <th><b>Switch</b></th>
          <th><b>Description</b></th>
        </tr>
        <tr>
            <td class="col-md-6">-verbosity=value</a></td>
            <td class="col-md-6">Specifies the amount of information to be displayed (quiet, minimal, normal, verbose, diagnostic).</td>
        </tr>
        <tr>
            <td class="col-md-6">-showdescription</a></td>
            <td class="col-md-6">Shows the description for tasks.</td>
        </tr>
        <tr>
            <td class="col-md-6">-dryrun</a></td>
            <td class="col-md-6">Performs a dry run.</td>
        </tr>
        <tr>
            <td class="col-md-6">-version</a></td>
            <td class="col-md-6">Displays version information.</td>
        </tr>
        <tr>
            <td class="col-md-6">-help</a></td>
            <td class="col-md-6">Displays usage information.</td>
        </tr>
        <tr>
            <td class="col-md-6">-experimental</a></td>
            <td class="col-md-6">Uses lastest build of the Roslyn script engine.</td>
        </tr>
    </tbody>
</table>

# Custom switches

All switches not recognized by Cake will be added to an argument list that is passed to the build script. You can access arguments with the [Argument alias methods](/dsl/arguments).

## Example

Arguments passed to Cake like this:

```bash
Cake.exe -showstuff=true
```

Can be accessed from the script with the `Argument` alias:

```csharp
Argument<bool>("showstuff", false);
```

<div class="attention attention-note">
    <h4>Note</h4>
    <p>
        The conversion uses <a href="https://msdn.microsoft.com/en-us/library/system.componentmodel.typeconverter">type converters</a> under the hood to convert the string value to the desired type.
    </p>
</div>

# Examples

To run Cake with default arguments:

```bash
Cake.exe
```

To run a specific script with a specific verbosity:

```bash
Cake.exe script.cake -verbosity=diagnostic
```

To dry run a specific script:

```bash
Cake.exe script.cake -dryrun
```
