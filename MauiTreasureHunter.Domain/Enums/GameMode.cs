namespace MauiTreasureHunter.Domain.Enums;

/// <summary>
/// Represents the current game mode the user is in
/// </summary>
public enum GameMode
{
    /// <summary>
    /// User is searching for and collecting treasures
    /// </summary>
    Hunting,
    
    /// <summary>
    /// User is placing new treasures
    /// </summary>
    Creating
}
