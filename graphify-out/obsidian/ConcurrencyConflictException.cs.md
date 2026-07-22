---
source_file: "src/CampCenter.Domain/Exceptions/ConcurrencyConflictException.cs"
type: "code"
community: "Domain Exceptions"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# ConcurrencyConflictException.cs

## Context

_Source: `src/CampCenter.Domain/Exceptions/ConcurrencyConflictException.cs` (defined near L1; showing L1–L12 of 12)._

```csharp
namespace CampCenter.Domain.Exceptions;

/// Signals an optimistic concurrency conflict: the entity was modified by another
/// process between read and write. Mapped to HTTP 409.
public class ConcurrencyConflictException : Exception
{
    public ConcurrencyConflictException(string message)
        : base(message) { }

    public ConcurrencyConflictException(string message, Exception innerException)
        : base(message, innerException) { }
}
```

## Connections
- [[CampCenter.Domain.Exceptions]] - `contains` [EXTRACTED]
- [[ConcurrencyConflictException]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions