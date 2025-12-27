namespace MauiTreasureHunter.MauiBlazor;

/// <summary>
/// Main application class
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes a new instance of the App class
    /// </summary>
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }
}
