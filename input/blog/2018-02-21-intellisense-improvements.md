---
title: Intellisense improvements in Visual Studio Code
category: Announcement
author: mholo65
---

## Intellisense improvements

In case you missed it, [intellisense support](/blog/2017/11/intellisense-vscode) for Cake files in Visual Studio Code was announced a couple of months ago. Since then, we have actively been working on improving it, and the v1.14.0 release of [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp) comes with some Cake specific improvements we'd like to share with you. In the [v1.28.0 release notes](https://github.com/OmniSharp/omnisharp-roslyn/releases/tag/v1.28.0) for Omnisharp-Roslyn we can read the following:

### Improvements to the Cake bakery resolver to resolve from both OmniSharp options and PATH. (PR: [#1047](https://github.com/OmniSharp/omnisharp-roslyn/pull/1047))

This was one of the most requested features regarding intellisense support. Previously, intellisense support was dependent on [Cake.Bakery](https://github.com/cake-build/bakery) being present inside the `tools` folder in your workspace. With this fix, you can simply install `Cake.Bakery` wherever you want and just specify the path to `Cake.Bakery.exe` using the [OmniSharp Configuration System](https://github.com/OmniSharp/omnisharp-roslyn/wiki/Configuration-Options). The following steps will tell `OmniSharp` to use a globally installed `Cake.Bakery`:
1. Install `Cake.Bakery` globally (replace <global_path> with the path of your choice).
  ```
  nuget.exe install Cake.Bakery -OutputDirectory <global_path> -ExcludeVersion
  ```
2. Create a file called `omnisharp.json` in `%USERPROFILE%/.omnisharp/` (`~/.omnisharp/` if you are on Linux/macOS) with the following content (replace <global_path> with the path you chose in the previous step):
  ```json
  {
    "cake": {
      "bakeryPath": "<global_path>/Cake.Bakery/tools/Cake.Bakery.exe"
    }
  }
  ```
3. Start Visual Studio Code and start editing your `.cake` files.

To further improve this feature, we are also looking into distributing `Cake.Bakery` via both `Chocolatey`, `Homebrew` and plan old `Zip-files`/ `tarballs`.

### Ensure that the Cake.Core assembly is not locked on disk when loading the host object type. (PR: [#1044](https://github.com/OmniSharp/omnisharp-roslyn/pull/1044))

This fix, along with the previous, will make sure no files in your workspace are actually getting locked while providing intellisense. I.e. no more errors when trying to clean `tools` folder while Visual Studio Code is open.

### Watch added/removed .cake-files and update workspace accordingly. (PR: [#1054](https://github.com/OmniSharp/omnisharp-roslyn/pull/1054))

Previously, when adding `.cake` files to the workspace you would not get intellisense for those files unless you restarted the omnisharp server. This fix will automatically watch for any added, removed or renamed `.cake` files and update the Roslyn workspace accordingly, thus removing the need for restarting the omnisharp server.

### Updated Cake.Scripting.Transport dependencies to 0.2.0 in order to improve performance when working with Cake files. (PR: [#1057](https://github.com/OmniSharp/omnisharp-roslyn/pull/1057))

This fix brings significant performance boost when editing `.cake` files. Unfortunately, this fix also introduced a breaking change. So after updating to v1.13.0 of the [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp) extension, please make sure v0.2.0 or newer of `Cake.Bakery` is installed.