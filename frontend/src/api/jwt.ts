// Utility to decode JWT payload (no verification; we trust tokens from our API).
interface JWTPayload {
  preferred_username?: string;
  email?: string;
  given_name?: string;
  family_name?: string;
  sub?: string;
  exp?: number;
  [key: string]: unknown;
}

export function decodeJWT(token: string): JWTPayload | null {
  try {
    const parts = token.split(".");
    if (parts.length !== 3) return null;
    const b64 = parts[1].replace(/-/g, "+").replace(/_/g, "/");
    const bytes = Uint8Array.from(atob(b64), (c) => c.charCodeAt(0));
    const payload = JSON.parse(new TextDecoder().decode(bytes));
    return payload;
  } catch {
    return null;
  }
}

export function getUserNameFromToken(token: string | null): string | null {
  if (!token) return null;
  const payload = decodeJWT(token);
  const firstName = payload?.given_name || "";
  const lastName = payload?.family_name || "";
  const fullName = `${firstName} ${lastName}`.trim();
  return fullName || null;
}

// The unique login ("preferred_username" claim).
export function getUserLoginFromToken(token: string | null): string | null {
  if (!token) return null;
  return decodeJWT(token)?.preferred_username ?? null;
}

// The user id ("sub" claim); used to tell own messages/participants from others'.
export function getUserIdFromToken(token: string | null): string | null {
  if (!token) return null;
  return decodeJWT(token)?.sub ?? null;
}
