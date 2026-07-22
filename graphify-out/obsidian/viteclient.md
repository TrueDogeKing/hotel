---
source_file: "frontend/tsconfig.app.json"
type: "concept"
community: "TypeScript App Config"
location: "L7"
tags:
  - graphify/concept
  - graphify/EXTRACTED
  - community/TypeScript_App_Config
---

# vite/client

## Context

_Source: `frontend/tsconfig.app.json` (defined near L7; showing L5–L25 of 25)._

```json
    "lib": ["ES2023", "DOM"],
    "module": "esnext",
    "types": ["vite/client"],
    "skipLibCheck": true,

    /* Bundler mode */
    "moduleResolution": "bundler",
    "allowImportingTsExtensions": true,
    "verbatimModuleSyntax": true,
    "moduleDetection": "force",
    "noEmit": true,
    "jsx": "react-jsx",

    /* Linting */
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "erasableSyntaxOnly": true,
    "noFallthroughCasesInSwitch": true
  },
  "include": ["src"]
}
```

## Connections
- [[types]] - `extends` [EXTRACTED]

#graphify/concept #graphify/EXTRACTED #community/TypeScript_App_Config