---
content-type: markdown
---

The philosophy behind Cake is to reduce the number of dependencies for Cake 
as well as to not store executable in the source code repository. Instead a 
set of conventions are used to locate executables on disc.

1. Use explicitly set tool path.
2. Try to find the tool by searching `./tools/**/mytool.exe`.
3. Look through all paths in the `PATH` environment variable and look for `mytool.exe`.
4. Ask the tool wrapper for additional, alternative paths.

By inheriting from the [Tool<T>](api://T:Cake.Core.Utilities.Tool`1) class 
when implementing tools, you will get the conventions "for free".
