---
title: "Cake.Sdk 5.0.25225.53-beta released"
category: Announcement
author: devlead
---

A new preview version of **Cake.Sdk** has been released! Version **5.0.25225.53-beta** brings compatibility with .NET 10 Preview 7. 🚀 🍰

### What's new in this release

- **Compiled with/against .NET 10 Preview 7** - Full compatibility with the latest .NET 10 preview
- **Updated dependencies** - All underlying packages updated to their latest versions
- **Fixes for new analyzer warnings** - Resolved potential compatibility issues with Preview 7 SDK

<!--excerpt-->

### SDK Versioning in File-based Apps

One notable improvement addresses the issue mentioned in our [previous announcement](/blog/2025/07/dotnet-cake-cs). The .NET 10 Preview 7 SDK now properly supports SDK versioning in file-based applications, allowing you to specify the Cake.Sdk version directly in your `.cs` files:

```csharp
#:sdk Cake.Sdk@5.0.25225.53-beta

Information("Hello");
```

### Getting Started

To try out the latest preview, update your `global.json` file:

```json
{
  "sdk": {
    "version": "10.0.100-preview.7.25358.103"
  },
  "msbuild-sdks": {
    "Cake.Sdk": "5.0.25225.53-beta"
  }
}
```

Or install the latest [template package](https://www.nuget.org/packages/Cake.Template#readme-body-tab):

```bash
dotnet new install Cake.Template@5.0.25225.53-beta
```

Create a `global.json` file to pin versions:

```bash
dotnet new cakeglobaljson
```

Then create a new Cake file-based project:

```bash
dotnet new cakefile --name cake
```

And run it with:

```bash
dotnet cake.cs
```


### Feedback Welcome

This is still a preview release, and we'd love your feedback! You can:

- Try out the new version and report any issues on [GitHub](https://github.com/cake-build/generator/issues)
- Join the discussion on our [discussion board](https://github.com/orgs/cake-build/discussions)
- Contribute to the [source code](https://github.com/cake-build/generator/)

We're excited to see what you build with the latest Cake.Sdk! 🍰
