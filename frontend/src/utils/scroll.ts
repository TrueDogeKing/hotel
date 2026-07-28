/**
 * Brings a freshly opened panel into view. The admin pages stack the calendar, the
 * day timetable and the group panel vertically, so a click near the top often
 * changes something well below the fold and looks like it did nothing.
 *
 * Honours the viewer's reduced-motion setting, like the animations in App.css.
 */
export function scrollPanelIntoView(element: HTMLElement | null): void {
  element?.scrollIntoView({
    behavior: window.matchMedia("(prefers-reduced-motion: reduce)").matches
      ? "auto"
      : "smooth",
    block: "start",
  });
}
