Order: 50
Title: Shebang directive
Description: Directive to define the interpreter for Unix-like systems
---

Under Unix-like operating systems, when a script with a shebang is run as a program, the program loader parses the rest of the script's initial line as an interpreter directive; the specified interpreter program is run instead, passing to it as an argument the path that was initially used when attempting to run the script.

Support for `#!` directives applies on Unix platforms only.

# Usage

Add a shebang at the top of your file:

```bash
#!/usr/local/share/dotnet/dotnet
```

or

```bash
#!/usr/bin/env dotnet
```

## Make file executable

After adding the shebang, make the file executable:

```bash
chmod +x cake.cs
```

## Run directly

You can then run the file directly:

```bash
./cake.cs
```

# Example

```csharp
#!/usr/bin/env dotnet
#:sdk Cake.Sdk

Information("Hello, {0}!", "World");
```

# See also

- [File-based apps - .NET | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/sdk/file-based-apps)
