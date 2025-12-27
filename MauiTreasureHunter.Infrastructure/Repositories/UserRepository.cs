using MauiTreasureHunter.Domain.Entities;
using MauiTreasureHunter.Domain.Interfaces;
using MauiTreasureHunter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MauiTreasureHunter.Infrastructure.Repositories;

/// <summary>
/// Implementation of the user repository
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    /// <summary>
    /// Initializes a new instance of the UserRepository class
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc/>
    public async Task<UserProfile?> GetByIdAsync(Guid id)
    {
        return await _context.UserProfiles
            .Include(u => u.Inventory)
                .ThenInclude(i => i.TreasureItem)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    /// <inheritdoc/>
    public async Task<UserProfile> UpdateAsync(UserProfile user)
    {
        _context.UserProfiles.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
