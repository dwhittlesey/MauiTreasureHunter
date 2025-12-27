using MauiTreasureHunter.Application.DTOs;
using MauiTreasureHunter.Application.UseCases.PlaceTreasure;
using MauiTreasureHunter.Application.UseCases.CollectTreasure;
using MauiTreasureHunter.Application.UseCases.GetNearbyTreasures;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MauiTreasureHunter.API.Controllers;

/// <summary>
/// Controller for treasure-related operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TreasuresController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TreasuresController> _logger;
    
    /// <summary>
    /// Initializes a new instance of the TreasuresController class
    /// </summary>
    public TreasuresController(IMediator mediator, ILogger<TreasuresController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    /// <summary>
    /// Place a new treasure
    /// </summary>
    /// <param name="command">The place treasure command</param>
    /// <returns>The result of the placement operation</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PlaceTreasureResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PlaceTreasureResponse>> PlaceTreasure([FromBody] PlaceTreasureCommand command)
    {
        try
        {
            var request = new PlaceTreasureCommandRequest { Command = command };
            var result = await _mediator.Send(request);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error placing treasure");
            return BadRequest(new PlaceTreasureResponse
            {
                Success = false,
                ErrorMessage = "An error occurred while placing the treasure"
            });
        }
    }
    
    /// <summary>
    /// Get nearby treasures within a specified radius
    /// </summary>
    /// <param name="latitude">The latitude of the search center</param>
    /// <param name="longitude">The longitude of the search center</param>
    /// <param name="radiusMeters">The search radius in meters</param>
    /// <returns>List of nearby treasures</returns>
    [HttpGet("nearby")]
    [ProducesResponseType(typeof(List<TreasureDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TreasureDto>>> GetNearbyTreasures(
        [FromQuery] double latitude,
        [FromQuery] double longitude,
        [FromQuery] double radiusMeters = 100)
    {
        try
        {
            var query = new GetNearbyTreasuresQuery
            {
                Latitude = latitude,
                Longitude = longitude,
                RadiusMeters = radiusMeters
            };
            
            var request = new GetNearbyTreasuresQueryRequest { Query = query };
            var result = await _mediator.Send(request);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting nearby treasures");
            return BadRequest("An error occurred while retrieving nearby treasures");
        }
    }
    
    /// <summary>
    /// Collect a treasure
    /// </summary>
    /// <param name="id">The treasure ID</param>
    /// <param name="command">The collect treasure command</param>
    /// <returns>The result of the collection operation</returns>
    [HttpPost("{id}/collect")]
    [ProducesResponseType(typeof(CollectTreasureResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CollectTreasureResponse>> CollectTreasure(
        Guid id,
        [FromBody] CollectTreasureCommand command)
    {
        try
        {
            command.TreasureId = id;
            var request = new CollectTreasureCommandRequest { Command = command };
            var result = await _mediator.Send(request);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            // Check for specific error conditions
            if (result.ErrorMessage != null)
            {
                if (result.ErrorMessage.IndexOf("not found", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return NotFound(result);
                }
            }
            
            return BadRequest(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error collecting treasure");
            return BadRequest(new CollectTreasureResponse
            {
                Success = false,
                ErrorMessage = "An error occurred while collecting the treasure"
            });
        }
    }
}
