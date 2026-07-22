---
source_file: "frontend/tsconfig.app.json"
type: "code"
community: "TypeScript App Config"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/TypeScript_App_Config
---

# skipLibCheck

## Context

_Source: `frontend/tsconfig.app.json` (defined near L8; showing L6–L25 of 25)._

```json
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
- [[compilerOptions]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/TypeScript_App_Config