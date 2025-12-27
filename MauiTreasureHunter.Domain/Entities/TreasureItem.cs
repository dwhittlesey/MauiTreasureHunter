using MauiTreasureHunter.Domain.Enums;
using MauiTreasureHunter.Domain.ValueObjects;

namespace MauiTreasureHunter.Domain.Entities;

/// <summary>
/// Represents a treasure item that can be placed and collected in the game
/// </summary>
public class TreasureItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the treasure
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the treasure
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the type of treasure
    /// </summary>
    public TreasureType Type { get; set; }
    
    /// <summary>
    /// Gets or sets the geographic location where the treasure is placed
    /// </summary>
    public GeoLocation Location { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the point value awarded when collecting this treasure
    /// </summary>
    public int PointValue { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the user who placed this treasure
    /// </summary>
    public Guid PlacedByUserId { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the treasure was placed
    /// </summary>
    public DateTime PlacedAt { get; set; }
    
    /// <summary>
    /// Gets or sets whether the treasure has been collected
    /// </summary>
    public bool IsCollected { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the user who collected this treasure (if collected)
    /// </summary>
    public Guid? CollectedByUserId { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the treasure was collected (if collected)
    /// </summary>
    public DateTime? CollectedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the path to the graphic resource for this treasure
    /// </summary>
    public string GraphicResourcePath { get; set; } = string.Empty;
}
