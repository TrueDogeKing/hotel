---
source_file: "src/CampCenter.Api/Properties/launchSettings.json"
type: "code"
community: "API Launch Settings"
location: "L4"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/API_Launch_Settings
---

# http

## Context

_Source: `src/CampCenter.Api/Properties/launchSettings.json` (defined near L4; showing L2–L23 of 23)._

```json
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "http://localhost:5298",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:7134;http://localhost:5298",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## Connections
- [[applicationUrl]] - `contains` [EXTRACTED]
- [[commandName]] - `contains` [EXTRACTED]
- [[dotnetRunMessages]] - `contains` [EXTRACTED]
- [[environmentVariables]] - `contains` [EXTRACTED]
- [[launchBrowser]] - `contains` [EXTRACTED]
- [[profiles]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/API_Launch_Settings