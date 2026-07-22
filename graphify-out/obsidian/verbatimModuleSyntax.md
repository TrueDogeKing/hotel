---
source_file: "frontend/tsconfig.app.json"
type: "code"
community: "TypeScript App Config"
location: "L13"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/TypeScript_App_Config
---

# verbatimModuleSyntax

## Context

_Source: `frontend/tsconfig.app.json` (defined near L13; showing L11–L25 of 25)._

```json
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
- [[compilerOptions]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/TypeScript_App_Config