Order: 100
---

Cake supports the concept of external configuration, to allow the internals of how Cake operates to be controlled, based on a specified priority, from either:

* Environment variables
* A Configuration File
* Arguments passed into Cake.exe

**NOTE:** These configuration options are prioritised in the order shown above.  i.e. The configuration file overrides any Environment variables, and arguments passed directly into cake.exe override both Environment variables and configuration file settings.

## Where can this configuration be applied?

As an example of where this overridable configuration can be applied, let's look at how Cake determines where to download NuGet packages from. By default, it does this by downloading the nuget packages user configured sources.  However, it may be necessary (for example, when running in an offline/local environment) to download these nuget packages from an alternative source.  This is where the Cake Configuration comes into play.

By creating an Environment variable with the name of `CAKE_NUGET_SOURCE` and setting the value to the URL that is required, Cake will use this alternative download source, rather than the defaults.

Alternatively, you can create a `cake.config` file with the following content:

```sh
; The configuration file for Cake.

[Nuget]
Source=https://mycustomurl
```
<br/>
Specifying a configuration value within a configuration file will override the same configuration value stored within an equivalent Environment variable.

**NOTE:** This configuration file should be located in the same directory as your `build.cake` file.

Finally, you can specify an input parameter directly to the Cake.exe, in the following format:

```sh
cake.exe --nuget_source=http://mycustomurl
```
<br/>
Passing a configuration value directly to the Cake.exe will override the same configuration value stored within an Environment variable and also any stored in a local configuration file.

Refer to the [default configuration values](/docs/fundamentals/default-configuration-values) for a list of all the available configuration options.
