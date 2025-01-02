
# How to Add NPM to Your Blazor App

## Overview
The sky is the limit! The idea is to have everything available. Since Blazor is relatively new, there aren’t many packages, but don’t worry. I will show you how to include NPM packages with your Blazor Hybrid app and make everything bulletproof by adding TypeScript. Since Microsoft does not natively support `.ts` or `.js` files, we’ll include that support. Microsoft will be jealous! 😄

## Requirement
- For the purpose of this tutorial let's clone this [repo](https://github.com/JerryUrena/Set-Up-a-Blazor-Hybrid-Project-from-Scratch-with-MAUI-and-WebAssembly/tree/Dependency-Injection) branch

## Setup

1. Navigate to `MyBlazorHybridApp` and create a new directory named `npm`.
2. Open a new terminal in that directory or `cd` into it.
3. Create a `package.json` file:
   ```bash
   npm init -y
   ```
4. Install necessary packages:
   ```bash
   npm install typescript ts-loader webpack webpack-cli webpack-dev-server babel-loader @babel/core @babel/preset-env --save-dev
   ```
5. Initialize a TypeScript configuration file:
   ```bash
   npx tsc --init
   ```
6. Create a new file named `webpack.config.js` and add the following content:
   ```javascript
   const path = require('path');
   const fs = require('fs');

   function findFiles(dir, extensions, fileList = []) {
       const files = fs.readdirSync(dir);
       files.forEach((file) => {
           const fullPath = path.join(dir, file);
           const stat = fs.statSync(fullPath);

           if (stat.isDirectory()) {
               findFiles(fullPath, extensions, fileList);
           } else if (extensions.includes(path.extname(file).toLowerCase())) {
               fileList.push(fullPath);
           }
       });
       return fileList;
   }

   module.exports = (env, argv) => {
       const mode = argv.mode || 'development';
       const entry = [
           ...findFiles(path.resolve(__dirname, '../Views'), ['.ts', '.js', '.razor.ts', '.razor.js']),
           './src/index.ts',
       ];

       return {
           mode,
           devtool: mode === 'development' ? 'source-map' : false,
           entry,
           output: {
               filename: 'index.bundle.js',
               path: path.resolve(__dirname, '../wwwroot/js'),
           },
           module: {
               rules: [
                   {
                       test: /\.ts$/,
                       use: 'ts-loader',
                       exclude: /node_modules/,
                   },
                   {
                       test: /\.js$/,
                       use: {
                           loader: 'babel-loader',
                           options: { presets: ['@babel/preset-env'] },
                       },
                       exclude: /node_modules/,
                   },
               ],
           },
           resolve: { extensions: ['.ts', '.js'] },
           devServer: {
               static: path.resolve(__dirname, '../wwwroot'),
               compress: true,
               port: 9000,
           },
       };
   };
   ```

7. Create a `src` directory inside the `npm` folder and add an `index.ts` file with the following content:
   ```javascript
   console.log("Hello from TypeScript");
   ```

8. Update the `tsconfig.json` file with the following:
   ```json
   {
       "compilerOptions": {
           "target": "es6",
           "module": "es6",
           "strict": true,
           "esModuleInterop": true,
           "forceConsistentCasingInFileNames": true,
           "skipLibCheck": true,
           "sourceMap": true
       },
       "include": ["src/**/*.ts"],
       "exclude": ["node_modules"]
   }
   ```

9. Add the following scripts to `package.json`:
   ```json
   "scripts": {
       "build": "webpack --mode production",
       "build-dev": "webpack --mode development",
       "debug": "webpack serve"
   },
   ```

## Consuming the Script

1. Add the `index.bundle.js` script to your app:
   - For `BlazorHybridApp.Web`, add the following to `wwwroot/index.html`:
     ```html
     <script src="_content/MyBlazorHybridApp/js/index.bundle.js"></script>
     ```
   - For `BlazorHybridApp.Maui`, add the same script to `wwwroot/index.html`.

2. Modify the `MyBlazorHybridApp.csproj` file to automate the build process:
   ```xml
   <Target Name="NpmBuild" AfterTargets="Compile">
       <Exec Command="npm install" WorkingDirectory="npm" />
       <Exec Command="npm run build-dev" WorkingDirectory="npm" Condition="'$(Configuration)' == 'Debug'" />
       <Exec Command="npm run build" WorkingDirectory="npm" Condition="'$(Configuration)' == 'Release'" />
   </Target>
   ```

## Testing

1. Add a new file named `Home.razor.js` in the `Views` directory with the following content:
   ```javascript
   // Show alert message
   window.showAlert = (message) => {
       alert(message);
   };
   ```

2. Update the `home.razor.cs` file:
   ```csharp
   using Microsoft.AspNetCore.Components;
   using Microsoft.JSInterop;

   namespace MyBlazorHybridApp.Views;

   public partial class Home
   {
       [Inject] private IJSRuntime JS { get; set; } = default!;

       private async Task TriggerAlert()
       {
           // Call JavaScript function from Blazor
           await JS.InvokeVoidAsync("showAlert", "Button clicked from Razor component!");
       }
   }
   ```

3. Add a button in `home.razor` to trigger the alert:
   ```html
   <button @onclick="TriggerAlert">Click Me</button>
   ```

Run your app and test the functionality. If everything was set up correctly, clicking the button should display an alert.
