---
source_file: "frontend/src/api/errors.ts"
type: "code"
community: "Frontend API Error Handling"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_API_Error_Handling
---

# ProblemDetails

## Context

_Source: `frontend/src/api/errors.ts` (defined near L3; showing L1–L30 of 30)._

```typescript
import { isAxiosError } from "axios";

interface ProblemDetails {
  title?: string;
  detail?: string;
  // FluentValidation / ASP.NET ValidationProblem: field name -> messages.
  errors?: Record<string, string[]>;
}

// Extracts a human-readable message from an API error. Handles RFC7807 ProblemDetails:
// field-level validation errors, the `detail` message (business rules / conflicts), or the title.
export function getApiErrorMessage(error: unknown, fallback = "Something went wrong."): string {
  if (isAxiosError(error)) {
    const raw = error.response?.data;
    // Some endpoints (e.g. avatar upload) return a plain string body via BadRequest(string)
    // instead of a ProblemDetails object.
    if (typeof raw === "string" && raw.trim()) return raw;

    const data = raw as ProblemDetails | undefined;
    if (data?.errors) {
      const messages = Object.values(data.errors).flat();
      if (messages.length > 0) {
        return messages.join(" ");
      }
    }
    if (data?.detail) return data.detail;
    if (data?.title) return data.title;
  }
  return fallback;
}
```

## Connections
- [[errors.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_API_Error_Handling