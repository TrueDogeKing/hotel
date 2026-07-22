---
source_file: "src/CampCenter.Api/Properties/launchSettings.json"
type: "code"
community: "API Launch Settings"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/API_Launch_Settings
---

# profiles

## Context

_Source: `src/CampCenter.Api/Properties/launchSettings.json` (defined near L3; showing L1–L23 of 23)._

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
- [[http]] - `contains` [EXTRACTED]
- [[https]] - `contains` [EXTRACTED]
- [[launchSettings.json]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/API_Launch_Settings