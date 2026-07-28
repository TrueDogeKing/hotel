import { api } from "./client";

// --- Rooms ---

export interface Room {
  id: string;
  number: string;
  capacity: number;
  isActive: boolean;
  description: string | null;
  rowVersion: number;
}

export interface RoomInput {
  number: string;
  capacity: number;
  description: string | null;
}

export async function getRooms(): Promise<Room[]> {
  const { data } = await api.get<Room[]>("/admin/rooms");
  return data;
}

export async function createRoom(input: RoomInput): Promise<Room> {
  const { data } = await api.post<Room>("/admin/rooms", input);
  return data;
}

export async function updateRoom(
  id: string,
  input: RoomInput & { isActive: boolean; rowVersion: number },
): Promise<Room> {
  const { data } = await api.put<Room>(`/admin/rooms/${id}`, input);
  return data;
}

// Hard-deletes an unreferenced room; a room with booking history is deactivated instead.
export async function deleteRoom(id: string): Promise<{ deleted: boolean }> {
  const { data } = await api.delete<{ deleted: boolean }>(`/admin/rooms/${id}`);
  return data;
}

// --- Closures (blokady) ---

export interface Closure {
  id: string;
  reason: string;
  startDate: string;
  endDate: string;
  roomId: string | null;
  roomNumber: string | null;
  rowVersion: number;
}

export interface ClosureInput {
  reason: string;
  startDate: string;
  endDate: string;
  roomId: string | null;
}

export async function getClosures(): Promise<Closure[]> {
  const { data } = await api.get<Closure[]>("/admin/closures");
  return data;
}

export async function createClosure(input: ClosureInput): Promise<Closure> {
  const { data } = await api.post<Closure>("/admin/closures", input);
  return data;
}

export async function updateClosure(
  id: string,
  input: ClosureInput & { rowVersion: number },
): Promise<Closure> {
  const { data } = await api.put<Closure>(`/admin/closures/${id}`, input);
  return data;
}

export async function deleteClosure(id: string): Promise<void> {
  await api.delete(`/admin/closures/${id}`);
}

// Grosze → "1 234,56 zł" style display; forms edit złote as decimal strings.
export function formatZl(grosze: number): string {
  return (
    new Intl.NumberFormat("pl-PL", { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(
      grosze / 100,
    ) + " zł"
  );
}

export function zlToGrosze(zl: string): number {
  return Math.round(Number(zl.replace(",", ".")) * 100);
}

export function groszeToZl(grosze: number): string {
  return (grosze / 100).toFixed(2);
}

// --- Bookings (admin) ---

export interface AdminAssignment {
  id: string;
  roomId: string;
  roomNumber: string;
  capacity: number;
  peopleCount: number;
}

export interface AdminBooking {
  id: string;
  startDate: string;
  endDate: string;
  nights: number;
  organizationName: string;
  contactName: string;
  email: string;
  phone: string;
  headcount: number;
  notes: string | null;
  dietaryNotes: string | null;
  status: "PendingDeposit" | "Confirmed" | "Cancelled" | "Completed";
  cancelReason: string | null;
  totalGrosze: number;
  depositGrosze: number;
  depositPaid: boolean;
  finalPaid: boolean;
  finalOverdue: boolean;
  finalPaymentDueDate: string;
  createdAt: string;
  assignments: AdminAssignment[];
}

export async function getAdminBookings(filters: { status?: string }): Promise<AdminBooking[]> {
  const { data } = await api.get<AdminBooking[]>("/admin/bookings", { params: filters });
  return data;
}

export async function cancelAdminBooking(id: string): Promise<void> {
  await api.post(`/admin/bookings/${id}/cancel`);
}

export type BookingStatus = AdminBooking["status"];

export const bookingStatuses: BookingStatus[] = [
  "PendingDeposit",
  "Confirmed",
  "Cancelled",
  "Completed",
];

export interface CreateAdminBookingInput {
  startDate: string;
  endDate: string;
  organizationName: string;
  contactName: string;
  email: string;
  phone: string;
  headcount: number;
  notes: string | null;
  status: BookingStatus;
  language: string;
}

/** Records a group taken by phone or at the door: rooms are picked automatically
 *  and the contact gets no confirmation email. */
export async function createAdminBooking(
  input: CreateAdminBookingInput,
): Promise<AdminBooking> {
  const { data } = await api.post<AdminBooking>("/admin/bookings", input);
  return data;
}

/** Manual status override. Cancelling frees the rooms and emails the group;
 *  moving back out of Cancelled takes the rooms again and can 409. */
export async function setBookingStatus(
  id: string,
  status: BookingStatus,
): Promise<AdminBooking> {
  const { data } = await api.put<AdminBooking>(`/admin/bookings/${id}/status`, { status });
  return data;
}

// --- Occupancy ---

export interface RoomOccupancy {
  roomId: string;
  roomNumber: string;
  capacity: number;
  isActive: boolean;
  bookingId: string | null;
  organizationName: string | null;
  bookingStatus: string | null;
  peopleCount: number | null;
  closed: boolean;
  closureReason: string | null;
  openTaskCount: number;
}

export interface Occupancy {
  startDate: string;
  endDate: string;
  totalBeds: number;
  occupiedBeds: number;
  rooms: RoomOccupancy[];
}

export async function getOccupancy(start: string, end: string): Promise<Occupancy> {
  const { data } = await api.get<Occupancy>("/admin/occupancy", { params: { start, end } });
  return data;
}

// --- Housekeeping tasks ---

export interface RoomTask {
  id: string;
  roomId: string;
  roomNumber: string;
  bookingId: string | null;
  text: string;
  status: "Open" | "Done";
  createdAt: string;
  doneAt: string | null;
}

export async function getTasks(filters: {
  status?: string;
  bookingId?: string;
}): Promise<RoomTask[]> {
  const { data } = await api.get<RoomTask[]>("/admin/tasks", { params: filters });
  return data;
}

export async function createTask(input: {
  roomId: string;
  text: string;
  bookingId: string | null;
}): Promise<RoomTask> {
  const { data } = await api.post<RoomTask>("/admin/tasks", input);
  return data;
}

export async function setTaskDone(id: string, done: boolean): Promise<RoomTask> {
  const { data } = await api.post<RoomTask>(`/admin/tasks/${id}/${done ? "done" : "reopen"}`);
  return data;
}

export async function deleteTask(id: string): Promise<void> {
  await api.delete(`/admin/tasks/${id}`);
}

// --- Dashboard ---

export interface DashboardBooking {
  id: string;
  organizationName: string;
  startDate: string;
  endDate: string;
  headcount: number;
  occupiedBeds: number;
  status: string;
}

export interface Dashboard {
  upcomingBookings: DashboardBooking[];
  pendingDepositCount: number;
  overdueFinalCount: number;
  openTaskCount: number;
  activeClosureCount: number;
}

export async function getDashboard(): Promise<Dashboard> {
  const { data } = await api.get<Dashboard>("/admin/dashboard");
  return data;
}

// --- Camp schedule (harmonogram) ---

export type ScheduleEntryKind = "Meal" | "Activity";
export type MealKind = "Breakfast" | "Lunch" | "Dinner" | "Snack";

export const mealKinds: MealKind[] = ["Breakfast", "Lunch", "Dinner", "Snack"];

export interface ScheduleEntry {
  id: string;
  bookingId: string;
  organizationName: string;
  headcount: number;
  kind: ScheduleEntryKind;
  mealKind: MealKind | null;
  date: string;
  /** "HH:mm:ss" — pipe through toTimeInput() before binding to an <input type="time">. */
  startTime: string;
  endTime: string;
  title: string;
  menu: string | null;
  prepNotes: string | null;
  location: string | null;
  /** This entry's time was set for this one day; a bulk re-time will skip it. */
  timesCustomized: boolean;
  rowVersion: number;
}

/** A group's stay as a calendar bar. endDate is inclusive: the bar spans nights + 1 days. */
export interface ScheduleCalendarBooking {
  bookingId: string;
  organizationName: string;
  startDate: string;
  endDate: string;
  nights: number;
  headcount: number;
  status: AdminBooking["status"];
}

export interface ScheduleCalendarDay {
  date: string;
  groupCount: number;
  peopleCount: number;
  mealCount: number;
  activityCount: number;
}

export interface ScheduleCalendar {
  start: string;
  end: string;
  bookings: ScheduleCalendarBooking[];
  days: ScheduleCalendarDay[];
}

export interface ScheduleDayGroup {
  bookingId: string;
  organizationName: string;
  headcount: number;
  status: AdminBooking["status"];
  isArrivalDay: boolean;
  isDepartureDay: boolean;
  dietaryNotes: string | null;
}

export interface ScheduleDay {
  date: string;
  groups: ScheduleDayGroup[];
  entries: ScheduleEntry[];
}

export interface BookingScheduleDay {
  date: string;
  isArrivalDay: boolean;
  isDepartureDay: boolean;
  entries: ScheduleEntry[];
}

export interface BookingSchedule {
  bookingId: string;
  organizationName: string;
  contactName: string;
  email: string;
  phone: string;
  startDate: string;
  endDate: string;
  nights: number;
  headcount: number;
  status: AdminBooking["status"];
  notes: string | null;
  dietaryNotes: string | null;
  bookingRowVersion: number;
  days: BookingScheduleDay[];
}

export interface ScheduleEntryInput {
  kind: ScheduleEntryKind;
  mealKind: MealKind | null;
  date: string;
  startTime: string;
  endTime: string;
  title: string;
  menu: string | null;
  prepNotes: string | null;
  location: string | null;
}

export async function getScheduleCalendar(start: string, end: string): Promise<ScheduleCalendar> {
  const { data } = await api.get<ScheduleCalendar>("/admin/schedule/calendar", {
    params: { start, end },
  });
  return data;
}

export async function getScheduleDay(date: string): Promise<ScheduleDay> {
  const { data } = await api.get<ScheduleDay>(`/admin/schedule/day/${date}`);
  return data;
}

export async function getBookingSchedule(bookingId: string): Promise<BookingSchedule> {
  const { data } = await api.get<BookingSchedule>(`/admin/schedule/bookings/${bookingId}`);
  return data;
}

export async function createScheduleEntry(
  input: ScheduleEntryInput & { bookingId: string },
): Promise<ScheduleEntry> {
  const { data } = await api.post<ScheduleEntry>("/admin/schedule/entries", input);
  return data;
}

export async function updateScheduleEntry(
  id: string,
  input: ScheduleEntryInput & { rowVersion: number },
): Promise<ScheduleEntry> {
  const { data } = await api.put<ScheduleEntry>(`/admin/schedule/entries/${id}`, input);
  return data;
}

export async function deleteScheduleEntry(id: string): Promise<void> {
  await api.delete(`/admin/schedule/entries/${id}`);
}

/** Idempotent — meals an admin deleted on purpose are not recreated. */
export async function generateMealsForBooking(bookingId: string): Promise<{ created: number }> {
  const { data } = await api.post<{ created: number }>(
    `/admin/schedule/bookings/${bookingId}/generate-meals`,
  );
  return data;
}

export async function generateMissingMeals(
  from: string,
  to: string,
): Promise<{ bookings: number; created: number }> {
  const { data } = await api.post<{ bookings: number; created: number }>(
    "/admin/schedule/generate-meals",
    null,
    { params: { from, to } },
  );
  return data;
}

export async function updateDietaryNotes(
  bookingId: string,
  input: { dietaryNotes: string | null; rowVersion: number },
): Promise<AdminBooking> {
  const { data } = await api.put<AdminBooking>(
    `/admin/bookings/${bookingId}/dietary-notes`,
    input,
  );
  return data;
}

// --- Meal-time defaults (domyślne pory posiłków) ---

export interface MealTimeDefault {
  id: string;
  mealKind: MealKind;
  label: string;
  startTime: string;
  endTime: string;
  sortOrder: number;
  isActive: boolean;
  rowVersion: number;
}

export interface MealTimeDefaultInput {
  mealKind: MealKind;
  label: string;
  startTime: string;
  endTime: string;
  sortOrder: number;
}

export async function getMealTimes(): Promise<MealTimeDefault[]> {
  const { data } = await api.get<MealTimeDefault[]>("/admin/meal-times");
  return data;
}

export async function createMealTime(input: MealTimeDefaultInput): Promise<MealTimeDefault> {
  const { data } = await api.post<MealTimeDefault>("/admin/meal-times", input);
  return data;
}

export async function updateMealTime(
  id: string,
  input: MealTimeDefaultInput & { isActive: boolean; rowVersion: number },
): Promise<MealTimeDefault> {
  const { data } = await api.put<MealTimeDefault>(`/admin/meal-times/${id}`, input);
  return data;
}

/** Hard-deletes an unused slot; deactivates one that already produced meals. */
export async function deleteMealTime(id: string): Promise<{ deleted: boolean }> {
  const { data } = await api.delete<{ deleted: boolean }>(`/admin/meal-times/${id}`);
  return data;
}

// --- Per-group meal times ---

/** A center meal slot as it applies to one group. */
export interface BookingMealTime {
  mealTimeDefaultId: string;
  mealKind: MealKind;
  label: string;
  sortOrder: number;
  defaultStartTime: string;
  defaultEndTime: string;
  startTime: string;
  endTime: string;
  isOverridden: boolean;
  /** 0 when the group has no override row yet. */
  rowVersion: number;
}

export interface ApplyBookingMealTimeResult {
  mealTime: BookingMealTime;
  updated: number;
  skippedCustomized: number;
  /** Meals the stay was missing and that were seeded as part of applying the time. */
  created: number;
}

export async function getBookingMealTimes(bookingId: string): Promise<BookingMealTime[]> {
  const { data } = await api.get<BookingMealTime[]>(
    `/admin/schedule/bookings/${bookingId}/meal-times`,
  );
  return data;
}

/**
 * Sets this group's own time for a meal slot. With applyToExisting the whole stay
 * is re-timed at once — days moved individually are always left alone.
 */
export async function setBookingMealTime(
  bookingId: string,
  mealTimeDefaultId: string,
  input: { startTime: string; endTime: string; applyToExisting: boolean; rowVersion: number },
): Promise<ApplyBookingMealTimeResult> {
  const { data } = await api.put<ApplyBookingMealTimeResult>(
    `/admin/schedule/bookings/${bookingId}/meal-times/${mealTimeDefaultId}`,
    input,
  );
  return data;
}

export async function resetBookingMealTime(
  bookingId: string,
  mealTimeDefaultId: string,
  applyToExisting: boolean,
): Promise<ApplyBookingMealTimeResult> {
  const { data } = await api.delete<ApplyBookingMealTimeResult>(
    `/admin/schedule/bookings/${bookingId}/meal-times/${mealTimeDefaultId}`,
    { params: { applyToExisting } },
  );
  return data;
}
