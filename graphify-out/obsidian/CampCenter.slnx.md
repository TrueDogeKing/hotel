---
source_file: "CampCenter.slnx"
type: "code"
community: "Project & NuGet Config"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Project__NuGet_Config
---

# CampCenter.slnx

## Context

_Source: `CampCenter.slnx` — full file embedded (12 lines)._

```xml
<Solution>
  <Folder Name="/src/">
    <Project Path="src/CampCenter.Api/CampCenter.Api.csproj" />
    <Project Path="src/CampCenter.Application/CampCenter.Application.csproj" />
    <Project Path="src/CampCenter.Domain/CampCenter.Domain.csproj" />
    <Project Path="src/CampCenter.Infrastructure/CampCenter.Infrastructure.csproj" />
  </Folder>
  <Folder Name="/tests/">
    <Project Path="tests/CampCenter.IntegrationTests/CampCenter.IntegrationTests.csproj" />
    <Project Path="tests/CampCenter.UnitTests/CampCenter.UnitTests.csproj" />
  </Folder>
</Solution>
```

## Connections
- [[CampCenter.Api.csproj]] - `contains` [EXTRACTED]
- [[CampCenter.Application.csproj]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.csproj]] - `contains` [EXTRACTED]
- [[CampCenter.Infrastructure.csproj]] - `contains` [EXTRACTED]
- [[CampCenter.IntegrationTests.csproj]] - `contains` [EXTRACTED]
- [[CampCenter.UnitTests.csproj]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Project__NuGet_Config