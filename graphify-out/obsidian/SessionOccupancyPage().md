---
source_file: "frontend/src/pages/admin/SessionOccupancyPage.tsx"
type: "code"
community: "Admin Tasks & Occupancy Pages"
location: "L17"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Tasks__Occupancy_Pages
---

# SessionOccupancyPage()

## Context

_Source: `frontend/src/pages/admin/SessionOccupancyPage.tsx` (defined near L17; showing L15–L62 of 169)._

```tsx
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
```

## Connections
- [[SessionOccupancyPage.tsx]] - `contains` [EXTRACTED]
- [[getOccupancy()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Tasks__Occupancy_Pages