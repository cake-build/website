Order: 10
---

Cake is a build automation system with a C# DSL to do things like compiling code, copy files/folders, running unit tests, compress files and build NuGet packages.

Cake uses a [dependency based programming model](http://martinfowler.com/articles/rake.html#DependencyBasedProgramming) just like Rake, FAKE and similar build automation systems where you declare tasks and the dependencies between those.

# Supported functionality

* MSBuild
* Build systems
  * AppVeyor
  * Bamboo
  * Continua CI
  * Jenkins
  * MyGet
  * TeamCity
  * Gitlab CI
* AssemblyInfo patching
* Release notes parsing
* Unit Test Runners
  * xUnit (v1 and v2)
  * NUnit
  * MSTest
* NuGet
  * Install
  * Pack
  * Push
  * Restore
  * SetApiKey
  * Sources
  * Update
* ILMerge
* WiX
* NSIS
* SignTool
* Octopus Deploy
* Compression (Zip)
* SpecFlow
* File hash calculation (MD5, SHA256, SHA512)

Cake also contains functionality to conveniently work with file system paths as well as performing common file/directory/environment operations, manipulating XML, parsing Visual Studio solutions, starting processes and more.
