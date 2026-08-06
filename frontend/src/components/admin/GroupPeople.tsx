import { useState } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import { updateBookingPeople } from "../../api/admin";
import { useAuth } from "../../auth/AuthContext";

interface Props {
  bookingId: string;
  /** Campers and supervisors together. */
  headcount: number;
  supervisorCount: number;
  /** People currently placed in rooms, where the caller knows. Left out where the
   *  room editor is on screen anyway and already says so itself. */
  placedInRooms?: number;
  /** The counts show up in the schedule, the occupancy grid and the price line. */
  onChanged: () => void | Promise<void>;
}

/**
 * How many campers and how many supervisors — corrected after the fact.
 *
 * A group is rarely the size it was booked at: someone is sent home, a parent
 * joins the kadra, two more sign up the week before. This changes the numbers and
 * nothing else. The price stays what was agreed and the rooms stay where the
 * owner put them — both have controls of their own, and neither follows
 * automatically from one child fewer.
 *
 * Where the caller can say how many people the rooms hold, a mismatch is called
 * out: the room editor refuses to save until the two agree, and finding that out
 * only once you are in there would be a surprise.
 */
export default function GroupPeople({
  bookingId,
  headcount,
  supervisorCount,
  placedInRooms,
  onChanged,
}: Props) {
  const { t } = useTranslation();
  const { canEdit } = useAuth();
  const [editing, setEditing] = useState(false);
  const [campers, setCampers] = useState("");
  const [supervisors, setSupervisors] = useState("");
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const camperCount = headcount - supervisorCount;

  function startEditing() {
    setCampers(String(camperCount));
    setSupervisors(String(supervisorCount));
    setError(null);
    setEditing(true);
  }

  const draftCampers = Number(campers) || 0;
  const draftSupervisors = Number(supervisors) || 0;
  const draftHeadcount = draftCampers + draftSupervisors;

  async function save(e: React.FormEvent) {
    e.preventDefault();
    setSaving(true);
    setError(null);
    try {
      await updateBookingPeople(bookingId, {
        headcount: draftHeadcount,
        supervisorCount: draftSupervisors,
      });
      setEditing(false);
      await onChanged();
    } catch (err) {
      const detail = isAxiosError(err)
        ? (err.response?.data as { detail?: string } | undefined)?.detail
        : undefined;
      setError(detail ?? t("groupPeople.saveError"));
    } finally {
      setSaving(false);
    }
  }

  return (
    <div className="group-people">
      {editing ? (
        <form className="group-people-edit" onSubmit={(e) => void save(e)}>
          <label>
            {t("groupPeople.campers")}
            <input
              type="number"
              min={0}
              max={2000}
              value={campers}
              onChange={(e) => setCampers(e.target.value)}
            />
          </label>
          <label>
            {t("groupPeople.supervisors")}
            <input
              type="number"
              min={0}
              max={2000}
              value={supervisors}
              onChange={(e) => setSupervisors(e.target.value)}
            />
          </label>
          <span className="group-people-total">
            {t("groupPeople.total", { count: draftHeadcount })}
          </span>
          <button type="submit" disabled={saving || draftHeadcount < 1}>
            {saving ? t("groupPeople.saving") : t("groupPeople.save")}
          </button>
          <button type="button" onClick={() => setEditing(false)}>
            {t("groupPeople.cancel")}
          </button>
        </form>
      ) : (
        <p className="group-people-summary">
          {t("groupPeople.summary", {
            campers: camperCount,
            supervisors: supervisorCount,
            count: headcount,
          })}{" "}
          {canEdit && (
            <button type="button" onClick={startEditing}>
              {t("groupPeople.edit")}
            </button>
          )}
        </p>
      )}

      {placedInRooms !== undefined && placedInRooms !== headcount && placedInRooms > 0 && (
        <p className="group-people-warning">
          {t("groupPeople.roomsOutOfStep", { placed: placedInRooms, headcount })}
        </p>
      )}
      {error && <p role="alert">{error}</p>}
    </div>
  );
}
