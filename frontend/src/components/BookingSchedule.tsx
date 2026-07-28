import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { getBookingSchedule, type PublicSchedule } from "../api/public";
import { formatDate, toTimeInput } from "../utils/dates";

interface Props {
  token: string;
}

// The group's own read-only camp programme on the manage page. The server never
// sends internal kitchen prep notes here.
export default function BookingSchedule({ token }: Props) {
  const { t, i18n } = useTranslation();
  const [schedule, setSchedule] = useState<PublicSchedule | null>(null);

  useEffect(() => {
    let cancelled = false;
    void getBookingSchedule(token)
      .then((data) => {
        if (!cancelled) setSchedule(data);
      })
      .catch(() => {
        // A missing programme is not an error worth showing the booker — the
        // section simply stays hidden.
        if (!cancelled) setSchedule(null);
      });
    return () => {
      cancelled = true;
    };
  }, [token]);

  const daysWithEntries = schedule?.days.filter((day) => day.entries.length > 0) ?? [];
  if (daysWithEntries.length === 0) return null;

  return (
    <section className="public-schedule">
      <h2>{t("manage.schedule.title")}</h2>
      <p>{t("manage.schedule.intro")}</p>

      {daysWithEntries.map((day) => (
        <div key={day.date}>
          <h3>{formatDate(day.date, i18n.language)}</h3>
          <ul>
            {day.entries.map((entry, index) => (
              <li key={`${day.date}-${index}`}>
                <span className="ps-time">
                  {toTimeInput(entry.startTime)}–{toTimeInput(entry.endTime)}
                </span>
                <span>
                  <strong>{entry.title}</strong>
                  {entry.location && <em> · {entry.location}</em>}
                  {entry.menu && <span className="ps-menu"> — {entry.menu}</span>}
                </span>
              </li>
            ))}
          </ul>
        </div>
      ))}
    </section>
  );
}
