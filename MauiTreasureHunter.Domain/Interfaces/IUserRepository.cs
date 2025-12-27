using MauiTreasureHunter.Domain.Entities;

namespace MauiTreasureHunter.Domain.Interfaces;

/// <summary>
/// Repository interface for user profile operations
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets a user profile by its unique identifier
    /// </summary>
    /// <param name="id">The user ID</param>
    /// <returns>The user profile if found, null otherwise</returns>
    Task<UserProfile?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Updates an existing user profile
    /// </summary>
    /// <param name="user">The user profile to update</param>
    /// <returns>The updated user profile</returns>
    Task<UserProfile> UpdateAsync(UserProfile user);
}
