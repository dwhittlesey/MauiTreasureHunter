# MauiTreasureHunter - AR Treasure Hunt Game

An augmented reality treasure hunting game built with .NET MAUI Blazor and ASP.NET Core, following Clean Architecture principles. Players can place virtual treasures at GPS locations and search for treasures placed by others using AR camera integration.

## 🎮 Features

- **AR Integration**: Place and view virtual treasures using augmented reality through device camera
- **GPS-Based Gameplay**: Treasures are tied to real-world locations using GPS coordinates
- **Dual Game Modes**:
  - **Hunting Mode**: Search for and collect nearby treasures
  - **Creation Mode**: Place new treasures at your current location
- **Points System**: Earn points by collecting treasures with sound feedback
- **Inventory System**: Manage collected items and place them back into the game world
- **Distance Validation**: Configurable collection radius ensures players are physically near treasures

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────────────────┐
│                  Presentation Layer                  │
│  ┌──────────────────┐      ┌──────────────────────┐ │
│  │  MAUI Blazor App │      │   ASP.NET Core API   │ │
│  │  (Android)       │      │   (RESTful)          │ │
│  └──────────────────┘      └──────────────────────┘ │
└─────────────────────────────────────────────────────┘
                        │
┌─────────────────────────────────────────────────────┐
│              Application Layer (Use Cases)           │
│  - PlaceTreasure      - CollectTreasure             │
│  - GetNearbyTreasures - DistanceValidator           │
│  - MediatR CQRS Pattern                             │
└─────────────────────────────────────────────────────┘
                        │
┌─────────────────────────────────────────────────────┐
│           Infrastructure Layer (Data Access)         │
│  - EF Core & SQL Server  - Repositories             │
│  - ARCore Service        - Geolocation Service      │
│  - Audio Service                                    │
└─────────────────────────────────────────────────────┘
                        │
┌─────────────────────────────────────────────────────┐
│            Domain Layer (Business Logic)             │
│  - Entities: TreasureItem, UserProfile              │
│  - Value Objects: GeoLocation (Haversine formula)   │
│  - Interfaces: ITreasureRepository, IUserRepository │
└─────────────────────────────────────────────────────┘
```

## 📁 Solution Structure

### **MauiTreasureHunter.Domain** (Core Business Logic)
- **Entities**: `TreasureItem`, `UserProfile`, `InventoryItem`
- **Value Objects**: `GeoLocation` with Haversine distance calculation
- **Enums**: `GameMode`, `TreasureType`
- **Interfaces**: Repository contracts

### **MauiTreasureHunter.Application** (Use Cases)
- **Use Cases**: Place/Collect/Query treasures using MediatR
- **Services**: Distance validation
- **DTOs**: Commands, queries, and responses
- **Packages**: MediatR, FluentValidation

### **MauiTreasureHunter.Infrastructure** (Data & External Services)
- **Data**: EF Core DbContext, entity configurations
- **Repositories**: SQL Server implementations
- **Services**: ARCore, Geolocation, Audio (stub implementations)
- **Packages**: EF Core SQL Server, Tools

### **MauiTreasureHunter.API** (Web API)
- **Controllers**: `TreasuresController`, `UsersController`
- **Endpoints**:
  - `POST /api/treasures` - Place a treasure
  - `GET /api/treasures/nearby` - Get treasures within radius
  - `POST /api/treasures/{id}/collect` - Collect a treasure
  - `GET /api/users/{id}` - Get user profile
  - `GET /api/users/{id}/inventory` - Get user inventory
- **Packages**: EF Core, Swashbuckle, NetTopologySuite, MediatR

### **MauiTreasureHunter.MauiBlazor** (Mobile App)
- **Pages**: `ARHuntingView`, `ARCreationView`
- **Components**: `ARCameraView` (AR camera wrapper)
- **Platform-specific**: Android configurations, permissions
- **Packages**: MAUI Controls, ARCore, Refit, Plugin.Maui.Audio

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 (17.8+) with:
  - .NET MAUI workload
  - Android SDK (API 24+)
- SQL Server (LocalDB or full instance)
- Android device or emulator with ARCore support

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/dwhittlesey/MauiTreasureHunter.git
   cd MauiTreasureHunter
   ```

2. **Update connection string**
   
   Edit `MauiTreasureHunter.API/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MauiTreasureHunterDb;Trusted_Connection=true;MultipleActiveResultSets=true"
   }
   ```

3. **Apply database migrations**
   ```bash
   cd MauiTreasureHunter.Infrastructure
   dotnet ef database update --startup-project ../MauiTreasureHunter.API/MauiTreasureHunter.API.csproj
   ```

4. **Run the API**
   ```bash
   cd ../MauiTreasureHunter.API
   dotnet run
   ```
   API will be available at `https://localhost:5001` (or check console output)

5. **Configure MAUI app**
   
   Update `MauiTreasureHunter.MauiBlazor/MauiProgram.cs` with your API URL:
   ```csharp
   builder.Services.AddRefitClient<ITreasureHunterApi>()
       .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://your-api-url.com"));
   ```

6. **Build and deploy MAUI app**
   
   Open solution in Visual Studio, set `MauiTreasureHunter.MauiBlazor` as startup project, and deploy to Android device/emulator.

## 🔧 Configuration

### Game Settings (appsettings.json)

```json
{
  "GameSettings": {
    "DefaultCollectionRadiusMeters": 50.0,
    "MaxPlacementRadiusMeters": 1000.0,
    "PointValues": {
      "Coin": 10,
      "Gem": 25,
      "Artifact": 50,
      "Chest": 100,
      "Custom": 20
    }
  }
}
```

## 📱 Android Permissions

The app requires the following permissions (configured in AndroidManifest.xml):
- `CAMERA` - For AR camera access
- `ACCESS_FINE_LOCATION` - For precise GPS coordinates
- `ACCESS_COARSE_LOCATION` - For approximate location
- `INTERNET` - For API communication

## 🧪 Testing

### API Testing

Use Swagger UI when running the API in development mode:
```
https://localhost:5001/swagger
```

### Sample API Requests

**Place a Treasure:**
```bash
POST /api/treasures
Content-Type: application/json

{
  "name": "Golden Coin",
  "type": 0,
  "latitude": 37.7749,
  "longitude": -122.4194,
  "pointValue": 10,
  "placedByUserId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "graphicResourcePath": "coin.png"
}
```

**Get Nearby Treasures:**
```bash
GET /api/treasures/nearby?latitude=37.7749&longitude=-122.4194&radiusMeters=100
```

## 🛠️ Technology Stack

- **.NET 8.0**: Core framework
- **ASP.NET Core**: Web API
- **.NET MAUI Blazor**: Mobile UI framework
- **Entity Framework Core**: ORM with SQL Server
- **MediatR**: CQRS pattern implementation
- **Xamarin.Google.ARCore**: AR functionality
- **Plugin.Maui.Audio**: Sound effects
- **Refit**: Type-safe API client
- **Swashbuckle**: API documentation

## 🗺️ Database Schema

**TreasureItems**
- Id (PK, Guid)
- Name, Type, PointValue
- Latitude, Longitude, Altitude (Owned GeoLocation)
- PlacedByUserId, PlacedAt
- IsCollected, CollectedByUserId, CollectedAt
- GraphicResourcePath

**UserProfiles**
- Id (PK, Guid)
- Username (Unique)
- TotalPoints
- CurrentMode

**InventoryItems**
- Id (PK, Guid)
- TreasureItemId (FK), UserId (FK)
- AcquiredAt, IsPlaced

## 📝 Implementation Notes

1. **Haversine Formula**: Used in `GeoLocation.DistanceTo()` for accurate distance calculations
2. **Owned Entities**: `GeoLocation` is configured as an owned entity in EF Core
3. **Spatial Queries**: Bounding box queries for efficient nearby treasure lookups
4. **Clean Architecture**: Dependencies flow inward (Presentation → Application → Domain)
5. **CQRS**: MediatR handlers separate commands from queries
6. **ARCore Integration**: Stub implementations provided; full AR requires platform-specific code

## 🔮 Future Enhancements

- [ ] Complete ARCore integration with 3D treasure models
- [ ] Real-time multiplayer features with SignalR
- [ ] Leaderboards and achievements
- [ ] Social features (friend lists, sharing)
- [ ] Advanced AR features (occlusion, lighting estimation)
- [ ] Offline mode with local caching
- [ ] iOS support with ARKit
- [ ] Custom treasure creation tools

## 📄 License

This project is provided as-is for educational and demonstration purposes.

## 👥 Contributing

Contributions are welcome! Please follow Clean Architecture principles and include appropriate tests.

## 🙏 Acknowledgments

- Built with .NET MAUI and ASP.NET Core
- Uses Google ARCore for augmented reality
- Inspired by location-based AR games