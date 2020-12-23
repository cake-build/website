---
title: Cake Visual Studio Extension Release 0.3.0
category: Release Notes
author: gep13
---

The Cake Team is very happy to announce the 0.3.0 release of the [Cake Extension for Visual Studio](/docs/integrations/editors/visualstudio/).  This release has been baking for quite a while now, the previous release shipped in 2017, but we are hoping that it will be worth the wait.

The main focus of this release, was to start to bring the extension within feature parity of the [Cake Extension for VSCode](/docs/integrations/editors/vscode). This is still a long way off, but we have made some good progress in the journey.  That is the main reason why this is a 0.3.0 release, rather than a 1.0.0 release.

The extension now supports Visual Studio 2019 (this has been a common request for a while now), however, we have had to drop support for Visual Studio 2015.  If you require running with Visual Studio 2015, we would recommend _not_ upgrading this extension.

Also, in addition to supporting a globally installed version of Cake using the [Chocolatey package](https://chocolatey.org/packages/cake.portable), the extension will now also look for (and execute) [Cake .NET Tool](/docs/running-builds/runners/dotnet-tool).

In the next release of the extension, we are looking at adding support for extension settings, which will allow finer grained control of **how** the extension works under the hood, instead of having these hard-coded.

### Release Notes

Check out the full set of [release notes](https://github.com/cake-build/cake-vs/releases/tag/0.3.0) for this version.

### Release Video

There is a [short video](https://youtu.be/fG93MjnxHoo) discussing the main aspects of this release.

<iframe width="560" height="315" src="https://www.youtube.com/embed/fG93MjnxHoo" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

### Thanks

I personally wanted to say a huge thank you to everyone who joined in on my weekly [Twitch](https://www.twitch.tv/gep13) stream, where I have been working on this extension for the last 2 weeks.  Without all of your input and guidance, this release wouldn't have happened.  Thank you all!

### Contribute

If you are interested in helping with the development of the Cake Extension for Visual Studio, then check out the [repository](https://github.com/cake-build/cake-vs) and have a read through the open issues, or raise a new one if there are any problems that you would like to see addressed, or features that you would like to see added.