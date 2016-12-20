---
title: How does Cake use MyGet?
category: Infrastructure
author: gep13
---

This the first in a series of blog posts which will attempt to explain how Cake uses a number of different online services in order to develop, build, and deploy Cake.

### What is MyGet?

[MyGet](https://www.myget.org) is a hosted package server that allows hosting of the following package types:

* NuGet
* Chocolatey
* Npm
* Bower
* Vsix
* Symbols
* PowerShell Modules

In addition MyGet also offers a build service to allow for the execution of build scripts.

<!--excerpt-->

### How does Cake use MyGet?

#### Package Hosting

Each time a commit is made on the develop or master branch, an [AppVeyor](https://www.appveyor.com/) build is triggered (**NOTE:** this could equally be a MyGet Build as mentioned below, it's just that back when the Cake project was started AppVeyor was chosen as the main Continuous Integration system that deployments would be triggered from), the end result of which is a number of NuGet packages are created.  These NuGet packages are then immediately pushed to the [Cake feed](https://www.myget.org/gallery/cake) on MyGet.  You can see this in action here:

![Package Hosting](/assets/img/how-does-cake-use-myget/MyGet-Packages.png)

Each of those badges (which are shown on the project [readme](https://github.com/cake-build/cake/blob/develop/README.md) file) show what that latest package version is in each location.

In the above screenshot, you can see that version 0.15.2 of Cake is the latest version on NuGet, Chocolatey and Homebrew.  However, in the MyGet feed, we have version 0.16.0-alpha0046.  All of the stable releases of Cake come from the [main](https://github.com/cake-build/cake/tree/main) branch and all of our work in progress work is done on the [develop](https://github.com/cake-build/cake/tree/develop) branch.  This segregation is a very common practice, and something that Cake has done since the project was first started.  This means that we, and anyone who is interested, can immediately start using the latest pre-release version of Cake as soon as a commit is made to the develop branch, and those who want to wait for an official release can wait until a merge is done in the master branch and the packages are pushed to NuGet.org.

Put simply, we want to make sure (or at least as sure as we can be) that the next version of Cake works as we expect it to, before publishing it to the wider community.  MyGet allows us to do exactly that by consuming Cake in the "normal" way, i.e. as a NuGet Package, however simply coming from an alternative source.  If you want to find out more about hosting your own packages on a MyGet feed of your own, check out the documentation [here](http://docs.myget.org/docs/walkthrough/getting-started-with-nuget).

#### Build service

In addition to the Package Hosting, we also take advantage of the MyGet Build Services.  Actually, we take advantage of a number of build services, as you can see in the below screenshot:

![Build Services](/assets/img/how-does-cake-use-myget/Build-Services.png)

Bottom line is, we want to make sure that Cake works, and has been tested on, as many Continuous Integration services as possible.  To that end, we build Cake using Cake, on all on the online services that you can see in screenshot above, including MyGet.

By doing this, we can make sure ahead of time, if there are any issues running Cake.  The MyGet Build Service (affectionately known as Wonka) is perfectly suited at executing a Cake script (other build systems do exist :-)).  If you want to find out more about the MyGet Build Services, be sure to check out the documentation [here](http://docs.myget.org/docs/reference/build-services).