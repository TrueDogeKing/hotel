---
source_file: "src/CampCenter.Api/Properties/launchSettings.json"
type: "code"
community: "API Launch Settings"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/API_Launch_Settings
---

# launchSettings.json

## Context

_Source: `src/CampCenter.Api/Properties/launchSettings.json` (defined near L1; showing L1–L23 of 23)._

```json
﻿{
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
- [[$schema]] - `contains` [EXTRACTED]
- [[profiles]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/API_Launch_Settings