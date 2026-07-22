---
source_file: "frontend/src/pages/admin/RoomsPage.tsx"
type: "code"
community: "Admin Frontend Pages"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# RoomsPage.tsx

## Context

_Source: `frontend/src/pages/admin/RoomsPage.tsx` (defined near L1; showing L1–L46 of 191)._

```tsx
import { useEffect, useState, type FormEvent } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import AdminLayout from "../../components/admin/AdminLayout";
import { createRoom, deleteRoom, getRooms, updateRoom, type Room } from "../../api/admin";

interface RoomFormState {
  number: string;
  capacity: string;
  description: string;
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
```

## Connections
- [[AdminLayout.tsx]] - `imports_from` [EXTRACTED]
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[Room]] - `imports` [EXTRACTED]
- [[RoomFormState]] - `contains` [EXTRACTED]
- [[RoomsPage()]] - `contains` [EXTRACTED]
- [[admin.ts]] - `imports_from` [EXTRACTED]
- [[createRoom()]] - `imports` [EXTRACTED]
- [[deleteRoom()]] - `imports` [EXTRACTED]
- [[emptyForm]] - `contains` [EXTRACTED]
- [[getRooms()]] - `imports` [EXTRACTED]
- [[updateRoom()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages