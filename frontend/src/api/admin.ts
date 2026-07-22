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
