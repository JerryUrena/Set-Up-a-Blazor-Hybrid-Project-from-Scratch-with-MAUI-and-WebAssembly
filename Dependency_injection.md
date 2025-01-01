# Setting Up Dependency Injection in the Shared Library

When working with a shared library, it is more efficient to define and extend dependency objects there rather than creating them separately in platform-specific projects such as .NET MAUI or Blazor. Unless a platform-specific service (e.g., for printing) is required, services should be defined in the shared library for better reusability and maintainability.

---

## Create the Service Classes
1. Navigate to the `MyBlazorHybridApp` project.
2. Create a new folder named `Services`.
3. Inside the `Services` folder, create a new file named `MyFirstService.cs`.
4. Add the following code to that class:

    ```csharp
    using System;

    namespace MyBlazorHybridApp.Services;

    public interface IMyFirstService
    {
    }

    public class MyFirstService : IMyFirstService
    {
    }
    ```


5. In the same folder, create another file named `MySecondService.cs`.
6. Add the following code:

    ```csharp
    using System;

    namespace MyBlazorHybridApp.Services;

    public interface IMySecondService
    {
    }

    public class MySecondService : IMySecondService
    {
    }
    ```

---

## Add Extension Methods for Dependency Injection
1. In the root directory of the `MyBlazorHybridApp` project, create a new file named `Extensions.cs`.
2. Copy and paste the following code into the file:

    ```csharp
    using Microsoft.Extensions.DependencyInjection;
    using MyBlazorHybridApp.Services;

    namespace MyBlazorHybridApp;

    public static class Extensions
    {
        public static IServiceCollection AddMyBlazorHybridAppServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMyFirstService, MyFirstService>()
                .AddSingleton<IMySecondService, MySecondService>();
        }
    }
    ```

This extension method allows you to register the services in a clean and reusable manner for both the .NET MAUI and Blazor projects.

---

## Consume the Services
1. In the `Views` directory, create a new file named `Home.razor.cs`.
2. Add the following code:

    ```csharp
    using Microsoft.AspNetCore.Components;
    using MyBlazorHybridApp.Services;

    namespace MyBlazorHybridApp.Views;

    public partial class Home
    {
        [Inject] 
        private MyFirstService MyFirstService { get; set; } = default!;
    }
    ```

> **Note:** This demonstrates how to inject dependencies using the `[Inject]` attribute.

---

## Using Constructor Injection
To demonstrate constructor injection, update `MyFirstService` to depend on `IMySecondService`.

1. Open `MyFirstService.cs`.
2. Modify the code as follows:

    ```csharp
    using System;

    namespace MyBlazorHybridApp.Services;

    public interface IMyFirstService
    {
    }

    public class MyFirstService : IMyFirstService
    {
        private readonly IMySecondService _mySecondService;

        public MyFirstService(IMySecondService mySecondService)
        {
            _mySecondService = mySecondService;
        }
    }
    ```

Alternatively, you can use a primary constructor (available in C# 9.0 and later):

    ```csharp
    public class MyFirstService(IMySecondService mySecondService) : IMyFirstService
    {
    }
    ```

Both approaches achieve the same result; choose the one that suits your project's style and requirements.

---

## Modify Blazor and Maui Program

Let's ensure that our Blazor and MAUI apps can access the shared dependency injection collection.

### Step 1: Modify `MyBlazorHybridApp.Web`
1. Navigate to the `MyBlazorHybridApp.Web` project.
2. Open the `Program.cs` file in the root directory.
3. Add `.MyBlazorHybridAppClient()` to the `builder.Services` configuration.

For reference, your `Program.cs` file should look like this:

```csharp
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorHybridApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<MyApp>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .MyBlazorHybridAppClient()
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
```

---

### Step 2: Modify `MyBlazorHybridApp.Maui`
1. Navigate to the `MyBlazorHybridApp.Maui` project.
2. Open the `MauiProgram.cs` file in the root directory.
3. Add `.MyBlazorHybridAppClient()` to the `builder.Services` configuration.

For reference, your `MauiProgram.cs` file should look like this:
```csharp
using Microsoft.Extensions.Logging;

namespace MyBlazorHybridApp.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // Services
        builder.Services.MyBlazorHybridAppClient();

        return builder.Build();
    }
}
```

---

If you've followed these steps, you've successfully configured your shared library for dependency injection. Your Blazor and MAUI projects are now integrated with the shared service collection. 🎉
