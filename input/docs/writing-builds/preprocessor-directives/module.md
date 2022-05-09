Order: 70
Title: Module directive
Excerpt: Directive to add Cake modules
---

The module directive lets you boostrap Cake modules by downloading them from a NuGet source.

# Usage

```cake
#module nuget:?package=Cake.UrlLoadDirective.Module&version=1.0.2
#module nuget:https://myget.org/f/Cake/?package=Cake.UrlLoadDirective.Module&version=1.0.3-beta&prerelease
```
As modules can change and extend the internals of Cake, this bootstrapping needs to be done before Cake executes. This is solved by bootrapping being it's own step by invoking Cake with a `--bootstrap` argument before you execute the script normally.

```bash
./cake.exe build.cake --bootstrap
./cake.exe build.cake
```