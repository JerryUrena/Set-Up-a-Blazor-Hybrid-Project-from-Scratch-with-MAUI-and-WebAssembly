# Setting Up Visual Studio Code for Blazor Hybrid Apps

## Overview
If you are coding in Visual Studio Code and want a way to debug and manage your Blazor Hybrid app, you can use `.vscode` tasks and configurations. This setup includes configurations for launching and debugging both a MAUI app and a Blazor WebAssembly app.

---

## Requirements
   - Install Visual Studio Code from [here](https://code.visualstudio.com/)
   - Install the latest version of .NET SDK from [here](https://dotnet.microsoft.com/en-us/download/dotnet).
   - Install Google chrome [here](https://google.com/chrome)
   - Open VSCODE and go to the extensions tab and install the latest version of `.NET MAUI`.
   - Create or clone a Blazor Hybrid app [Clone](https://github.com/JerryUrena/Set-Up-a-Blazor-Hybrid-Project-from-Scratch-with-MAUI-and-WebAssembly)


## Setting Up `.vscode`

**Create Files**
- In your project’s root directory, create a folder named `.vscode`.
- Inside the `.vscode` folder, create the following files:
  - `launch.json`
  - `tasks.json`
  - `settings.json`

---

## File Contents

1. **launch.json**
This file defines the debugging configurations for Visual Studio Code. It includes setups for debugging both the MAUI app and the Blazor WebAssembly app.

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "MAUI:Debug",
            "type": "maui",
            "request": "launch",
            "preLaunchTask": "MAUI"
        },
        {
            "name": "Blazor:debug",
            "type": "blazorwasm",
            "request": "launch",
            "url": "http://localhost:${config:BLAZOR_PORT}",
            "browser": "chrome",
            "timeout": 60000
        }
    ]
}
```

---

2. **tasks.json**
This file contains tasks that automate common operations like building, watching, cleaning, and killing background processes.

```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "MAUI",
            "type": "shell",
            "command": "dotnet",
            "args": [
                "build",
                "${workspaceFolder}/${config:MAUI_PROJECT_NAME}",
                "--framework",
                "${config:MAUI_FRAMEWORK}"
            ],
            "problemMatcher": [
                "$msCompile"
            ]
        },
        {
            "label": "Blazor:watch",
            "type": "shell",
            "command": "dotnet",
            "args": [
                "watch",
                "--launch-profile",
                "${config:BLAZOR_PROFILE}",
                "--project",
                "${workspaceFolder}/${config:BLAZOR_PROJECT_NAME}",
                "--verbose"
            ],
            "problemMatcher": [
                "$msCompile"
            ],
            "isBackground": true
        },
        {
            "label": "Kill Background Tasks",
            "type": "shell",
            "command": "taskkill",
            "args": [
                "/F",
                "/IM",
                "dotnet.exe"
            ]
        },
        {
            "label": "Clean and Restore Cache",
            "type": "shell",
            "command": "powershell",
            "args": [
                "-Command",
                "dotnet restore; dotnet clean; dotnet nuget locals all --clear"
            ],
            "problemMatcher": []
        }
    ]
}
```

---

3. **settings.json**
This file defines reusable variables for tasks and debugging configurations. Update the values based on your project setup.

```json
{
    "MAUI_FRAMEWORK": "net9.0-windows10.0.19041.0",
    "MAUI_PROJECT_NAME": "MyBlazorHybridApp.MAUI",
    "BLAZOR_PROFILE": "http",
    "BLAZOR_PORT": 5236,
    "BLAZOR_PROJECT_NAME": "MyBlazorHybridApp.Web"
}
```

---

## How to Use This Setup in VS Code

1. **Build and Run the MAUI App**
- Open the **Command Palette** in VS Code (`Ctrl+Shift+P` or `Cmd+Shift+P` on macOS).
- Run the task labeled **"MAUI"**:
  - Navigate to `Terminal > Run Task > MAUI`.

2. **Debug the MAUI App**
- Open the Debug panel in VS Code (`Ctrl+Shift+D` or `Cmd+Shift+D` on macOS).
- Select the **"MAUI:Debug"** configuration.
- Click the **Start Debugging** button or press `F5`.

3 **Watch and Debug Blazor WebAssembly**
- Start the Blazor task:
  - Navigate to `Terminal > Run Task > Blazor:watch`.
- Select the **"Blazor:debug"** configuration in the Debug panel.
- Start debugging by pressing `F5`.

4. **Clean and Restore Cache**
- Run the task labeled **"Clean and Restore Cache"** if you encounter build or NuGet issues.

5. **Kill Background Tasks**
- If you need to terminate any lingering `dotnet` processes, run the **"Kill Background Tasks"** task.