Order: 50
Title: Rider templates
Excerpt: Support for templates
---

# Configuring templates

Templates are provided by the [Cake for Rider](https://plugins.jetbrains.com/plugin/15729-cake-rider) plugin.

# Project templates

In the context menu of a solution in the project window, 
select **Add | New Project** to create a new project.

![Run configurations](/assets/img/cake-rider/docs/project-templates.png){.img-responsive}

# File templates

In the context menu of a solution item (project or folder) in the project window, 
select **Add | Cake File** to create a new Cake file containing some sample code.

![Run configurations](/assets/img/cake-rider/docs/item-templates.png){.img-responsive}

# Live templates (sometimes called code snippets)

Live templates are only available in Cake files.

Available live templates:

* `cake-addin`
  * Provides a basic `#addin` pre-processor directive, where the package name and version can be changed
  * _Default Value:_ `#addin "nuget:?package=Cake.Foo&version=1.2.3"`
* `cake-addin-full`
  * Provides a more complete `#addin` pre-processor directive, where source, package name and version can be changed
  * _Default Value:_ `#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Foo&version=1.2.3"`
* `cake-argument`
  * Provides code for basic input argument parsing, where variable name, argument name and default value can be changed
  * _Default Value:_ `var argumentName = Argument("Argument name", "Default value");`
* `cake-load`
  * Provides a basic `#load` pre-processor directive, where the path to the .cake file can be changed
  * _Default Value:_ `#load "scripts/utilities.cake"`
* `cake-load-nuget`
  * Provides a more complex `#load` pre-processor directive, where package name and version can be changed
  * _Default Value:_ `#load "nuget:?package=Cake.Foo&version=1.2.3"`
* `cake-load-nuget-full`
  * Provides a more complex `#load` pre-processor directive, where source, package name and version can be changed
  * _Default Value:_ `#load "nuget:https://www.nuget.org/api/v2?package=Cake.Foo&version=1.2.3"`
* `cake-module-nuget`
  * Provides a `#module` pre-processor directive, where package name and version can be changed
  * _Default Value:_ `#module "nuget:?package=Cake.Foo.module&version=1.2.3"`
* `cake-module-nuget-full`
  * Provides a more complex `#module` pre-processor directive, where source, package name and version can be changed
  * _Default Value:_ `#module "nuget:https://www.nuget.org/api/v2?package=Cake.Foo.module&version=1.2.3"`
* `cake-reference`
  * Provides a basic `#reference` pre-processor directive, where path to the assembly can be changed
  * _Default Value:_ `#reference "bin/myassembly.dll"`
* `cake-sample`
  * Provides a complete sample build Cake script including setup and teardown actions, a single task, and argument parsing
* `cake-tool`
  * Provides a basic `#tool` pre-processor directive, where the package name and version can be changed
  * _Default Value:_ `#tool "nuget:?package=Cake.Foo&version=1.2.3"`
* `cake-tool-full`
  * Provides a more complete `#tool` pre-processor directive, where source, package name and version can be changed
  * _Default Value:_ `#tool "nuget:https://www.nuget.org/api/v2?package=Cake.Foo&version=1.2.3"`
* `setup`
  * Provides a sample setup definition
* `task-simple`
  * Provides a basic task definition, where the name of the task can be changed
  * _Default Value:_ `Task("name");`
* `task-action`
  * Provides a more complex task definition, including a `.Does` body, where the name of the task can be changed
* `teardown`
  * Provides a sample teardown definition