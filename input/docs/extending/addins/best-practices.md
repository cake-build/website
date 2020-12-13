Order: 30
Description: Best practices for writing addins
---

This page gives some best practices for writing Cake addins.

# Naming convention for addins

Addins should be named `Cake.xxx`, where xxx is a meaningful and unique name that describes what the addin does.
We strongly encourage to be consistent and use the same name for the GitHub repo, the C# solution name,
the assembly generated from the project and the NuGet package.
The `Cake.` prefix in the naming convention is particularly important for the NuGet package and the assembly in the package,
because it's used by the automated [AddinDiscoverer](https://github.com/cake-contrib/Cake.AddinDiscoverer) to identify addins for Cake.

For example, `Cake.Email` is the name that clearly identifies the addin for Cake that allows emails to be sent from the build script.
This name is used in the [GitHub repo](https://github.com/cake-contrib/Cake.Email),
it's the name of the [solution file](https://github.com/cake-contrib/Cake.Email/blob/develop/Source/Cake.Email.sln),
it's the name of the [project file](https://github.com/cake-contrib/Cake.Email/blob/develop/Source/Cake.Email/Cake.Email.csproj),
the name of the [generated assembly](https://github.com/cake-contrib/Cake.Email/blob/develop/Source/Cake.Email/Cake.Email.csproj#L10)
and finally, it's also the name of the [NuGet package](https://www.nuget.org/packages/Cake.Email/).

# Cake version to build addins against

To have the best support for different versions of Cake, the addin should currently be built against Cake version 0.33.0,
or, if a specific newer functionality is required, the lowest version providing the specific functionality.
Please note  that addins built against newer versions of Cake might not be compatible with previous versions of Cake and vice-versa,
addins built against older versions might not be compatible with future versions of Cake (this is especially true when a
future version of Cake introduces breaking changes).

It is incumbent on addin authors to upgrade their references and publish new version of their NuGet packages when a new
version of Cake with breaking changes becomes available.

Cake versions which are API compatible with a specific addin will be shown when the addin is added to the Cake website.

# Target .NET version for addins

As .NET Framework < 4.7.2 has issues with running .NET Standard assemblies, and Cake itself can run on .NET Framework 4.6.1 it is
suggested to multi-target addins to `netstandard2.0` and `net461` to have the maximum compatibility.

:::{.alert .alert-info}
This replaces the previous suggestion to only target `netstandard2.0` starting with Cake 0.26.0 as since then issues were found with running `netstandard2.0`
on .NET Framwork < 4.7.2.
:::

Multi-targeting was suggested by Microsoft in [this .NET Conf 2018 talk](https://www.youtube.com/watch?v=hLFyycJVo0I#t=44m48s) and the underlying issues
are explained in [this tweet](https://twitter.com/terrajobst/status/1031999730320986112)

## Cake.Core / Cake.Common references in addins

Those references are being implicitly added by the Cake engine.
Thus there is no need to add them as dependencies inside the `nuspec` file.

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#dotnet">dotnet CLI / MSBuild</a></li>
    <li><a data-toggle="tab" href="#nuget">nuget.exe CLI</a></li>
</ul>

<div class="tab-content">
    <div id="dotnet" class="tab-pane fade in active">
        <p>
            References to <code>Cake.Core</code> and <code>Cake.Common</code> need to be marked as private assets in the project file:
        </p>
        <p>
<pre><code class="language-xml hljs">&lt;ItemGroup&gt;
  &lt;PackageReference Include="Cake.Core" Version="0.33.0" PrivateAssets="All" /&gt;
  &lt;PackageReference Include="Cake.Common" Version="0.33.0" PrivateAssets="All" /&gt;
&lt;/ItemGroup&gt;
</code></pre>
        </p>
    </div>
    <div id="nuget" class="tab-pane fade">
        <p>
            When using a <code>nuspec</code> file omit the references to <code>Cake.Core</code> and <code>Cake.Common</code>
        </p>
    </div>
</div>

# NuGet package icon for addins

When an addin is not part of a product, addins should use the [Cake Contrib icon](https://github.com/cake-contrib/graphics/blob/master/png/cake-contrib-medium.png) rather than the Cake icon or any other custom icon.
Icons should be embedded in the package rather than rely on an external web site which hosts the icon.

This can be done with the following line in the addins `.csproj`:

```xml
<PackageIcon>PackageIcon.png</PackageIcon>
```

The addins `.csproj` should also contain a reference to the `png` file:

```xml
<ItemGroup>
    <None Include="..\PackageIcon.png" Pack="true" PackagePath="" />
</ItemGroup>
```

Notice the `Pack` attribute, this is particularly important to ensure the file is embedded in the NuGet package.

:::{.alert .alert-info}
Until early 2019, the recommendation was to reference the Cake Contrib icon hosted on the rawgit CDN but rawgit announced that it would shutdown in October 2019 therefore the recommendation changed to reference the Cake Contrib icon hosted on the jsDelivr CDN.
This recommendation changed once again in the fall of 2019 when NuGet started supporting embedded icons.
:::

# Documentation of addins

Addins should follow the [Cake Documentation Guidelines](/community/contributing/documentation) and
have documentation XML files added to the NuGet package.

:::{.alert .alert-info}
Proper XML comments and usage of [CakeAliasCategoryAttribute](/api/cake.core.annotations/cakealiascategoryattribute/)
is important to be able to show documentation for the addin when the it is added to the Cake website.
:::
