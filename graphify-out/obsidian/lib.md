---
source_file: "frontend/tsconfig.app.json"
type: "code"
community: "TypeScript App Config"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/TypeScript_App_Config
---

# lib

## Context

_Source: `frontend/tsconfig.app.json` (defined near L5; showing L3–L25 of 25)._

```json
    "tsBuildInfoFile": "./node_modules/.tmp/tsconfig.app.tsbuildinfo",
    "target": "es2023",
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
- [[DOM]] - `extends` [EXTRACTED]
- [[ES2023]] - `extends` [EXTRACTED]
- [[compilerOptions]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/TypeScript_App_Config