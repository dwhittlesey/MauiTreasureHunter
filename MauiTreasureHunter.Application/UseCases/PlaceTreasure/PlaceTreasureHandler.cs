using MauiTreasureHunter.Application.DTOs;
using MauiTreasureHunter.Domain.Entities;
using MauiTreasureHunter.Domain.Interfaces;
using MauiTreasureHunter.Domain.ValueObjects;
using MediatR;

namespace MauiTreasureHunter.Application.UseCases.PlaceTreasure;

/// <summary>
/// Command for placing a treasure, implements MediatR IRequest
/// </summary>
public class PlaceTreasureCommandRequest : IRequest<PlaceTreasureResponse>
{
    /// <summary>
    /// Gets or sets the command data
    /// </summary>
    public PlaceTreasureCommand Command { get; set; } = null!;
}

/// <summary>
/// Handler for placing a treasure
/// </summary>
public class PlaceTreasureHandler : IRequestHandler<PlaceTreasureCommandRequest, PlaceTreasureResponse>
{
    private readonly ITreasureRepository _treasureRepository;
    
    /// <summary>
    /// Initializes a new instance of the PlaceTreasureHandler class
    /// </summary>
    /// <param name="treasureRepository">The treasure repository</param>
    public PlaceTreasureHandler(ITreasureRepository treasureRepository)
    {
        _treasureRepository = treasureRepository;
    }
    
    /// <summary>
    /// Handles the place treasure command
    /// </summary>
    public async Task<PlaceTreasureResponse> Handle(PlaceTreasureCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command = request.Command;
            
            var treasure = new TreasureItem
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Type = command.Type,
                Location = new GeoLocation(command.Latitude, command.Longitude, command.Altitude),
                PointValue = command.PointValue,
                PlacedByUserId = command.PlacedByUserId,
                PlacedAt = DateTime.UtcNow,
                IsCollected = false,
                GraphicResourcePath = command.GraphicResourcePath
            };
            
            await _treasureRepository.CreateAsync(treasure);
            
            return new PlaceTreasureResponse
            {
                TreasureId = treasure.Id,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new PlaceTreasureResponse
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
