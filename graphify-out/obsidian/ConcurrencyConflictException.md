---
source_file: "src/CampCenter.Domain/Exceptions/ConcurrencyConflictException.cs"
type: "code"
community: "Domain Exceptions"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# ConcurrencyConflictException

## Context

_Source: `src/CampCenter.Domain/Exceptions/ConcurrencyConflictException.cs` (defined near L5; showing L3–L12 of 12)._

```csharp
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
- [[ConcurrencyConflictException.cs]] - `contains` [EXTRACTED]
- [[Exception_1]] - `inherits` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions