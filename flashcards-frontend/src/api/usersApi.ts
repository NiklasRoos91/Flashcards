const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

async function apiFetch(url: string, options: RequestInit = {}) {
  const token = sessionStorage.getItem("userData")
    ? JSON.parse(sessionStorage.getItem("userData")!).token
    : null;

  const res = await fetch(API_BASE_URL + url, {
    headers: {
      "Content-Type": "application/json",
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
    },
    ...options,
  });

  if (!res.ok) {
    const error = await res.text();
    throw new Error(error || "API error");
  }

  return res.json();
}

// ---------------------
// TYPES
// ---------------------
export interface GetUserInfoDto {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  address?: string | null;
  phoneNumber?: string | null;
}

export interface UpdateCurrentUserDto {
  firstName?: string;
  lastName?: string;
  address?: string;
  phoneNumber?: string;
}

export interface UpdateCurrentUserResponseDto {
  firstName?: string | null;
  lastName?: string | null;
  address?: string | null;
  phoneNumber?: string | null;
}

// ---------------------
// GET current user
// ---------------------
export async function getCurrentUser(): Promise<GetUserInfoDto> {
  return apiFetch("/api/Users/current");
}

// ---------------------
// PATCH update current user
// ---------------------
export async function updateCurrentUser(data: UpdateCurrentUserDto): Promise<UpdateCurrentUserResponseDto> {
  return apiFetch("/api/Users/current", {
    method: "PATCH",
    body: JSON.stringify(data),
  });
}

// ---------------------
// DELETE current user
// ---------------------
export async function deleteCurrentUser(): Promise<void> {
  await apiFetch("/api/Users/current", {
    method: "DELETE",
  });
}