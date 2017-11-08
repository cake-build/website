---
title: Intellisense in Visual Studio Code
category: Announcement
author: mholo65
---

## Intellisense

As some of you might have noticed, we have been working on getting intellisense working for Cake files in Visual Studio Code.  Today we are happy to announce that the [OmniSharp](http://www.omnisharp.net/) project have merged our pull requests ([#932](https://github.com/OmniSharp/omnisharp-roslyn/pull/932) and [#1681](https://github.com/OmniSharp/omnisharp-vscode/pull/1681)) and released v1.13.0 of the [C# extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp), which includes support for Cake.  In addition to this, we have released a new tool, called [bakery](https://github.com/cake-build/bakery), which is the script analysis and code generation engine making this possible. More information about bakery and how everything works behind the scenes are coming in an upcoming blog.

## How

How do you get this working then? Well, just follow the following steps (we assume that you already have Visual Studio Code installed):

1. Open Visual Studio Code to the folder that you have a Cake file in.
1. Make sure v1.13.0 of the [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp) extension is installed in Visual Studio Code.
1. Make sure v0.10.1 of the [Visual Studio Code Extension for Cake](https://marketplace.visualstudio.com/items?itemName=cake-build.cake-vscode) is installed in Visual Studio Code.
1. Make sure that [Cake](https://www.nuget.org/packages/Cake/) is installed in your `tools` folder. We recommend v0.22.0 or later, otherwise your addins will be installed twice.  The easiest way to do this would be to run your `.\build.ps1` file.
1. Make sure that [Bakery](https://www.nuget.org/packages/Cake.Bakery/) is installed in your `tools` folder.  The easiest way to do this would be using the `Install intellisense support` command which ships with the Visual Studio Code Extension for Cake.  Simply open the command palette, type `cake` and then select the option.
1. At this point, you will need to close and re-open the folder within Visual Studio Code.
1. Success!
<br/>![Intellisense](/assets/img/intellisense-vscode/intellisense-vscode.png)

## Troubleshooting

**Q: I've followed the steps, but I don't get any intellisense.**

The C# Extension for Visual Studio Code will automatically locate any `*.sln` file and use that as the target directory when starting `omnisharp`. If your `*.cake` files are located in a different location, you might need to select `Cake` project in the [project selector](https://code.visualstudio.com/docs/languages/csharp#_roslyn-and-omnisharp).

**Q: I've selected the `Cake` project, but still don't get intellisense.**

Look in the `OmniSharp Log` for errors or warnings.

```
[warn]: OmniSharp.Cake.CakeProjectSystem
    Cake script service not connected. Aborting.
```

The above is an indication that `Cake.Bakery` isn't installed in your `tools` folder, while the below usually means that `Cake` isn't installed in your `tools` folder. (Yes, we are working on better error messages :))

```
[fail]: OmniSharp.Cake.CakeProjectSystem
    c:\Users\mb\src\gh\bakery\setup.cake will be ignored due to an following error
    System.TypeLoadException: A null or zero length string does not represent a valid Type.
```

**Q: I don't see any error message and still don't get intellisense.**

Look in the `OmniSharp Log` do you see anything even related to `Cake` if everything is setup correctly, you should at least see something similar to this:

```
[info]: OmniSharp.Cake.CakeProjectSystem
    Detecting Cake files in 'c:\Users\mb\src\gh\bakery'.
[info]: OmniSharp.Cake.CakeProjectSystem
    Found 29 Cake files.
```

If you don't. You probably have issues with getting v1.13.0 of the C# extension for Visual Studio Code installed. Please uninstall the extension and then try installing it again. Look for `OmniSharp.Cake.dll` in `%userprofile%\.vscode\extensions\ms-vscode.csharp-1.13.0\.omnisharp\` if you're on Windows and `~/.vscode/extensions/ms-vscode.csharp-1.13.0/.omnisharp/` if you are running Linux or Mac OS.

**Q: Ok, I get intellisense. But files I've added after opening Visual Studio Code won't light up.**

This is a known issue in `omnisharp` also when it comes to `*.csx` files. We are working with the `omnisharp` team on getting this fixed in a later release.

**Q: I have `Cake.CoreCLR` installed in my tools folder, isn't that enough?.**

No, at the moment `Cake.CoreCLR` wont work.

**Q: I tried everything above, I still don't get intellisense.**

Submit an issue in the [bakery](https://github.com/cake-build/bakery) repository on Github or reach out to us on [Gitter](https://gitter.im/cake-build/cake).