---
source_file: "frontend/src/pages/admin/RoomsPage.tsx"
type: "code"
community: "Admin Frontend Pages"
location: "L13"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# emptyForm

## Context

_Source: `frontend/src/pages/admin/RoomsPage.tsx` (defined near L13; showing L11–L58 of 191)._

```tsx
}

const emptyForm: RoomFormState = { number: "", capacity: "4", description: "" };

export default function RoomsPage() {
  const { t } = useTranslation();
  const [rooms, setRooms] = useState<Room[]>([]);
  const [form, setForm] = useState<RoomFormState>(emptyForm);
  const [editing, setEditing] = useState<Room | null>(null);
  const [error, setError] = useState<string | null>(null);

  async function reload() {
    setRooms(await getRooms());
  }

  useEffect(() => {
    let cancelled = false;
    void getRooms().then((data) => {
      if (!cancelled) setRooms(data);
    });
    return () => {
      cancelled = true;
    };
  }, []);

  function handleApiError(err: unknown) {
    if (isAxiosError(err) && err.response?.status === 409) {
      setError(t("adminRooms.conflict"));
    } else {
      setError(t("adminRooms.genericError"));
    }
  }

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setError(null);
    const input = {
      number: form.number.trim(),
      capacity: Number(form.capacity),
      description: form.description.trim() || null,
    };
    try {
      if (editing) {
        await updateRoom(editing.id, {
          ...input,
          isActive: editing.isActive,
          rowVersion: editing.rowVersion,
        });
```

## Connections
- [[RoomsPage.tsx]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages