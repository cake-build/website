Order: 30
Title: Visual Studio Code snippets
Description: Support for code snippets
RedirectFrom: docs/editors/vscode/snippets
---

# Configuring snippets

Snippets in Visual Studio Code are provided by the [Cake extension for Visual Studio Code](https://marketplace.visualstudio.com/items/cake-build.cake-vscode).

:::{.alert .alert-info}
For instructions how to install an extension in Visual Studio Code see [Extension Marketplace documentation](https://code.visualstudio.com/docs/editor/extension-gallery).
:::

## Supported runners

| Runner                           | Supported                                       | Remarks                                            |
|----------------------------------|-------------------------------------------------|----------------------------------------------------|
| [Cake .NET Tool]                 | <i class="fa fa-check" style="color:green"></i> | Available for all `.cake` files.                   |
| [Cake Frosting]                  | <i class="fa fa-times" style="color:red"></i>   |                                                    |

[Cake .NET Tool]: dotnet-tool
[Cake Frosting]: cake-frosting
[Cake runner for .NET Framework]: cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: cake-runner-for-dotnet-core

# Available snippets

* `cake-addin`
  * Provides a basic addin pre-processor directive, where the package name and version can be changed
  * _Default Value:_ `#addin "nuget:?package=Cake.Foo&version=1.2.3"`
* `cake-addin-full`
  * Provides a more complete addin pre-processor directive, where source, package name and version can be changed
  * _Default Value:_ `#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Foo&version=1.2.3"`
* `cake-argument`
  * Provides code for basic input argument parsing, where variable name, argument name and default value can be changed
  * _Default Value:_ `var target = Argument("target", "Default");`
* `cake-load`
  * Provides a basic load pre-processor directive, where the path to the .cake file can be changed
  * _Default Value:_ `#load "scripts/utilities.cake"`
* `cake-load-nuget`
  * Provides a more complex load pre-processor directive, where source, package name and version can be changed
  * _Default Value:_ `#load "nuget:https://www.nuget.org/api/v2?package=Cake.Foo&version=1.2.3"`
* `cake-reference`
  * Provides a basic reference pre-processor directive, where path to the assembly can be changed
  * _Default Value:_ `#reference "bin/myassembly.dll"`
* `cake-sample`
  * Provides a complete sample build Cake script including setup and teardown actions, a single task, and argument parsing
* `cake-tool`
  * Provides a basic tool pre-processor directive, where the package name and version can be changed
  * _Default Value:_ `#tool "nuget:?package=Cake.Foo&version=1.2.3"`
* `cake-tool-full`
  * Provides a more complete tool pre-processor directive, where source, package name and version can be changed
  * _Default Value:_ `#tool "nuget:https://www.nuget.org/api/v2?package=Cake.Foo&version=1.2.3"`
* `task`
  * Provides a basic task definition, where the name of the task can be changed
  * _Default Value:_ `Task("name");`
* `task` (With Action)
  * Provides a more complex task definition, including an `.Does` body, where the name of the task can be changed

[Cake extension for Visual Studio Code]: https://marketplace.visualstudio.com/items/cake-build.cake-vscode
[Extension Marketplace documentation]: https://code.visualstudio.com/docs/editor/extension-gallery
