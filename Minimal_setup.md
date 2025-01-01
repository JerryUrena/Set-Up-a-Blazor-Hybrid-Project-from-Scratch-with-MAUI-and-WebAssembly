# Set Up Your Shared Library for Blazor Hybrid

## Overview

To simplify our development process, the goal is to set up a clean, minimal shared library that allows you to **code once and deploy everywhere**. This setup ensures that unnecessary components and files are removed, leaving you with an empty project.

Since .NET does not currently provide a blank template for Blazor Hybrid projects, this cleanup process creates a minimal starting point for your application.

---

## Clean Up

### 1. Clean Up `MyBlazorHybrid.Web`
This project will serve as the Web component of your Blazor Hybrid app. Follow these steps to remove unused files and directories:

- Navigate to the `MyBlazorHybrid.Web` project.
- Delete the following items:
  - **`Layout` directory**: This folder is unused for this setup.
  - **`Pages` directory**: Remove the default pages that are no longer needed.
  - **`App.razor` component**: Delete the `App.razor` file as it will be replaced later.
  - **`wwwroot` contents**: Delete everything inside the `wwwroot` directory except `index.html`. Keep `index.html` for the app's entry point.

### 2. Clean Up the Shared Library
This project will serve as the shared codebase for both the MAUI and Web apps. Here’s how to clean it:

- Navigate to the shared library project.
- Delete the following:
  - **`Component1.razor`**: Remove the default Razor component.
  - **`Component1.razor.css`**: Remove the associated CSS file for the component.
  - **`ExampleJsInterop.cs`**: Delete the default interop class.
  - **`wwwroot` contents**: Delete everything from the `wwwroot` directory.

### 3. Clean Up `MyBlazorHybrid.MAUI`
This project serves as the MAUI component of your app. Clean it up by removing unnecessary files:

- Navigate to the `MyBlazorHybrid.MAUI` project.
- Delete the following:
  - **CSS folder in `wwwroot`**: Remove the `wwwroot/css` folder as it is not needed.

---

# Setting Up Views and Linking Shared Library in Blazor Hybrid

This guide will help you set up your **shared library** with views, styles, and routing, and integrate it into your **Blazor** and **MAUI** projects. The goal is to streamline the development process by using a shared library for components, CSS, and routing logic.

---

## Views Setup

### 1. Create the Views in the Shared Library
1. Go to the **library project** `MyBlazorHybridApp`.
2. Create a new folder named `Views`.
3. Inside the `Views` folder, create the following files:
   - `Home.razor`
   - `MyLayout.razor`
   - `NotFoundView.razor`

4. Add the following code to each file:

#### `Home.razor`
```razor
@page "/"
@layout MyLayout

<h1>Hello, world!</h1>
```

#### `NotFoundView.razor`
```razor
<h1>404 error</h1>
```

#### `MyLayout.razor`
```razor
@inherits LayoutComponentBase

<div class="main-layout">
    <header>My App Header</header>
    <main>
        @Body
    </main>
    <footer>Footer Content</footer>
</div>
```

Now you have the basic view components set up in the shared library.

---

## CSS Setup

### 1. Create CSS Folder and Files
1. Navigate to the **`wwwroot`** folder in the shared library.
   - If the `wwwroot` folder is hidden, it may be empty. Use the file explorer to create it.
2. Inside `wwwroot`, create the following:
   - A folder named `css`.
   - Inside the `css` folder, create a file named `app.css`.
3. Create another folder in `wwwroot` named `images` for storing app assets like icons.

### 2. Add Default CSS
Add the following code to `wwwroot/css/app.css`:

```css
/* Default Loading styles */
.loading-progress {
    position: relative;
    display: block;
    width: 8rem;
    height: 8rem;
    margin: 20vh auto 1rem auto;
}

.loading-progress circle {
    fill: none;
    stroke: #e0e0e0;
    stroke-width: 0.6rem;
    transform-origin: 50% 50%;
    transform: rotate(-90deg);
}

.loading-progress circle:last-child {
    stroke: #1b6ec2;
    stroke-dasharray: calc(3.141 * var(--blazor-load-percentage, 0%) * 0.8), 500%;
    transition: stroke-dasharray 0.05s ease-in-out;
}

.loading-progress-text {
    position: absolute;
    text-align: center;
    font-weight: bold;
    inset: calc(20vh + 3.25rem) 0 auto 0.2rem;
}

.loading-progress-text:after {
    content: var(--blazor-load-percentage-text, "Loading");
}

/* Default Error styles */
#blazor-error-ui {
    color-scheme: light only;
    background: lightyellow;
    bottom: 0;
    box-shadow: 0 -1px 2px rgba(0, 0, 0, 0.2);
    box-sizing: border-box;
    display: none;
    left: 0;
    padding: 0.6rem 1.25rem 0.7rem 1.25rem;
    position: fixed;
    width: 100%;
    z-index: 1000;
}

#blazor-error-ui .dismiss {
    cursor: pointer;
    position: absolute;
    right: 0.75rem;
    top: 0.5rem;
}
```

---

## Router Setup

### 1. Create Routing Files
In the shared library, create the following files:
- `AuthRouteView.cs`
- `MyApp.razor`
- `MyApp.razor.cs`

### 2. Add Code to the Routing Files

#### `AuthRouteView.cs`
```cs
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace MyBlazorHybridApp;

public class AuthRouteView : RouteView
{
    [Parameter][EditorRequired] 
    public MyApp MyAppComponent { get; set; } = default!;

    protected override void Render(RenderTreeBuilder builder)
    {
        MyAppComponent.CurrentPage = RouteData.PageType;
        base.Render(builder);
    }
}
```

#### `MyApp.razor`
```razor
<Router AppAssembly="@typeof(MyApp).Assembly">
    <Found Context="routeData">
        <AuthRouteView 
            RouteData="@routeData"
            MyAppComponent="this"/>
    </Found>

    <NotFound>
        <NotFoundView />
    </NotFound>
</Router>
```

#### `MyApp.razor.cs`
```cs
namespace MyBlazorHybridApp;

public partial class MyApp : IDisposable
{
    public Type? CurrentPage { get; set; }

    public void Dispose() { }
}
```

---

### 3. Update `_Imports.razor`
Replace the content of `_Imports.razor` in the shared library with:
```razor
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Routing
@using MyBlazorHybridApp.Views;
```

Now you have a basic shared library component with routing and views.

---

## Linking Shared Library to Blazor and MAUI Projects

### 1. Link to the Blazor Project (`MyBlazorHybridApp.Web`)
1. Open `wwwroot/index.html` and replace the `<head>` section with:
```html
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>MyBlazorHybridApp.Web</title>
    <base href="/" />
    <link rel="icon" type="image/png" href="_content/MyBlazorHybridApp/images/favicon.png" />
    <link rel="stylesheet" href="_content/MyBlazorHybridApp/css/app.css" />
</head>
```

2. Open `_Imports.razor` and remove the line:
```razor
@using MyBlazorHybridApp.Web.Layout
```

3. Replace the content of `Program.cs` with:
```cs
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorHybridApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<MyApp>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
```

This links the shared library to the Blazor project.

---

### 2. Link to the MAUI Project (`MyBlazorHybridApp.MAUI`)
1. Open `wwwroot/index.html` and replace the `<head>` section with:
```html
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no, viewport-fit=cover" />
    <title>MyBlazorHybridApp.MAUI</title>
    <base href="/" />
    <link rel="stylesheet" href="_content/MyBlazorHybridApp/css/app.css" />
    <link rel="icon" href="data:,">
</head>
```

2. Open `MainPage.xaml` and replace its content with:
```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:MyBlazorHybridApp.MAUI"
             x:Class="MyBlazorHybridApp.MAUI.MainPage"
             xmlns:myapp="clr-namespace:MyBlazorHybridApp;assembly=MyBlazorHybridApp">

    <BlazorWebView x:Name="blazorWebView" HostPage="wwwroot/index.html">
        <BlazorWebView.RootComponents>
            <RootComponent Selector="#app" ComponentType="{x:Type myapp:MyApp}" />
        </BlazorWebView.RootComponents>
    </BlazorWebView>

</ContentPage>
```

This links the shared library to the MAUI project.

---

## Summary

This setup involves cleaning unnecessary files from the Blazor Hybrid projects (Web, MAUI, and shared library) to create a minimal foundation. Key steps include setting up shared views (Home.razor, MyLayout.razor, NotFoundView.razor), centralizing styles in app.css, configuring routing with AuthRouteView and MyApp, and linking the shared library to both Blazor and MAUI projects for unified development.
