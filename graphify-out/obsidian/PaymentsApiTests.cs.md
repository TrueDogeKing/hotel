---
source_file: "tests/CampCenter.IntegrationTests/PaymentsApiTests.cs"
type: "code"
community: "Application DTO Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_DTO_Namespaces
---

# PaymentsApiTests.cs

## Context

_Source: `tests/CampCenter.IntegrationTests/PaymentsApiTests.cs` (defined near L1; showing L1–L46 of 242)._

```csharp
using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CampCenter.IntegrationTests;

/// Records register/verify calls and lets tests craft valid "notifications".
public class FakePaymentGateway : IPaymentGateway
{
    public List<GatewayRegisterRequest> Registered { get; } = [];

    public List<(string SessionId, long Amount, long OrderId)> Verified { get; } = [];

    public bool RejectSignature { get; set; }

    public Task<GatewayRegisterResult> RegisterTransactionAsync(
        GatewayRegisterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        Registered.Add(request);
        return Task.FromResult(
            new GatewayRegisterResult(
                "fake-token",
                $"https://sandbox.przelewy24.pl/trnRequest/fake-token-{request.SessionId}"
            )
        );
    }

    public bool VerifyNotificationSignature(GatewayNotification notification) => !RejectSignature;

    public Task VerifyTransactionAsync(
        string sessionId,
        long amountGrosze,
        long orderId,
        CancellationToken cancellationToken = default
    )
    {
        Verified.Add((sessionId, amountGrosze, orderId));
        return Task.CompletedTask;
```

## Connections
- [[CampCenter.Application.DTOs.Public]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.Rooms]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.Sessions]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.IntegrationTests]] - `contains` [EXTRACTED]
- [[FakePaymentGateway]] - `contains` [EXTRACTED]
- [[PaymentsApiTests]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_DTO_Namespaces