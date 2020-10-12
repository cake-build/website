Order: 40
Description: How to make builds reproducible when using modules
---

If modules are referenced through a `packages.config` file in the modules folder, they can be pinned in the same way as the Cake version (when using the Cake Runner for .Net Framework or .NET Core).

If modules are referenced using the `#module` preprocessor directive they can be pinned like this:

```
#module nuget:?package=Cake.DotNetTool.Module&version=0.1.0
```

:::{.alert .alert-info}
Using this technique will require calling Cake once using the `--bootstrap` option, so that the modules are first downloaded before executing Cake.
This is due to the fact that modules assemblies need to exist prior to Cake executing, otherwise, they can't be included in the current execution.
:::
