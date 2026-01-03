Order: 20
Title: Property directive
Description: Directive to set MSBuild property values
---

The property directive is used to set MSBuild property values. This is equivalent to specifying properties in a `.csproj` file.

# Usage

The directive uses the format `PropertyName=PropertyValue`.

## Set MSBuild property

```csharp
#:property LangVersion=preview
```

This is equivalent to the following `.csproj`:

```xml
<PropertyGroup>
  <LangVersion>preview</LangVersion>
</PropertyGroup>
```

## Multiple properties

You can specify multiple properties by using multiple directives:

```csharp
#:property LangVersion=preview
#:property TargetFramework=net10.0
#:property PublishAot=false
```

# See also

- [File-based apps - .NET | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/sdk/file-based-apps)
