---
source_file: "frontend/tsconfig.node.json"
type: "code"
community: "TypeScript Node Config"
location: "L15"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/TypeScript_Node_Config
---

# noEmit

## Context

_Source: `frontend/tsconfig.node.json` (defined near L15; showing L13–L24 of 24)._

```json
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