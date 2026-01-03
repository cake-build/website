Order: 40
Title: Project directive
Description: Directive to reference C# projects
---

The project directive is used to reference other C# project files or directories that contain a project file. This is equivalent to adding a `ProjectReference` in a `.csproj` file.

# Usage

The directive has one parameter which is the path to the project file or directory containing a project file.

## Reference project file

```csharp
#:project ../SharedLibrary/SharedLibrary.csproj
```

This is equivalent to the following `.csproj`:

```xml
<ItemGroup>
  <ProjectReference Include="../SharedLibrary/SharedLibrary.csproj" />
</ItemGroup>
```

## Reference project directory

You can also reference a directory that contains a project file:

```csharp
#:project ../SharedLibrary
```

# See also

- [File-based apps - .NET | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/sdk/file-based-apps)
