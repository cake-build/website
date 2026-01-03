Order: 30
Title: Package directive
Description: Directive to fetch assemblies from NuGet
---

The package directive is used to fetch assemblies from NuGet packages. This is equivalent to adding a `PackageReference` in a `.csproj` file.

# Usage

The directive has one parameter which is the package name, optionally followed by a version.

## Reference NuGet package

```csharp
#:package Newtonsoft.Json@13.0.4
```

This is equivalent to the following `.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="Newtonsoft.Json" Version="13.0.4" />
</ItemGroup>
```

## Reference Cake packages

You can reference Cake addins and modules:

```csharp
#:package Cake.Slack@5.0.0
#:package Cake.BuildSystems.Module@8.0.0
```

These are equivalent to the following `.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="Cake.Slack" Version="5.0.0" />
  <PackageReference Include="Cake.BuildSystems.Module" Version="8.0.0" />
</ItemGroup>
```

# See also

- [File-based apps - .NET | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/sdk/file-based-apps)
