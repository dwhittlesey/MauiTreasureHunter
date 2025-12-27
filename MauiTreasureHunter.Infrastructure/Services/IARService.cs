namespace MauiTreasureHunter.Infrastructure.Services;

/// <summary>
/// Service interface for AR (Augmented Reality) operations
/// </summary>
public interface IARService
{
    /// <summary>
    /// Checks if AR is supported on the current device
    /// </summary>
    /// <returns>True if AR is supported, false otherwise</returns>
    Task<bool> IsARSupportedAsync();
    
    /// <summary>
    /// Initializes the AR session
    /// </summary>
    /// <returns>True if initialization was successful</returns>
    Task<bool> InitializeAsync();
    
    /// <summary>
    /// Gets the current camera pose (position and orientation)
    /// </summary>
    /// <returns>Camera pose data</returns>
    Task<CameraPose?> GetCameraPoseAsync();
    
    /// <summary>
    /// Places a virtual object at a specific location
    /// </summary>
    /// <param name="objectId">The object identifier</param>
    /// <param name="latitude">The latitude</param>
    /// <param name="longitude">The longitude</param>
    /// <param name="altitude">The altitude</param>
    /// <returns>True if placement was successful</returns>
    Task<bool> PlaceVirtualObjectAsync(Guid objectId, double latitude, double longitude, double? altitude);
    
    /// <summary>
    /// Detects nearby virtual objects within a specified radius
    /// </summary>
    /// <param name="radiusMeters">The detection radius in meters</param>
    /// <returns>List of nearby object IDs</returns>
    Task<List<Guid>> DetectNearbyObjectsAsync(double radiusMeters);
}

/// <summary>
/// Represents camera pose data
/// </summary>
public class CameraPose
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
    /// Gets or sets the heading in degrees
    /// </summary>
    public double Heading { get; set; }
}
