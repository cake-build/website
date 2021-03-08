Order: 40
Title: Tool directive
Description: Directive to install external command-line tools
---

The tool directive installs external command-line tools.

# Available providers

Tools can be installed using different providers.
The following providers are available by default.
Click on the provider for detailed documentation.

| Provider | Description                                                       | Available since |
|----------|-------------------------------------------------------------------|-----------------|
| [dotnet] | Package installer for installing tools using the dotnet CLI       | 1.1.0           |
| [nuget]  | Package installer for installing tools packaged as NuGet packages | 0.13.0          |

:::{.alert .alert-info}
More providers are available through [Modules](/extensions/).
:::

[dotnet]: /docs/writing-builds/preprocessor-directives/tool/dotnet-provider
[nuget]: /docs/writing-builds/preprocessor-directives/tool/nuget-provider
