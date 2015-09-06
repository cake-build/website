---
content-type: markdown
---

This guide will show you how to setup Cake for a new project.

### 1. Install the bootstrapper

The bootstrapper is used to download Cake and the tools required 
by the build script. This is an optional step, but recommended since 
it removes the need to store binaries in the source code repository.

#### Windows

Open a PowerShell and run the following line.

```powershell
PS> Invoke-WebRequest http://cakebuild.net/bootstrapper/windows -OutFile ./build.ps1
```

#### Linux

Open a bash shell and run the following line.

```shell
curl -Lsfo ./build.sh http://cakebuild.net/bootstrapper/linux
```

#### OS X

Open a bash shell and run the following line.

```shell
curl -Lsfo ./build.sh http://cakebuild.net/bootstrapper/osx
```

### 2. Create a Cake script

Add a cake script called `build.cake` where you added your Cake script 
(preferably in the root folder of your source code repository).

```csharp
var target = Argument<string>("target", "Default");

Task("Default")
  .Does(() =>
{
  Information("Hello World!");
});

RunTarget(target);
```

### 3. Run the Cake script

To be able to execute the bash script on Linux or OS X you should 
give the owner of the script permission to execute it.

```shell
chmod 100 ./build.sh
```

Now you should be able to run your Cake script by invoking the bootstrapper.