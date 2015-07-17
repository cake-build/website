---
content-type: markdown
---

This is a guide to get started with Cake and to show you how Cake works.

### 1. Clone the example repo

First, clone the [example repository](https://github.com/cake-build/example) 
or download the latest zip. This repository contains a simple project and 
all files necessary to run the cake script.

#### Files of interest

You need three files for a build (these will be the only files you need to 
commit to you repo to use Cake):

<ul class="fa-ul" style="padding-left: 0px; padding-top: 10px;">
    <li style="padding-left: 0px">
        <i class="fa-li fa fa-file-o"></i><b>build.ps1</b>
        <ul style="padding-left: 0px; list-style-type: none;">
            <li style="padding-left: 3px; margin-top: 5px;">
                This is a bootstrapper powershell script that ensures you have 
                Cake and required dependencies installed. The bootstrapper 
                script is also responsible for invoking Cake.
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

### 2. Run the build script

Open up a Powershell prompt and execute the bootstrapper script:

```powershell
PS> .\build.ps1
```

The script will detect that you don't have Cake and automatically download 
it from NuGet. 

It will then run the very simple build.cake script that will clean up 
the output directory, restore all NuGet packages and build the project. 

**Congratulations, you've run you first Cake script!**

### 3. Bonus points!

The script is a fairly bare-bones implementation. But extending it is easy.

For instance to run the extensive unit tests for the awesome application 
you need to add a test target:

```csharp
Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    XUnit2("./src/**/bin/" + configuration + "/*.Tests.dll");
});
```

This is using xUnit out of the box but you have MSTest and NUnit 
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

#### Acknowledgement

This tutorial was adapted from Mark's excellent Cake tutorial that's 
available at [https://github.com/marcosnz/Cake-Example](https://github.com/marcosnz/Cake-Example).
