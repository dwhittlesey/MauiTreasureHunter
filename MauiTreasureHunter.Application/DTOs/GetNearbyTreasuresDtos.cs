using MauiTreasureHunter.Domain.Enums;

namespace MauiTreasureHunter.Application.DTOs;

/// <summary>
/// Query to get nearby treasures
/// </summary>
public class GetNearbyTreasuresQuery
{
    /// <summary>
    /// Gets or sets the latitude of the search center
    /// </summary>
    public double Latitude { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude of the search center
    /// </summary>
    public double Longitude { get; set; }
    
    /// <summary>
    /// Gets or sets the search radius in meters
    /// </summary>
    public double RadiusMeters { get; set; }
}

/// <summary>
/// DTO for treasure information
/// </summary>
public class TreasureDto
{
    /// <summary>
    /// Gets or sets the treasure ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the treasure name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the treasure type
    /// </summary>
    public TreasureType Type { get; set; }
    
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
    /// Gets or sets the point value
    /// </summary>
    public int PointValue { get; set; }
    
    /// <summary>
    /// Gets or sets the graphic resource path
    /// </summary>
    public string GraphicResourcePath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the distance from the query point in meters
    /// </summary>
    public double? DistanceMeters { get; set; }
}
