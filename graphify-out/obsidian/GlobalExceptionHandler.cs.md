---
source_file: "src/CampCenter.Api/Errors/GlobalExceptionHandler.cs"
type: "code"
community: "Domain Exceptions"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain_Exceptions
---

# GlobalExceptionHandler.cs

## Context

_Source: `src/CampCenter.Api/Errors/GlobalExceptionHandler.cs` (defined near L1; showing L1–L46 of 89)._

```csharp
using CampCenter.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Api.Errors;

/// Maps unhandled exceptions to consistent ProblemDetails responses. Domain exceptions map to
/// their dedicated status codes; anything else becomes a 500 without leaking internal details.
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    /// Creates the handler with dependencies.
    public GlobalExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger
    )
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var (statusCode, title, detail) = Map(exception);

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(
                exception,
                "Unhandled exception processing {Method} {Path}.",
                httpContext.Request.Method,
                httpContext.Request.Path
            );
        }

        httpContext.Response.StatusCode = statusCode;

        return await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
```

## Connections
- [[CampCenter.Api.Errors]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Exceptions]] - `imports` [EXTRACTED]
- [[GlobalExceptionHandler]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain_Exceptions