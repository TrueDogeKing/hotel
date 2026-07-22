---
source_file: "frontend/src/pages/admin/SessionOccupancyPage.tsx"
type: "code"
community: "Admin Tasks & Occupancy Pages"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Tasks__Occupancy_Pages
---

# SessionOccupancyPage.tsx

## Context

_Source: `frontend/src/pages/admin/SessionOccupancyPage.tsx` (defined near L1; showing L1–L46 of 169)._

```tsx
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
```

## Connections
- [[AdminLayout.tsx]] - `imports_from` [EXTRACTED]
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[RoomOccupancy]] - `imports` [EXTRACTED]
- [[RoomTask]] - `imports` [EXTRACTED]
- [[SessionOccupancy]] - `imports` [EXTRACTED]
- [[SessionOccupancyPage()]] - `contains` [EXTRACTED]
- [[admin.ts]] - `imports_from` [EXTRACTED]
- [[createTask()]] - `imports` [EXTRACTED]
- [[getOccupancy()]] - `imports` [EXTRACTED]
- [[getTasks()]] - `imports` [EXTRACTED]
- [[setTaskDone()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Tasks__Occupancy_Pages