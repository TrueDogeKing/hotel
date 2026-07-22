---
source_file: "src/CampCenter.Domain/Exceptions/BusinessRuleViolationException.cs"
type: "code"
community: "Domain Exceptions"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# BusinessRuleViolationException

## Context

_Source: `src/CampCenter.Domain/Exceptions/BusinessRuleViolationException.cs` (defined near L5; showing L3–L9 of 9)._

```csharp
/// Signals that a request violates a domain rule (e.g. an invalid category/subcategory
/// combination). Mapped to HTTP 400.
public class BusinessRuleViolationException : Exception
{
    public BusinessRuleViolationException(string message)
        : base(message) { }
}
```

## Connections
- [[BusinessRuleViolationException.cs]] - `contains` [EXTRACTED]
- [[Exception_1]] - `inherits` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions