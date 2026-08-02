import { useEffect, useId, useRef, useState, type ReactNode } from "react";

interface Props {
  label: string;
  /** The value read back as text on the trigger. */
  summary: ReactNode;
  /** Renders the trigger muted while nothing is chosen. */
  empty?: boolean;
  /** Trailing note on the trigger — the nights count, for a range. */
  aside?: ReactNode;
  /** The picker itself, given a way to close the panel once it has an answer. */
  children: (close: () => void) => ReactNode;
}

/** Distance kept between the trigger and the panel. */
const GAP = 6;

/**
 * A form field whose editor is folded away behind it: the value reads back as text
 * and the picker opens on click.
 *
 * Built on the native popover API rather than an absolutely-positioned div: the
 * panel then lives in the top layer, so no ancestor's `overflow` can clip it, and
 * Escape, click-away and focus return come from the platform instead of from
 * hand-written listeners.
 */
export default function PopoverField({ label, summary, empty, aside, children }: Props) {
  const triggerRef = useRef<HTMLButtonElement>(null);
  const panelRef = useRef<HTMLDivElement>(null);
  const [open, setOpen] = useState(false);
  const panelId = useId();

  // The platform closes this popover on Escape and on a click away, without
  // telling React — so the open flag behind aria-expanded is tracked by listening
  // to the element rather than by assuming every close came from our own code.
  useEffect(() => {
    const panel = panelRef.current;
    if (!panel) return;
    const onToggle = (event: Event) => setOpen((event as ToggleEvent).newState === "open");
    panel.addEventListener("toggle", onToggle);
    return () => panel.removeEventListener("toggle", onToggle);
  }, []);

  /** Places the panel under the trigger, flipping above it when the space below
   *  runs out. Measured at open time and held in fixed coordinates, because a
   *  top-layer element is positioned against the viewport, not against the form. */
  function positionPanel() {
    const trigger = triggerRef.current;
    const panel = panelRef.current;
    if (!trigger || !panel) return;

    const anchor = trigger.getBoundingClientRect();
    const panelHeight = panel.offsetHeight;
    const panelWidth = panel.offsetWidth;

    const below = window.innerHeight - anchor.bottom - GAP;
    const flip = below < panelHeight && anchor.top > below;
    panel.style.top = flip
      ? `${Math.max(GAP, anchor.top - panelHeight - GAP)}px`
      : `${anchor.bottom + GAP}px`;
    // Clamped so a field near the right edge does not push the panel off-screen.
    panel.style.left = `${Math.max(
      GAP,
      Math.min(anchor.left, window.innerWidth - panelWidth - GAP),
    )}px`;
  }

  function openPanel() {
    const panel = panelRef.current;
    if (!panel) return;
    // Shown first, then measured: a popover has no size until it is in the top
    // layer, so positioning it beforehand would place a zero-height box.
    panel.showPopover();
    positionPanel();
  }

  return (
    <div className="date-range-field">
      <span className="date-range-label">{label}</span>
      <button
        type="button"
        ref={triggerRef}
        className={`date-range-trigger${empty ? " empty" : ""}`}
        aria-haspopup="dialog"
        aria-expanded={open}
        aria-controls={panelId}
        onClick={openPanel}
      >
        <span>{summary}</span>
        {aside && <span className="date-range-nights">{aside}</span>}
      </button>

      <div
        id={panelId}
        ref={panelRef}
        // "auto" gives light dismiss and Escape, and closes any other open popover.
        popover="auto"
        className="date-range-popover"
      >
        {children(() => panelRef.current?.hidePopover())}
      </div>
    </div>
  );
}
