Order: 20
Title: Cake runner for .NET Core
RedirectFrom: docs/running-builds/runners/cake-runner-for-dotnet-core
---

:::{.alert .alert-danger}
The Cake runner for .NET Core is deprecated and no longer updated since Cake 2.0.
It is suggested to use [Cake .NET Tool] for running Cake scripts.
:::

# Requirements

The [Cake.CoreCLR](https://www.nuget.org/packages/Cake.CoreCLR) NuGet package was a runner compiled for .NET Core 2.0.

# Usage

```powershell
dotnet Cake.dll [script] [switches]
```

^"../../../../Shared/switches-1-0.txt"

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
