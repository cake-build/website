Order: 10
Title: Best practices for writing modules
Description: Best practices for writing modules
---

This page gives some best practices for writing Cake modules.
Each guideline describes either a good or bad practice.

The wording of each guideline indicates how strong the recommendation is:

> **Do** guidelines should nearly always be followed.
> You need a really unusual case for breaking a _Do_ guideline.
>
> **Consider** guidelines should generally be followed.
> You can deviate from a _Consider_ guideline if you fully understand the meaning behind the guideline and have a good reason to do so.
>
> **Avoid** guidelines indicates something you should almost never do.

# Naming

**_§1.1_** **Do** use `Cake.xxx.Module` as the naming schema for modules, where `xxx` is a meaningful and unique name that describes what the module does.

> **Why?** Following a convention makes it easier for users to find and work with modules.
> The automated [AddinDiscoverer](https://github.com/cake-contrib/Cake.AddinDiscoverer), which is used for auditing and keeping website up to date, also uses this convention to identify Cake modules.

> **Example:**
>
> `Cake.Npm.Module` is the name that clearly identifies the module for Cake adds support for the [npm](https://www.npmjs.com) package manager when installing tools in your Cake build scripts.
> This name is used in the [GitHub repo](https://github.com/cake-contrib/Cake.Npm.Module),
> it's the name of the [solution file](https://github.com/cake-contrib/Cake.Npm.Module/blob/develop/src/Cake.Npm.Module.sln),
> it's the name of the [project file](https://github.com/cake-contrib/Cake.Npm.Module/blob/develop/src/Cake.Npm.Module/Cake.Npm.Module.csproj),
> the name of the [generated assembly](https://github.com/cake-contrib/Cake.Npm.Module/blob/develop/src/Cake.Npm.Module/Cake.Npm.Module.csproj#L9)
> and finally, it's also the name of the [NuGet package](https://www.nuget.org/packages/Cake.Npm.Module/).

----------------------------------------------------------------------------------------------------

**_§1.2_** **Do** use the same name for the GitHub repo, the C# solution name, the assembly generated from the project and the NuGet package.

> **Why?** Using the same name across different artifacts makes it easier for users and improves results of automated processes.

# References

## Cake reference

**_§2.1_** **Do** reference the lowest version of Cake with API compatibility to the latest version (currently `6.0.0`).

> **Why?** This gives the best support for different versions of Cake.
> Modules built against newer versions of Cake might not be compatible with previous versions of Cake and vice-versa,
> modules built against older versions might not be compatible with future versions of Cake (this is especially true when a future version of Cake introduces breaking changes).
>
> Cake versions which are API compatible with a specific module will be shown when the module is added to the Cake website.

----------------------------------------------------------------------------------------------------

**_§2.2_** **Do** reference a newer version than Cake `6.0.0` if the module requires a specific functionality.

> **Why?** If a specific feature of Cake is required in a module the lowest version of Cake which introduces this feature should be referenced
> to have access to the feature and the best support for different versions of Cake.

----------------------------------------------------------------------------------------------------

**_§2.3_** **Do** update module to a newer version of Cake if a version of Cake with breaking changes becomes available.

> **Why?** Cake will output a warning that the module was built against an incompatible version of Cake and the module might no longer work.
> It is incumbent on module authors to upgrade their references and publish new version of their NuGet packages

----------------------------------------------------------------------------------------------------

**_§2.4_** **Avoid** dependencies to `Cake.Core` and `Cake.Common` in the NuGet package.

> **Why?** Those references are being implicitly added by the Cake engine.

<blockquote class="blockquote">
  <p>
    <strong>Example:</strong>
  </p>
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
  &lt;PackageReference Include="Cake.Core" Version="1.0.0" PrivateAssets="All" /&gt;
  &lt;PackageReference Include="Cake.Common" Version="1.0.0" PrivateAssets="All" /&gt;
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
</blockquote>

## .NET target version

**_§2.5_** **Do** target `net8.0`.

> **Why?** Targeting to `net8.0` should work for most modules to support the latest version of available Cake runners, operating systems and platforms.

**_§2.6_** **Avoid** targeting additional frameworks such as `net9.0` or `net10.0`.

> **Why?** Due to current limitations ([1](https://github.com/cake-build/cake/issues/2256), [2](https://github.com/cake-build/cake/issues/2525)) on how modules are loaded, Cake attempts to load all assemblies included in the NuGet package of the module (of all targets) which causes warnings to be displayed and the impression that the module was not loaded and/or is not working.
> Once those limitations are fixed, we'll update this recommendation to align with the recommendations for [Cake Addins](/docs/extending/addins/best-practices#net-target-version).

# Package metadata

## Icons

**_§3.1_** **Do** define an icon for the NuGet package.

> **Why?** Icons helps the user to identify Cake modules.

----------------------------------------------------------------------------------------------------

**_§3.2_** **Consider** using the [Cake Contrib icon](https://github.com/cake-contrib/graphics/blob/master/png/cake-contrib-medium.png).

> **Why?** When the module doesn't have an own product icon, using the [Cake Contrib icon](https://github.com/cake-contrib/graphics/blob/master/png/cake-contrib-medium.png)
> gives a common branding to Cake modules.

----------------------------------------------------------------------------------------------------

**_§3.3_** **Avoid** using the Cake icon.

> **Why?** The Cake icon is reserved for core Cake packages.

----------------------------------------------------------------------------------------------------

**_§3.4_** **Do** embed the icon in the package.

> **Why?** Using an icon embedded in the package is more reliable then linking to an icon an external web site which hosts the icon.

> **Example:**
>
> This can be done with the following line in the modules `.csproj`:
>
> ```xml
> <PackageIcon>PackageIcon.png</PackageIcon>
> ```
>
> The modules `.csproj` should also contain a reference to the `png` file:
>
> ```xml
> <ItemGroup>
>   <None Include="..\PackageIcon.png" Pack="true" PackagePath="" />
> </ItemGroup>
> ```
>
> Notice the `Pack` attribute, this is particularly important to ensure the file is embedded in the NuGet package.

:::{.alert .alert-info}
Until early 2019, the recommendation was to reference the Cake Contrib icon hosted on the rawgit CDN but rawgit announced that it would shutdown in October 2019 therefore the recommendation changed to reference the Cake Contrib icon hosted on the jsDelivr CDN.
This recommendation changed once again in the fall of 2019 when NuGet started supporting embedded icons.
:::

## Tags

**_§3.5_** **Do** add `cake-module` to the NuGet-Tags.

> **Why?** NuGet can show instructions on how to install a package in Cake since [NuGet/NuGetGallery#8381](https://github.com/NuGet/NuGetGallery/issues/8381). Only if the correct tag is used, the correct installation instructions can be shown.

# Documentation

**_§4.1_** **Do** follow the [Cake Documentation Guidelines](/community/contributing/documentation).

> **Why?** Proper XML comments and usage of [CakeModuleAttribute](/api/Cake.Core.Annotations/CakeModuleAttribute/) is important to be able to show documentation for the module when it is added to the Cake website.

**_§4.2_** **Do** add documentation XML files to the NuGet package.

> **Why?** XML documentation files are used to show documentation for the API provided by the module on the Cake website.

# Testing

**_§5.1_** **Do** test the module with the different [runners](/docs/running-builds/runners).

> **Why?** .NET Tool, script runners and Cake Frosting have slight differences on what dependencies are loaded by the runner and how dependencies are loaded.

:::{.alert .alert-info}
Runners with which a module is not compatible should be documented in the XML comment of the class implementing the [ICakeModule](/api/Cake.Core.Composition/ICakeModule/) interface.
:::

**_§5.2_** **Do** test the module on all [operating systems](/docs/running-builds/runners/#supported-operating-systems) supported by the different Cake runners.

> **Why?** Different platforms have differences in for example file system or available tools.

:::{.alert .alert-info}
Operating systems with which a module is not compatible should be documented in the XML comment of the class implementing the [ICakeModule](/api/Cake.Core.Composition/ICakeModule/) interface.
:::

**_§5.3_** **Do** test the module on all [platforms](/docs/running-builds/runners/#supported-platforms) supported by the different Cake runners.

> **Why?** Different platforms have slight differences in implementation and might break a module.

:::{.alert .alert-info}
Platforms, supported by Cake, with which a module is not compatible should be documented in the XML comment of the class implementing the [ICakeModule](/api/Cake.Core.Composition/ICakeModule/) interface.
:::
