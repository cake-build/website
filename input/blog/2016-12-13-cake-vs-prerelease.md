---
title: Installing prerelease versions of Cake for Visual Studio
category: How To's
author: agc93
---

We released the Cake for Visual Studio extension back in September and have been thrilled with the response from the whole community. There's been plenty of activity over [on the GitHub repo](https://github.com/cake-build/cake-vs) with new issues, bug reports, features and even PRs. Now, you can get the benefits of all the brand new features as they arrive, right in Visual Studio.

<!--excerpt-->

### CI Releases

We've always built and uploaded a VSIX package as part of our [CI builds on AppVeyor](https://ci.appveyor.com/project/cakebuild/cake-vs) to make sure the extension still works and we can test our changes locally. However, installing a new VSIX every time there's a new feature gets cumbersome pretty quickly.

Today, however, we don't have to rely on our CI builds for packages anymore (although they'll still be there too!).

### MyGet VSIX Feed

Thanks to the awesome guys over at [MyGet](https://www.myget.org/), we now have a custom VSIX feed to use for our CI releases. The MyGet VSIX feed works much like the MyGet NuGet feed (you can read more about that [in an earlier blog post](https://cakebuild.net/blog/2016/08/how-does-cake-use-myget)), but instead of serving NuGet packages, it serves a VSIX *feed*, ready for Visual Studio to consume. This means you will be able to get every new feature and fix as it is merged, rather than waiting for the next stable release.

Note that being bleeding-edge builds, there may be occasional issues with these packages. New stable releases will still be published on [the Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio).

### Getting started with the MyGet feed

To install the feed in Visual Studio, just open your options window (Tools > Options) and go to 'Extensions and Updates' under 'Environment'. Next, click Add on the right, give your new feed a name (here I'm using 'MyGet'), and use the url `https://www.myget.org/F/cake/vsix`. Make sure to click Apply, then OK.

![Adding a new VSIX feed](/assets/img/cake-for-vs-myget/cake-vs-myget.gif)

Now, open your Extensions and Updates window (Tools > Extensions and Updates) and under the Updates entry on the left, you will see your new feed with one pending update. Click Update there and restart Visual Studio when prompted to get started with your bleeding-edge update to Cake for Visual Studio.

![Updating Cake for Visual Studio](/assets/img/cake-for-vs-myget/cake-vs-update.png)

### New features

Not only will you get the latest fixes, you'll also get new features as we introduce them. Updating now will bring you updated project and item templates as well as a significantly improved experience in the Task Runner Explorer, including support for nested Cake files, more name formats, and support for the experimental flag. Coming up we have even more features planned including auto-indentation, Cake-specific IDE settings, and special drag-and-drop support!

Remember, you can always install the latest stable release direct from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio), and we always welcome contributions and feedback [on GitHub](https://github.com/cake-build/cake-vs).

---

My name is Alistair Chapman and I'm an InfoSec engineer and occasional .NET developer from Brisbane, Australia who started working on Cake in 2016 and coincidentally hasn't been bored ever since. You can find me on [GitHub](https://github.com/agc93) and [Twitter](https://twitter.com/agc93) as agc93, or check out [my blog](http://blog.agchapman.com).
