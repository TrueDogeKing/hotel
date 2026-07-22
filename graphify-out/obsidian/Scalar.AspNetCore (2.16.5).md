---
source_file: "src/CampCenter.Api/CampCenter.Api.csproj"
type: "code"
community: "Project & NuGet Config"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Project__NuGet_Config
---

# Scalar.AspNetCore (2.16.5)

## Context

_Source: `src/CampCenter.Api/CampCenter.Api.csproj` — full file embedded (24 lines)._

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="10.0.9" />
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.9" />
    <!-- Explicit pin: the transitive 2.0.0 has a known vulnerability (GHSA-v5pm-xwqc-g5wc). -->
    <PackageReference Include="Microsoft.OpenApi" Version="2.10.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.4">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Scalar.AspNetCore" Version="2.16.5" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\CampCenter.Application\CampCenter.Application.csproj" />
    <ProjectReference Include="..\CampCenter.Infrastructure\CampCenter.Infrastructure.csproj" />
  </ItemGroup>
</Project>
```

## Connections
- [[CampCenter.Api.csproj]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Project__NuGet_Config