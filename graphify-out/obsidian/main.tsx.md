---
source_file: "frontend/src/main.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# main.tsx

## Context

_Source: `frontend/src/main.tsx` (defined near L1; showing L1–L11 of 11)._

```tsx
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import "./i18n";
import App from "./App.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <App />
  </StrictMode>,
);
```

## Connections
- [[App()]] - `imports` [EXTRACTED]
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[index.ts]] - `imports_from` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n