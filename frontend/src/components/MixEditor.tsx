import { useTranslation } from "react-i18next";

interface Props {
  /** Every room capacity the centre offers over these dates. */
  capacities: string[];
  /** How many rooms of each capacity this cohort has taken. */
  counts: Record<string, number>;
  /** Rooms of a capacity still available to this cohort. */
  freeFor: (capacity: string) => number;
  onAdjust: (capacity: string, delta: number) => void;
}

/**
 * The stepper grid for choosing rooms, largest first.
 *
 * Its own component because the wizard draws it twice when a group brings
 * supervisors — once for the kadra and once for the children — and the two must
 * behave identically apart from which pool of free rooms they are capped at.
 */
export default function MixEditor({ capacities, counts, freeFor, onAdjust }: Props) {
  const { t } = useTranslation();

  return (
    <div className="mix-editor">
      {[...capacities]
        .sort((a, b) => Number(b) - Number(a))
        .map((capacity) => {
          const free = freeFor(capacity);
          const chosen = counts[capacity] ?? 0;
          return (
            <div key={capacity} className="mix-row">
              <span>{t("wizard.roomType", { capacity })}</span>
              <span className="mix-free">{t("wizard.freeRooms", { count: free })}</span>
              <div className="mix-stepper">
                <button
                  type="button"
                  disabled={chosen === 0}
                  onClick={() => onAdjust(capacity, -1)}
                  aria-label={t("wizard.removeRoom", { capacity })}
                >
                  −
                </button>
                <span>{chosen}</span>
                <button
                  type="button"
                  disabled={chosen >= free}
                  onClick={() => onAdjust(capacity, 1)}
                  aria-label={t("wizard.addRoom", { capacity })}
                >
                  +
                </button>
              </div>
            </div>
          );
        })}
    </div>
  );
}
