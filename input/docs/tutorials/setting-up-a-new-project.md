Order: 20
---

This guide will show you how to setup Cake for a new project.

# 1. Install the bootstrapper

The bootstrapper is used to download Cake and the tools required by the
build script. This is (kind of) an optional step, but recommended since
it removes the need to store binaries in the source code repository.

We install the bootstrapper by downloading it from the [Cake Resources
repository](https://github.com/cake-build/resources).

## Windows

Open a new PowerShell window and run the following command.

```powershell
Invoke-WebRequest https://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1
```

## Linux

Open a new shell and run the following command.

```bash
curl -Lsfo build.sh https://cakebuild.net/download/bootstrapper/linux
```

## OS X

Open a new shell and run the following command.

```bash
curl -Lsfo build.sh https://cakebuild.net/download/bootstrapper/osx
```

# 2. Create a Cake script

Add a cake script called `build.cake` to the same location as the
bootstrapper script that you downloaded.

```csharp
var target = Argument("target", "Default");

Task("Default")
  .Does(() =>
{
  Information("Hello World!");
});

RunTarget(target);
```

# 3. Run the Cake script

Now you should be able to run your Cake script by invoking the bootstrapper.

## Windows

```powershell
./build.ps1
```

If script execution fail due to the execution policy, you might have to
tell PowerShell to allow running scripts. You do this by
[changing the execution policy](https://technet.microsoft.com/en-us/library/ee176961.aspx).

## Linux/macOS

To be able to execute the bash script on Linux or macOS you should
give the owner of the script permission to execute it.

```bash
chmod +x build.sh
```

When this has been done, you should be able to run your Cake script
by invoking the bootstrapper.

```bash
./build.sh
```

## Note

If you are downloading the `build.sh` file on a Windows machine (for example, you are using something like [Travis CI](https://travis-ci.org/) to run your Linux/OS builds) you can give the script permission to execute using the following command:

```bash
git update-index --add --chmod=+x build.sh
```

Obviously, this assumes that you have the git command line tool installed.
