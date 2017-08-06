---
title: How to debug a Cake addin
category: How To's
author: RLittlesII
---

It's no secret, I like Cake.  When I have to write a script to build my source code, I use Cake.  On my most recent expedition writing a Cake script to publish a UWP application, I noticed Hockey App publish failed, [Cake.HockeyApp](https://github.com/cake-contrib/Cake.HockeyApp) was throwing an exception.

When I realized it wasn't the way my script interacted with Cake, but an issue with the addin itself, I immediately hit the Cake Gitter channel. I was in search of some documentation that allowed me to debug a Cake addin so I could address the issue that I was having Friday afternoon at half past quitting time.

One of the members of the project team suggested the following:

> build your addin in debug, reference it from Cake script and attach to process from VS.
> Start Cake.exe with `--debug` flag so you have time to attach to process

Now that I know where to start I created myself a task list:

1. Change build.ps1 to run with --debug flag
2. Build addin source
3. Load addin from source in build.cake
4. Find the error and fix it!

For the purposes of this post, I will cover steps 1 and 3.  Step 2 is no more than building the addin source code in Visual Studio.

# Changing the bootstrapper

This is the easiest part.  I modified the `Invoke-Expression` line of the bootstrapper to resemble the following.

**NOTE**: We should revert this before we commit.

``` powershell
Invoke-Expression "& `"$CAKE_EXE`" `"$Script`" -target=`"$Target`" -configuration=`"$Configuration`" -verbosity=`"$Verbosity`" $UseMono $UseDryRun $UseExperimental $ScriptArgs --debug"
```

And on the next execution of `.\build.ps1`

``` powershell
Preparing to run build script...
Running build script...
Attach debugger to process 80276 to continue
```

# Loading the addin from source

In the build.cake file I add the following [preprocessor directive](https://cakebuild.net/docs/fundamentals/preprocessor-directives)

``` csharp
#r "./Cake.HockeyApp/src/Cake.HockeyApp/bin/Debug/net45/Cake.HockeyApp.dll"
```

I navigate back to Visual Studio Debug -> Attach to Process (or Ctrl + Alt + P), pick the Cake execution with the corresponding process id and I hit break points!!!

![Attach To Process](https://raw.githubusercontent.com/RLittlesII/rodneylittlesii/master/src/images/Attach-To-Process.jpg)

Now I can debug the issue I am seeing, determine if it is user error or the addin, and resolve it either way!