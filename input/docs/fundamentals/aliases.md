Order: 10
Title: Script Aliases
Description: Convenience methods for Cake scripts
---

Cake supports something called script aliases. Script aliases are convenience methods that are easily accessible directly from a Cake script. Every single [DSL method](/dsl) in Cake is implemented like an alias method.

:::{.alert .alert-info}
See [Reference](/dsl) for a list of available aliases.
:::

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

# Custom aliases

Additional aliases can be defined in [addins](../extending/addins).
