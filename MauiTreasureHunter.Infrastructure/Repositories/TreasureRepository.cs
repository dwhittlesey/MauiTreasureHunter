using MauiTreasureHunter.Domain.Entities;
using MauiTreasureHunter.Domain.Interfaces;
using MauiTreasureHunter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MauiTreasureHunter.Infrastructure.Repositories;

/// <summary>
/// Implementation of the treasure repository
/// </summary>
public class TreasureRepository : ITreasureRepository
{
    private readonly ApplicationDbContext _context;
    
    /// <summary>
    /// Initializes a new instance of the TreasureRepository class
    /// </summary>
    /// <param name="context">The database context</param>
    public TreasureRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc/>
    public async Task<TreasureItem?> GetByIdAsync(Guid id)
    {
        return await _context.TreasureItems.FindAsync(id);
    }
    
    /// <inheritdoc/>
    public async Task<List<TreasureItem>> GetNearbyTreasuresAsync(double latitude, double longitude, double radiusMeters)
    {
        // Simple bounding box query for nearby treasures
        // For a more precise implementation, consider using spatial database features
        const double earthRadiusKm = 6371.0;
        var latDelta = (radiusMeters / 1000.0) / earthRadiusKm * (180.0 / Math.PI);
        var lonDelta = (radiusMeters / 1000.0) / (earthRadiusKm * Math.Cos(latitude * Math.PI / 180.0)) * (180.0 / Math.PI);
        
        var minLat = latitude - latDelta;
        var maxLat = latitude + latDelta;
        var minLon = longitude - lonDelta;
        var maxLon = longitude + lonDelta;
        
        return await _context.TreasureItems
            .Where(t => !t.IsCollected &&
                        t.Location.Latitude >= minLat &&
                        t.Location.Latitude <= maxLat &&
                        t.Location.Longitude >= minLon &&
                        t.Location.Longitude <= maxLon)
            .ToListAsync();
    }
    
    /// <inheritdoc/>
    public async Task<TreasureItem> CreateAsync(TreasureItem treasure)
    {
        _context.TreasureItems.Add(treasure);
        await _context.SaveChangesAsync();
        return treasure;
    }
    
    /// <inheritdoc/>
    public async Task<TreasureItem> UpdateAsync(TreasureItem treasure)
    {
        _context.TreasureItems.Update(treasure);
        await _context.SaveChangesAsync();
        return treasure;
    }
    
    /// <inheritdoc/>
    public async Task<List<TreasureItem>> GetUserPlacedTreasuresAsync(Guid userId)
    {
        return await _context.TreasureItems
            .Where(t => t.PlacedByUserId == userId)
            .OrderByDescending(t => t.PlacedAt)
            .ToListAsync();
    }
}
