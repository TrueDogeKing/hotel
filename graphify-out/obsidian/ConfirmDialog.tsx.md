---
source_file: "frontend/src/components/ConfirmDialog.tsx"
type: "code"
community: "Public Booking Frontend"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# ConfirmDialog.tsx

## Context

_Source: `frontend/src/components/ConfirmDialog.tsx` (defined near L1; showing L1–L46 of 54)._

```tsx
import { useEffect } from "react";

interface Props {
  title: string;
  message: string;
  confirmLabel?: string;
  cancelLabel?: string;
  // Disables the buttons and shows a busy label while the action runs.
  busy?: boolean;
  onConfirm: () => void;
  onCancel: () => void;
}

// Reusable modal confirmation dialog. Closes on overlay click or Escape.
export default function ConfirmDialog({
  title,
  message,
  confirmLabel = "Confirm",
  cancelLabel = "Cancel",
  busy = false,
  onConfirm,
  onCancel,
}: Props) {
  useEffect(() => {
    function onKeyDown(event: KeyboardEvent) {
      if (event.key === "Escape" && !busy) onCancel();
    }
    document.addEventListener("keydown", onKeyDown);
    return () => document.removeEventListener("keydown", onKeyDown);
  }, [busy, onCancel]);

  return (
    <div className="modal-overlay" role="presentation" onClick={() => !busy && onCancel()}>
      <div
        className="modal"
        role="dialog"
        aria-modal="true"
        aria-labelledby="confirm-title"
        onClick={(e) => e.stopPropagation()}
      >
        <h2 id="confirm-title">{title}</h2>
        <p>{message}</p>
        <div className="modal-actions">
          <button type="button" className="secondary" onClick={onCancel} disabled={busy}>
            {cancelLabel}
          </button>
```

## Connections
- [[AdminBookingsPage.tsx]] - `imports_from` [EXTRACTED]
- [[BookingManagePage.tsx]] - `imports_from` [EXTRACTED]
- [[ConfirmDialog()]] - `contains` [EXTRACTED]
- [[Props]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend