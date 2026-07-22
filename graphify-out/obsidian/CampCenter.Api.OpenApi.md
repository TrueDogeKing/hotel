---
source_file: "src/CampCenter.Api/OpenApi/BearerSecuritySchemeTransformer.cs"
type: "code"
community: "OpenAPI Security Scheme"
location: "L4"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/OpenAPI_Security_Scheme
---

# CampCenter.Api.OpenApi

## Context

_Source: `src/CampCenter.Api/OpenApi/BearerSecuritySchemeTransformer.cs` (defined near L4; showing L2–L42 of 42)._

```csharp
using Microsoft.OpenApi;

namespace CampCenter.Api.OpenApi;

/// Registers the JWT Bearer security scheme on the OpenAPI document so the UI shows an
/// "Authorize" button and protected endpoints.
public class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    private const string SchemeName = "Bearer";

    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken
    )
    {
        var scheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Paste the JWT access token (without the 'Bearer' prefix).",
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes[SchemeName] = scheme;

        document.Security ??= new List<OpenApiSecurityRequirement>();
        document.Security.Add(
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(SchemeName, document, null)] =
                    new List<string>(),
            }
        );

        return Task.CompletedTask;
    }
}
```

## Connections
- [[BearerSecuritySchemeTransformer.cs]] - `contains` [EXTRACTED]
- [[Program.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/OpenAPI_Security_Scheme