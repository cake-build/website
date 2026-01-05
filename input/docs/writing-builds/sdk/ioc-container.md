Order: 30
Title: IoC Container
Description: Register and resolve services using dependency injection
---

Cake.Sdk provides built-in support for dependency injection using the .NET IoC container. You can register services and resolve them in your tasks using the `IServiceCollection` and `ServiceProvider`.

# Registering services

Register services using the `RegisterServices` partial method. This method is automatically discovered and called during the build initialization.

## Basic service registration

```csharp
#:sdk Cake.Sdk

public static partial class Program
{
    static partial void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IMyService, MyService>();
    }
}
```

## Registering multiple services

You can register multiple services in the same method:

```csharp
static partial void RegisterServices(IServiceCollection services)
{
    services.AddSingleton<IMyService, MyService>();
    services.AddTransient<IAnotherService, AnotherService>();
    services.AddScoped<IDataService, DataService>();
}
```

## Service lifetime options

Cake.Sdk supports the standard .NET service lifetime options:

- `AddSingleton` - Creates a single instance for the entire build
- `AddScoped` - Creates an instance per scope (typically per task execution)
- `AddTransient` - Creates a new instance each time it's requested

# Resolving services

Resolve services in your tasks using the `ServiceProvider` property:

```csharp
Task("MyTask")
    .Does(() =>
    {
        var service = ServiceProvider.GetRequiredService<IMyService>();
        service.DoSomething();
    });
```

## Optional service resolution

Use `GetService` for optional dependencies that may not be registered:

```csharp
Task("MyTask")
    .Does(() =>
    {
        var service = ServiceProvider.GetService<IMyService>();
        if (service != null)
        {
            service.DoSomething();
        }
    });
```

# Registering tasks as dependencies

You can register tasks as dependencies using the IoC container. This is useful for dynamically creating tasks based on configuration or other services:

```csharp
public static partial class Program
{
    static partial void RegisterServices(IServiceCollection services)
    {
        // Register MyService
        services.AddSingleton<IMyService, MyService>();
        
        // Injects IOC-Task as a dependency of Build task
        services.AddSingleton(new Action<IScriptHost>(
            host => host.Task("IOC-Task")
                        .IsDependeeOf("Build")
                        .Does(() => Information("Hello from IOC-Task"))));
    }
}
```

# Example

Here's a complete example showing how to use the IoC container:

## Service interface and implementation

```csharp
public interface ILoggerService
{
    void LogInfo(string message);
    void LogError(string message);
}

public class LoggerService : ILoggerService
{
    public void LogInfo(string message)
    {
        Information($"INFO: {message}");
    }
    
    public void LogError(string message)
    {
        Error($"ERROR: {message}");
    }
}
```

## Registering and using the service

```csharp
#:sdk Cake.Sdk

public static partial class Program
{
    static partial void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, LoggerService>();
    }
}

var target = Argument("target", "Default");

Task("Default")
    .Does(() =>
    {
        var logger = ServiceProvider.GetRequiredService<ILoggerService>();
        logger.LogInfo("Hello from IoC Container!");
    });

RunTarget(target);
```

# Multi-file structure

When using a multi-file structure, you can organize your service registrations in separate files:

## Main file (build.cs)

```csharp
#:sdk Cake.Sdk
#:property IncludeAdditionalFiles=build/**/*.cs

var target = Argument("target", "Default");

Task("Default")
    .Does(() =>
    {
        var logger = ServiceProvider.GetRequiredService<ILoggerService>();
        logger.LogInfo("Hello from IoC Container!");
    });

RunTarget(target);
```

## IoC registration file (build/IoC.cs)

```csharp
public static partial class Program
{
    static partial void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddSingleton<IBuildService, BuildService>();
    }
}
```

## Service implementations (build/Services.cs)

```csharp
public interface ILoggerService
{
    void LogInfo(string message);
}

public class LoggerService : ILoggerService
{
    public void LogInfo(string message)
    {
        Information($"INFO: {message}");
    }
}
```

# See also

- [Additional files](/docs/writing-builds/sdk/preprocessor-directives/additional-files)
- [.NET Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
