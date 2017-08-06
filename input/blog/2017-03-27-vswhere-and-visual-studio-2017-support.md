---
title: VSWhere and Visual Studio 2017 Support
category: How To's
author: phillipsj
---

Since version 0.18.0 of Cake, there has been support created for Visual Studio 2017. The support was added to the MSBuild tool to search for the default installation locations of the various flavors of MSBuild. The default locations did not require an external dependency on the [Microsoft.VisualStudio.Setup.Configuration.Interop](https://www.nuget.org/packages/Microsoft.VisualStudio.Setup.Configuration.Interop/) package that was created by Microsoft to help locate Visual Studio 2017 installations.

Shortly after the release of version 0.18.0, Microsoft released a tool called [vswhere](https://github.com/Microsoft/vswhere) to help locate Visual Studio and other products in the family. This tool was released with Cake version 0.19.1 to allow everyone that moved to Visual Studio 2017 to get their builds up and running.

This post is going to walk through the different pieces of functionality supported by the [VSWhere tool](https://cakebuild.net/dsl/vswhere/).

<!--excerpt-->

## Getting Started

VSWhere is available on [NuGet](https://www.nuget.org/packages/vswhere/) (and [Chocolatey](https://chocolatey.org/packages/vswhere)) which means you can fetch it using the tool pre-processor directive like this:

```csharp
#tool nuget:?package=vswhere
```

## Getting the **Latest** installation

If you just want to use the latest Visual Studio version available - then there's the [VSWhereLatest](https://cakebuild.net/api/Cake.Common.Tools.VSWhere/VSWhereAliases/59EB3043) alias. A complete example of fetching the VSWhere tool and using it to find path to MSBuild is shown below:

```csharp
#tool nuget:?package=vswhere

DirectoryPath vsLatest  = VSWhereLatest();
FilePath msBuildPathX64 = (vsLatest==null)
                            ? null
                            : vsLatest.CombineWithFilePath("./MSBuild/15.0/Bin/amd64/MSBuild.exe");

MSBuild("./src/Example.sln", new MSBuildSettings {
    ToolPath = msBuildPathX64
});

```

If you are looking for the latest installation that supports a specific, optional component, like the .NET Framework (desktop) workload you would specify that in the *Requires* property in the settings.

To find a comprehensive list of components, please see the Microsoft Documentation [here](https://docs.microsoft.com/en-us/visualstudio/install/workload-and-component-ids)

```csharp
#tool nuget:?package=vswhere

DirectoryPath vsLatest  = VSWhereLatest(new VSWhereLatestSettings { Requires = "Microsoft.VisualStudio.Workload.ManagedDesktop"});
FilePath msBuildPathX64 = (vsLatest==null)
                            ? null
                            : vsLatest.CombineWithFilePath("./MSBuild/15.0/Bin/amd64/MSBuild.exe");

MSBuild("./src/Example.sln", new MSBuildSettings {
    ToolPath = msBuildPathX64
});

```

## Finding all Visual Studio installations

If you would like to find all Visual Studio installations regardless if they are complete installations you would run the following.

```csharp
#tool nuget:?package=vswhere

DirectoryPathCollection allInstalled  = VSWhereAll();

foreach(var install in allInstalled)
{
    // Find the installation you need
}
```

Of course you can find all restricted to the type of components or workloads installed by using the settings and setting the required component. In this instance it restricts it to only installations that have MSBuild installed.

```csharp
#tool nuget:?package=vswhere

DirectoryPathCollection allInstalled  = VSWhereAll(new VSWhereAllSettings { Requires = "Microsoft.Component.MSBuild" });

foreach(var install in allInstalled)
{
    // Find the installation you need
}
```

## Finding a specific product, including VSTest

If you know you would like to find the directory for a specific product you can do the following.

```csharp
#tool nuget:?package=vswhere

DirectoryPath buildToolsInstallation  = VSWhereProducts("Microsoft.VisualStudio.Product.BuildTools").FirstOrDefault();

if(buildToolsInstallation != null)
{
    MSBuild("./src/Example.sln", new MSBuildSettings {
        ToolPath = buildToolsInstallation.CombineWithFilePath("./MSBuild/15.0/Bin/amd64/MSBuild.exe")
    });
}

```

If you know you need the build tools installation with a specific component or workload installed then you can search for it.

```csharp
#tool nuget:?package=vswhere

DirectoryPath buildToolsInstallation  = VSWhereProducts("Microsoft.VisualStudio.Product.BuildTools",
                                            new VSWhereLatestSettings { Requires = "Microsoft.VisualStudio.Workload.ManagedDesktop"}).FirstOrDefault();

if(buildToolsInstallation != null)
{
    MSBuild("./src/Example.sln", new MSBuildSettings {
        ToolPath = buildToolsInstallation.CombineWithFilePath("./MSBuild/15.0/Bin/amd64/MSBuild.exe")
    });
}
else
{
    Information("Could not find suitable build environment.");
}

```

You can also find VSTest using VSWhere.

```csharp
#tool nuget:?package=vswhere

DirectoryPath vsTestInstallationPath  = VSWhereProducts("*", new VSWhereLatestSettings { Requires = "Microsoft.VisualStudio.PackageGroup.TestTools.Core"}).FirstOrDefault();

FilePath vsTestPath = (vsTestInstallationPath==null)
                            ? null
                            : vsTestInstallationPath.CombineWithFilePath("./Common7/IDE/CommonExtensions/Microsoft/TestWindow/vstest.console.exe");

VSTest("./Tests/*.UnitTests.dll", new VSTestSettings() {
                                            Logger = VSTestLogger.Trx,
                                            ToolPath = vsTestPath });
```

## Finding legacy products

VSWhere also has support for finding legacy products.

```csharp
#tool nuget:?package=vswhere

DirectoryPath legacyInstallationPath = VSWhereLegacy(true);  // Passing true gets the latest version.

```

If you know you are looking for a specific version of a legacy product then you can do the following.

```csharp
#tool nuget:?package=vswhere

DirectoryPath legacyInstallationPath = VSWhereLegacy(new VSWhereLegacySettings { Version = "10.0"}).FirstOrDefault();

```
