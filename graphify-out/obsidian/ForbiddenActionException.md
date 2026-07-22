---
source_file: "src/CampCenter.Domain/Exceptions/ForbiddenActionException.cs"
type: "code"
community: "Domain Exceptions"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# ForbiddenActionException

## Context

_Source: `src/CampCenter.Domain/Exceptions/ForbiddenActionException.cs` (defined near L5; showing L3–L9 of 9)._

```csharp
/// Signals that the authenticated caller is not allowed to perform the requested action
/// (e.g. changing another account's password). Mapped to HTTP 403.
public class ForbiddenActionException : Exception
{
    public ForbiddenActionException(string message)
        : base(message) { }
}
```

## Connections
- [[Exception_1]] - `inherits` [EXTRACTED]
- [[ForbiddenActionException.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions