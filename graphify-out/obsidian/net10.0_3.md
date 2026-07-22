---
source_file: "src/CampCenter.Infrastructure/CampCenter.Infrastructure.csproj"
type: "concept"
community: "Project & NuGet Config"
tags:
  - graphify/concept
  - graphify/EXTRACTED
  - community/Project__NuGet_Config
---

# net10.0

## Context

_Source: `src/CampCenter.Infrastructure/CampCenter.Infrastructure.csproj` — full file embedded (24 lines)._

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <ProjectReference Include="..\CampCenter.Domain\CampCenter.Domain.csproj" />
    <ProjectReference Include="..\CampCenter.Application\CampCenter.Application.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="BCrypt.Net-Next" Version="4.2.0" />
    <PackageReference Include="MailKit" Version="4.14.1" />
    <PackageReference Include="Microsoft.Extensions.Http" Version="10.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.4">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="10.0.2" />
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.19.1" />
  </ItemGroup>

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

## Connections
- [[CampCenter.Infrastructure.csproj]] - `references` [EXTRACTED]

#graphify/concept #graphify/EXTRACTED #community/Project__NuGet_Config