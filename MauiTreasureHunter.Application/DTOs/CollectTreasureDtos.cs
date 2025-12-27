namespace MauiTreasureHunter.Application.DTOs;

/// <summary>
/// Command to collect a treasure
/// </summary>
public class CollectTreasureCommand
{
    /// <summary>
    /// Gets or sets the ID of the treasure to collect
    /// </summary>
    public Guid TreasureId { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the user collecting the treasure
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the current latitude of the user
    /// </summary>
    public double CurrentLatitude { get; set; }
    
    /// <summary>
    /// Gets or sets the current longitude of the user
    /// </summary>
    public double CurrentLongitude { get; set; }
}

/// <summary>
/// Response after attempting to collect a treasure
/// </summary>
public class CollectTreasureResponse
{
    /// <summary>
    /// Gets or sets whether the collection was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Gets or sets the points awarded
    /// </summary>
    public int PointsAwarded { get; set; }
    
    /// <summary>
    /// Gets or sets the new total points for the user
    /// </summary>
    public int NewTotalPoints { get; set; }
    
    /// <summary>
    /// Gets or sets any error message
    /// </summary>
    public string? ErrorMessage { get; set; }
}
