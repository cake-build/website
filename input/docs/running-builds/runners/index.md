Order: 10
Description: Information about different build runners
RedirectFrom:
  - docs/tutorials/extending-the-bootstrapper
  - docs/tutorials/powershell-security
  - docs/overview/requirements
  - docs/getting-started/running-cake-scripts
  - docs/getting-started/bootstrapping-scripts
  - docs/running-builds/using-cake-pre-release
---

There are different runners available for running Cake scripts.

## Feature overview

| Runner | Minimum required .NET version  | Supported | IntelliSense |
|-|-|-|-|
| [.NET Tool] | .NET Core 2.1 | <span class="glyphicon glyphicon-ok" style="color:green"></span> | <span class="glyphicon glyphicon-ok" style="color:orange"></span> [[1]](#1) |
| [Cake Frosting] | .NET Framework 4.6.1 or .NET Core 3.1 | <span class="glyphicon glyphicon-ok" style="color:green"></span> | <span class="glyphicon glyphicon-ok" style="color:green"></span> |
| [Cake runner for .NET Framework] | .NET Framework 4.6.1 or Mono 5.0.12 | <span class="glyphicon glyphicon-ok" style="color:green"></span> | <span class="glyphicon glyphicon-ok" style="color:orange"></span> [[1]](#1) |
| [Cake runner for .NET Core] | .NET Core 2.0 | <span class="glyphicon glyphicon-remove" style="color:red"></span> | <span class="glyphicon glyphicon-ok" style="color:orange"></span> [[1]](#1) |

<a id="1"></a>
[1]: Limited support in Visual Studio Code. See [IntelliSense in Visual Studio Code]

## Supported operating systems

The following table shows the supported operating systems for each runner.

| Runner                           | Windows | macOS | Linux |
|----------------------------------|---------|-------|-------|
| [.NET Tool]                      | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [Cake Frosting]                  | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [Cake runner for .NET Framework] | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:orange"></i> [[2]](#footnote2) | <i class="fa fa-check" style="color:orange"></i> [[2]](#footnote2) |
| [Cake runner for .NET Core]      | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |

<a id="footnote2"></a>
[2]: Requiring Mono 5.0.12 or newer

## Supported platforms

The following table shows the supported platforms for each runner.

:::{.alert .alert-info}
Note that the platform under which a build is running doesn't limit its build capabilities.
It's absolutely possible to build a .NET Core application with Cake running on .NET Framework or vice-versa.
:::

| Runner                           | .NET 5 | .NET Core 3.1 | .NET Core 3.0 | .NET Core 2.1 | .NET Core 2.0 | .NET Framework 4.6.1 or newer | Mono 5.0.12 or newer|
|----------------------------------|--------|---------------|---------------|---------------|---------------|-------------------------------|---------------------|
| [.NET Tool]                      | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-times" style="color:red"></i> |
| [Cake Frosting]                  | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [Cake runner for .NET Framework] | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [Cake runner for .NET Core]      | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-times" style="color:red"></i> | <i class="fa fa-times" style="color:red"></i> |

## Supported Build Systems

The following table shows build systems for which Cake provides specific integrations.

:::{.alert .alert-info}
Cake can run on any build system, even if not included in this list.
For the listed build systems Cake provides 
:::

| Runner                           | AppVeyor | Azure Pipelines | Bamboo | Bitbucket Pipelines | Bitrise | Continua CI | GitHub Actions | GitLab CI | GoCD | Jenkins | MyGet | TeamCity | TravisCI |
|----------------------------------| - | - | - | - | - | - | - | - | - | - | - | - | - |
| [.NET Tool]                      | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [Cake Frosting]                  | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [Cake runner for .NET Framework] | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [Cake runner for .NET Core]      | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |

[.NET Tool]: dotnet-tool
[Cake Frosting]: cake-frosting
[Cake runner for .NET Framework]: cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: cake-runner-for-dotnet-core
[IntelliSense in Visual Studio Code]: ../../integrations/editors/vscode/intellisense
