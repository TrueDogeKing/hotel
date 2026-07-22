---
source_file: "frontend/src/components/icons.tsx"
type: "code"
community: "Frontend Icon Components"
location: "L39"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Icon_Components
---

# IconLogOut()

## Context

_Source: `frontend/src/components/icons.tsx` (defined near L39; showing L37–L84 of 183)._

```tsx
}

export function IconLogOut(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
      <path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4" />
      <path d="M16 17l5-5-5-5" />
      <path d="M21 12H9" />
    </Icon>
  );
}

export function IconMoon(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
      <path d="M21 12.79A9 9 0 1 1 11.21 3 7 7 0 0 0 21 12.79z" />
    </Icon>
  );
}

export function IconSun(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
      <circle cx="12" cy="12" r="4.5" />
      <path d="M12 2v2M12 20v2M4.9 4.9l1.4 1.4M17.7 17.7l1.4 1.4M2 12h2M20 12h2M4.9 19.1l1.4-1.4M17.7 6.3l1.4-1.4" />
    </Icon>
  );
}

export function IconMessage(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
      <path d="M21 15a2 2 0 0 1-2 2H8l-4 4V5a2 2 0 0 1 2-2h13a2 2 0 0 1 2 2z" />
    </Icon>
  );
}

export function IconUsers(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
      <path d="M16 20v-1a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v1" />
      <circle cx="9" cy="8" r="3.5" />
      <path d="M22 20v-1a4 4 0 0 0-3-3.85" />
      <path d="M16 4.15A4 4 0 0 1 16 12" />
    </Icon>
  );
}

```

## Connections
- [[icons.tsx]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Icon_Components