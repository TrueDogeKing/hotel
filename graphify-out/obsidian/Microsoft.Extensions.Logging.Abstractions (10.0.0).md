---
source_file: "src/CampCenter.Application/CampCenter.Application.csproj"
type: "code"
community: "Project & NuGet Config"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Project__NuGet_Config
---

# Microsoft.Extensions.Logging.Abstractions (10.0.0)

## Context

_Source: `src/CampCenter.Application/CampCenter.Application.csproj` — full file embedded (17 lines)._

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <ProjectReference Include="..\CampCenter.Domain\CampCenter.Domain.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="12.1.1" />
    <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="10.0.0" />
    <PackageReference Include="Microsoft.Extensions.Options" Version="10.0.0" />
  </ItemGroup>

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

## Connections
- [[CampCenter.Application.csproj]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Project__NuGet_Config