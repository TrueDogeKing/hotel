/**
 * A group as it reads in a schedule: "Karatecy (3 op.)".
 *
 * The kitchen and housekeeping read these lists too, so the number is labelled
 * rather than left as a bare bracket — on its own, "(3)" could be rooms, nights
 * or anything else.
 */
export function groupLabel(
  organizationName: string,
  supervisorCount: number,
  supervisorsShort: (count: number) => string,
): string {
  return supervisorCount > 0
    ? `${organizationName} ${supervisorsShort(supervisorCount)}`
    : organizationName;
}
