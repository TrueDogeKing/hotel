import Modal from "./Modal";

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

// A question with two answers. Everything about being a dialog — the overlay,
// Escape, the focus trap — lives in Modal; this is just its content.
export default function ConfirmDialog({
  title,
  message,
  confirmLabel = "Confirm",
  cancelLabel = "Cancel",
  busy = false,
  onConfirm,
  onCancel,
}: Props) {
  return (
    <Modal
      title={title}
      busy={busy}
      onClose={onCancel}
      footer={
        <>
          <button type="button" className="secondary" onClick={onCancel} disabled={busy}>
            {cancelLabel}
          </button>
          <button type="button" className="danger" onClick={onConfirm} disabled={busy}>
            {busy ? "Working…" : confirmLabel}
          </button>
        </>
      }
    >
      <p>{message}</p>
    </Modal>
  );
}
