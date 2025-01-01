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