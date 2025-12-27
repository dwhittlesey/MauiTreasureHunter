using MauiTreasureHunter.Application.Services;
using MauiTreasureHunter.Domain.Interfaces;
using MauiTreasureHunter.Infrastructure.Data;
using MauiTreasureHunter.Infrastructure.Repositories;
using MauiTreasureHunter.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MauiTreasureHunter API", Version = "v1" });
});

// Configure database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(MauiTreasureHunter.Application.UseCases.PlaceTreasure.PlaceTreasureHandler).Assembly);
});

// Configure repositories
builder.Services.AddScoped<ITreasureRepository, TreasureRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure services
var collectionRadius = builder.Configuration.GetValue<double>("GameSettings:DefaultCollectionRadiusMeters", 50.0);
builder.Services.AddSingleton<IDistanceValidator>(new DistanceValidator(collectionRadius));
builder.Services.AddScoped<IARService, ARCoreService>();
builder.Services.AddScoped<IGeolocationService, GeolocationService>();
builder.Services.AddScoped<IAudioService, AudioService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
