using MauiTreasureHunter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MauiTreasureHunter.Infrastructure.Data;

/// <summary>
/// Database context for the treasure hunter application
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the ApplicationDbContext class
    /// </summary>
    /// <param name="options">The options for this context</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    /// <summary>
    /// Gets or sets the TreasureItems DbSet
    /// </summary>
    public DbSet<TreasureItem> TreasureItems { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the UserProfiles DbSet
    /// </summary>
    public DbSet<UserProfile> UserProfiles { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the InventoryItems DbSet
    /// </summary>
    public DbSet<InventoryItem> InventoryItems { get; set; } = null!;
    
    /// <summary>
    /// Configures the model that was discovered by convention from entity types
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure TreasureItem
        modelBuilder.Entity<TreasureItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.GraphicResourcePath)
                .HasMaxLength(500);
            
            // Configure GeoLocation as owned entity
            entity.OwnsOne(e => e.Location, location =>
            {
                location.Property(l => l.Latitude)
                    .IsRequired()
                    .HasColumnName("Latitude");
                
                location.Property(l => l.Longitude)
                    .IsRequired()
                    .HasColumnName("Longitude");
                
                location.Property(l => l.Altitude)
                    .HasColumnName("Altitude");
                    
                // Note: Spatial indexes should be created via raw SQL migrations for better performance
            });
            
            entity.HasIndex(e => e.IsCollected)
                .HasDatabaseName("IX_TreasureItems_IsCollected");
        });
        
        // Configure UserProfile
        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.HasIndex(e => e.Username)
                .IsUnique()
                .HasDatabaseName("IX_UserProfiles_Username");
            
            entity.HasMany(e => e.Inventory)
                .WithOne()
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Configure InventoryItem
        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.TreasureItem)
                .WithMany()
                .HasForeignKey(e => e.TreasureItemId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasIndex(e => new { e.UserId, e.IsPlaced })
                .HasDatabaseName("IX_InventoryItems_UserId_IsPlaced");
        });
    }
}
