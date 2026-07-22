---
source_file: "src/CampCenter.Domain/Exceptions/NotFoundException.cs"
type: "code"
community: "Domain Exceptions"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# NotFoundException.cs

## Context

_Source: `src/CampCenter.Domain/Exceptions/NotFoundException.cs` (defined near L1; showing L1–L9 of 9)._

```csharp
namespace CampCenter.Domain.Exceptions;

/// Thrown when a requested resource does not exist (mapped to HTTP 404).
public class NotFoundException : Exception
{
    /// Creates the exception with a message describing the missing resource.
    public NotFoundException(string message)
        : base(message) { }
}
```

## Connections
- [[CampCenter.Domain.Exceptions]] - `contains` [EXTRACTED]
- [[NotFoundException]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions