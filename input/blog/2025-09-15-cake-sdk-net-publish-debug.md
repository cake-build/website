---
title: "Cake.Sdk 5.0.25257.82-beta released"
category: Announcement
author: devlead
---

We're excited to announce the release of **Cake.Sdk 5.0.25257.82-beta**, a minor preview release that brings improvements to performance, the debugging experience, and **native .NET CLI publish support**.

## .NET Publish Support

The biggest improvement in this release is that `dotnet publish` now works with Cake.Sdk. This means you can now create self-contained precompiled binaries and containers, which can provide substantial performance gains when the same code is executed multiple times across different stages.

<!--excerpt-->

### Creating Self-Contained Binaries

You can now publish your Cake scripts as self-contained executables:

```bash
dotnet publish cake.cs --output cake.sdk
```

This will result in a self-contained `cake.sdk` binary in the output folder, eliminating the need for the .NET runtime to be installed on the target machine.

### Container Support

Building containers is now straightforward with Cake.Sdk:

```bash
dotnet publish cake.cs \
    --output cake.sdk \
    --target:PublishContainer \
    -p ContainerBaseImage='mcr.microsoft.com/dotnet/runtime-deps:10.0-noble-chiseled' \
    -p ContainerArchiveOutputPath=Cake.Sdk.tar.gz 
```

Once built, you can import and run the container:

```bash
# Import the container
podman load -i Cake.Sdk.tar.gz

# Run the container
podman run -it --rm localhost/cake-sdk:latest
```

You can also publish directly to a container image registry. This enables powerful scenarios, for example, you can have your build pipeline pre-compiled and cached on build agents for even faster execution.

### Feedback Welcome

This is still a preview release, and we'd love your feedback! You can:

- Report issues on [GitHub](https://github.com/cake-build/generator/issues)
- Join the discussion on our [discussion board](https://github.com/orgs/cake-build/discussions)
- Contribute to the [source code](https://github.com/cake-build/generator/)

### Package References

- [Cake.Sdk](https://www.nuget.org/packages/Cake.Sdk) - The main SDK package
- [Cake.Generator](https://www.nuget.org/packages/Cake.Generator) - Source generator for Cake aliases
- [Cake.Template](https://www.nuget.org/packages/Cake.Template) - Templates for creating Cake projects

We're excited to see what you build with the enhanced Cake.Sdk capabilities! 🍰
