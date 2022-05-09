Order: 30
Excerpt: How to make builds reproducible when using external tools
---

# Cake .NET Tool

When using [Cake .NET Tool], tools referenced using the `#tool` preprocessor directive can be pinned like this:

```
#tool nuget:?package=Tool.Foo&version=1.2.3
```

# Cake Frosting

When using [Cake Frosting], tools consumed from NuGet packages can be pinned like any other NuGet package.

```
<PackageReference Include="Tool.Foo" Version="1.2.3" />
```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting