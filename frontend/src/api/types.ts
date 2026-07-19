// Shared API response/request shapes mirroring the backend DTOs.

export interface LoginRequest {
  login: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  expiresAtUtc: string;
  login: string;
}
