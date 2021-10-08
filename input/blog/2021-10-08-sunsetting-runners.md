---
title: Sunsetting of .NET Framework and .NET Core runners
category: Announcement
author: Pascal Berger
---

The Cake project started 2014 with [Cake runner for .NET Framework] which was downloaded more than 10 million times.
Since then [Cake runner for .NET Core], [Cake .NET Tool] and [Cake Frosting] have been added as officially supported runners.
Across these runners, Cake runs on Mono, .NET Framework and .NET Core 2.0 and newer up to .NET 6.

This [broad range of supported runners and platforms] comes also with a cost.
On one hand it limits the usage of new features available in newer platforms in Cake itself,
on the other hand it is also  reflected in our best practices, that extension should be supported on all runners and platforms,
leading to additional maintenance work in the whole ecosystem.
Having to choose between different runners could also lead to confusion for users, which is the best choice for their use-case.

To allow Cake to make use of modern platform features, make life easier for extension authors and simplify decision process of users
we decided to stop shipping [Cake runner for .NET Framework] and the already deprecated [Cake runner for .NET Core] with 2.0.
This will mean that starting with 2.0, Cake no longer will run on .NET Framework, Mono and .NET Core 2.0.
Additionally we will drop support to run on .NET Core 2.1 and .NET Core 3.0 for [Cake .NET Tool] and [Cake Frosting].

Supported platform matrix for Cake 2.0 will look like this:

| Runner                           | .NET 6 | .NET 5 | .NET Core 3.1 |
|----------------------------------|--------|--------|---------------|
| [Cake .NET Tool]                 | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |
| [Cake Frosting]                  | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> | <i class="fa fa-check" style="color:green"></i> |

[Cake runner for .NET Core] has been deprecated since version 1.0 with [Cake .NET Tool] as the suggested replacement.
For users of [Cake runner for .NET Framework] it is also suggested to switch to [Cake .NET Tool] and run builds on .NET Core 3.1 or newer.
For users which rely on extension or other dependencies which require .NET Framework or .NET Core 3.0 or older, suggestion is to stay on Cake 1.x.

:::{.alert .alert-info}
Note that only support for running Cake on .NET Framework and older versions of .NET Core will be dropped.
Cake will continue to support building of .NET Framework projects as well as projects targeting .NET Core 3.0 or older.
:::

If you have any questions please join the [discussion].

[Cake runner for .NET Framework]: https://www.nuget.org/packages/Cake/
[Cake runner for .NET Core]: https://www.nuget.org/packages/Cake.CoreCLR/
[Cake .NET Tool]: https://www.nuget.org/packages/Cake.Tool/
[Cake Frosting]: https://www.nuget.org/packages/Cake.Frosting/
[broad range of supported runners and platforms]: /docs/running-builds/runners/
[discussion]: https://github.com/cake-build/cake/discussions/???
