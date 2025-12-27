using MauiTreasureHunter.Domain.Enums;

namespace MauiTreasureHunter.Application.DTOs;

/// <summary>
/// Command to place a new treasure in the game
/// </summary>
public class PlaceTreasureCommand
{
    /// <summary>
    /// Gets or sets the name of the treasure
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the type of treasure
    /// </summary>
    public TreasureType Type { get; set; }
    
    /// <summary>
    /// Gets or sets the latitude where the treasure is placed
    /// </summary>
    public double Latitude { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude where the treasure is placed
    /// </summary>
    public double Longitude { get; set; }
    
    /// <summary>
    /// Gets or sets the altitude where the treasure is placed (optional)
    /// </summary>
    public double? Altitude { get; set; }
    
    /// <summary>
    /// Gets or sets the point value of the treasure
    /// </summary>
    public int PointValue { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the user placing the treasure
    /// </summary>
    public Guid PlacedByUserId { get; set; }
    
    /// <summary>
    /// Gets or sets the path to the graphic resource
    /// </summary>
    public string GraphicResourcePath { get; set; } = string.Empty;
}

/// <summary>
/// Response after placing a treasure
/// </summary>
public class PlaceTreasureResponse
{
    /// <summary>
    /// Gets or sets the ID of the created treasure
    /// </summary>
    public Guid TreasureId { get; set; }
    
    /// <summary>
    /// Gets or sets whether the operation was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Gets or sets any error message
    /// </summary>
    public string? ErrorMessage { get; set; }
}
