---
source_file: "src/CampCenter.Domain/Exceptions/ForbiddenActionException.cs"
type: "code"
community: "Domain Exceptions"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# ForbiddenActionException.cs

## Context

_Source: `src/CampCenter.Domain/Exceptions/ForbiddenActionException.cs` (defined near L1; showing L1–L9 of 9)._

```csharp
namespace CampCenter.Domain.Exceptions;

/// Signals that the authenticated caller is not allowed to perform the requested action
/// (e.g. changing another account's password). Mapped to HTTP 403.
public class ForbiddenActionException : Exception
{
    public ForbiddenActionException(string message)
        : base(message) { }
}
```

## Connections
- [[CampCenter.Domain.Exceptions]] - `contains` [EXTRACTED]
- [[ForbiddenActionException]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions