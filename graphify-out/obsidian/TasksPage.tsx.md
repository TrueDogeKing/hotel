---
source_file: "frontend/src/pages/admin/TasksPage.tsx"
type: "code"
community: "Admin Tasks & Occupancy Pages"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Tasks__Occupancy_Pages
---

# TasksPage.tsx

## Context

_Source: `frontend/src/pages/admin/TasksPage.tsx` (defined near L1; showing L1–L46 of 59)._

```tsx
import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import AdminLayout from "../../components/admin/AdminLayout";
import { deleteTask, getTasks, setTaskDone, type RoomTask } from "../../api/admin";

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
```

## Connections
- [[AdminLayout.tsx]] - `imports_from` [EXTRACTED]
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[RoomTask]] - `imports` [EXTRACTED]
- [[TasksPage()]] - `contains` [EXTRACTED]
- [[admin.ts]] - `imports_from` [EXTRACTED]
- [[deleteTask()]] - `imports` [EXTRACTED]
- [[getTasks()]] - `imports` [EXTRACTED]
- [[setTaskDone()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Tasks__Occupancy_Pages