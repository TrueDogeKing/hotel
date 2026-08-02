import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import AdminLayout from "../../components/admin/AdminLayout";
import DateRangeField from "../../components/calendar/DateRangeField";
import {
  createTask,
  getOccupancy,
  getTasks,
  setTaskDone,
  type Occupancy,
  type RoomOccupancy,
  type RoomTask,
} from "../../api/admin";
import { addDaysIso, todayIso } from "../../utils/dates";

// Occupancy grid over a date range: rooms colored by occupancy or blocked by a
// closure. Clicking a room shows its booking and lets the admin add tasks.
export default function OccupancyPage() {
  const { t } = useTranslation();
  const [start, setStart] = useState(todayIso);
  const [end, setEnd] = useState(() => addDaysIso(todayIso(), 7));
  const [occupancy, setOccupancy] = useState<Occupancy | null>(null);
  const [selected, setSelected] = useState<RoomOccupancy | null>(null);
  const [roomTasks, setRoomTasks] = useState<RoomTask[]>([]);
  const [taskText, setTaskText] = useState("");
  const [error, setError] = useState<string | null>(null);

  const reload = useCallback(async () => {
    if (!start || !end || end <= start) return;
    try {
      const data = await getOccupancy(start, end);
      setOccupancy(data);
      setError(null);
    } catch {
      setError(t("occupancy.loadError"));
    }
  }, [start, end, t]);

  useEffect(() => {
    if (!start || !end || end <= start) return;
    let cancelled = false;
    void getOccupancy(start, end)
      .then((data) => {
        if (!cancelled) {
          setOccupancy(data);
          setError(null);
        }
      })
      .catch(() => {
        if (!cancelled) setError(t("occupancy.loadError"));
      });
    return () => {
      cancelled = true;
    };
  }, [start, end, t]);

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
    if (room.closed) return "room-cell closed";
    if (!room.bookingId) return "room-cell free";
    return room.bookingStatus === "PendingDeposit" ? "room-cell pending" : "room-cell occupied";
  }

  return (
    <AdminLayout>
      <h1>{t("occupancy.title")}</h1>

      <div className="admin-form">
        {/* The same folded picker the booking forms use, so the range being viewed
            is read back as text and the grid keeps the room for itself. Past dates
            are allowed: occupancy is looked back on as often as ahead. */}
        <DateRangeField
          label={t("occupancy.range")}
          startDate={start}
          endDate={end}
          headcount={0}
          allowPast
          onChange={(range) => {
            setStart(range.startDate);
            setEnd(range.endDate);
          }}
        />
      </div>

      {error && <p role="alert">{error}</p>}

      {!occupancy ? (
        <p>{t("common.loading")}</p>
      ) : (
        <>
          <p className="occupancy-summary">
            {t("occupancy.summary", {
              occupied: occupancy.occupiedBeds,
              total: occupancy.totalBeds,
            })}
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
                    {room.closed
                      ? t("occupancy.blocked")
                      : `${room.peopleCount ?? 0}/${room.capacity}`}
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
                {selected.closed ? (
                  <p>
                    {t("occupancy.closedRoom")}
                    {selected.closureReason ? ` — ${selected.closureReason}` : ""}
                  </p>
                ) : selected.bookingId ? (
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
              </aside>
            )}
          </div>
        </>
      )}
    </AdminLayout>
  );
}
