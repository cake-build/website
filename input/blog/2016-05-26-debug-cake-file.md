---
title: How to debug a Cake file
category: How To's
author: gep13
---

As you might have noticed, as part of the v0.12.0 release of Cake, thanks to a [community contribution](https://github.com/cake-build/cake/pull/858) from [mholo65](https://github.com/mholo65), Cake now has debug support for Cake files.

### What does this mean?

This means that while creating/modifying your Cake build script, you can use Visual Studio to step into the Cake file and get full debug support, in the same way as if you were writing something like a Windows Forms Application.  This includes things like:

* Breakpoints
* Watches
* Immediate Window
* etc.

### How does this work?

In order to enable debugging of a Cake file, follow these steps:

1. Launch cake.exe with `-debug` flag
1. Process will stop prior to executing script and report `Attach debugger to process xxx to continue`
<br/>![Pass Debug Flag](/assets/img/debugging-cake-file/pass_debug_flag.png)
1. Open Visual Studio, open the cake file, add a breakpoint
1. Open Debug > Attach to Process...
<br/>![Attach To Process](/assets/img/debugging-cake-file/attach_to_process.png)
1. Attach to process with PID reported in step 2.
<br/>![Find Process Id](/assets/img/debugging-cake-file/find_process_id.png)
1. The execution of the Cake script will then continue, and your breakpoints will be hit
<br/>![Hit Breakpoint](/assets/img/debugging-cake-file/hit_breakpoint.png)

### Alternative Approach

Another way to enable debugging of a Cake script is to directly debug the Cake Source Code, passing in the location of the Cake file that you would like to debug.  This can be done using the following steps:

1. Clone the Cake project from GitHub
1. Open the Cake solution file `src/Cake.sln`
1. Modify the project properties to specify the build.cake file that you would like to debug
<br/>![Hit Breakpoint](/assets/img/debugging-cake-file/project_properties.png)
1. Start debugging by pushing F5, or clicking the green play button

### break preprocessor directive

In addition to directly setting breakpoints, it is also possible to place a `#break` pre-processor directive at the location where you want the debugger to break in your script.  At runtime, this `#break` directive will be replaced by `System.Diagnostics.Debugger.Break();` which will cause the debugger to stop at that location.

![Using Break Directive](/assets/img/debugging-cake-file/using_break_directive.png)

**NOTE:** If the debugger is not currently attached, then the `#break` directive is simply ignored.

### Gotchas!

1. In order to allow debugging of Cake Scripts, Visual Studio might need to run in elevated mode.  If you attempt to debug a Cake script and elevation is required, you will see the following message appear.
<br/>![Elevated Permissions](/assets/img/debugging-cake-file/elevated_permissions.png)
1. Sometimes VS will fail and report "Source not found", if this is the case, close the open Cake file and then from the call stack window right click on the first row and select "Go to source". This should open the source file and everything is fine.
<br/>![Source Not Found](/assets/img/debugging-cake-file/source_not_found.png)
1. Sometimes VS will display an "opened by another project" message box.  Simply click OK
<br/>![Source Not Found](/assets/img/debugging-cake-file/opened_by_another_project.png)

### Known Issues

1. Debugging is currently not supported when using mono