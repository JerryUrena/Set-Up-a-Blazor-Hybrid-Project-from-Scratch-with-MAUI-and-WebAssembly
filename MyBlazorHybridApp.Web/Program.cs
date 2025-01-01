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
