namespace MauiTreasureHunter.Application.Services;

/// <summary>
/// Service for validating distances in the game
/// </summary>
public interface IDistanceValidator
{
    /// <summary>
    /// Checks if a distance is within the allowed range
    /// </summary>
    /// <param name="distanceMeters">The distance to validate in meters</param>
    /// <returns>True if within range, false otherwise</returns>
    bool IsWithinRange(double distanceMeters);
}

/// <summary>
/// Implementation of distance validator with configurable threshold
/// </summary>
public class DistanceValidator : IDistanceValidator
{
    private readonly double _thresholdMeters;
    
    /// <summary>
    /// Initializes a new instance of the DistanceValidator class
    /// </summary>
    /// <param name="thresholdMeters">The threshold distance in meters</param>
    public DistanceValidator(double thresholdMeters = 50.0)
    {
        _thresholdMeters = thresholdMeters;
    }
    
    /// <inheritdoc/>
    public bool IsWithinRange(double distanceMeters)
    {
        return distanceMeters <= _thresholdMeters;
    }
}
