---
source_file: "src/CampCenter.Api/Background/BookingMaintenanceService.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# BookingMaintenanceService.cs

## Context

_Source: `src/CampCenter.Api/Background/BookingMaintenanceService.cs` (defined near L1; showing L1–L46 of 103)._

```csharp
using CampCenter.Application.Interfaces;
using CampCenter.Application.Services;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;

namespace CampCenter.Api.Background;

/// Periodic booking upkeep: releases rooms of expired unpaid holds and marks
/// finished sessions' bookings as Completed. A 2-hour grace window protects
/// bookings with an in-flight payment attempt from being swept mid-checkout.
public class BookingMaintenanceService : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan PaymentGrace = TimeSpan.FromHours(2);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BookingMaintenanceService> _logger;

    public BookingMaintenanceService(
        IServiceScopeFactory scopeFactory,
        ILogger<BookingMaintenanceService> logger
    )
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(Interval);
        do
        {
            try
            {
                await SweepAsync(stoppingToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "Booking maintenance sweep failed.");
            }
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task SweepAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
```

## Connections
- [[BookingMaintenanceService]] - `contains` [EXTRACTED]
- [[CampCenter.Api.Background]] - `contains` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Services]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces