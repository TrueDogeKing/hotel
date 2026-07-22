---
source_file: "frontend/src/pages/admin/TasksPage.tsx"
type: "code"
community: "Admin Tasks & Occupancy Pages"
location: "L7"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Tasks__Occupancy_Pages
---

# TasksPage()

## Context

_Source: `frontend/src/pages/admin/TasksPage.tsx` (defined near L7; showing L5–L52 of 59)._

```tsx

// Housekeeping worklist: open tasks first, one-click done/reopen.
export default function TasksPage() {
  const { t } = useTranslation();
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
                onChange={() => void setTaskDone(task.id, task.status === "Open").then(reload)}
              />
              <strong>
                {t("tasks.room")} {task.roomNumber}:
              </strong>{" "}
              {task.text}
            </label>
            <button type="button" onClick={() => void deleteTask(task.id).then(reload)}>
              {t("tasks.delete")}
```

## Connections
- [[TasksPage.tsx]] - `contains` [EXTRACTED]
- [[deleteTask()]] - `calls` [EXTRACTED]
- [[getTasks()]] - `calls` [EXTRACTED]
- [[setTaskDone()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Tasks__Occupancy_Pages