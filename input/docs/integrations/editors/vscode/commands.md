Order: 40
Title: Visual Studio code commands
Description: Commands available to work with Cake
---

# Configuring commands

Commands are provided by the [Cake extension for Visual Studio Code].

# Using commands

The [Cake extension for Visual Studio Code] provides the following commands in the Command Palette for working with Cake files:

| Command                            | Description |
|------------------------------------|-------------|
| Cake: Install a bootstrapper       | Installs a Cake bootstrapper for Windows, macOS or Linux in the root folder. |
| Cake: Install to workspace         | Will run through all of the available commands at once, to save having to run them one by one. |
| Cake: Install debug dependencies   | Installs either the [Cake .NET tool] globally or alternatively download the [Cake runner for .NET Core] into the `tools` folder. |
| Cake: Install sample build file    | Installs a sample Cake file that contains setup and teardown actions, a sample task, and argument parsing. |
| Cake: Add addin from NuGet         | Add or update an addin from NuGet in the specified Cake file. |
| Cake: Add tool from NuGet          | Add or update a tool from NuGet in the specified Cake file. |
| Cake: Add module from NuGet        | Add or update a module from NuGet in the modules `package.config`. |
| Cake: Install a configuration file | Installs the default Cake configuration file for controlling internal components of Cake. |
| Cake: Install intellisense support | Downloads the Cake.Bakery NuGet Package into the tools folder, which in conjunction with OmniSharp provides IntelliSense support for Cake Files. See [IntelliSense in Visual Studio Code] for details. |

[Cake extension for Visual Studio Code]: https://marketplace.visualstudio.com/items/cake-build.cake-vscode
[Cake .NET tool]: /docs/running-builds/runners/dotnet-tool
[Cake runner for .NET Core]: /docs/running-builds/runners/cake-runner-for-dotnet-core
[IntelliSense in Visual Studio Code]: /docs/integrations/editors/vscode/intellisense