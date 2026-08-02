import { useTranslation } from "react-i18next";
import PopoverField from "./PopoverField";
import RangeCalendar from "./RangeCalendar";
import { dayNumber, formatDate } from "../../utils/dates";

interface Props {
  label: string;
  startDate: string;
  endDate: string;
  /** Passed through to the calendar: decides which nights are greyed. */
  headcount: number;
  /** Lets a past date be picked — see RangeCalendar. */
  allowPast?: boolean;
  onChange: (range: { startDate: string; endDate: string }) => void;
}

/**
 * The chosen stay as a single field, with the calendar folded away behind it
 * until it is needed.
 *
 * A form that asks for a group's name, contact, size and status should not give
 * six weeks of grid to the one field that happens to need a calendar — so the
 * field reads the range back as text and opens the picker on click.
 */
export default function DateRangeField({
  label,
  startDate,
  endDate,
  headcount,
  allowPast = false,
  onChange,
}: Props) {
  const { t, i18n } = useTranslation();

  const nights = startDate && endDate ? dayNumber(endDate) - dayNumber(startDate) : 0;
  const summary =
    startDate && endDate
      ? `${formatDate(startDate, i18n.language)} – ${formatDate(endDate, i18n.language)}`
      : startDate
        ? `${formatDate(startDate, i18n.language)} – …`
        : t("dateRange.placeholder");

  return (
    <PopoverField
      label={label}
      summary={summary}
      empty={!(startDate && endDate)}
      aside={nights > 0 ? t("dateRange.nights", { count: nights }) : undefined}
    >
      {(close) => (
        <RangeCalendar
          startDate={startDate}
          endDate={endDate}
          headcount={headcount}
          allowPast={allowPast}
          onChange={(range) => {
            onChange(range);
            // Closes once the range is whole. Picking the arrival leaves it open,
            // because the departure is the next thing to choose.
            if (range.startDate && range.endDate) close();
          }}
        />
      )}
    </PopoverField>
  );
}
