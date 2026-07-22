---
source_file: "frontend/eslint.config.js"
type: "code"
community: "ESLint Config File"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/ESLint_Config_File
---

# eslint.config.js

## Context

_Source: `frontend/eslint.config.js` (defined near L1; showing L1–L22 of 22)._

```javascript
import js from "@eslint/js";
import globals from "globals";
import reactHooks from "eslint-plugin-react-hooks";
import reactRefresh from "eslint-plugin-react-refresh";
import tseslint from "typescript-eslint";
import { defineConfig, globalIgnores } from "eslint/config";

export default defineConfig([
  globalIgnores(["dist"]),
  {
    files: ["**/*.{ts,tsx}"],
    extends: [
      js.configs.recommended,
      tseslint.configs.recommended,
      reactHooks.configs.flat.recommended,
      reactRefresh.configs.vite,
    ],
    languageOptions: {
      globals: globals.browser,
    },
  },
]);
```

#graphify/code #graphify/EXTRACTED #community/ESLint_Config_File