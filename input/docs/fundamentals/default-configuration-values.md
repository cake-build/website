Order: 110
---

The following shows all of the configuration options currently available within Cake, as well as their default values.

Refer to [Configuration](/docs/fundamentals/configuration) for instructions on using a config file.

# NuGet Download Url

This allows the control of where Cake downloads NuGet packages from when using the addin and tool preprocessor directives.  This can be useful when it is necessary to work in an offline mode, where direct access to nuget.org is not available.

## Default Value

```sh
https://packages.nuget.org/api/v2
```

## Environment Variable Name

```sh
CAKE_NUGET_SOURCE
```

## Config File Contents

```sh
[Nuget]
Source=http://myfeed/nuget/
```

## Direct Argument

```sh
cake.exe --nuget_source=http://myfeed/nuget/
```

<hr/>

# Tools Path

This allows the configuration of the tools folder which is used by Cake when restoring tools.

## Default Value

```sh
./tools
```

## Environment Variable Name

```sh
CAKE_PATHS_TOOLS
```

## Config File Contents

```sh
[Paths]
Tools=./tools
```

## Direct Argument

```sh
cake.exe --paths_tools=./tools
```

<hr/>

# Addins Path

This allows the configuration of the Addins folder which is used by Cake when restoring Addins.

## Default Value

```sh
./tools/Addins
```

## Environment Variable Name

```sh
CAKE_PATHS_ADDINS
```

## Config File Contents

```sh
[Paths]
Addins=./tools/Addins
```

## Direct Argument

```sh
cake.exe --paths_addins=./tools/Addins
```

<hr/>

# Modules Path

This allows the configuration of the Modules folder which is used by Cake when loading custom Modules.

## Default Value

```sh
./tools/Modules
```

## Environment Variable Name

```sh
CAKE_PATHS_MODULES
```

## Config File Contents

```sh
[Paths]
Modules=./tools/Modules
```

## Direct Argument

```sh
cake.exe --paths_modules=./tools/Modules
```


<hr/>

# Skip Verification

If any breaking changes are introduced to Cake, we'll set the minimum supported version of the `Cake.Core` assembly, any addin/assembly that references an older version of `Cake.Core` will generate an exception and ultimately abort the execution of the script. This is to avoid runtime errors during execution of the script.

But if you really need to use an addin/assembly and have verified that the breaking change introduced doesn't concern the addin/assembly directly, i.e. it doesn't use a removed property or changed interface, for this scenario we'll allow you to temporarily opt-out of the assembly verification until addin/assembly author has released a version of the assembly targeting the minimum required version of `Cake.Core`.

A word of **caution**, this setting is global for all addins/assemblies your script references and should therefore be considered a temporary quick fix.

## Default Value

```sh
false
```
## Valid Values

```sh
true
or
false
```

## Environment Variable Name

```sh
CAKE_SETTINGS_SKIPVERIFICATION
```

## Config File Contents

```sh
[Settings]
SkipVerification=true
```

## Direct Argument

```sh
cake.exe --settings_skipverification=true
```

<hr/>

# In-Process NuGet installation

From version `0.25.0` of Cake, the default is now to use the In-Process NuGet Client.  If you require to use NuGet.exe instead, you can opt-out by setting this value to false.

## Default Value

```sh
true
```

## Valid Values

```sh
true
or
false
```

## Environment Variable Name

```sh
CAKE_NUGET_USEINPROCESSCLIENT
```

## Config File Contents

```sh
[NuGet]
UseInProcessClient=false
```

## Direct Argument

```sh
cake.exe --nuget_useinprocessclient=false
```


<hr/>

# Addin NuGet dependencies

When using In-Process NuGet installation, available since Cake `0.22.0`, you can also opt in to automatically installing and referencing NuGet package dependencies.

## Default Value

```sh
false
```

## Valid Values

```sh
true
or
false
```

## Environment Variable Name

```sh
CAKE_NUGET_LOADDEPENDENCIES
```

## Config File Contents

```sh
[NuGet]
LoadDependencies=true
```

## Direct Argument

```sh
cake.exe --nuget_loaddependencies=true
```


<hr/>

# Roslyn NuGet Download Url

**_This only applies to Cake version `0.21.1` and older, Roslyn ships with Cake from version `0.22.0`._**

This allows the control of where Cake downloads the required Roslyn NuGet packages.  This can be useful when it is necessary to work in an offline mode, where direct access to nuget.org is not available.

## Default Value

```sh
https://packages.nuget.org/api/v2
```

## Environment Variable Name

```sh
CAKE_ROSLYN_NUGETSOURCE
```

## Config File Contents

```sh
[Roslyn]
NuGetSource=https://mycustomurl
```

## Direct Argument

```sh
cake.exe --roslyn_nugetsource=http://mycustomurl
```

<hr/>
