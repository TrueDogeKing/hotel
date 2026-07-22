---
source_file: "src/CampCenter.Infrastructure/Persistence/DesignTimeDbContextFactory.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# DesignTimeDbContextFactory

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/DesignTimeDbContextFactory.cs` (defined near L8; showing L6–L23 of 23)._

```csharp
/// EF Core context factory used by tooling (e.g. <c>dotnet ef migrations</c>)
/// at design time, independently of the application host.
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Connection string fetched from the environment variable or default for development.
        var connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=campcenter;Username=campcenter;Password=campcenter";

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new AppDbContext(options);
    }
}
```

## Connections
- [[.CreateDbContext()]] - `method` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[DesignTimeDbContextFactory.cs]] - `contains` [EXTRACTED]
- [[IDesignTimeDbContextFactory]] - `implements` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities