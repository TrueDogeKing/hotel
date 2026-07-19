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
      } else {
        await createRoom(input);
      }
      setForm(emptyForm);
      setEditing(null);
      await reload();
    } catch (err) {
      handleApiError(err);
    }
  }

  function startEdit(room: Room) {
    setEditing(room);
    setForm({
      number: room.number,
      capacity: String(room.capacity),
      description: room.description ?? "",
    });
  }

  async function toggleActive(room: Room) {
    setError(null);
    try {
      await updateRoom(room.id, {
        number: room.number,
        capacity: room.capacity,
        description: room.description,
        isActive: !room.isActive,
        rowVersion: room.rowVersion,
      });
      await reload();
    } catch (err) {
      handleApiError(err);
    }
  }

  async function remove(room: Room) {
    setError(null);
    try {
      const result = await deleteRoom(room.id);
      if (!result.deleted) {
        setError(t("adminRooms.deactivatedInstead"));
      }
      await reload();
    } catch (err) {
      handleApiError(err);
    }
  }

  return (
    <AdminLayout>
      <h1>{t("adminRooms.title")}</h1>

      <form className="admin-form" onSubmit={handleSubmit}>
        <label>
          {t("adminRooms.number")}
          <input
            value={form.number}
            onChange={(e) => setForm({ ...form, number: e.target.value })}
            required
            maxLength={32}
          />
        </label>
        <label>
          {t("adminRooms.capacity")}
          <input
            type="number"
            min={1}
            max={20}
            value={form.capacity}
            onChange={(e) => setForm({ ...form, capacity: e.target.value })}
            required
          />
        </label>
        <label>
          {t("adminRooms.description")}
          <input
            value={form.description}
            onChange={(e) => setForm({ ...form, description: e.target.value })}
            maxLength={512}
          />
        </label>
        <button type="submit">{editing ? t("adminRooms.save") : t("adminRooms.add")}</button>
        {editing && (
          <button
            type="button"
            onClick={() => {
              setEditing(null);
              setForm(emptyForm);
            }}
          >
            {t("adminRooms.cancelEdit")}
          </button>
        )}
      </form>

      {error && <p role="alert">{error}</p>}

      <table className="admin-table">
        <thead>
          <tr>
            <th>{t("adminRooms.number")}</th>
            <th>{t("adminRooms.capacity")}</th>
            <th>{t("adminRooms.status")}</th>
            <th>{t("adminRooms.description")}</th>
            <th />
          </tr>
        </thead>
        <tbody>
          {rooms.map((room) => (
            <tr key={room.id} className={room.isActive ? "" : "inactive"}>
              <td>{room.number}</td>
              <td>{room.capacity}</td>
              <td>{room.isActive ? t("adminRooms.active") : t("adminRooms.inactive")}</td>
              <td>{room.description}</td>
              <td className="row-actions">
                <button type="button" onClick={() => startEdit(room)}>
                  {t("adminRooms.edit")}
                </button>
                <button type="button" onClick={() => void toggleActive(room)}>
                  {room.isActive ? t("adminRooms.deactivate") : t("adminRooms.activate")}
                </button>
                <button type="button" onClick={() => void remove(room)}>
                  {t("adminRooms.delete")}
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </AdminLayout>
  );
}
