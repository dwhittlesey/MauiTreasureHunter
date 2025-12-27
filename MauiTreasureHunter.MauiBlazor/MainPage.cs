namespace MauiTreasureHunter.MauiBlazor;

/// <summary>
/// Main page that hosts the Blazor WebView
/// </summary>
public class MainPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the MainPage class
    /// </summary>
    public MainPage()
    {
        var blazorWebView = new BlazorWebView
        {
            HostPage = "wwwroot/index.html"
        };
        
        blazorWebView.RootComponents.Add(new RootComponent
        {
            Selector = "#app",
            ComponentType = typeof(Main)
        });
        
        Content = blazorWebView;
    }
}
