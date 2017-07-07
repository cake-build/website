Order: 10
---

This is a guide to get started with Cake and to show you how Cake works.

# 1. Clone the example repo

First, clone the [example repository](https://github.com/cake-build/example)
or download the latest zip.  This repository contains a simple project and all
files necessary to run the cake script.

**NOTE:** If downloading the latest zip file, ensure that it
is [unblocked](http://www.howtogeek.com/70012/what-causes-the-file-downloaded-from-the-internet-warning-and-how-can-i-easily-remove-it/)
otherwise you will run into issues when attempting to execute Cake.

## Files of interest

You need three files for a build (these will be the only files you need to
commit to your repo to use Cake, see how to [pinning cake version](/docs/tutorials/pinning-cake-version) for further guidance.):

<ul class="fa-ul" style="padding-left: 0px; padding-top: 10px;">
    <li style="padding-left: 0px">
        <i class="fa-li fa fa-file-o"></i><b>build.ps1 and build.sh</b>
        <ul style="padding-left: 0px; list-style-type: none;">
            <li style="padding-left: 3px; margin-top: 5px;">
                These are bootstrapper scripts that ensure you have
                Cake and other required dependencies installed. The bootstrapper
                scripts are also responsible for invoking Cake. These files are optional,
                and not a hard requirement.  If you would prefer not to use these scripts
                you can invoke Cake directly from the command line, once you have downloaded
                and extracted it.
            </li>
        </ul>
    </li>
    <li style="padding-left: 0px; margin-top: 5px;">
        <i class="fa-li fa fa-file-o"></i><b>build.cake</b>
        <ul style="padding-left: 0px; list-style-type: none;">
            <li style="padding-left: 3px; margin-top: 5px;">
                This is the actual build script. It doesn't have to be named
                this but this will be found by default.
            </li>
        </ul>
    </li>
    <li style="padding-left: 0px; margin-top: 5px;">
        <i class="fa-li fa fa-file-o"></i><b>tools/packages.config</b>
        <ul style="padding-left: 0px; list-style-type: none;">
            <li style="padding-left: 3px; margin-top: 5px;">
                This is the package configuration that tells the
                bootstrapper script what NuGet packages to install in
                the tools folder. An example of this is Cake itself or
                additional tools such as unit test runners, ILMerge etc.
            </li>
        </ul>
    </li>
</ul>

# 2. Run the build script

Open up a Powershell prompt and execute the bootstrapper script:

```powershell
PS> .\build.ps1
```

The script will detect that you don't have Cake and automatically download
it from NuGet.

It will then run the very simple build.cake script that will clean up
the output directory, restore all NuGet packages and build the project.

**Congratulations, you've run your first Cake script!**

**NOTE:** If you are running Cake on a 32 bit Operating System, you will need to provide an additional
parameter to ensure that the build script runs correctly.  This is due to an issue with Roslyn.  Either use:

```powershell
PS> .\cake\cake.exe build.cake -mono
```

or:

```powershell
PS> .\cake\cake.exe build.cake -experimental
```

If you are using the bootstrapper build.ps1 file, you will need to modify the calling of the Cake.exe
to include this parameter as well.

# 3. Bonus points!

The script is a fairly bare-bones implementation. But extending it is easy.

For instance to run the extensive unit tests for the awesome application
you need to add a test target:

```csharp
Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit("./src/**/bin/" + configuration + "/*.Tests.dll");
});
```

This is using NUnit out of the box but you have MSTest and xUnit
test helpers as well.

Adding the target doesn't necessarily run it unless another target is
dependent on it or you call it explicitly. In our case we can just
change the default task to be dependent on our test task
(which in turn is dependent on the build task):

```csharp
Task("Default")
    .IsDependentOn("Run-Unit-Tests");
```

Execute the build script again (as we did before), and the script
will run all the tests as expected.

## Acknowledgement

This tutorial was adapted from Mark's excellent Cake tutorial that's
available at [https://github.com/marcosnz/Cake-Example](https://github.com/marcosnz/Cake-Example).
