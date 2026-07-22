---
source_file: "src/CampCenter.Infrastructure/CampCenter.Infrastructure.csproj"
type: "code"
community: "Project & NuGet Config"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Project__NuGet_Config
---

# CampCenter.Infrastructure.csproj

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
- [[BCrypt.Net-Next (4.2.0)]] - `imports` [EXTRACTED]
- [[CampCenter.Api.csproj]] - `imports` [EXTRACTED]
- [[CampCenter.Application.csproj]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.csproj]] - `imports` [EXTRACTED]
- [[CampCenter.UnitTests.csproj]] - `imports` [EXTRACTED]
- [[CampCenter.slnx]] - `contains` [EXTRACTED]
- [[MailKit (4.14.1)]] - `imports` [EXTRACTED]
- [[Microsoft.EntityFrameworkCore.Design (10.0.4)_1]] - `imports` [EXTRACTED]
- [[Microsoft.Extensions.Http (10.0.0)]] - `imports` [EXTRACTED]
- [[Microsoft.NET.Sdk_2]] - `references` [EXTRACTED]
- [[Npgsql.EntityFrameworkCore.PostgreSQL (10.0.2)]] - `imports` [EXTRACTED]
- [[System.IdentityModel.Tokens.Jwt (8.19.1)]] - `imports` [EXTRACTED]
- [[net10.0_3]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Project__NuGet_Config