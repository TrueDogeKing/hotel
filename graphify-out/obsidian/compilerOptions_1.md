---
source_file: "frontend/tsconfig.node.json"
type: "code"
community: "TypeScript Node Config"
location: "L2"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/TypeScript_Node_Config
---

# compilerOptions

## Context

_Source: `frontend/tsconfig.node.json` (defined near L2; showing L1–L24 of 24)._

```json
{
  "compilerOptions": {
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
- [[allowImportingTsExtensions_1]] - `contains` [EXTRACTED]
- [[erasableSyntaxOnly_1]] - `contains` [EXTRACTED]
- [[lib_1]] - `contains` [EXTRACTED]
- [[module_1]] - `contains` [EXTRACTED]
- [[moduleDetection_1]] - `contains` [EXTRACTED]
- [[moduleResolution_1]] - `contains` [EXTRACTED]
- [[noEmit_1]] - `contains` [EXTRACTED]
- [[noFallthroughCasesInSwitch_1]] - `contains` [EXTRACTED]
- [[noUnusedLocals_1]] - `contains` [EXTRACTED]
- [[noUnusedParameters_1]] - `contains` [EXTRACTED]
- [[skipLibCheck_1]] - `contains` [EXTRACTED]
- [[target_1]] - `contains` [EXTRACTED]
- [[tsBuildInfoFile_1]] - `contains` [EXTRACTED]
- [[tsconfig.node.json]] - `contains` [EXTRACTED]
- [[types_1]] - `contains` [EXTRACTED]
- [[verbatimModuleSyntax_1]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/TypeScript_Node_Config