namespace MyBlazorHybridApp;

public partial class MyApp : IDisposable
{

    /// <summary>
    /// Supplied by <see cref="AuthRouteView"/>.
    /// </summary>
    public Type? CurrentPage { get; set; }


    public void Dispose() { }
}
