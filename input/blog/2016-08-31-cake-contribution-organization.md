---
title: Cake Contribution Organization
category: News
author: gep13
---

As some of you may know, we recently opened an issue on GitHub to discuss the problem of [Cake addins becoming stale](https://github.com/cake-build/cake/issues/1136).  This blog post aims to recap the discussion that was had within that issue, as well as the plan going forward.

The first thing that I want to re-iterate is that the Cake Team are not attempting to throttle or take over the development of Cake addins/modules in anyway.  We love the fact that there is a thriving Community behind Cake, and we want to encourage that.  However, we do have a responsibility to the wider Cake Community and we want to ensure that any addins and modules that are created, can continue to be maintained, should the original developer no longer be in a position to maintain it.

<!--excerpt-->

In order to achieve this, starting today, we are going to start suggesting to maintainers of Cake addins and modules, that are published to [nuget.org](https://www.nuget.org), to add the [cake-contrib](https://www.nuget.org/profiles/cake-contrib) user as a co-maintainer of that package.  As an example, see the [Cake.Gem](https://www.nuget.org/packages/Cake.Gem/) package on nuget.org.

The thought process here is that once a package as been pushed to the nuget.org, it is seen as the **official** package.  We want to avoid duplication of packages on nuget.org, and if the cake-contrib user is added as a co-maintainer of the package, if/when required, new versions of the package can be pushed using the same package id.  A secondary benefit to this approach is that by checking the [cake-contrib](https://www.nuget.org/profiles/cake-contrib) profile on nuget.org will show all of the addins and modules that are currently available.

In addition to the above, we have also created the [Cake Contribution Organization](https://github.com/cake-contrib) on GitHub.  This is intended as a place where the source code for any addins and modules for Cake can reside.  **However**, this is **not** going to be mandatory, this is merely a suggestion.  The thought process here again is to avoid duplication.  Rather than having to fork a repository in order to continue development on an addin or module, since the source code exists within an organization that is under the control of the Cake Team we can add additional maintainers to an individual repository within the organization to people that require access.

As of today, there are 7 repositories in the Cake Contribution Organization:

* Cake.ReSharperReports
* Cake.Gem
* Cake.Tfx
* Cake.Twitter
* Cake.Gitter
* Cake.VsCode
* Cake.Coveralls

These repositories were originally created by me (gep13), and I have moved them into this Organization.  If I were not a member of the Cake Team, having moved my repository into this organization, I would still have complete control over the repository.  Nothing would change from the perspective.  The expectation is that you would still be the main contributor to the repository, it would just be that it exists under the Cake Contribution Organization.  Again, this is **not** mandatory, and we are never going to force anyone to move their code into this repository, however, if you would like to move your repository into this organization then please get in touch with the team on [gitter](https://gitter.im/cake-contrib/Lobby) and we can start the process of moving over the repository.

Both Patrik and Mattias will be moving the addins and modules that they have created into this organization in the near future.

You might notice that the above gitter link takes you to a new gitter room that we have created.  We decided to segregate the conversations.  We still have the main [Cake gitter room](https://gitter.im/cake-build/cake), it is just an attempt to keep the conversations in the correct location.

In addition to the new gitter room, we also have a new [Cake Contrib Twitter account](https://twitter.com/cakecontrib) which I would encourage you to follow.  Any time an addin or module which exists in the Cake Contribution Organization is updated, a new tweet will be sent out through this account to let people know when a new release is out.