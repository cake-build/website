Order: 40
---

To pin the Cake version you will need to make sure that the *packages.config* file located in the *tools* directory is commited to your source control repository.

# Pinning Cake version
 To pin the Cake version you will need to make sure that the *packages.config* file located in the *tools* directory is commited to your source control repository. This will require some tweaks to your gitignore file. Below are the tweaks that will be required, the first line says to ignore all files underneath the tool directory and the second says to not ignore the packages.config file.

### gitignore
```
tools/*
!tools/packages.config
```

# Updating the Cake version after pinning
 To update the version of Cake you are using after you have pinned it, all you need to do is update the version in the packages.config file to the newer version you would like to use.

# Video

Further information on this topic can be seen in the following video:

<iframe width="640" height="360" src="https://www.youtube.com/embed/xq41JgaGN7k" frameborder="0" allowfullscreen></iframe>

# Pinning addin version

If addins are referenced through the *packages.config* file they can be pinned the same way as the Cake version.

If addins are referenced using the `#addin` preprocessor directive they can be pinned like this:

```
#addin nuget:?package=Cake.Foo&version=1.2.3
```

# Pinning tool version

If tools are referenced through the *packages.config* file they can be pinned the same way as the Cake version.

If tools are referenced using the `#tool` preprocessor directive they can be pinned like this:

```
#tool nuget:?package=Tool.Foo&version=1.2.3
```
