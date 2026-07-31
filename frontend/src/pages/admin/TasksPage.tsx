import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import AdminLayout from "../../components/admin/AdminLayout";
import { deleteTask, getTasks, setTaskDone, type RoomTask } from "../../api/admin";
import { useAuth } from "../../auth/AuthContext";

// Housekeeping worklist: open tasks first, one-click done/reopen.
export default function TasksPage() {
  const { t } = useTranslation();
  // A worker reads the list. Ticking a task off and deleting one are both writes.
  const { canEdit } = useAuth();
  const [tasks, setTasks] = useState<RoomTask[]>([]);
  const [showDone, setShowDone] = useState(false);

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

  return (
    <AdminLayout>
      <h1>{t("tasks.title")}</h1>

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
              <strong>
                {t("tasks.room")} {task.roomNumber}:
              </strong>{" "}
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
