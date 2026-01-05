Order: 10
Title: SDK directive
Description: Directive to reference .NET SDK from NuGet
---

The SDK directive is used to reference a .NET SDK from NuGet. This is equivalent to specifying the SDK in a `.csproj` file.

# Usage

The directive has one parameter which is the SDK name, optionally followed by a version.

## Reference SDK from NuGet

```csharp
#:sdk Cake.Sdk
```

This is equivalent to the following `.csproj`:

```xml
<Project Sdk="Cake.Sdk" />
```

## Reference SDK from NuGet with version

You can specify a version in two ways:

### Versioned in global.json

```csharp
#:sdk Cake.Sdk@6.0.0
```

This is equivalent to the following `.csproj`:

```xml
<Project Sdk="Cake.Sdk/6.0.0" />
```

### Versioned in file

The version can also be specified directly in the directive:

```csharp
#:sdk Cake.Sdk@6.0.0
```

This is equivalent to the following `.csproj`:

```xml
<Project Sdk="Cake.Sdk/6.0.0" />
```

# See also

- [File-based apps - .NET | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/sdk/file-based-apps)
