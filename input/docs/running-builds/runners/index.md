Order: 10
RedirectFrom:
  - docs/tutorials/extending-the-bootstrapper
  - docs/tutorials/powershell-security
  - docs/overview/requirements
  - docs/getting-started/running-cake-scripts
  - docs/getting-started/bootstrapping-scripts
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

[.NET Core Tool]: dotnet-core-tool
[Cake Frosting]: cake-frosting
[Cake runner for .NET Framework]: cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: cake-runner-for-dotnet-core
[IntelliSense in Visual Studio Code]: ../integrations/editors/vscode/intellisense
