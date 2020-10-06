Order: 40
Title: Arguments And Environment Variables
RedirectFrom: docs/fundamentals/args-and-environment-vars
---

How you can pass settings to build.cake.

# Arguments

Call the Argument() alias in your cake file to get the argument from the command line.

```csharp
var mySetting = Argument("my_setting", "default value");
```

For example:
```csharp
var mySetting = Argument("my_setting", "default value");

Task("Default")
    .Does(() =>
{
    Information("My setting is: " + mySetting);
});

RunTarget("Default");
```

PowerShell:
```powershell
.\build.ps1 -ScriptArgs '-my_setting="from PowerShell"'
```

The output is
```
My setting is: from PowerShell
```


Batch File:
```batchfile
powershell -File build.ps1 -ScriptArgs '-my_setting="from a batch file"'
```

The output is
```
My setting is: from a batch file
```

Bash (Linux/macOS):
```
./build.sh --my_setting="from Bash"
```

The output is
```
My setting is: from Bash
```

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