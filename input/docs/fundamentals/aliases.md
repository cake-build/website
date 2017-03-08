Order: 80
Title: Script Aliases
---

Cake supports something called script aliases. Script aliases are convenience methods that are easily accessible directly from a Cake script. Every single [DSL method](/dsl) in Cake is implemented like an alias method.

```csharp
Task("Clean")
    .Does(() =>
{
    // Delete a file.
    DeleteFile("./file.txt");

    // Clean a directory.
    CleanDirectory("./temp");
});
```

# Creating an alias

Start by creating a new class library project and add a reference to the [Cake.Core](/api/Cake.Core/) NuGet package via the package manager.

```powershell
PM> Install-Package Cake.Core
```

Add the alias method that you want to expose to your Cake script. A script alias method is simply an extension method for [ICakeContext](/api/Cake.Core/ICakeContext) that's been marked with the [CakeMethodAliasAttribute](/api/Cake.Core.Annotations/CakeMethodAliasAttribute) attribute.  The method can make use of any standard CLR features, including optional parameters.

You could also add a script alias property, which works the same way as a script alias method, except that it accepts no arguments and is marked with the [CakePropertyAliasAttribute](/api/Cake.Core.Annotations/CakePropertyAliasAttribute) attribute.

```csharp
using Cake.Core;
using Cake.Core.Annotations;

public static class MyCakeExtension
{
    [CakeMethodAlias]
    public static int GetMagicNumber(this ICakeContext context, bool value)
    {
        return value ? int.MinValue : int.MaxValue;
    }

    [CakeMethodAlias]
    public static int GetMagicNumberOrDefault(this ICakeContext context, bool value, Func<int> defaultValueProvider = null)
    {
        if (value)
        {
            return int.MinValue;
        }

        return defaultValueProvider == null ? int.MaxValue : defaultValueProvider();
    }

    [CakePropertyAlias]
    public static int TheAnswerToLife(this ICakeContext context)
    {
        return 42;
    }
}
```

# Using the alias

Compile the assembly and add a reference to it in the build script via the `#r` directive.

```csharp
#r "tools/MyCakeExtension.dll"
```

Now you should be able to call the method from the script.

```csharp
Task("GetSomeAnswers")
    .Does(() =>
{
    // Write the values to the console.
    Information("Magic number: {0}", GetMagicNumber(false));
    Information("The answer to life: {0}", TheAnswerToLife);
});
```
