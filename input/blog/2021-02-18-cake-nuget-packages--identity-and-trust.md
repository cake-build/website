---
title: "Cake NuGet Packages: Identity and Trust"
category: Announcement
author: augustoproiete
---
The NuGet team at Microsoft introduced a concept known as "[Package ID prefix reservation](https://docs.microsoft.com/en-us/nuget/nuget-org/id-prefix-reservation)" for packages published on [nuget.org](https://www.nuget.org), which allows developers to reserve particular package ID prefixes with a specific account/owner and offers a number of features that are [described in detail on Microsoft's docs](https://docs.microsoft.com/en-us/nuget/nuget-org/id-prefix-reservation).

One of these features is that packages with a reserved prefix include a visual indicator for NuGet package consumers to identify that the packages they are consuming originate from package owners that have reserved the prefix.

![Cake package visual indicator on NuGet Gallery](/blog/media/2021-02-18-cake-nuget-packages--identity-and-trust/cake-package-visual-indicator-nuget-screenshot.png)

<!--excerpt-->

We have submitted an application to the NuGet team to reserve the `Cake` and `Cake.` prefixes, with the goal of allowing Cake users to more easily identify the official packages that are published and maintained by the [Cake team](/docs/team/), and thankfully we were approved! :tada:

We asked those prefixes to be made **publicly accessible** to all developers who want to release their own extensions for Cake, so that the community can continue to publish all kinds of helpful addins, modules, and recipes for Cake using the same `Cake.xxx` [naming convention](/docs/extending/addins/best-practices#naming) that you're already familiar with.

## What does that mean for me, as a user of Cake?

You will notice that some Cake packages will display a "blue checkmark". Those are packages that are published and maintained by the [Cake team](/docs/team/) a.k.a. "official packages". Packages without the "blue checkmark" are published and maintained by the community.

## What does that mean for me, as a Cake contributor?

Thankfully nothing changes for you, and you can continue to publish extensions to Cake on [nuget.org](https://www.nuget.org) using the `Cake.` prefix as recommended in our [best practices for naming addins](/docs/extending/addins/best-practices#naming).

---

If you have any questions about Cake's package identity and trust, or any security-related topics about the Cake project, please write to us at [security@cakebuild.net](mailto:security@cakebuild.net)
