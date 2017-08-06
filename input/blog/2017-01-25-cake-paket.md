---
title: Cake.Paket - An Addin and Module to Add Support for Paket in Cake 
category: Addins
author: Larz White
---

Most .NET developers are comfortable using [NuGet](https://www.nuget.org/) to manage their dependencies. This is probably why Cake has such great support for it. Unfortunately, [Paket](https://fsprojects.github.io/Paket/) which is a popular alternative has very little support. Luckily, with the help of [Cake.Paket](https://github.com/larzw/Cake.Paket) you can make Paket a first class citizen in your Cake scripts!

<!--excerpt-->

### Paket

> Paket is a dependency manager for .NET and mono projects, which is designed to work well with NuGet packages and also enables referencing files directly from Git repositories or any HTTP resource. It enables precise and predictable control over what packages the projects within your application reference. ~ Paket Website

Although this is not a tutorial on Paket, a brief discussion is necessary.

Dependencies such as NuGet packages are handled external to Visual Studio by specifying them in the *paket.dependencies* file. This is analogous to the *packages.config* file which is updated when you install NuGet packages inside Visual Studio. For example, to install *xUnit*

```
# paket.dependencies

source https://nuget.org/api/v2
nuget xunit # Required in order to run the unit tests.
```

and then run `paket.exe install`.

For the above example we used *paket.exe* which is analogous to *nuget.exe*. In fact, a lot of the Paket commands are similar to NuGet commands: `paket.exe restore`, `paket.exe pack`, and `paket.exe push`. For a complete list of Paket commands see Paket's website.

### Cake.Paket: Cake Addin

[Cake.Paket](https://www.nuget.org/packages/Cake.Paket/) is an addin that provides support for Paket commands such as *restore*, *pack*, and *push*. For example,

```csharp
// Required in order to run the unit tests.
#tool nuget:?package=xunit.runner.console

// Required in order to use the addin Cake.Paket because it downloads paket.exe.
#tool nuget:?package=Paket

// Required in order to use PaketRestore, PaketPack, and PaketPush.
#addin nuget:?package=Cake.Paket

// Restores xUnit dependency specified in the paket.dependencies file.
Task("Paket-Restore")
    .Does(() =>
{
    PaketRestore();
});

Task("Build")
    .IsDependentOn("Paket-Restore")
    .Does(() =>
{
    MSBuild("./src/Cake.sln");
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    XUnit2("./src/**/bin/Release/*.Tests.dll");
});

// Creates a NuGet package.
Task("Paket-Pack")
    .IsDependentOn("Build")
    .Does(() =>
{
    PaketPack("./NuGet");
});

// Pushes the package to a NuGet feed.
Task("Paket-Push")
    .IsDependentOn("Paket-Pack")
    .Does(() =>
{
    PaketPush("./NuGet/foo.nupkg", new PaketPushSettings { ApiKey = "00000000-0000-0000-0000-000000000000" });
});
```

where the script can be run using the Cake teams bootstrapper scripts [build.ps1](https://github.com/cake-build/example/blob/master/build.ps1) and [build.sh](https://github.com/cake-build/example/blob/master/build.sh).

### Cake.Paket.Module: Cake Module

The astute reader will notice we used NuGet to manage our dependencies in the previous Cake script because we used the preprocessor directives `#tool nuget:?package=foo` and `#addin nuget:?package=bar`. Wouldn't it be great if we could use Paket instead? That's where [Cake.Paket.Module](https://www.nuget.org/packages/Cake.Paket.Module/) comes to the rescue! The Cake script above can be rewritten using the syntax `#tool paket:?package=foo` and `#addin paket:?package=bar`

```csharp
// Required in order to run the unit tests.
#tool paket:?package=xunit.runner.console

// Required in order to use PaketPack and PaketPush.
#addin paket:?package=Cake.Paket

// Paket restore is handled by the bootstrapper.

Task("Build")
    .Does(() =>
{
    MSBuild("./src/Cake.sln");
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    XUnit2("./src/**/bin/Release/*.Tests.dll");
});

// Creates a NuGet package.
Task("Paket-Pack")
    .IsDependentOn("Build")
    .Does(() =>
{
    PaketPack("./NuGet");
});

// Pushes the package to a NuGet feed.
Task("Paket-Push")
    .IsDependentOn("Paket-Pack")
    .Does(() =>
{
    PaketPush("./NuGet/foo.nupkg", new PaketPushSettings { ApiKey = "00000000-0000-0000-0000-000000000000" });
});
```

where the Cake script can be run using the **modified bootstrapper scripts** [build.ps1](https://github.com/larzw/Cake.Paket/blob/master/build.ps1) and/or [build.sh](https://github.com/larzw/Cake.Paket/blob/master/build.sh) and the following *paket.dependencies* file

```
# paket.dependencies

source https://nuget.org/api/v2
nuget xunit # Required in order to run the unit tests.

group tools
    source https://nuget.org/api/v2
    nuget Cake # Required in order to download Cake.exe.
    nuget xunit.runner.console # Required in order to run the unit tests.

group addins
    source https://nuget.org/api/v2
    nuget Cake.Paket # Required in order to use PaketPack and PaketPush.

group modules
    source https://nuget.org/api/v2
    nuget Cake.Paket # Required in order to use paket in the preprocessor directives.
```

In the above *paket.dependencies* file we used [Dependency Groups](https://fsprojects.github.io/Paket/groups.html) to organize the file. It's important to note that you can organize the file however you want! For example, you don't need to use *tools*, *addins* and *modules* for the default group names. You can set them to whatever you want. See the command line arguments in the **modified bootstrapper scripts** [build.ps1](https://github.com/larzw/Cake.Paket/blob/master/build.ps1) and/or [build.sh](https://github.com/larzw/Cake.Paket/blob/master/build.sh) for more detail. Additionally, the dependencies don't have to be in the same group! If you need to specify a dependency in another group just include it in the URI `#tool paket:?package=foo&group=bar`. 

A few side notes are in order. First, if you need to specify a package without a group (Paket calls it the *main* group) use `#tool paket:?package=foo&group=main`. Second, a version number in the URI is not allowed. This is because version's are handled in the *paket.dependencies* file.

### Conclusion

The Cake.Paket addin and module make Paket a first class citizen in your Cake scripts. The addin provides support for common Paket commands such as *restore*, *pack*, and *push* while the module allows you to use Paket for the dependency management system in your Cake scripts. Checkout [Cake.Paket.Example](https://github.com/larzw/Cake.Paket.Example) for a working example of how to use the addin and module as well as the [API](https://cakebuild.net/dsl/paket/) documentation on Cake's website.

---

My name is Larz White. My educational background is in computational nuclear physics. This is how I developed a passion for computing. For the past few years I've been working in industry as a C# developer. You can find me on [GitHub](https://github.com/larzw) as larzw.
