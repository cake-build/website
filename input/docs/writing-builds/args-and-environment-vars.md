Order: 40
Title: Arguments And Environment Variables
Excerpt: How to work with arguments and environment variables
RedirectFrom: docs/fundamentals/args-and-environment-vars
---

This page explains how settings can be passed to Cake file.

# Passing And Reading Arguments

Call the [Argument alias](/dsl/arguments/) in your Cake file to read arguments from the command line:

```csharp
Argument<bool>("myargument", false);
```

The argument can be passed while running Cake:

```powershell
--myargument=true
```

:::{.alert .alert-info}
The conversion uses [type converters](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.typeconverter) under the hood to convert the string value to the desired type.
:::

# Reading Environment Variables


Call the [Environment variables alias](/dsl/environment/#Environment-Variables) alias in your Cake file to get the environment variable.

The following example will read the value of the `BUILD_NUMBER` environment variable or return `42` if the environment variable is not defined:

```csharp
Information(EnvironmentVariable<int>("BUILD_NUMBER", 42));
```