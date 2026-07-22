---
source_file: "src/CampCenter.Domain/Exceptions/BusinessRuleViolationException.cs"
type: "code"
community: "Domain Exceptions"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# BusinessRuleViolationException.cs

## Context

_Source: `src/CampCenter.Domain/Exceptions/BusinessRuleViolationException.cs` (defined near L1; showing L1–L9 of 9)._

```csharp
namespace CampCenter.Domain.Exceptions;

/// Signals that a request violates a domain rule (e.g. an invalid category/subcategory
/// combination). Mapped to HTTP 400.
public class BusinessRuleViolationException : Exception
{
    public BusinessRuleViolationException(string message)
        : base(message) { }
}
```

## Connections
- [[BusinessRuleViolationException]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Exceptions]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions