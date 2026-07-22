---
source_file: "src/CampCenter.Api/Properties/launchSettings.json"
type: "code"
community: "API Launch Settings"
location: "L9"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/API_Launch_Settings
---

# environmentVariables

## Context

_Source: `src/CampCenter.Api/Properties/launchSettings.json` (defined near L9; showing L7–L23 of 23)._

```json
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
- [[ASPNETCORE_ENVIRONMENT]] - `contains` [EXTRACTED]
- [[http]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/API_Launch_Settings