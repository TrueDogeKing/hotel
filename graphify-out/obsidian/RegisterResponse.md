---
source_file: "src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs"
type: "code"
community: "Przelewy24 Payment Client"
location: "L127"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Przelewy24_Payment_Client
---

# RegisterResponse

## Context

_Source: `src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs` (defined near L127; showing L125–L138 of 138)._

```csharp
    }

    private sealed class RegisterResponse
    {
        [JsonPropertyName("data")]
        public RegisterData? Data { get; set; }
    }

    private sealed class RegisterData
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
```

## Connections
- [[Przelewy24Client.cs]] - `contains` [EXTRACTED]
- [[RegisterData]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Przelewy24_Payment_Client