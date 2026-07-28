import type { CSSProperties } from "react";
import { dayNumber, fromIsoDate, mondayIndex, todayIso } from "../../utils/dates";
import { groupHue } from "./lanes";
import type { CalendarBar, CalendarDayBadge } from "./MonthCalendar";

interface Props {
  iso: string;
  month0: number;
  bars: CalendarBar[];
  lanes: Map<string, number>;
  laneCount: number;
  badge?: CalendarDayBadge;
  isSelected: boolean;
  onSelectDay: (iso: string) => void;
  onSelectBar: (id: string) => void;
}

export default function CalendarTile({
  iso,
  month0,
  bars,
  lanes,
  laneCount,
  badge,
  isSelected,
  onSelectDay,
  onSelectBar,
}: Props) {
  const date = fromIsoDate(iso);
  const dayNum = dayNumber(iso);
  const weekday = mondayIndex(iso); // 0 = Monday
  const isOutside = date.getMonth() !== month0;
  const isToday = iso === todayIso();
  const isSunday = weekday === 6;

  // One slot per lane, always rendered, so segments line up across days.
  const slots: (CalendarBar | null)[] = Array.from({ length: laneCount }, () => null);
  for (const bar of bars) {
    if (dayNum < dayNumber(bar.startIso) || dayNum > dayNumber(bar.endIso)) continue;
    const lane = lanes.get(bar.id);
    if (lane !== undefined) slots[lane] = bar;
  }

  const classes = ["cal-tile"];
  if (isOutside) classes.push("outside");
  if (isToday) classes.push("today");
  if (isSelected) classes.push("selected");
  if (isSunday) classes.push("sunday");

  return (
    <div className={classes.join(" ")}>
      <button
        type="button"
        className="cal-date"
        onClick={() => onSelectDay(iso)}
        aria-label={iso}
      >
        {date.getDate()}
      </button>

      <div className="cal-lanes">
        {slots.map((bar, lane) => {
          if (!bar) return <div className="cal-lane" key={lane} />;

          const barStart = dayNumber(bar.startIso);
          const barEnd = dayNumber(bar.endIso);
          const isStart = dayNum === barStart;
          const isEnd = dayNum === barEnd;
          // A stay that runs on into tomorrow bridges the grid gap, so a 3-day camp
          // reads as one unbroken line rather than three separate blocks. Not at the
          // end of a week — there the next day is on the row below.
          const continues = !isEnd && weekday !== 6;
          // The label is drawn once per week-run, starting on the first day of that
          // run, and allowed to overflow across the remaining tiles. A stay that wraps
          // onto the next week row repeats its name there, so every row reads on its
          // own. Equal-width grid columns mean one segment == one tile width, so this
          // needs no DOM measurement.
          const showLabel = isStart || weekday === 0;
          const daysLeftInWeek = 7 - weekday;
          const daysLeftInBar = barEnd - dayNum + 1;
          const span = Math.min(daysLeftInWeek, daysLeftInBar);

          // Rounding only at the true ends; interior joins stay square so
          // consecutive segments read as one continuous pill.
          const segClasses = ["cal-bar"];
          if (isStart) segClasses.push("start");
          if (isEnd) segClasses.push("end");
          if (continues) segClasses.push("continues");
          if (bar.muted) segClasses.push("pending");
          // Lifts this segment above the following days' segments, which are later in
          // the DOM and would otherwise paint over the label overhanging into them.
          if (showLabel) segClasses.push("labeled");

          return (
            <div className="cal-lane" key={lane}>
              <button
                type="button"
                className={segClasses.join(" ")}
                style={{ "--bar-hue": groupHue(bar.id) } as CSSProperties}
                onClick={() => onSelectBar(bar.id)}
                title={bar.label}
              >
                {showLabel && (
                  <span
                    className="cal-bar-title"
                    style={{ width: `calc(${span} * 100% + ${(span - 1) * 4}px - 20px)` }}
                  >
                    {bar.label}
                  </span>
                )}
              </button>
            </div>
          );
        })}
      </div>

      {badge && badge.people > 0 && (
        <div className="cal-badges">
          <span>{badge.people}👥</span>
          {badge.meals > 0 && <span>{badge.meals}🍽</span>}
          {badge.activities > 0 && <span>{badge.activities}★</span>}
        </div>
      )}
    </div>
  );
}
