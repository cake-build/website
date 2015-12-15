---
content-type: markdown
---

This guide will show you how to setup Cake for a new project.

### 1. Install the bootstrapper

The bootstrapper is used to download Cake and the tools required by the
build script. This is (kind of) an optional step, but recommended since
it removes the need to store binaries in the source code repository.

We install the bootstrapper by downloading it from the Cake bootstrapper
repository.

#### Windows

Open a new PowerShell window and run the following command.

```powershell
Invoke-WebRequest http://cakebuild.net/bootstrapper/windows -OutFile build.ps1
```

#### Linux

Open a new shell and run the following command.

```bash
curl -Lsfo build.sh http://cakebuild.net/bootstrapper/linux
```

#### OS X

Open a new shell and run the following command.

```bash
curl -Lsfo build.sh http://cakebuild.net/bootstrapper/osx
```

### 2. Create a Cake script

Add a cake script called `build.cake` to the same location as the
bootstrapper script that you downloaded. If you want a script that
actually do something, check out the
[minimal, convention based cake script](https://github.com/cake-build/bootstrapper/blob/master/res/scripts/build.cake)
available in the bootstrapper repository.

```csharp
var target = Argument("target", "Default");

Task("Default")
  .Does(() =>
{
  Information("Hello World!");
});

RunTarget(target);
```

### 3. Run the Cake script

Now you should be able to run your Cake script by invoking the bootstrapper.

#### Windows

```powershell
./build.ps1
```

If script execution fail due to the execution policy, you might have to
tell PowerShell to allow running scripts. You do this by
[changing the execution policy](https://technet.microsoft.com/en-us/library/ee176961.aspx).

#### Linux/OS X

To be able to execute the bash script on Linux or OS X you should
give the owner of the script permission to execute it.

```bash
chmod +x build.sh
```

When this have been done, you should be able to run your Cake script
by invoking the bootstrapper.

```bash
./build.sh
```