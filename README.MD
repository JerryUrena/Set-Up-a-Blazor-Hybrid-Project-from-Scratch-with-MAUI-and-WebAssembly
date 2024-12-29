# How to Set Up a Blazor Hybrid Project from Scratch with MAUI and WebAssembly

## Overview
This tutorial will guide you through setting up a Blazor Hybrid project from scratch, integrating MAUI for desktop applications and Blazor WebAssembly for web applications. The setup also includes an optional ASP.NET Core server project for backend API services.

## Environment Setup
1. **Install .NET SDK**:
   - Download and install the latest version of .NET SDK from [here](https://dotnet.microsoft.com/en-us/download/dotnet).

2. **Prepare a Directory**:
   - Open Command Prompt (`CMD`) and navigate to your desired directory:
   ```bash
   cd Downloads
   ```
   - Create a new directory for your solution:
   ```bash
   mkdir MyBlazorHybridApp
   ```
   - Navigate into the new directory:
   ```bash
   cd MyBlazorHybridApp
   ```

## Create Projects
1. **Shared Library**:
   - Create a Razor Class Library (RCL) for shared Razor components:
   ```bash
   dotnet new razorclasslib -n MyBlazorHybridApp
   ```

2. **Blazor MAUI App**:
   - Create a Blazor MAUI project for cross-platform desktop applications:
   ```bash
   dotnet new maui-blazor -n MyBlazorHybridApp.MAUI
   ```

3. **Blazor WebAssembly App**:
   - Create a Blazor WebAssembly project for web applications:
   ```bash
   dotnet new blazorwasm -n MyBlazorHybridApp.Web
   ```

4. **ASP.NET Core Server App (Optional)**:
   - Create an ASP.NET Core Web API project for backend services:
   ```bash
   dotnet new webapi -n MyBlazorHybridApp.Server
   ```

## Configure the Shared Library
1. **Navigate to the Shared Library**:
   - Change to the shared library directory:
   ```bash
   cd MyBlazorHybridApp
   ```

2. **Add References**:
   - Add the shared library to other projects:
   ```bash
   # Add to MAUI project
   dotnet add ../MyBlazorHybridApp.MAUI/MyBlazorHybridApp.MAUI.csproj reference MyBlazorHybridApp.csproj

   # Add to Web project
   dotnet add ../MyBlazorHybridApp.Web/MyBlazorHybridApp.Web.csproj reference MyBlazorHybridApp.csproj
   ```

3. **Return to the Root Directory**:
   ```bash
   cd ..
   ```

## Configure Workloads
1. **List Installed Workloads**:
   - Check for installed .NET workloads:
   ```bash
   dotnet workload list
   ```

2. **Install Missing Workloads**:
   - Install workloads based on your target environments:
   ```bash
   dotnet workload install android
   dotnet workload install ios
   dotnet workload install maccatalyst
   dotnet workload install maui-windows
   dotnet workload install wasm-tools
   ```

## Create Solution Files
1. **General Solution File**:
   - Create a solution file and add projects:
   ```bash
   dotnet new sln -n MyBlazorHybridApp
   dotnet sln MyBlazorHybridApp.sln add MyBlazorHybridApp.MAUI/MyBlazorHybridApp.MAUI.csproj
   dotnet sln MyBlazorHybridApp.sln add MyBlazorHybridApp.Web/MyBlazorHybridApp.Web.csproj
   dotnet sln MyBlazorHybridApp.sln add MyBlazorHybridApp/MyBlazorHybridApp.csproj

   # Optional: Add server project if created
   dotnet sln MyBlazorHybridApp.sln add MyBlazorHybridApp.Server/MyBlazorHybridApp.Server.csproj
   ```

## Running the Applications
1. **Run the MAUI Desktop App**:
   ```bash
   dotnet run --project MyBlazorHybridApp.MAUI --framework net9.0-windows10.0.19041.0
   ```

2. **Run the Blazor WebAssembly App**:
   ```bash
   dotnet run --project MyBlazorHybridApp.Web
   ```

3. **Run the Server App** (if created):
   ```bash
   dotnet run --project MyBlazorHybridApp.Server
   ```

## Create a .gitignore File
1. **Generate a Basic .gitignore**:
   - Create an empty `.gitignore` file:
   ```bash
   echo. > .gitignore
   ```

2. **Use Recommended .gitignore**:
   - Copy and paste the recommended `.gitignore` from the [official .NET MAUI repository](https://github.com/dotnet/maui/blob/main/.gitignore).
