Order: 10
Title: .NET Core Tool
---

:::{.alert .alert-success}
This is the recommended way to run Cake Scripts.
:::

# Requirements

The [Cake.Tool](https://www.nuget.org/packages/Cake.Tool) NuGet package, is a .NET Core global tool compiled for .NET Core 2.1 or newer.

# Bootstrapping for .NET Core Tool

:::{.alert .alert-info}
The following instructions require .NET Core 3.0 or newer.
See [How to manage .NET Core tools](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) for details and other options.
:::

## Setup

There's a one-time setup required for configuring a repository to use Cake .NET Core tool.

:::{.alert .alert-info}
If you have .NET Core Tool already available in your environment you can skip the steps in this chapter.
:::

Make sure to have a tool manifest available in your repository or create one using the following command:

```shell
dotnet new tool-manifest
```

Install Cake as a local tool using the `dotnet tool` command:

```shell
dotnet tool install Cake.Tool --version x.y.z
```

## Running build script

Make sure tools are restored:

```shell
dotnet tool restore
```

Once installed, you can launch Cake using the .NET CLI:

```shell
dotnet cake
```

:::{.alert .alert-info}
By convention this will execute the build script named `build.cake`.
You can override this behavior by additionally passing the name of the build script.
:::
