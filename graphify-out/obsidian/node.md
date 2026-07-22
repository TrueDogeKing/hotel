---
source_file: "frontend/tsconfig.node.json"
type: "concept"
community: "TypeScript Node Config"
location: "L7"
tags:
  - graphify/concept
  - graphify/EXTRACTED
  - community/TypeScript_Node_Config
---

# node

## Context

_Source: `frontend/tsconfig.node.json` (defined near L7; showing L5–L24 of 24)._

```json
    "lib": ["ES2023"],
    "module": "esnext",
    "types": ["node"],
    "skipLibCheck": true,

    /* Bundler mode */
    "moduleResolution": "bundler",
    "allowImportingTsExtensions": true,
    "verbatimModuleSyntax": true,
    "moduleDetection": "force",
    "noEmit": true,

    /* Linting */
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "erasableSyntaxOnly": true,
    "noFallthroughCasesInSwitch": true
  },
  "include": ["vite.config.ts"]
}
```

## Connections
- [[types_1]] - `extends` [EXTRACTED]

#graphify/concept #graphify/EXTRACTED #community/TypeScript_Node_Config