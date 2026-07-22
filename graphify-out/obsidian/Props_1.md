---
source_file: "frontend/src/components/Select.tsx"
type: "code"
community: "Select Component"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Select_Component
---

# Props

## Context

_Source: `frontend/src/components/Select.tsx` (defined near L8; showing L6–L53 of 178)._

```tsx
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
  // clamped to the viewport, flipped above when the space below can't fit it.
  useLayoutEffect(() => {
    if (!open) return;
    const trigger = triggerRef.current;
    const list = listRef.current;
    if (!trigger || !list) return;
    const rect = trigger.getBoundingClientRect();
```

## Connections
- [[Select.tsx]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Select_Component