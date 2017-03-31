---
title: How to debug a Cake file using Visual Studio Code
category: How To's
author: mholo65
---

As you might already know, debugging Cake scripts using Visual Studio has been supported since the v0.12.0 release of Cake. But since the v0.16.1 release of Cake, thanks to [porting Cake to .NET Core](https://github.com/cake-build/cake/issues/1015), it is now also possible to debug Cake files using Visual Studio Code.

### What does this mean?

This means that while creating/modifying your Cake build script, you can use Visual Studio Code to step into the Cake file and get full debug support regardless of which operating system you are running (Windows / Linux / Mac).

### How does this work?

In order to enable debugging of a Cake file using Visual Studio Code, follow these steps:

1. Install [Cake.CoreCLR](https://www.nuget.org/packages/Cake.CoreCLR) NuGet package to your `tools` folder
1. Install [Cake Extension](https://marketplace.visualstudio.com/items?itemName=cake-build.cake-vscode) for Visual Studio Code
1. Set up .NET Core debugger in Visual Studio Code. See [http://aka.ms/vscclrdebugger](http://aka.ms/vscclrdebugger) for details
1. Open the directory containing your Cake files in Visual Studio Code 
1. Create file `.vscode/launch.json` and add the following content (assuming your Cake file is `build.cake`)

```json
{
        "version": "0.2.0",
        "configurations": [
            {
                "name": ".NET Core Launch (console)",
                "type": "coreclr",
                "request": "launch",
                "program": "${workspaceRoot}/tools/Cake.CoreCLR/Cake.dll",
                "args": [
                    "${workspaceRoot}/build.cake",
                    "--debug",
                    "--verbosity=diagnostic"
                ],
                "cwd": "${workspaceRoot}",
                "stopAtEntry": true,
                "externalConsole": false
            }
        ]
}
```
1. Open your Cake file and add a breakpoint by hitting `F9`
1. Hit `F5` to start debugging
<br/>![Debugging](/assets/img/debugging-cake-file-vscode/debugging.png)

### Alternative Approach

Another way to enable debugging of a Cake script is to start Cake from the commandline and then attach the debugger from within Visual Studio Code. This can be done using the following steps:

1. Add the following `configuration` to the `.vscode/launch.json` file that you created in the previous steps
```json
{
        "name": ".NET Core Attach",
        "type": "coreclr",
        "request": "attach",
        "processId": "${command.pickProcess}"
}
```
1. Open your Cake file and add a breakpoint by hitting `F9`
1. Select the `configuration` you added in the previous step
<br/>![Select config](/assets/img/debugging-cake-file-vscode/select_config.png)
1. Open terminal and run
<br/><pre><code class="bash">dotnet tools/Cake.CoreCLR/Cake.dll build.cake --debug</code></pre>
1. Hit `F5` and select the process from the drop-down list
<br/>![Select process](/assets/img/debugging-cake-file-vscode/select_process.png)

### Example Code

A working example can be found [here](https://github.com/mholo65/cake-vscode-debug-example)
