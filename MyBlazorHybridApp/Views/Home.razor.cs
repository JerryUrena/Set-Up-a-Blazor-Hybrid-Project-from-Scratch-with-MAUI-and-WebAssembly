using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyBlazorHybridApp.Services;

namespace MyBlazorHybridApp.Views;

public partial class Home
{
    [Inject]
    private IMyFirstService MyFirstService { get; set; } = default!;

    [Inject] private IJSRuntime JS { get; set; } = default!;

    private async Task TriggerAlert()
    {
        // Call JavaScript function from Blazor
        await JS.InvokeVoidAsync("showAlert", "Button clicked from Razor component!");
    }
}
