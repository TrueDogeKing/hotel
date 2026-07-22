---
source_file: "tests/CampCenter.IntegrationTests/CampCenter.IntegrationTests.csproj"
type: "code"
community: "Project & NuGet Config"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Project__NuGet_Config
---

# CampCenter.IntegrationTests.csproj

## Context

_Source: `tests/CampCenter.IntegrationTests/CampCenter.IntegrationTests.csproj` — full file embedded (25 lines)._

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.4" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="10.0.9" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
    <PackageReference Include="Testcontainers.PostgreSql" Version="4.12.0" />
    <PackageReference Include="xunit" Version="2.9.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.4" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\CampCenter.Api\CampCenter.Api.csproj" />
  </ItemGroup>
</Project>
```

## Connections
- [[CampCenter.Api.csproj]] - `imports` [EXTRACTED]
- [[CampCenter.slnx]] - `contains` [EXTRACTED]
- [[Microsoft.AspNetCore.Mvc.Testing (10.0.9)]] - `imports` [EXTRACTED]
- [[Microsoft.NET.Sdk_3]] - `references` [EXTRACTED]
- [[Microsoft.NET.Test.Sdk (17.14.1)]] - `imports` [EXTRACTED]
- [[Testcontainers.PostgreSql (4.12.0)]] - `imports` [EXTRACTED]
- [[coverlet.collector (6.0.4)]] - `imports` [EXTRACTED]
- [[net10.0_4]] - `references` [EXTRACTED]
- [[xunit (2.9.3)]] - `imports` [EXTRACTED]
- [[xunit.runner.visualstudio (3.1.4)]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Project__NuGet_Config