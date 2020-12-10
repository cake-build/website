---
title: Cake VSCode Extension Release 1.0.0
category: Release Notes
author: gep13
---

The Cake Team is very happy to announce the 1.0.0 release of the [Cake Extension for VSCode](https://marketplace.visualstudio.com/items?itemName=cake-build.cake-vscode).  This release has been a long time in the making, but we are hoping that it will be worth the wait!

With the upcoming release of Cake 1.0.0, and the transition in our documentation to recommend the usage of the [Cake .NET Tool](/docs/running-builds/runners/dotnet-tool), the main focus of this version of the extension was to start using the Cake .NET Tool for all of the built in actions.  For example:

* If you choose `Run Task` from the command palette, the chosen task will be executed using the Cake .Net Tool
* If you use the code lens options to either `run task` or `debug task`, the Cake .Net Tool will be used

:::{.alert .alert-info}
If the Cake .Net Tool isn't installed, it will be installed for you.
:::

In all of these cases, you can override the default options to revert to using either the [Cake runner for .NET Framework](/docs/running-builds/runners/cake-runner-for-dotnet-framework) or the [Cake runner for .NET Core](/docs/running-builds/runners/cake-runner-for-dotnet-core).

### Release Notes

Check out the full set of [release notes](https://github.com/cake-build/cake-vscode/releases/tag/1.0.0) for this version.

### Release Video

There is a [short video](https://youtu.be/7Ba-pAHzO9w) discussing the main aspects of this release.

<iframe width="560" height="315" src="https://www.youtube.com/embed/7Ba-pAHzO9w" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

### Thanks

I personally wanted to say a huge thank you to everyone who joined in on my weekly [Twitch](https://www.twitch.tv/gep13) stream, where I have been working on this extension for the last 6 weeks.  Without all of your input and guidance, this release wouldn't have happened.  Also, a huge thank you to [Nils Andersen](https://github.com/nils-a) who has made a number of contributions to the extension.  Thank you all!

### Contribute

If you are interested in helping with the development of the Cake Extension for VSCode, then check out the [repository](https://github.com/cake-build/cake-vscode) and have a read through the open issues, or raise a new one if there are any problems that you would like to see addressed, or features that you would like to see added.