import { api } from "./client";
import { getStoredLanguage } from "../i18n";

export interface Availability {
  startDate: string;
  endDate: string;
  nights: number;
  centerClosed: boolean;
  centerClosedReason: string | null;
  pricePerPersonPerNightGrosze: number;
  depositPerPersonPerNightGrosze: number;
  remainingCapacity: number;
  freeRoomsByCapacity: Record<string, number>;
  supervisorPricePerPersonPerNightGrosze: number;
  fits: boolean | null;
  suggestedMix: Record<string, number> | null;
  /** Rooms suggested for the supervisors — their own, and the smallest that fit. */
  suggestedSupervisorMix: Record<string, number> | null;
  totalGrosze: number | null;
  depositGrosze: number | null;
}

export async function getAvailability(
  start: string,
  end: string,
  headcount?: number,
  supervisors?: number,
): Promise<Availability> {
  const { data } = await api.get<Availability>("/public/availability", {
    params: {
      start,
      end,
      ...(headcount ? { headcount } : {}),
      ...(supervisors ? { supervisors } : {}),
    },
  });
  return data;
}

/** One night as the booking calendar draws it. `date` is the night starting on
 *  it — a stay's departure day is not a night, so a day that is unavailable can
 *  still be a valid checkout. */
export interface AvailabilityDay {
  date: string;
  closed: boolean;
  closureReason: string | null;
  freeBeds: number;
  /** Enough free beds for the headcount asked about. */
  fits: boolean;
}

export interface AvailabilityCalendar {
  start: string;
  end: string;
  headcount: number | null;
  days: AvailabilityDay[];
}

/** Both ends inclusive: these are the days a calendar draws, not a stay. */
export async function getAvailabilityCalendar(
  start: string,
  end: string,
  headcount?: number,
): Promise<AvailabilityCalendar> {
  const { data } = await api.get<AvailabilityCalendar>("/public/availability/calendar", {
    params: { start, end, ...(headcount ? { headcount } : {}) },
  });
  return data;
}

/** The centre's rates, with no stay attached — what the booking wizard quotes
 *  before any dates have been picked. */
export interface PublicPricing {
  pricePerPersonPerNightGrosze: number;
  supervisorPricePerPersonPerNightGrosze: number;
  depositPerPersonPerNightGrosze: number;
}

export async function getPublicPricing(): Promise<PublicPricing> {
  const { data } = await api.get<PublicPricing>("/public/pricing");
  return data;
}

export interface PublicClosure {
  reason: string;
  startDate: string;
  endDate: string;
}

export async function getPublicClosures(): Promise<PublicClosure[]> {
  const { data } = await api.get<PublicClosure[]>("/public/availability/closures");
  return data;
}

export interface CreateBookingInput {
  startDate: string;
  endDate: string;
  /** Campers and supervisors together. */
  headcount: number;
  supervisorCount: number;
  /** Rooms for the campers; the kadra get their own, below. */
  roomCounts: Record<string, number>;
  supervisorRoomCounts: Record<string, number>;
  organizationName: string;
  contactName: string;
  email: string;
  phone: string;
  notes: string | null;
}

export interface CreateBookingResult {
  bookingId: string;
  manageToken: string;
}

export async function createBooking(input: CreateBookingInput): Promise<CreateBookingResult> {
  const { data } = await api.post<CreateBookingResult>("/public/bookings", {
    ...input,
    language: getStoredLanguage(),
  });
  return data;
}

export interface BookingPayment {
  id: string;
  kind: "Deposit" | "Final";
  status: "Pending" | "Completed" | "Failed";
  amountGrosze: number;
  createdAt: string;
  completedAt: string | null;
}

export interface BookingDetails {
  id: string;
  status: "PendingDeposit" | "Confirmed" | "Cancelled" | "Completed";
  cancelReason: string | null;
  startDate: string;
  endDate: string;
  nights: number;
  organizationName: string;
  contactName: string;
  email: string;
  phone: string;
  headcount: number;
  roomCounts: Record<string, number>;
  totalGrosze: number;
  depositGrosze: number;
  holdExpiresAt: string | null;
  finalPaymentDueDate: string;
  payments: BookingPayment[];
}

export async function getBooking(token: string): Promise<BookingDetails> {
  const { data } = await api.get<BookingDetails>(`/public/bookings/${token}`);
  return data;
}

export async function cancelBooking(token: string): Promise<void> {
  await api.post(`/public/bookings/${token}/cancel`);
}

// --- The group's own camp programme (read-only) ---

// Deliberately has no prepNotes field: kitchen prep notes are internal and the
// server never sends them here.
export interface PublicScheduleEntry {
  kind: "Meal" | "Activity";
  mealKind: "Breakfast" | "Lunch" | "Dinner" | "Snack" | null;
  startTime: string;
  endTime: string;
  title: string;
  menu: string | null;
  location: string | null;
}

export interface PublicScheduleDay {
  date: string;
  entries: PublicScheduleEntry[];
}

export interface PublicSchedule {
  startDate: string;
  endDate: string;
  status: BookingDetails["status"];
  days: PublicScheduleDay[];
}

export async function getBookingSchedule(token: string): Promise<PublicSchedule> {
  const { data } = await api.get<PublicSchedule>(`/public/bookings/${token}/schedule`);
  return data;
}

/** Client-side mirror of ValidateSplitMix: the two cohorts are judged against
 *  their own rooms, so a double for two supervisors is not redundant just because
 *  the whole group is fifty. Both draw on the same free rooms, so availability is
 *  checked across the union first. */
export function validateSplitMix(
  campers: number,
  supervisors: number,
  camperCounts: Record<string, number>,
  supervisorCounts: Record<string, number>,
  free: Record<string, number>,
): "ok" | "too-small" | "unavailable" | "redundant" {
  const union: Record<string, number> = { ...camperCounts };
  for (const [cap, count] of Object.entries(supervisorCounts)) {
    union[cap] = (union[cap] ?? 0) + count;
  }
  for (const [cap, count] of Object.entries(union)) {
    if (count > (free[cap] ?? 0)) return "unavailable";
  }

  if (supervisors > 0) {
    const staff = validateMix(supervisors, supervisorCounts, union);
    if (staff !== "ok") return staff;
  }

  return validateMix(campers, camperCounts, union);
}

// Client-side mirror of the server's mix rules, for live wizard feedback.
export function validateMix(
  headcount: number,
  counts: Record<string, number>,
  free: Record<string, number>,
): "ok" | "too-small" | "unavailable" | "redundant" {
  const entries = Object.entries(counts).filter(([, v]) => v > 0);
  for (const [cap, count] of entries) {
    if (count > (free[cap] ?? 0)) return "unavailable";
  }
  const total = entries.reduce((sum, [cap, count]) => sum + Number(cap) * count, 0);
  if (total < headcount) return "too-small";
  for (const [cap] of entries) {
    if (total - Number(cap) >= headcount) return "redundant";
  }
  return "ok";
}

// Online payment is switched off — the endpoint behind this is commented out in
// PublicBookingsController. Left here for whenever card payment comes back.
// export async function initiatePayment(
//   token: string,
//   kind: "Deposit" | "Final",
// ): Promise<{ redirectUrl: string }> {
//   const { data } = await api.post<{ redirectUrl: string }>(`/public/bookings/${token}/payments`, {
//     kind,
//   });
//   return data;
// }
