import { useEffect, useId, useRef, type ReactNode } from "react";
import { createPortal } from "react-dom";

interface Props {
  title: string;
  /** 26rem for a question, 34rem for a short form, 48rem for a long one. */
  size?: "sm" | "md" | "lg";
  /** Disables dismissal while the dialog's action is running. */
  busy?: boolean;
  onClose: () => void;
  children: ReactNode;
  /** Buttons, laid out at the end of the dialog. */
  footer?: ReactNode;
}

/** Every element that can hold focus inside the dialog, in tab order. */
const FOCUSABLE =
  'a[href], button:not([disabled]), input:not([disabled]), select:not([disabled]), textarea:not([disabled]), [tabindex]:not([tabindex="-1"])';

/**
 * The one modal in the app: overlay click and Escape to dismiss, focus kept
 * inside while it is open and handed back to whatever opened it on the way out.
 *
 * Rendered through a portal so it is positioned against the viewport rather than
 * against whichever panel happened to contain the button — the schedule layout
 * creates a stacking context that would otherwise clip it.
 */
export default function Modal({
  title,
  size = "sm",
  busy = false,
  onClose,
  children,
  footer,
}: Props) {
  const titleId = useId();
  const dialogRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    // Remembered before the dialog steals focus, restored when it closes: a
    // dialog opened from a row button should hand the row back afterwards.
    const opener = document.activeElement as HTMLElement | null;
    dialogRef.current?.querySelector<HTMLElement>(FOCUSABLE)?.focus();

    function onKeyDown(event: KeyboardEvent) {
      if (event.key === "Escape" && !busy) {
        onClose();
        return;
      }

      if (event.key !== "Tab") return;

      // Wrap Tab around the dialog's own controls, so focus cannot wander into
      // the page behind it.
      const focusable = [...(dialogRef.current?.querySelectorAll<HTMLElement>(FOCUSABLE) ?? [])];
      if (focusable.length === 0) return;
      const first = focusable[0];
      const last = focusable[focusable.length - 1];
      if (event.shiftKey && document.activeElement === first) {
        event.preventDefault();
        last.focus();
      } else if (!event.shiftKey && document.activeElement === last) {
        event.preventDefault();
        first.focus();
      }
    }

    document.addEventListener("keydown", onKeyDown);
    return () => {
      document.removeEventListener("keydown", onKeyDown);
      opener?.focus?.();
    };
  }, [busy, onClose]);

  return createPortal(
    <div className="modal-overlay" role="presentation" onClick={() => !busy && onClose()}>
      <div
        ref={dialogRef}
        className={`modal modal-${size}`}
        role="dialog"
        aria-modal="true"
        aria-labelledby={titleId}
        onClick={(e) => e.stopPropagation()}
      >
        <h2 id={titleId}>{title}</h2>
        {children}
        {footer && <div className="modal-actions">{footer}</div>}
      </div>
    </div>,
    document.body,
  );
}
