using MauiTreasureHunter.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MauiTreasureHunter.API.Controllers;

/// <summary>
/// Controller for user-related operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UsersController> _logger;
    
    /// <summary>
    /// Initializes a new instance of the UsersController class
    /// </summary>
    public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    
    /// <summary>
    /// Get a user profile by ID
    /// </summary>
    /// <param name="id">The user ID</param>
    /// <returns>The user profile</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(Guid id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            
            if (user == null)
            {
                return NotFound($"User with ID {id} not found");
            }
            
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user {UserId}", id);
            return BadRequest("An error occurred while retrieving the user");
        }
    }
    
    /// <summary>
    /// Get a user's inventory
    /// </summary>
    /// <param name="id">The user ID</param>
    /// <returns>The user's inventory items</returns>
    [HttpGet("{id}/inventory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserInventory(Guid id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            
            if (user == null)
            {
                return NotFound($"User with ID {id} not found");
            }
            
            return Ok(user.Inventory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting inventory for user {UserId}", id);
            return BadRequest("An error occurred while retrieving the inventory");
        }
    }
}
