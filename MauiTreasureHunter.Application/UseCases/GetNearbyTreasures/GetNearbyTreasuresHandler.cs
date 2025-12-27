using MauiTreasureHunter.Application.DTOs;
using MauiTreasureHunter.Domain.Interfaces;
using MauiTreasureHunter.Domain.ValueObjects;
using MediatR;

namespace MauiTreasureHunter.Application.UseCases.GetNearbyTreasures;

/// <summary>
/// Query for getting nearby treasures, implements MediatR IRequest
/// </summary>
public class GetNearbyTreasuresQueryRequest : IRequest<List<TreasureDto>>
{
    /// <summary>
    /// Gets or sets the query data
    /// </summary>
    public GetNearbyTreasuresQuery Query { get; set; } = null!;
}

/// <summary>
/// Handler for getting nearby treasures
/// </summary>
public class GetNearbyTreasuresHandler : IRequestHandler<GetNearbyTreasuresQueryRequest, List<TreasureDto>>
{
    private readonly ITreasureRepository _treasureRepository;
    
    /// <summary>
    /// Initializes a new instance of the GetNearbyTreasuresHandler class
    /// </summary>
    /// <param name="treasureRepository">The treasure repository</param>
    public GetNearbyTreasuresHandler(ITreasureRepository treasureRepository)
    {
        _treasureRepository = treasureRepository;
    }
    
    /// <summary>
    /// Handles the get nearby treasures query
    /// </summary>
    public async Task<List<TreasureDto>> Handle(GetNearbyTreasuresQueryRequest request, CancellationToken cancellationToken)
    {
        var query = request.Query;
        var treasures = await _treasureRepository.GetNearbyTreasuresAsync(
            query.Latitude, 
            query.Longitude, 
            query.RadiusMeters);
        
        var queryLocation = new GeoLocation(query.Latitude, query.Longitude);
        
        return treasures
            .Where(t => !t.IsCollected)
            .Select(t => new TreasureDto
            {
                Id = t.Id,
                Name = t.Name,
                Type = t.Type,
                Latitude = t.Location.Latitude,
                Longitude = t.Location.Longitude,
                Altitude = t.Location.Altitude,
                PointValue = t.PointValue,
                GraphicResourcePath = t.GraphicResourcePath,
                DistanceMeters = t.Location.DistanceTo(queryLocation)
            })
            .OrderBy(t => t.DistanceMeters)
            .ToList();
    }
}
