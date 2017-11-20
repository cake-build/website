Order: 10
Title: Visual Studio Team Services
---

The Cake VSTS build task makes it easy to run a Cake script directly, without having to invoke PowerShell or other command line scripts. This makes it easy even for team members that are not familiar with Cake to add or adjust parameters passed to your build scripts.

# 1. Install the VSTS extension

To install the VSTS extension, go to the
[Cake Build Task page](https://marketplace.visualstudio.com/items/cake-build.cake)
on the Visual Studio Marketplace and click "Install".

# 2. Add a build task

Go to the `Build` section in your VSTS tenant  and create a build definition, or
select an existing one. Add a new build step by clicking the
"Add build step..." button.

![Add build step](https://raw.githubusercontent.com/cake-build/cake-vso/develop/Images/addbuildstep.png)

A dialog should appear with different tasks. Select the "Cake" task and click
the "Add" button once. After that, click the "Close" button to close the window.

![Add build step](https://raw.githubusercontent.com/cake-build/cake-vso/develop/Images/addtasks.png)

# 3. Configure the build task

You should now be able to edit the build task information such as which
build script to execute, what build target to run, the amount of logging
(verbosity) and additional arguments sent to the build script.

By default, the Cake build step (when added to a build) will try to run the `build.cake` build script (found in the root of your repository) with the target `Default`. If you wish to run another build script or build target you can change this in the build step settings.

![Add build step](https://raw.githubusercontent.com/cake-build/cake-vso/develop/Images/configurebuildstep.png)

For those who are already using Cake making use of the [Bootstrapper file](https://cakebuild.net/docs/tutorials/setting-up-a-new-project), this is not called when using the VSO task.
To use the bootstrapper file you should add a "PowerShell" or "Shell Script" task instead.

## Notes about Variables

Environment Variables which are marked secret in VSTS, cannot be resolved using `EnvironmentVariable()` in a Cake script. Instead they can be passed as arguments to the build script. You can read more about this limitation in the [Visual Studio documentation under the Secret Variables section](https://www.visualstudio.com/en-us/docs/build/define/variables#SecretVariables).
