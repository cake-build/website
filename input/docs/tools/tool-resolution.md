Order: 20
---

The philosophy behind Cake is to reduce the number of dependencies for Cake
as well as to not store executable in the source code repository. Instead a
set of conventions are used to locate executables on disc.

1. Use explicitly set tool path on [ToolSettings](/api/Cake.Core.Tooling/ToolSettings) [ToolPath](/api/Cake.Core.Tooling/ToolSettings/F6956DA8) property.
2. Via [IToolLocator](/api/Cake.Core.Tooling/IToolLocator/) registered path (see example below).
3. Try to find the tool by searching `./tools/**/mytool.exe`.
4. Look through all paths in the `PATH` environment variable and look for `mytool.exe`.
5. Ask the tool wrapper for additional, alternative paths.

By inheriting from the [Tool<T>](/api/Cake.Core.Tooling/Tool_1) class
when implementing tools, you will get the conventions "for free".

You can override and register tool paths using the [IToolLocator](/api/Cake.Core.Tooling/IToolLocator/) [Tools](/api/Cake.Core/ICakeContext/8C889AB4) property on the [ICakeContext](/api/Cake.Core/ICakeContext/):

```csharp
Setup(context => {
    context.Tools.RegisterFile("C:/ProgramData/chocolatey/bin/NuGet.exe");
});
```

If you want to use the tool resolution from your Cake script for generic tasks, i.e. starting a process, that's certainly possible using the [Resolve(string) ](/api/Cake.Core.Tooling/IToolLocator/D57090B2) method:
```csharp
Task("NuGet-Help-Install")
    .Does(()=> {
    FilePath nugetPath = Context.Tools.Resolve("nuget.exe");
    StartProcess(nugetPath, new ProcessSettings {
        Arguments = new ProcessArgumentBuilder()
            .Append("help")
            .Append("install")
        });
});
```
