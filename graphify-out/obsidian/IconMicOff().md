---
source_file: "frontend/src/components/icons.tsx"
type: "code"
community: "Frontend Icon Components"
location: "L114"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Icon_Components
---

# IconMicOff()

## Context

_Source: `frontend/src/components/icons.tsx` (defined near L114; showing L112–L159 of 183)._

```tsx
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
      <path d="M2 2l20 20" />
      <path d="M16 10.5 22 7v10l-6-3.5" />
      <path d="M9.34 6H14a2 2 0 0 1 2 2v4.66" />
      <path d="M2 8.34V18a2 2 0 0 0 2 2h9.66" />
    </Icon>
  );
}

export function IconScreenShare(props: SVGProps<SVGSVGElement>) {
  return (
    <Icon {...props}>
      <rect x="2" y="4" width="20" height="13" rx="2" />
      <path d="M8 21h8" />
      <path d="M12 17v4" />
      <path d="M12 7v6" />
      <path d="M9 10l3-3 3 3" />
    </Icon>
  );
}

```

## Connections
- [[icons.tsx]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Icon_Components