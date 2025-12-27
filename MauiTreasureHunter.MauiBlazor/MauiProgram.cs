using MauiTreasureHunter.Application.Services;
using MauiTreasureHunter.Domain.Interfaces;
using MauiTreasureHunter.Infrastructure.Data;
using MauiTreasureHunter.Infrastructure.Repositories;
using MauiTreasureHunter.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace MauiTreasureHunter.MauiBlazor;

/// <summary>
/// Entry point for MAUI application configuration
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Creates and configures the MAUI application
    /// </summary>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        // Configure API client with Refit
        // TODO: Configure Refit API client when API URL is available
        // builder.Services.AddRefitClient<ITreasureHunterApi>()
        //     .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://your-api-url.com"));

        // Configure MediatR
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(MauiTreasureHunter.Application.UseCases.PlaceTreasure.PlaceTreasureHandler).Assembly);
        });

        // Configure repositories
        builder.Services.AddScoped<ITreasureRepository, TreasureRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // Configure services
        builder.Services.AddSingleton<IDistanceValidator>(new DistanceValidator(50.0));
        builder.Services.AddScoped<IARService, ARCoreService>();
        builder.Services.AddScoped<IGeolocationService, GeolocationService>();
        builder.Services.AddScoped<IAudioService, AudioService>();

        // Note: In production, configure with actual database connection
        // For now, this is a stub configuration
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            // TODO: Configure for SQLite or remote SQL Server
            options.UseSqlServer("Data Source=:memory:");
        });

        return builder.Build();
    }
}
