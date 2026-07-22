---
source_file: "frontend/src/components/Select.tsx"
type: "code"
community: "Select Component"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Select_Component
---

# Select.tsx

## Context

_Source: `frontend/src/components/Select.tsx` (defined near L1; showing L1–L46 of 178)._

```tsx
import { useEffect, useId, useLayoutEffect, useRef, useState } from "react";

export interface SelectOption {
  value: string;
  label: string;
}

interface Props {
  id?: string;
  value: string;
  options: SelectOption[];
  onChange: (value: string) => void;
  title?: string;
  ariaLabel?: string;
}

// Themed replacement for the native <select>. The browser paints a native dropdown
// list itself: it ignores the app theme (a white list over the dark UI), sizes to
// the longest option instead of the control, and never wraps long device names.
// This trigger + listbox pair uses the app tokens, matches the trigger's width
// exactly, and clamps each option to two lines. The listbox is position:fixed so
// it escapes overflow/scroll clipping (settings modal, call popover) and flips
// above the trigger when the viewport has no room below.
export default function Select({ id, value, options, onChange, title, ariaLabel }: Props) {
  const [open, setOpen] = useState(false);
  const [activeIndex, setActiveIndex] = useState(0);
  const rootRef = useRef<HTMLDivElement>(null);
  const triggerRef = useRef<HTMLButtonElement>(null);
  const listRef = useRef<HTMLUListElement>(null);
  const uid = useId();

  const selectedIndex = options.findIndex((o) => o.value === value);

  function openList() {
    setActiveIndex(selectedIndex >= 0 ? selectedIndex : 0);
    setOpen(true);
  }

  function commit(index: number) {
    const option = options[index];
    setOpen(false);
    triggerRef.current?.focus();
    if (option && option.value !== value) onChange(option.value);
  }

  // Anchor the fixed listbox to the trigger: same width, right below it, height
```

## Connections
- [[Props_1]] - `contains` [EXTRACTED]
- [[Select()]] - `contains` [EXTRACTED]
- [[SelectOption]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Select_Component