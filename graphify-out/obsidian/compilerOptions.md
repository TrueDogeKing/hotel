---
source_file: "frontend/tsconfig.app.json"
type: "code"
community: "TypeScript App Config"
location: "L2"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/TypeScript_App_Config
---

# compilerOptions

## Context

_Source: `frontend/tsconfig.app.json` (defined near L2; showing L1–L25 of 25)._

```json
{
  "compilerOptions": {
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
- [[allowImportingTsExtensions]] - `contains` [EXTRACTED]
- [[erasableSyntaxOnly]] - `contains` [EXTRACTED]
- [[jsx]] - `contains` [EXTRACTED]
- [[lib]] - `contains` [EXTRACTED]
- [[module]] - `contains` [EXTRACTED]
- [[moduleDetection]] - `contains` [EXTRACTED]
- [[moduleResolution]] - `contains` [EXTRACTED]
- [[noEmit]] - `contains` [EXTRACTED]
- [[noFallthroughCasesInSwitch]] - `contains` [EXTRACTED]
- [[noUnusedLocals]] - `contains` [EXTRACTED]
- [[noUnusedParameters]] - `contains` [EXTRACTED]
- [[skipLibCheck]] - `contains` [EXTRACTED]
- [[target]] - `contains` [EXTRACTED]
- [[tsBuildInfoFile]] - `contains` [EXTRACTED]
- [[tsconfig.app.json]] - `contains` [EXTRACTED]
- [[types]] - `contains` [EXTRACTED]
- [[verbatimModuleSyntax]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/TypeScript_App_Config