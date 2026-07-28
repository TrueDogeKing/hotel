/**
 * Lane packing for the month calendar.
 *
 * Multi-day bars are not absolutely positioned. Instead every event gets a lane
 * (row) index, and each day tile renders one fixed-height slot per lane — either
 * a bar segment or an empty spacer. That keeps segments of the same bar aligned
 * across days without any DOM measurement.
 */

export interface LaneEvent {
  id: string;
  /** Inclusive epoch day numbers — see utils/dates.dayNumber. */
  start: number;
  end: number;
}

/**
 * Greedy assignment: each event takes the lowest lane whose last occupied day
 * ends before this event starts. Input is not mutated.
 */
export function packLanes(events: readonly LaneEvent[]): Map<string, number> {
  const laneEnds: number[] = [];
  const lanes = new Map<string, number>();
  const sorted = [...events].sort((a, b) => a.start - b.start || a.end - b.end);

  for (const event of sorted) {
    let lane = laneEnds.findIndex((end) => end < event.start);
    if (lane === -1) lane = laneEnds.length;
    laneEnds[lane] = event.end;
    lanes.set(event.id, lane);
  }

  return lanes;
}

/**
 * Deterministic hue per group, banded to the app's forest-green → lake-blue range
 * (accent #005236 is roughly 159°) so bars stay on-brand.
 *
 * Only the hue is returned. The CSS picks saturation and lightness per theme —
 * baking a full hsl() here, as the reference implementation does, would give
 * unreadable bars in one of the two themes.
 */
export function groupHue(id: string): number {
  let hash = 0;
  for (let i = 0; i < id.length; i++) {
    hash = ((hash << 5) - hash + id.charCodeAt(i)) | 0;
  }
  return 130 + (Math.abs(hash) % 70);
}
