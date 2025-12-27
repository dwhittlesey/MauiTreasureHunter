namespace MauiTreasureHunter.Infrastructure.Services;

/// <summary>
/// Service interface for audio operations
/// </summary>
public interface IAudioService
{
    /// <summary>
    /// Plays the sound when a treasure is collected
    /// </summary>
    /// <returns>Task representing the async operation</returns>
    Task PlayCollectionSoundAsync();
    
    /// <summary>
    /// Plays the sound when a treasure is placed
    /// </summary>
    /// <returns>Task representing the async operation</returns>
    Task PlayPlacementSoundAsync();
}

/// <summary>
/// Stub implementation of audio service
/// Note: Full implementation requires Plugin.Maui.Audio
/// </summary>
public class AudioService : IAudioService
{
    /// <inheritdoc/>
    public Task PlayCollectionSoundAsync()
    {
        // TODO: Implement with Plugin.Maui.Audio
        return Task.CompletedTask;
    }
    
    /// <inheritdoc/>
    public Task PlayPlacementSoundAsync()
    {
        // TODO: Implement with Plugin.Maui.Audio
        return Task.CompletedTask;
    }
}
