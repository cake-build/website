---
content-type: markdown
---

The Cake VSO build tasks makes it easy to run a Cake script directly without having to invoke PowerShell or other commands line scripts. This makes it easy even for team members not familiar with Cake to add or adjust parameters passed to your build scripts.

### 1. Install the VSO extension

To install the VSO extension for a VSO account, go to the
[Cake Build Task page](https://marketplace.visualstudio.com/items/cake-build.cake-build-tasks)
at the Visual Studio Marketplace and click "Install".

### 2. Add a build task

Go to the `Build` section fo VSO online and create a build definition if
you doesn't already have one. Add a new build step by
clicking the "Add build step..." button.

![Add build step](https://raw.githubusercontent.com/cake-build/cake-vso/develop/Images/addbuildstep.png)

A dialog should appear with different tasks. Select the "Cake" task.

![Add build step](https://raw.githubusercontent.com/cake-build/cake-vso/develop/Images/addtasks.png)

### 3. Configure the build task

Now you should be able to edit the build task information such as build script,
build target, verbosity and arguments.

By default, the Cake build step (when added to a build) will try to run the `build.cake` build script (found in the root of your repository) with the target `Default`. If you wish to run another build script or build target you can change this in the build step settings.

![Add build step](https://raw.githubusercontent.com/cake-build/cake-vso/develop/Images/configurebuildstep.png)

The bootstrapper that you might normally use is not called when using the VSO task.
To use the bootstrapper you should add a "PowerShell" or "Shell Script" task instead.
