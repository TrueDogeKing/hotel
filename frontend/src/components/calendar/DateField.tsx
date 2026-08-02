import { useTranslation } from "react-i18next";
import PopoverField from "./PopoverField";
import DayCalendar from "./DayCalendar";
import { formatDate } from "../../utils/dates";

interface Props {
  label: string;
  /** The chosen day, or "" while nothing is chosen. */
  value: string;
  onChange: (iso: string) => void;
}

/** One day as a single field, with the calendar folded away behind it — the
 *  single-date counterpart of DateRangeField. */
export default function DateField({ label, value, onChange }: Props) {
  const { t, i18n } = useTranslation();

  return (
    <PopoverField
      label={label}
      summary={value ? formatDate(value, i18n.language) : t("dateRange.placeholder")}
      empty={!value}
    >
      {(close) => (
        <DayCalendar
          value={value}
          onChange={(iso) => {
            onChange(iso);
            // One click is the whole answer, so the panel has no reason to stay.
            close();
          }}
        />
      )}
    </PopoverField>
  );
}
