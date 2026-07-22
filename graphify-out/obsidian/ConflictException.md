---
source_file: "src/CampCenter.Domain/Exceptions/ConflictException.cs"
type: "code"
community: "Domain Exceptions"
location: "L4"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# ConflictException

## Context

_Source: `src/CampCenter.Domain/Exceptions/ConflictException.cs` (defined near L4; showing L2–L8 of 8)._

```csharp

/// Signals that a unique value (login or email) is already taken. Mapped to HTTP 409.
public class ConflictException : Exception
{
    public ConflictException(string message)
        : base(message) { }
}
```

## Connections
- [[ConflictException.cs]] - `contains` [EXTRACTED]
- [[Exception_1]] - `inherits` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions