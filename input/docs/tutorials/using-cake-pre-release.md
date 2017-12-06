Order: 35
---

Cake uses [MyGet](https://www.myget.org/) as a NuGet feed for testing and pre-release builds. With these pre-release builds the next version of Cake can be accessed and utilized for getting the latest features or testing addins or build scripts to know if the next release will be safe when you need to upgrade. 

# Using the pre-release version of Cake

These instructions assume you are using the nuget CLI as done in the [default bootstrapper](https://github.com/cake-build/resources/blob/develop/build.ps1). We will start by editing the following line in the bootstrapper. This will add the MyGet source.

### Unmodified Bootstrapper
```powershell
$NuGetOutput = Invoke-Expression "&`"$NUGET_EXE`" install -ExcludeVersion -OutputDirectory `"$TOOLS_DIR`""
```
Now add the following.

### MyGet source added to the Bootstrapper
```powershell
$NuGetOutput = Invoke-Expression "&`"$NUGET_EXE`" install -ExcludeVersion -OutputDirectory `"$TOOLS_DIR`" -Source https://www.myget.org/F/cake/api/v3/index.json"
```

With the following in place, modify your packages.config to pin the Cake version to one of the pre-releases.

### packages.config
```xml
<?xml version="1.0" encoding="utf-8"?>
<packages>
	<package id="Cake" version="0.24.0-alpha0028" />
</packages>
```
