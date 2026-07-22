---
source_file: "frontend/src/components/Select.tsx"
type: "code"
community: "Select Component"
location: "L24"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Select_Component
---

# Select()

## Context

_Source: `frontend/src/components/Select.tsx` (defined near L24; showing L22–L69 of 178)._

```tsx
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
    list.style.left = `${rect.left}px`;
    list.style.width = `${rect.width}px`;
    list.style.maxHeight = "none";
    const natural = list.offsetHeight;
    const below = window.innerHeight - rect.bottom - 12;
    const above = rect.top - 12;
    const flip = natural > below && above > below;
    const height = Math.min(natural, flip ? above : below);
    list.style.maxHeight = `${height}px`;
    list.style.top = flip ? `${rect.top - 4 - height}px` : `${rect.bottom + 4}px`;
    list.querySelector('[aria-selected="true"]')?.scrollIntoView({ block: "nearest" });
  }, [open]);

  // Keep the keyboard-active option visible while arrowing through the list.
  useEffect(() => {
    if (!open) return;
```

## Connections
- [[Select.tsx]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Select_Component