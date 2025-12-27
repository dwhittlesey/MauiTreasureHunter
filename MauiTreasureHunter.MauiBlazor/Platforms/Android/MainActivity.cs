using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MauiTreasureHunter.MauiBlazor;

/// <summary>
/// Main Android Activity with ARCore initialization
/// </summary>
[Activity(
    Theme = "@style/Maui.SplashTheme", 
    MainLauncher = true, 
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        // Request camera permission
        RequestCameraPermission();
        
        // Request location permissions
        RequestLocationPermissions();
        
        // TODO: Initialize ARCore
        // Check if ARCore is installed and up to date
        // ArCoreApk.getInstance().requestInstall(this, true);
    }
    
    private void RequestCameraPermission()
    {
        if (CheckSelfPermission(Android.Manifest.Permission.Camera) != Permission.Granted)
        {
            RequestPermissions(new[] { Android.Manifest.Permission.Camera }, 100);
        }
    }
    
    private void RequestLocationPermissions()
    {
        var permissionsToRequest = new List<string>();
        
        if (CheckSelfPermission(Android.Manifest.Permission.AccessFineLocation) != Permission.Granted)
        {
            permissionsToRequest.Add(Android.Manifest.Permission.AccessFineLocation);
        }
        
        if (CheckSelfPermission(Android.Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
        {
            permissionsToRequest.Add(Android.Manifest.Permission.AccessCoarseLocation);
        }
        
        if (permissionsToRequest.Count > 0)
        {
            RequestPermissions(permissionsToRequest.ToArray(), 101);
        }
    }
}
