import { useCallback, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";
import AdminLayout from "../../components/admin/AdminLayout";
import {
  createTask,
  getOccupancy,
  getTasks,
  setTaskDone,
  type RoomOccupancy,
  type RoomTask,
  type SessionOccupancy,
} from "../../api/admin";

// Occupancy grid of one session: rooms colored by occupancy; clicking a room
// shows its booking and lets the admin add housekeeping tasks (e.g. extra bed).
export default function SessionOccupancyPage() {
  const { t } = useTranslation();
  const { id } = useParams<{ id: string }>();
  const [occupancy, setOccupancy] = useState<SessionOccupancy | null>(null);
  const [selected, setSelected] = useState<RoomOccupancy | null>(null);
  const [roomTasks, setRoomTasks] = useState<RoomTask[]>([]);
  const [taskText, setTaskText] = useState("");
  const [error, setError] = useState<string | null>(null);

  const reload = useCallback(async () => {
    if (!id) return;
    setOccupancy(await getOccupancy(id));
  }, [id]);

  useEffect(() => {
    let cancelled = false;
    if (id) {
      void getOccupancy(id).then((data) => {
        if (!cancelled) setOccupancy(data);
      });
    }
    return () => {
      cancelled = true;
    };
  }, [id]);

  async function selectRoom(room: RoomOccupancy) {
    setSelected(room);
    setTaskText("");
    const tasks = await getTasks({});
    setRoomTasks(tasks.filter((task) => task.roomId === room.roomId));
  }

  async function addTask() {
    if (!selected || !taskText.trim()) return;
    setError(null);
    try {
      await createTask({
        roomId: selected.roomId,
        text: taskText.trim(),
        campSessionId: occupancy?.sessionId ?? null,
        bookingId: selected.bookingId,
      });
      setTaskText("");
      await selectRoom(selected);
      await reload();
    } catch {
      setError(t("occupancy.taskError"));
    }
  }

  async function toggleTask(task: RoomTask) {
    await setTaskDone(task.id, task.status === "Open");
    if (selected) await selectRoom(selected);
    await reload();
  }

  function roomClass(room: RoomOccupancy): string {
    if (!room.isActive) return "room-cell inactive";
    if (!room.bookingId) return "room-cell free";
    return room.bookingStatus === "PendingDeposit" ? "room-cell pending" : "room-cell occupied";
  }

  return (
    <AdminLayout>
      {!occupancy ? (
        <p>{t("common.loading")}</p>
      ) : (
        <>
          <h1>
            {occupancy.sessionName}{" "}
            <span className="occupancy-summary">
              {t("occupancy.summary", {
                occupied: occupancy.occupiedBeds,
                total: occupancy.totalBeds,
              })}
            </span>
          </h1>
          <p>
            <Link to="/admin/turnusy">← {t("occupancy.backToSessions")}</Link>
          </p>

          <div className="occupancy-layout">
            <div className="room-grid">
              {occupancy.rooms.map((room) => (
                <button
                  key={room.roomId}
                  type="button"
                  className={`${roomClass(room)}${selected?.roomId === room.roomId ? " selected" : ""}`}
                  onClick={() => void selectRoom(room)}
                >
                  <strong>{room.roomNumber}</strong>
                  <span>
                    {room.peopleCount ?? 0}/{room.capacity}
                  </span>
                  {room.openTaskCount > 0 && (
                    <span className="task-badge">{room.openTaskCount}</span>
                  )}
                </button>
              ))}
            </div>

            {selected && (
              <aside className="room-panel">
                <h2>
                  {t("occupancy.room")} {selected.roomNumber} ({selected.capacity}{" "}
                  {t("occupancy.beds")})
                </h2>
                {selected.bookingId ? (
                  <p>
                    {selected.organizationName} — {selected.peopleCount} {t("occupancy.people")}
                  </p>
                ) : (
                  <p>{t("occupancy.freeRoom")}</p>
                )}

                <h3>{t("occupancy.tasks")}</h3>
                {roomTasks.length === 0 && <p>{t("occupancy.noTasks")}</p>}
                <ul className="task-list">
                  {roomTasks.map((task) => (
                    <li key={task.id} className={task.status === "Done" ? "done" : ""}>
                      <label>
                        <input
                          type="checkbox"
                          checked={task.status === "Done"}
                          onChange={() => void toggleTask(task)}
                        />
                        {task.text}
                      </label>
                    </li>
                  ))}
                </ul>

                <div className="task-add">
                  <input
                    value={taskText}
                    onChange={(e) => setTaskText(e.target.value)}
                    placeholder={t("occupancy.taskPlaceholder")}
                    maxLength={1000}
                  />
                  <button type="button" disabled={!taskText.trim()} onClick={() => void addTask()}>
                    {t("occupancy.addTask")}
                  </button>
                </div>
                {error && <p role="alert">{error}</p>}
              </aside>
            )}
          </div>
        </>
      )}
    </AdminLayout>
  );
}
