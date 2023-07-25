Order: 80
Description: How to reuse build code in different projects and repositories
---

Cake supports different ways to share build code across multiple projects and repositories.

| Method                         | Sharing of classes                                    | Sharing of aliases                                    | Sharing of tasks                                      |
|--------------------------------|-------------------------------------------------------|-------------------------------------------------------|-------------------------------------------------------|
| [Recipe NuGet packages]        | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-xmark" style="color:red"></i>   | <i class="fa-solid fa-check" style="color:green"></i> |
| [Addins]                       | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-xmark" style="color:red"></i>   |
| [Assemblies in NuGet packages] | <i class="fa-solid fa-check" style="color:green"></i> | <i class="fa-solid fa-xmark" style="color:red"></i>   | <i class="fa-solid fa-xmark" style="color:red"></i>   |

[Recipe NuGet packages]: #recipe-nuget-packages
[Addins]: #addins
[Assemblies in NuGet packages]: #assemblies-in-nuget-packages

# Recipe NuGet packages

Cake build scripts can be published as NuGet packages, so called Recipes.
These packages can contain shared tasks and can be consumed by other build scripts.

## Writing Recipe NuGet packages

To create a Recipe NuGet package add the `.cake` files to the `Content` folder in the NuGet package.

## Consuming Recipe NuGet packages

:::{.alert .alert-warning}
Make sure to pick a Recipe NuGet package compatible with your runner.
:::

### Cake .NET Tool

When using [Cake .NET Tool], the [load directive] can be used with the `nuget` scheme
to download the Recipe NuGet packages and load all `.cake` files in the `content` folder.
The following example loads version 1.0.0 of the `MyRecipePackage` NuGet package:

```csharp
#load nuget:?package=MyRecipePackage&version=1.0.0
```
### Cake Frosting

When using [Cake Frosting], Recipe NuGet package can be referenced like any other NuGet package:

```csharp
<PackageReference Include="MyRecipePackage" Version="1.0.0" />
```

# Addins

Code can be shared as a [Cake addin](/docs/extending/addins/) which provides [aliases](/docs/fundamentals/aliases)
that can be used in Cake builds.

## Writing addins

See [Creating addins](/docs/extending/addins/creating-addins) for instructions how to create your custom addin.

## Consuming addins

Addins can be loaded using the [addin directive].

# Assemblies in NuGet packages

Classes can be shared in .NET assemblies deployed in NuGet packages.

## Consuming assemblies from NuGet packages

NuGet packages can be loaded using the [addin directive].

[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting
[addin directive]: /docs/writing-builds/preprocessor-directives#add-in-directive
[load directive]: /docs/writing-builds/preprocessor-directives#load-directive
