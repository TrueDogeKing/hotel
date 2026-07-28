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
  fits: boolean | null;
  suggestedMix: Record<string, number> | null;
  totalGrosze: number | null;
  depositGrosze: number | null;
}

export async function getAvailability(
  start: string,
  end: string,
  headcount?: number,
): Promise<Availability> {
  const { data } = await api.get<Availability>("/public/availability", {
    params: { start, end, ...(headcount ? { headcount } : {}) },
  });
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
  headcount: number;
  roomCounts: Record<string, number>;
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

export async function initiatePayment(
  token: string,
  kind: "Deposit" | "Final",
): Promise<{ redirectUrl: string }> {
  const { data } = await api.post<{ redirectUrl: string }>(`/public/bookings/${token}/payments`, {
    kind,
  });
  return data;
}
