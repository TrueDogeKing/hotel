import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import AdminLayout from "../../components/admin/AdminLayout";
import {
  createTask,
  deleteTask,
  getRooms,
  getTasks,
  setTaskDone,
  type Room,
  type RoomTask,
} from "../../api/admin";
import { useAuth } from "../../auth/AuthContext";

// Housekeeping worklist: open tasks first, one-click done/reopen.
export default function TasksPage() {
  const { t } = useTranslation();
  // A worker reads the list. Ticking a task off, deleting one and adding a new
  // one are all writes.
  const { canEdit } = useAuth();
  const [tasks, setTasks] = useState<RoomTask[]>([]);
  const [rooms, setRooms] = useState<Room[]>([]);
  const [showDone, setShowDone] = useState(false);
  const [newRoomId, setNewRoomId] = useState("");
  const [newText, setNewText] = useState("");
  const [addError, setAddError] = useState<string | null>(null);

  const reload = useCallback(async () => {
    setTasks(await getTasks(showDone ? {} : { status: "Open" }));
  }, [showDone]);

  useEffect(() => {
    let cancelled = false;
    void getTasks(showDone ? {} : { status: "Open" }).then((data) => {
      if (!cancelled) setTasks(data);
    });
    return () => {
      cancelled = true;
    };
  }, [showDone]);

  useEffect(() => {
    if (!canEdit) return;
    let cancelled = false;
    void getRooms().then((data) => {
      if (!cancelled) setRooms(data.filter((room) => room.isActive));
    });
    return () => {
      cancelled = true;
    };
  }, [canEdit]);

  async function addTask(e: React.FormEvent) {
    e.preventDefault();
    const text = newText.trim();
    if (!text) return;
    setAddError(null);
    try {
      // The room field doubles as "no room": an empty selection makes a
      // standalone reminder instead of a housekeeping note on a specific room.
      await createTask({ roomId: newRoomId || null, text, bookingId: null });
      setNewText("");
      setNewRoomId("");
      await reload();
    } catch {
      setAddError(t("tasks.addError"));
    }
  }

  return (
    <AdminLayout>
      <h1>{t("tasks.title")}</h1>

      {canEdit && (
        <form className="tasks-add" onSubmit={(e) => void addTask(e)}>
          <select value={newRoomId} onChange={(e) => setNewRoomId(e.target.value)}>
            <option value="">{t("tasks.noRoom")}</option>
            {rooms.map((room) => (
              <option key={room.id} value={room.id}>
                {t("tasks.room")} {room.number}
              </option>
            ))}
          </select>
          <input
            type="text"
            value={newText}
            onChange={(e) => setNewText(e.target.value)}
            placeholder={t("tasks.textPlaceholder")}
            maxLength={1000}
          />
          <button type="submit" disabled={!newText.trim()}>
            {t("tasks.add")}
          </button>
        </form>
      )}
      {addError && <p role="alert">{addError}</p>}

      <label className="tasks-filter">
        <input type="checkbox" checked={showDone} onChange={(e) => setShowDone(e.target.checked)} />
        {t("tasks.showDone")}
      </label>

      {tasks.length === 0 && <p>{t("tasks.empty")}</p>}

      <ul className="task-list task-list-page">
        {tasks.map((task) => (
          <li key={task.id} className={task.status === "Done" ? "done" : ""}>
            <label>
              <input
                type="checkbox"
                checked={task.status === "Done"}
                disabled={!canEdit}
                onChange={() => void setTaskDone(task.id, task.status === "Open").then(reload)}
              />
              <strong>{task.roomNumber ? `${t("tasks.room")} ${task.roomNumber}:` : `${t("tasks.general")}:`}</strong>{" "}
              {task.text}
            </label>
            {canEdit && (
              <button type="button" onClick={() => void deleteTask(task.id).then(reload)}>
                {t("tasks.delete")}
              </button>
            )}
          </li>
        ))}
      </ul>
    </AdminLayout>
  );
}
