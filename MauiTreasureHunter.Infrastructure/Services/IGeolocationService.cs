namespace MauiTreasureHunter.Infrastructure.Services;

/// <summary>
/// Service interface for geolocation operations
/// </summary>
public interface IGeolocationService
{
    /// <summary>
    /// Gets the current location of the device
    /// </summary>
    /// <returns>Current location data</returns>
    Task<GeolocationData?> GetCurrentLocationAsync();
    
    /// <summary>
    /// Observes location changes
    /// </summary>
    /// <param name="onLocationChanged">Callback for location updates</param>
    /// <returns>Task representing the observation operation</returns>
    Task ObserveLocationChanges(Action<GeolocationData> onLocationChanged);
}

/// <summary>
/// Represents geolocation data
/// </summary>
public class GeolocationData
{
    /// <summary>
    /// Gets or sets the latitude
    /// </summary>
    public double Latitude { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude
    /// </summary>
    public double Longitude { get; set; }
    
    /// <summary>
    /// Gets or sets the altitude
    /// </summary>
    public double? Altitude { get; set; }
    
    /// <summary>
    /// Gets or sets the accuracy in meters
    /// </summary>
    public double? Accuracy { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }
}
