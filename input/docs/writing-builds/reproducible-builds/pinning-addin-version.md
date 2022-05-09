Order: 20
Excerpt: How to make builds reproducible when using addins
---

# Cake .NET Tool

When using [Cake .NET Tool], addins referenced using the `#addin` preprocessor directive can be pinned like this:

```
#addin nuget:?package=Cake.Foo&version=1.2.3
```

# Cake Frosting

When using [Cake Frosting], addins can be pinned like any other NuGet package.

```
<PackageReference Include="Cake.Foo" Version="1.2.3" />
```

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting