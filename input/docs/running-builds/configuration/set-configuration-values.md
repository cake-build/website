Order: 10
Description: Ways to set configuration values
RedirectFrom:
  - docs/fundamentals/configuration
---

:::{.alert .alert-warning}
Configuration is currently not supported for [Cake Frosting].
:::

[Cake .NET Tool], [Cake runner for .NET Framework] and [Cake runner for .NET Core] support the concept of external configuration, to allow the internals of how Cake operates to be controlled, based on a specified priority, from either:

* Environment variables
* A configuration file
* Arguments passed into Cake

:::{.alert .alert-info}
These configuration options are prioritised in the order shown above.
This means the configuration file overrides any environment variables, and arguments passed directly into Cake override both environment variables and configuration file settings.
:::

As an example of where this overridable configuration can be applied, let's look at how Cake determines where to download NuGet packages from. By default, it does this by downloading the NuGet packages user configured sources.  However, it may be necessary (for example, when running in an offline/local environment) to download these NuGet packages from an alternative source.  This is where the Cake Configuration comes into play.

:::{.alert .alert-info}
Refer to the [default configuration values](default-configuration-values) for a list of all available configuration options.
:::

# Environment Variable

By creating an Environment variable with the name of `CAKE_NUGET_SOURCE` and setting the value to the URL that is required, Cake will use this alternative download source, rather than the defaults.

# Configuration File

Alternatively, you can create a `cake.config` file with the following content:

```sh
; The configuration file for Cake.

[Nuget]
Source=https://mycustomurl
```

:::{.alert .alert-info}
The configuration file should be located in the same directory as your Cake script.
:::

Specifying a configuration value within a configuration file will override the same configuration value stored within an equivalent environment variable.

## Environment variable substitution
The config file also supports environment variable substitution for values(the right hand side of the equal sign).  Sections and keys cannot be substituted 
for environment variables.  Example below:

```sh
Source=https://%NUGET_REPOSITORY%/api/v2
```
If `NUGET_REPOSITORY` is set as an environment variable, the value of it will replace `%NUGET_REPOSITORY%` in example above when cake reads the config file.

:::{.alert .alert-info}
Environment variable substitution only work for values, and must use windows annotation syntax with a % sign before and after environment variables, regardless of 
operating system.
:::



# Command Line

Finally, you can specify an input parameter directly to Cake, in the following format:

```powershell
--nuget_source=http://mycustomurl
```

Passing a configuration value directly to Cake will override the same configuration value stored within an environment variable and also any stored in a local configuration file.

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake runner for .NET Framework]: /docs/running-builds/runners/cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: /docs/running-builds/runners/cake-runner-for-dotnet-core
[Cake Frosting]: /docs/running-builds/runners/cake-frosting