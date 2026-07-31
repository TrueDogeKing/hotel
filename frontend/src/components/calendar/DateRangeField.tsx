import { useEffect, useId, useRef, useState } from "react";
import { useTranslation } from "react-i18next";
import RangeCalendar from "./RangeCalendar";
import { dayNumber, formatDate } from "../../utils/dates";

interface Props {
  label: string;
  startDate: string;
  endDate: string;
  /** Passed through to the calendar: decides which nights are greyed. */
  headcount: number;
  /** Lets a past date be picked — see RangeCalendar. */
  allowPast?: boolean;
  onChange: (range: { startDate: string; endDate: string }) => void;
}

/** Distance kept between the trigger and the panel. */
const GAP = 6;

/**
 * The chosen stay as a single field, with the calendar folded away behind it
 * until it is needed.
 *
 * A form that asks for a group's name, contact, size and status should not give
 * six weeks of grid to the one field that happens to need a calendar — so the
 * field reads the range back as text and opens the picker on click.
 *
 * Built on the native popover API rather than an absolutely-positioned div: the
 * panel then lives in the top layer, so no ancestor's `overflow` can clip it, and
 * Escape, click-away and focus return come from the platform instead of from
 * hand-written listeners.
 */
export default function DateRangeField({
  label,
  startDate,
  endDate,
  headcount,
  allowPast = false,
  onChange,
}: Props) {
  const { t, i18n } = useTranslation();
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

  const nights = startDate && endDate ? dayNumber(endDate) - dayNumber(startDate) : 0;
  const summary =
    startDate && endDate
      ? `${formatDate(startDate, i18n.language)} – ${formatDate(endDate, i18n.language)}`
      : startDate
        ? `${formatDate(startDate, i18n.language)} – …`
        : t("dateRange.placeholder");

  return (
    <div className="date-range-field">
      <span className="date-range-label">{label}</span>
      <button
        type="button"
        ref={triggerRef}
        className={`date-range-trigger${startDate && endDate ? "" : " empty"}`}
        aria-haspopup="dialog"
        aria-expanded={open}
        aria-controls={panelId}
        onClick={openPanel}
      >
        <span>{summary}</span>
        {nights > 0 && (
          <span className="date-range-nights">{t("dateRange.nights", { count: nights })}</span>
        )}
      </button>

      <div
        id={panelId}
        ref={panelRef}
        // "auto" gives light dismiss and Escape, and closes any other open popover.
        popover="auto"
        className="date-range-popover"
      >
        <RangeCalendar
          startDate={startDate}
          endDate={endDate}
          headcount={headcount}
          allowPast={allowPast}
          onChange={(range) => {
            onChange(range);
            // Closes once the range is whole. Picking the arrival leaves it open,
            // because the departure is the next thing to choose.
            if (range.startDate && range.endDate) panelRef.current?.hidePopover();
          }}
        />
      </div>
    </div>
  );
}
