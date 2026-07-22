---
source_file: "frontend/package.json"
type: "code"
community: "Frontend Package Manifest"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Package_Manifest
---

# version

## Context

_Source: `frontend/package.json` (defined near L3; showing L1–L37 of 37)._

```json
{
  "name": "frontend",
  "version": "0.0.0",
  "private": true,
  "type": "module",
  "scripts": {
    "dev": "vite",
    "build": "tsc -b && vite build",
    "lint": "eslint .",
    "format": "prettier --write .",
    "format:check": "prettier --check .",
    "preview": "vite preview"
  },
  "dependencies": {
    "axios": "^1.18.1",
    "react": "^19.2.6",
    "react-dom": "^19.2.6",
    "react-router-dom": "^7.18.0",
    "i18next": "^25.3.2",
    "react-i18next": "^15.6.0"
  },
  "devDependencies": {
    "@eslint/js": "^10.0.1",
    "@types/node": "^24.12.3",
    "@types/react": "^19.2.14",
    "@types/react-dom": "^19.2.3",
    "@vitejs/plugin-react": "^6.0.1",
    "eslint": "^10.3.0",
    "eslint-plugin-react-hooks": "^7.1.1",
    "eslint-plugin-react-refresh": "^0.5.2",
    "globals": "^17.6.0",
    "prettier": "^3.8.5",
    "typescript": "~6.0.2",
    "typescript-eslint": "^8.59.2",
    "vite": "^8.0.12"
  }
}
```

## Connections
- [[package.json]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Package_Manifest