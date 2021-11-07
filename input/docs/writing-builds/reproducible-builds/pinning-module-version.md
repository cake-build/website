Order: 40
Description: How to make builds reproducible when using modules
---

# Cake .NET Tool

When using [Cake .NET Tool], modules referenced using the `#module` preprocessor directive can be pinned like this:

```
#module nuget:?package=Cake.BuildSystems.Module&version=3.0.0
```

# Cake Frosting

When using [Cake Frosting], modules consumed from NuGet packages can be pinned like any other NuGet package.

```
<PackageReference Include="Cake.BuildSystems.Module" Version="3.0.0" />
```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting
