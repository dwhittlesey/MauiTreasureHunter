namespace MauiTreasureHunter.Domain.Entities;

/// <summary>
/// Represents an item in a user's inventory
/// </summary>
public class InventoryItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the inventory item
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the associated treasure item
    /// </summary>
    public Guid TreasureItemId { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the user who owns this item
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the item was acquired
    /// </summary>
    public DateTime AcquiredAt { get; set; }
    
    /// <summary>
    /// Gets or sets whether the item has been placed back in the game world
    /// </summary>
    public bool IsPlaced { get; set; }
    
    /// <summary>
    /// Navigation property to the associated treasure item
    /// </summary>
    public TreasureItem? TreasureItem { get; set; }
}
