using MauiTreasureHunter.Domain.Enums;

namespace MauiTreasureHunter.Domain.Entities;

/// <summary>
/// Represents a user profile in the treasure hunting game
/// </summary>
public class UserProfile
{
    /// <summary>
    /// Gets or sets the unique identifier for the user
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the username
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the total points earned by the user
    /// </summary>
    public int TotalPoints { get; set; }
    
    /// <summary>
    /// Gets or sets the current game mode the user is in
    /// </summary>
    public GameMode CurrentMode { get; set; }
    
    /// <summary>
    /// Gets or sets the user's inventory
    /// </summary>
    public List<InventoryItem> Inventory { get; set; } = new();
}
