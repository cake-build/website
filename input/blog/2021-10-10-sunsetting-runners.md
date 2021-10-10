---
title: Sunsetting of .NET Framework and .NET Core runners in Cake 2.0
category: Announcement
author: Pascal Berger
---

:::{.alert .alert-info}
Note that the following explanations are only about required platforms to run Cake.
Cake will continue to support building of .NET Framework and .NET Core projects.
:::

The Cake project started in 2014 with the [Cake runner for .NET Framework] which has since been downloaded more than 10 million times.
Since then [Cake runner for .NET Core], [Cake .NET Tool] and [Cake Frosting] have been added as officially supported runners.
Across these runners, Cake runs on Mono, .NET Framework and .NET Core 2.0 and newer up to .NET 6.

This [broad range of supported runners and platforms] comes also with a cost.
On one hand it limits the usage of new features available in newer platforms in Cake itself,
on the other hand it is also reflected in our best practices, that extensions should be supported on all runners and platforms,
leading to additional maintenance work in the whole ecosystem.
Having to choose between different runners could also lead to confusion for users, regarding which is the best choice for their use-case.

To allow Cake to make use of modern platform features, make life easier for extension authors and simplify decision process of users
we have decided to stop shipping [Cake runner for .NET Framework] and the already deprecated [Cake runner for .NET Core] with 2.0.
Additionally we will drop support to run on .NET Core 2.1 and .NET Core 3.0 for [Cake .NET Tool] and [Cake Frosting].

This means that, starting with Cake 2.0, you will need to have the .NET SDK installed on your build machine, at a minimum .NET Core 3.1,
but .NET 6 is recommended, in order to **run** Cake.
In other words, Cake itself will no longer **run** on .NET Framework, Mono and .NET Core 3.0 or older.
Cake will continue to support building of .NET Framework projects as well as projects targeting .NET Core 3.0 or older.

Supported platform matrix for Cake 2.0 will look like this:

| Runner                           | .NET 6 | .NET 5 | .NET Core 3.1 |
| -------------------------------- |:------:|:------:|:-------------:|
| [Cake .NET Tool]                 | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [Cake Frosting]                  | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |

As a consequence of no longer shipping [Cake runner for .NET Framework] we will also stop shipping the [Cake.Portable Chocolatey package]
and [Homebrew Cake formulae].

[Cake runner for .NET Core] has been deprecated since version 1.0 with [Cake .NET Tool] as the suggested replacement.
For users of [Cake runner for .NET Framework] it is also suggested to switch to [Cake .NET Tool] and run builds on .NET Core 3.1 or newer.
For users which rely on extension or other dependencies which require .NET Framework or .NET Core 3.0 or older, suggestion is to stay on Cake 1.x.

If you have any questions please join the [discussion].

[Cake runner for .NET Framework]: /docs/running-builds/runners/cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: /docs/running-builds/runners/cake-runner-for-dotnet-core
[Cake .NET Tool]: /docs/running-builds/runners/dotnet-tool
[Cake Frosting]: /docs/running-builds/runners/cake-frosting
[Cake.Portable Chocolatey package]: https://community.chocolatey.org/packages/cake.portable
[Homebrew Cake formulae]: https://formulae.brew.sh/formula/cake
[broad range of supported runners and platforms]: /docs/running-builds/runners/
[discussion]: https://github.com/cake-build/cake/discussions/3575
