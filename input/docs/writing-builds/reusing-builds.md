Order: 80
Description: How to reuse build code in different projects and repositories
---

Cake supports different ways to share build code across multiple projects and repositories.

| Method                         | Sharing of classes                              | Sharing of aliases                              | Sharing of tasks                                |
|--------------------------------|-------------------------------------------------|-------------------------------------------------|-------------------------------------------------|
| [Recipe NuGet packages]        | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-times" style="color:red"></i>   | <i class="fa fa-check" style="color:green"></i> |
| [Addins]                       | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-times" style="color:red"></i>   |
| [Assemblies in NuGet packages] | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-times" style="color:red"></i>   | <i class="fa fa-times" style="color:red"></i>   |

[Recipe NuGet packages]: #recipe-nuget-packages
[Addins]: #addins
[Assemblies in NuGet packages]: #assemblies-in-nuget-packages

# Recipe NuGet packages

Cake build scripts can be published as NuGet packages, so called Recipes.
These packages can contain shared tasks and can be consumed by other build scripts.

## Writing Recipe NuGet packages

To create a Recipe NuGet package add the `.cake` files to the `Content` folder in the NuGet package.

## Consuming Recipe NuGet packages

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#load">Load directive</a></li>
    <li><a data-toggle="tab" href="#packages">packages.config</a></li>
</ul>

<div class="tab-content">
    <div id="load" class="tab-pane fade in active">
        <p>
            The <a href="/docs/writing-builds/preprocessor-directives#load-directive">load directive</a> can be used with the <code>nuget</code> scheme
            to download the Recipe NuGet packages and load all <code>.cake</code> files in the <code>content</code> folder.
            The following example loads version 1.0.0 of the <code>MyRecipePackage</code> NuGet package:
        </p>
        <p>
<pre><code class="language-csharp hljs">#load nuget:?package=MyRecipePackage&version=1.0.0</code></pre>
        </p>
    </div>
    <div id="packages" class="tab-pane fade">
        <p>
            The package can be added to the <code>packages.config</code> file.
            The following example loads version 1.0.0 of the <code>MyRecipePackage</code> NuGet package:
        </p>
        <p>
<pre><code class="language-xml hljs">&lt;package id="MyRecipePackage" version="1.0.0" /&gt;</code></pre>
        </p>
        <p>
            Additionally the required files need to be loaded using the <a href="/docs/writing-builds/preprocessor-directives#load-directive">load directive</a>.
            The following example loads the <code>MyScript</code> file which is part of the <code>MyRecipePackage</code> package:
        </p>
        <p>
<pre><code class="language-csharp hljs">#load tools/MyRecipePackage/content/MyScript.cake</code></pre>
        </p>
    </div>
</div>

# Addins

Code can be shared as a [Cake addin](/docs/extending/addins/) which provides [script aliases](/docs/fundamentals/aliases)
that can be used in Cake builds.

## Writing addins

See [Creating addins](/docs/extending/addins/creating-addins) for instructions how to create your custom addin.

## Consuming addins

Addins can be loaded using the [addin directive].

# Assemblies in NuGet packages

Classes can be shared in .NET assemblies deployed in NuGet packages.

## Consuming assemblies from NuGet packages

NuGet packages can be loaded using the [addin directive].

[addin directive]: /docs/writing-builds/preprocessor-directives#add-in-directive
