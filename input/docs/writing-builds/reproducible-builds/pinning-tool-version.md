Order: 30
Description: How to make builds reproducible when using external tools
---

If tools are referenced through a `packages.config` file they can be pinned the same way as the Cake version (when using the Cake Runner for .Net Framework or .NET Core).

If tools are referenced using the `#tool` preprocessor directive they can be pinned like this:

```
#tool nuget:?package=Tool.Foo&version=1.2.3
```
