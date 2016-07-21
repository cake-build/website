---
content-type: markdown
---

The following shows all of the configuration options currently available within Cake, as well as their default values.

### Roslyn NuGet Download Url

This allows the control of where Cake downloads the required Roslyn NuGet packages.  This can be useful when it is necessary to work in an offline mode, where direct access to nuget.org is not available.

#### Default Value

```sh
https://packages.nuget.org/api/v2
```

#### Environment Variable Name

```sh
CAKE_ROSLYN_NUGETSOURCE
```

#### ini File Contents

```sh
[Roslyn]
NuGetSource=https://mycustomurl
```

#### Direct Argument

```sh
cake.exe --roslyn_nugetsource=http://mycustomurl
```

<hr/>

### Tools Path

This allows the configuration of the tools folder which is used by Cake when restoring tools.

#### Default Value

```sh
./tools
```

#### Environment Variable Name

```sh
CAKE_PATHS_TOOLS
```

#### ini File Contents

```sh
[Paths]
Tools=./tools
```

#### Direct Argument

```sh
cake.exe --paths_tools=./tools
```

<hr/>

### Addins Path

This allows the configuration of the Addins folder which is used by Cake when restoring Addins.

#### Default Value

```sh
./tools/Addins
```

#### Environment Variable Name

```sh
CAKE_PATHS_ADDINS
```

#### ini File Contents

```sh
[Paths]
Addins=./tools/Addins
```

#### Direct Argument

```sh
cake.exe --paths_addins=./tools/Addins
```

<hr/>

### Modules Path

This allows the configuration of the Modules folder which is used by Cake when loading custom Modules.

#### Default Value

```sh
./tools/Modules
```

#### Environment Variable Name

```sh
CAKE_PATHS_MODULES
```

#### ini File Contents

```sh
[Paths]
Modules=./tools/Modules
```

#### Direct Argument

```sh
cake.exe --paths_modules=./tools/Modules
```
