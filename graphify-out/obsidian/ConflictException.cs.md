---
source_file: "src/CampCenter.Domain/Exceptions/ConflictException.cs"
type: "code"
community: "Domain Exceptions"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# ConflictException.cs

## Context

_Source: `src/CampCenter.Domain/Exceptions/ConflictException.cs` (defined near L1; showing L1–L8 of 8)._

```csharp
namespace CampCenter.Domain.Exceptions;

/// Signals that a unique value (login or email) is already taken. Mapped to HTTP 409.
public class ConflictException : Exception
{
    public ConflictException(string message)
        : base(message) { }
}
```

## Connections
- [[CampCenter.Domain.Exceptions]] - `contains` [EXTRACTED]
- [[ConflictException]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions