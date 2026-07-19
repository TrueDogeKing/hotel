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

// --- Camp sessions (turnusy) ---

export type CampSessionStatus = "Draft" | "Published" | "Archived";

export interface CampSession {
  id: string;
  name: string;
  startDate: string;
  endDate: string;
  pricePerPersonGrosze: number;
  depositPerPersonGrosze: number;
  status: CampSessionStatus;
  rowVersion: number;
}

export interface CampSessionInput {
  name: string;
  startDate: string;
  endDate: string;
  pricePerPersonGrosze: number;
  depositPerPersonGrosze: number;
}

export async function getSessions(): Promise<CampSession[]> {
  const { data } = await api.get<CampSession[]>("/admin/sessions");
  return data;
}

export async function createSession(input: CampSessionInput): Promise<CampSession> {
  const { data } = await api.post<CampSession>("/admin/sessions", input);
  return data;
}

export async function updateSession(
  id: string,
  input: CampSessionInput & { rowVersion: number },
): Promise<CampSession> {
  const { data } = await api.put<CampSession>(`/admin/sessions/${id}`, input);
  return data;
}

export async function publishSession(id: string): Promise<CampSession> {
  const { data } = await api.post<CampSession>(`/admin/sessions/${id}/publish`);
  return data;
}

export async function archiveSession(id: string): Promise<CampSession> {
  const { data } = await api.post<CampSession>(`/admin/sessions/${id}/archive`);
  return data;
}

export async function deleteSession(id: string): Promise<void> {
  await api.delete(`/admin/sessions/${id}`);
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
