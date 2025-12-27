using MauiTreasureHunter.Domain.Entities;

namespace MauiTreasureHunter.Domain.Interfaces;

/// <summary>
/// Repository interface for treasure item operations
/// </summary>
public interface ITreasureRepository
{
    /// <summary>
    /// Gets a treasure by its unique identifier
    /// </summary>
    /// <param name="id">The treasure ID</param>
    /// <returns>The treasure item if found, null otherwise</returns>
    Task<TreasureItem?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Gets treasures near a specific location within a given radius
    /// </summary>
    /// <param name="latitude">The latitude of the center point</param>
    /// <param name="longitude">The longitude of the center point</param>
    /// <param name="radiusMeters">The search radius in meters</param>
    /// <returns>List of nearby treasures</returns>
    Task<List<TreasureItem>> GetNearbyTreasuresAsync(double latitude, double longitude, double radiusMeters);
    
    /// <summary>
    /// Creates a new treasure item
    /// </summary>
    /// <param name="treasure">The treasure to create</param>
    /// <returns>The created treasure</returns>
    Task<TreasureItem> CreateAsync(TreasureItem treasure);
    
    /// <summary>
    /// Updates an existing treasure item
    /// </summary>
    /// <param name="treasure">The treasure to update</param>
    /// <returns>The updated treasure</returns>
    Task<TreasureItem> UpdateAsync(TreasureItem treasure);
    
    /// <summary>
    /// Gets all treasures placed by a specific user
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <returns>List of treasures placed by the user</returns>
    Task<List<TreasureItem>> GetUserPlacedTreasuresAsync(Guid userId);
}
