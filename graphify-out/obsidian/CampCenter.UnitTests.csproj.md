---
source_file: "tests/CampCenter.UnitTests/CampCenter.UnitTests.csproj"
type: "code"
community: "Project & NuGet Config"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Project__NuGet_Config
---

# CampCenter.UnitTests.csproj

## Context

_Source: `tests/CampCenter.UnitTests/CampCenter.UnitTests.csproj` — full file embedded (27 lines)._

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
    <PackageReference Include="FluentValidation" Version="12.1.1" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
    <PackageReference Include="NSubstitute" Version="5.3.0" />
    <PackageReference Include="xunit" Version="2.9.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.4" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\CampCenter.Domain\CampCenter.Domain.csproj" />
    <ProjectReference Include="..\..\src\CampCenter.Application\CampCenter.Application.csproj" />
    <ProjectReference Include="..\..\src\CampCenter.Infrastructure\CampCenter.Infrastructure.csproj" />
  </ItemGroup>
</Project>
```

## Connections
- [[CampCenter.Application.csproj]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.csproj]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.csproj]] - `imports` [EXTRACTED]
- [[CampCenter.slnx]] - `contains` [EXTRACTED]
- [[FluentValidation (12.1.1)]] - `imports` [EXTRACTED]
- [[Microsoft.NET.Sdk_4]] - `references` [EXTRACTED]
- [[Microsoft.NET.Test.Sdk (17.14.1)_1]] - `imports` [EXTRACTED]
- [[NSubstitute (5.3.0)]] - `imports` [EXTRACTED]
- [[coverlet.collector (6.0.4)_1]] - `imports` [EXTRACTED]
- [[net10.0_5]] - `references` [EXTRACTED]
- [[xunit (2.9.3)_1]] - `imports` [EXTRACTED]
- [[xunit.runner.visualstudio (3.1.4)_1]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Project__NuGet_Config