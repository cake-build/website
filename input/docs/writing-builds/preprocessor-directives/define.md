Order: 80
Title: Define directive
Excerpt: Directive for conditional code execution
---

The define directive - `#define` creates a symbol, it can be later use to decide, what Cake will execute. You use it like `#define` in C#.
To check if symbol is defined, you have to use directives `#if` ... `#else` ... `#endif`

# Usage

```csharp
#define FOO

#if (FOO)
    // code in this branch will execute
#else
    // code in this branch will be skipped
#endif
```

# Predefined symbols

Some symbols can be predefined by Cake depending on the environment.

| Symbol Name      | Description                                                                          | Available Since                                                    |
| ---------------- | ------------------------------------------------------------------------------------ | -------------------------------------------------------------------|
| `CAKE`           | Always defined for code executed by Cake                                             | [v0.35.0](https://github.com/cake-build/cake/releases/tag/v0.35.0) |
| `NETCOREAPP`     | Defined when the CLR is .NET Core                                                    | [v0.35.0](https://github.com/cake-build/cake/releases/tag/v0.35.0) |
| `NETFRAMEWORK`   | Defined when the CLR is .NET Framework                                               | [v0.35.0](https://github.com/cake-build/cake/releases/tag/v0.35.0) |
| `NET461`         | Defined when the runtime framework is .NET Framework 4.6.1                           | [v0.35.0](https://github.com/cake-build/cake/releases/tag/v0.35.0) |
| `NETCOREAPP2_0`  | Defined when the runtime framework is .NET Core 2.0                                  | [v0.35.0](https://github.com/cake-build/cake/releases/tag/v0.35.0) |
| `NETCOREAPP2_1`  | Defined when the runtime framework is .NET Core 2.1                                  | [v0.35.0](https://github.com/cake-build/cake/releases/tag/v0.35.0) |
| `NETCOREAPP2_2`  | Defined when the runtime framework is .NET Core 2.2                                  | [v0.35.0](https://github.com/cake-build/cake/releases/tag/v0.35.0) |
| `NETCOREAPP3_0`  | Defined when the runtime framework is .NET Core 3.0                                  | [v0.35.0](https://github.com/cake-build/cake/releases/tag/v0.35.0) |
| `NETCOREAPP3_1`  | Defined when the runtime framework is .NET Core 3.1                                  | [v0.35.0](https://github.com/cake-build/cake/releases/tag/v0.35.0) |
| `NET5_0`         | Defined when the runtime framework is .NET 5                                         | [v1.0.0](https://github.com/cake-build/cake/releases/tag/v1.0.0)   |
| `NETSTANDARD2_0` | Defined when the runtime could not be determined but is .NET Standard 2.0 compatible | [v0.35.0](https://github.com/cake-build/cake/releases/tag/v0.35.0) |