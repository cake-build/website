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

Cake comes in two different flavors, which require different runners:

| Flavor | Runner |
|-|-|
| Cake Scripting | [Cake .NET Tool] |
| Standard .NET console application | [Cake Frosting] |

A console application has the advantage of full IDE support, like IntelliSense, refactoring and debugging, but requires a full .NET project.
A scripting approach can be an easier solution for simple build scenarios, as it allows to use a single file.

# Feature overview

| Runner | Minimum required .NET version  | Supported | IntelliSense |
|-|-|-|-|
| [Cake .NET Tool] | .NET 6 | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:orange"></i> [[1]](#1) |
| [Cake Frosting] | .NET 6 | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> |

<a id="1"></a>
[1]: Limited support in Visual Studio Code. See [IntelliSense in Visual Studio Code]

# Supported operating systems

The following table shows the supported operating systems for each runner.

| Runner                           | Windows | macOS | Linux |
|----------------------------------|---------|-------|-------|
| [Cake .NET Tool]                 | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> |
| [Cake Frosting]                  | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> |

# Supported platforms

The following table shows the supported platforms for each runner.

:::{.alert .alert-info}
Note that the platform under which a build is running doesn't limit its build capabilities.
It's absolutely possible for example to build a .NET Framework application with Cake running on .NET 6.
:::

| Runner                           | .NET 8 | .NET 7 | .NET 6 |
|----------------------------------|--------|--------|--------|
| [Cake .NET Tool]                 | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> |
| [Cake Frosting]                  | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> |

# Supported Build Systems

The following table shows build systems for which Cake provides specific integrations.

:::{.alert .alert-info}
Cake can run on any build system, even if not included in this list.
For the listed build systems Cake provides out of the box integrations.
See [Build Systems](/docs/integrations/build-systems/) for details.
:::

| Runner                           | AppVeyor | Azure Pipelines | Bamboo | Bitbucket Pipelines | Bitrise | Continua CI | GitHub Actions | GitLab CI | GoCD | Jenkins | MyGet | TeamCity | TravisCI |
|----------------------------------| - | - | - | - | - | - | - | - | - | - | - | - | - |
| [Cake .NET Tool]                 | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> |
| [Cake Frosting]                  | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> |

[Cake .NET Tool]: dotnet-tool
[Cake Frosting]: cake-frosting
[Cake runner for .NET Framework]: cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: cake-runner-for-dotnet-core
[IntelliSense in Visual Studio Code]: /docs/integrations/editors/vscode/intellisense
