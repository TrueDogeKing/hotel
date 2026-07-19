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
    listRef.current
      ?.querySelector(`[data-index="${activeIndex}"]`)
      ?.scrollIntoView({ block: "nearest" });
  }, [open, activeIndex]);

  useEffect(() => {
    if (!open) return;
    function onDocumentClick(e: MouseEvent) {
      if (!rootRef.current?.contains(e.target as Node)) setOpen(false);
    }
    // Any scroll outside the list itself de-anchors the fixed listbox — close it.
    function onScroll(e: Event) {
      if (e.target instanceof Node && listRef.current?.contains(e.target)) return;
      setOpen(false);
    }
    function onResize() {
      setOpen(false);
    }
    document.addEventListener("click", onDocumentClick);
    window.addEventListener("scroll", onScroll, true);
    window.addEventListener("resize", onResize);
    return () => {
      document.removeEventListener("click", onDocumentClick);
      window.removeEventListener("scroll", onScroll, true);
      window.removeEventListener("resize", onResize);
    };
  }, [open]);

  function onTriggerKeyDown(e: React.KeyboardEvent) {
    if (!open) {
      if (["ArrowDown", "ArrowUp", "Enter", " ", "Home", "End"].includes(e.key)) {
        e.preventDefault();
        openList();
      }
      return;
    }
    switch (e.key) {
      case "ArrowDown":
        e.preventDefault();
        setActiveIndex((i) => Math.min(i + 1, options.length - 1));
        break;
      case "ArrowUp":
        e.preventDefault();
        setActiveIndex((i) => Math.max(i - 1, 0));
        break;
      case "Home":
        e.preventDefault();
        setActiveIndex(0);
        break;
      case "End":
        e.preventDefault();
        setActiveIndex(options.length - 1);
        break;
      case "Enter":
      case " ":
        e.preventDefault();
        commit(activeIndex);
        break;
      case "Escape":
        e.preventDefault();
        setOpen(false);
        break;
      case "Tab":
        setOpen(false);
        break;
    }
  }

  const listboxId = `${uid}-listbox`;
  return (
    <div className="select" ref={rootRef}>
      <button
        type="button"
        id={id}
        ref={triggerRef}
        className="select-trigger"
        title={title}
        role="combobox"
        aria-label={ariaLabel}
        aria-haspopup="listbox"
        aria-expanded={open}
        aria-controls={open ? listboxId : undefined}
        aria-activedescendant={open ? `${uid}-opt-${activeIndex}` : undefined}
        onClick={() => (open ? setOpen(false) : openList())}
        onKeyDown={onTriggerKeyDown}
      >
        {options[selectedIndex]?.label ?? ""}
      </button>
      {open && (
        <ul className="select-listbox" ref={listRef} id={listboxId} role="listbox">
          {options.map((option, i) => (
            <li
              key={`${i}-${option.value}`}
              id={`${uid}-opt-${i}`}
              data-index={i}
              role="option"
              aria-selected={i === selectedIndex}
              className={i === activeIndex ? "select-option active" : "select-option"}
              onPointerMove={() => setActiveIndex(i)}
              onClick={() => commit(i)}
            >
              {option.label}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
