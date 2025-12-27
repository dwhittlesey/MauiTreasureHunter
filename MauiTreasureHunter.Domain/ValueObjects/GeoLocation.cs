namespace MauiTreasureHunter.Domain.ValueObjects;

/// <summary>
/// Value object representing a geographic location with distance calculation capabilities
/// </summary>
public class GeoLocation
{
    /// <summary>
    /// Gets the latitude in degrees
    /// </summary>
    public double Latitude { get; }
    
    /// <summary>
    /// Gets the longitude in degrees
    /// </summary>
    public double Longitude { get; }
    
    /// <summary>
    /// Gets the altitude in meters (optional)
    /// </summary>
    public double? Altitude { get; }
    
    /// <summary>
    /// Initializes a new instance of the GeoLocation class
    /// </summary>
    /// <param name="latitude">The latitude in degrees</param>
    /// <param name="longitude">The longitude in degrees</param>
    /// <param name="altitude">The altitude in meters (optional)</param>
    public GeoLocation(double latitude, double longitude, double? altitude = null)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentException("Latitude must be between -90 and 90 degrees", nameof(latitude));
        
        if (longitude < -180 || longitude > 180)
            throw new ArgumentException("Longitude must be between -180 and 180 degrees", nameof(longitude));
        
        Latitude = latitude;
        Longitude = longitude;
        Altitude = altitude;
    }
    
    /// <summary>
    /// Calculates the distance to another location using the Haversine formula
    /// </summary>
    /// <param name="other">The other location</param>
    /// <returns>Distance in meters</returns>
    public double DistanceTo(GeoLocation other)
    {
        const double earthRadiusMeters = 6371000;
        
        var lat1Rad = ToRadians(Latitude);
        var lat2Rad = ToRadians(other.Latitude);
        var deltaLatRad = ToRadians(other.Latitude - Latitude);
        var deltaLonRad = ToRadians(other.Longitude - Longitude);
        
        var a = Math.Sin(deltaLatRad / 2) * Math.Sin(deltaLatRad / 2) +
                Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                Math.Sin(deltaLonRad / 2) * Math.Sin(deltaLonRad / 2);
        
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        
        return earthRadiusMeters * c;
    }
    
    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}
