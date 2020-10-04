Order: 10
RedirectFrom: docs/overview/requirements
---

There are different runners available for running Cake scripts.

| Runner | Minimum required .NET version  | Supported | IntelliSense |
|-|-|-|
| [.NET Core Tool] | .NET Core 2.1 | <span class="glyphicon glyphicon-ok" style="color:green"></span> | <span class="glyphicon glyphicon-ok" style="color:orange"></span> [[1]](#1) |
| [Cake Frosting] | .NET Framework 4.6.1 or .NET Core 3.1 | <span class="glyphicon glyphicon-ok" style="color:green"></span> | <span class="glyphicon glyphicon-ok" style="color:green"></span> |
| [Cake runner for .NET Framework] | .NET Framework 4.6.1 or Mono 5.0.12 | <span class="glyphicon glyphicon-ok" style="color:green"></span> | <span class="glyphicon glyphicon-ok" style="color:orange"></span> [[1]](#1) |
| [Cake runner for .NET Core] | .NET Core 2.0 | <span class="glyphicon glyphicon-remove" style="color:red"></span> | <span class="glyphicon glyphicon-ok" style="color:orange"></span> [[1]](#1) |

<a id="1"></a>
[1]: Limited support in Visual Studio Code. See [IntelliSense in Visual Studio Code]

[.NET Core Tool]: #net-core-tool
[Cake Frosting]: #cake-frosting
[Cake runner for .NET Framework]: #cake-runner-for.net-framework
[Cake runner for .NET Core]: #cake-runner-for.net-core
[IntelliSense in Visual Studio Code]: ../editors/vscode/intellisense

# .NET Core Tool

:::{.alert .alert-info}
This is the recommended way to run Cake Scripts.
:::

The [Cake.Tool](https://www.nuget.org/packages/Cake.Tool) NuGet package, is a .NET Core global tool compiled for .NET Core 2.1 or newer.

For bootstrapping .NET Core Tool see [Bootstrapping .NET Core Tool](bootstrapping-scripts#bootstrapping-for.net-core-tool).

# Cake Frosting

[Cake.Frosting](https://github.com/cake-build/frosting) is a .NET host which allows you to write your build scripts as a console application
(`netcoreapp3.1` or `net461`).

:::{.alert .alert-info}
A console application has the advantage of full IDE support, like IntelliSense, refactoring and debugging.
:::

For bootstrapping Cake Frosting see [Bootstrapping for Cake Frosting](bootstrapping-scripts#bootstrapping-for-cake-frosting).

# Cake runner for .NET Framework

The [Cake](https://www.nuget.org/packages/Cake) NuGet package is a runner requiring [.NET Framework 4.6.1](https://www.microsoft.com/net/download/dotnet-framework/net461)
or newer on Windows and Mono `5.12.0` or newer on Mac or Linux.

:::{.alert .alert-warning}
It is suggested to use [.NET Framework 4.7.2](https://www.microsoft.com/net/download/dotnet-framework/net472) or newer to run build scripts
which are using addins targeting .NET Standard 2.0 only.
:::

This runner is mainly for backwards compatibility where scripts or addins are used which require .NET Framework.
In all other cases it is recommended to use [.NET Core Tool](#net-core-tool).

For bootstrapping Cake runner for .NET Framework see [Bootstrapping for Cake runner for .NET Framework](bootstrapping-scripts/#bootstrapping-for-cake-runner-for-net-framework).

# Cake runner for .NET Core

The [Cake.CoreCLR](https://www.nuget.org/packages/Cake.CoreCLR) NuGet package is a runner compiled for .NET Core 2.0.

:::{.alert .alert-warning}
The Cake runner for .NET Core is deprecated and it is suggested to use [.NET Core Tool](#net-core-tool) for running Cake under .NET Core.
:::
