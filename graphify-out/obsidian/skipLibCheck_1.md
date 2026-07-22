---
source_file: "frontend/tsconfig.node.json"
type: "code"
community: "TypeScript Node Config"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/TypeScript_Node_Config
---

# skipLibCheck

## Context

_Source: `frontend/tsconfig.node.json` (defined near L8; showing L6–L24 of 24)._

```json
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
- [[compilerOptions_1]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/TypeScript_Node_Config