using Microsoft.AspNetCore.Components;
using MyBlazorHybridApp.Services;

namespace MyBlazorHybridApp.Views;

public partial class Home
{
    [Inject]
    private IMyFirstService MyFirstService { get; set; } = default!;
}
