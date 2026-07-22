---
source_file: "frontend/index.html"
type: "code"
community: "Frontend Theme Bootstrap"
tags:
  - graphify/code
  - graphify/INFERRED
  - community/Frontend_Theme_Bootstrap
---

# Frontend index.html (SPA entry)

## Context

_Source: `frontend/index.html` — full file embedded (23 lines)._

```html
<!doctype html>
<html lang="pl">
  <head>
    <meta charset="UTF-8" />
    <link rel="icon" type="image/svg+xml" href="/favicon.svg" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="color-scheme" content="dark light" />
    <title>CampCenter</title>
    <script>
      // Apply the saved theme before first paint (dark is the default).
      try {
        document.documentElement.dataset.theme =
          localStorage.getItem("theme") === "light" ? "light" : "dark";
      } catch (e) {
        document.documentElement.dataset.theme = "dark";
      }
    </script>
  </head>
  <body>
    <div id="root"></div>
    <script type="module" src="/src/main.tsx"></script>
  </body>
</html>
```

## Connections
- [[Frontend README (React + TS + Vite template)]] - `conceptually_related_to` [INFERRED]
- [[Pre-paint Theme Bootstrap (localStorage)]] - `references` [EXTRACTED]

#graphify/code #graphify/INFERRED #community/Frontend_Theme_Bootstrap