Order: 20
---

If addins are referenced through a `packages.config` in the addins folder, file they can be pinned the same way as the Cake version (when using the Cake Runner for .Net Framework or .NET Core).

If addins are referenced using the `#addin` preprocessor directive they can be pinned like this:

```
#addin nuget:?package=Cake.Foo&version=1.2.3
```
