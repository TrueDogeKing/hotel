---
source_file: "frontend/src/components/icons.tsx"
type: "code"
community: "Frontend Icon Components"
location: "L94"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Icon_Components
---

# IconClose()

## Context

_Source: `frontend/src/components/icons.tsx` (defined near L94; showing L92–L139 of 183)._

```tsx
}

export function IconClose(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
      <path d="M18 6 6 18" />
      <path d="M6 6l12 12" />
    </Icon>
  );
}

export function IconMic(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
      <rect x="9" y="2" width="6" height="12" rx="3" />
      <path d="M5 10a7 7 0 0 0 14 0" />
      <path d="M12 19v3" />
      <path d="M8 22h8" />
    </Icon>
  );
}

export function IconMicOff(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
      <path d="M2 2l20 20" />
      <path d="M9 9v3a3 3 0 0 0 4.24 2.73" />
      <path d="M15 9.34V5a3 3 0 0 0-5.94-.6" />
      <path d="M5 10a7 7 0 0 0 10.3 6.15" />
      <path d="M19 10a7 7 0 0 1-.34 2.16" />
      <path d="M12 19v3" />
      <path d="M8 22h8" />
    </Icon>
  );
}

export function IconCamera(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
      <rect x="2" y="6" width="14" height="12" rx="2" />
      <path d="M16 10.5 22 7v10l-6-3.5" />
    </Icon>
  );
}

export function IconCameraOff(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
```

## Connections
- [[icons.tsx]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Icon_Components