---
source_file: "src/CampCenter.Domain/Exceptions/NotFoundException.cs"
type: "code"
community: "Domain Exceptions"
location: "L4"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# NotFoundException

## Context

_Source: `src/CampCenter.Domain/Exceptions/NotFoundException.cs` (defined near L4; showing L2–L9 of 9)._

```csharp

/// Thrown when a requested resource does not exist (mapped to HTTP 404).
public class NotFoundException : Exception
{
    /// Creates the exception with a message describing the missing resource.
    public NotFoundException(string message)
        : base(message) { }
}
```

## Connections
- [[Exception_1]] - `inherits` [EXTRACTED]
- [[NotFoundException.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions