using MauiTreasureHunter.Application.DTOs;
using MauiTreasureHunter.Application.Services;
using MauiTreasureHunter.Domain.Interfaces;
using MauiTreasureHunter.Domain.ValueObjects;
using MediatR;

namespace MauiTreasureHunter.Application.UseCases.CollectTreasure;

/// <summary>
/// Command for collecting a treasure, implements MediatR IRequest
/// </summary>
public class CollectTreasureCommandRequest : IRequest<CollectTreasureResponse>
{
    /// <summary>
    /// Gets or sets the command data
    /// </summary>
    public CollectTreasureCommand Command { get; set; } = null!;
}

/// <summary>
/// Handler for collecting a treasure
/// </summary>
public class CollectTreasureHandler : IRequestHandler<CollectTreasureCommandRequest, CollectTreasureResponse>
{
    private readonly ITreasureRepository _treasureRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDistanceValidator _distanceValidator;
    
    /// <summary>
    /// Initializes a new instance of the CollectTreasureHandler class
    /// </summary>
    public CollectTreasureHandler(
        ITreasureRepository treasureRepository,
        IUserRepository userRepository,
        IDistanceValidator distanceValidator)
    {
        _treasureRepository = treasureRepository;
        _userRepository = userRepository;
        _distanceValidator = distanceValidator;
    }
    
    /// <summary>
    /// Handles the collect treasure command
    /// </summary>
    public async Task<CollectTreasureResponse> Handle(CollectTreasureCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command = request.Command;
            
            var treasure = await _treasureRepository.GetByIdAsync(command.TreasureId);
            if (treasure == null)
            {
                return new CollectTreasureResponse
                {
                    Success = false,
                    ErrorMessage = "Treasure not found"
                };
            }
            
            if (treasure.IsCollected)
            {
                return new CollectTreasureResponse
                {
                    Success = false,
                    ErrorMessage = "Treasure has already been collected"
                };
            }
            
            // Check distance
            var userLocation = new GeoLocation(command.CurrentLatitude, command.CurrentLongitude);
            var distance = treasure.Location.DistanceTo(userLocation);
            
            if (!_distanceValidator.IsWithinRange(distance))
            {
                return new CollectTreasureResponse
                {
                    Success = false,
                    ErrorMessage = $"Too far from treasure. Distance: {distance:F2}m"
                };
            }
            
            // Update treasure
            treasure.IsCollected = true;
            treasure.CollectedByUserId = command.UserId;
            treasure.CollectedAt = DateTime.UtcNow;
            await _treasureRepository.UpdateAsync(treasure);
            
            // Update user points
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user != null)
            {
                user.TotalPoints += treasure.PointValue;
                await _userRepository.UpdateAsync(user);
                
                return new CollectTreasureResponse
                {
                    Success = true,
                    PointsAwarded = treasure.PointValue,
                    NewTotalPoints = user.TotalPoints
                };
            }
            
            return new CollectTreasureResponse
            {
                Success = false,
                ErrorMessage = "User not found"
            };
        }
        catch (Exception ex)
        {
            return new CollectTreasureResponse
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
