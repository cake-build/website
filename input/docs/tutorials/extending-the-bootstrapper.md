Order: 30
---

The Cake Bootstrapper that you can get directly from [cakebuild.net](https://cakebuild.net) is intended as a starting point for what can be done. It is the developer's discretion to extend the bootstrapper to solve for your own requirements.

# Adding an additional parameter
 For example, let's say you want to add an input parameter to the bootstrapper for mobile build configurations.  These are the changes that you could make in order to achieve this...

- Add parameter to the parameter list

### PowerShell
```powershell
Param(
    [string]$Script = "build.cake",
    [string]$Target = "Default",
    [ValidateSet("Release", "Debug")]
    [string]$Configuration = "Release",
    [ValidateSet("Debug", "Release", "AdHoc", "AppStore")]
    [string]$BuildConfiguration = "AdHoc", # Added parameter
    [ValidateSet("Quiet", "Minimal", "Normal", "Verbose", "Diagnostic")]
    [string]$Verbosity = "Verbose",
    [switch]$Experimental,
    [switch]$WhatIf,
    [switch]$Mono,
    [switch]$SkipToolPackageRestore,
    [Parameter(Position=0,Mandatory=$false,ValueFromRemainingArguments=$true)]
    [string[]]$ScriptArgs
)
```
### bash
```bash
for i in "$@"; do
    case $1 in
        -s|--script) SCRIPT="$2"; shift ;;
        -t|--target) TARGET="$2"; shift ;;
        -c|--configuration) CONFIGURATION="$2"; shift ;;
        -b|--buildconfiguration) BUILDCONFIGURATION="$2"; shift ;; # Added parameter
        -v|--verbosity) VERBOSITY="$2"; shift ;;
        -d|--dryrun) DRYRUN=true ;;
        --version) SHOW_VERSION=true ;;
    esac
    shift
done
```
- Pass the additional parameter into the cake runtime

### PowerShell
```powershell
Invoke-Expression "$CAKE_EXE `"$Script`" -target=`"$Target`" -configuration=`"$Configuration`" -verbosity=`"$Verbosity`" -buildconfiguration=`"$BuildConfiguration`" $UseMono $UseDryRun $UseExperimental $ScriptArgs"
```
### bash
```bash
mono "$CAKE_EXE" $SCRIPT -verbosity=$VERBOSITY -configuration=$CONFIGURATION -target=$TARGET -buildconfiguration=$BUILDCONFIGURATION
```

- Use the parameter in build.cake
```csharp
var buildConfiguration = Argument("buildconfiguration", "Debug");
```

- Call the Bootstrapper

### PowerShell
```powershell
.\build.ps1 -BuildConfiguration AppStore
```
### bash
```bash
./build.sh -b AppStore
```
