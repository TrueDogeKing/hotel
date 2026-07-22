---
source_file: "frontend/tsconfig.node.json"
type: "code"
community: "TypeScript Node Config"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/TypeScript_Node_Config
---

# lib

## Context

_Source: `frontend/tsconfig.node.json` (defined near L5; showing L3–L24 of 24)._

```json
    "tsBuildInfoFile": "./node_modules/.tmp/tsconfig.node.tsbuildinfo",
    "target": "es2023",
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
- [[ES2023_1]] - `extends` [EXTRACTED]
- [[compilerOptions_1]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/TypeScript_Node_Config