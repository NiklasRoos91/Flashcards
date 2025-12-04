const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

async function apiFetch(url: string, options: RequestInit = {}) {
  const res = await fetch(API_BASE_URL + url, {
    headers: {
      "Content-Type": "application/json",
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
// LOGIN
// ---------------------
export interface LoginResponse {
  username: string;
  token: string;
  userId: string;
}

export async function login(email: string, password: string) {
  return apiFetch("/api/Auth/login", {
    method: "POST",
    body: JSON.stringify({
      LoginDto: {
        email,
        password,
      },
    }),
  });
}

// ---------------------
// REGISTER
// ---------------------
export async function register(data: {
  email: string;
  password: string;
  username: string;
  firstName: string;
  lastName: string;
  address?: string;
  phoneNumber?: string;
}) {
  return apiFetch("/api/Auth/register", {
    method: "POST",
    body: JSON.stringify({
      registerUserDto: {
        email: data.email,
        password: data.password,
        username: data.username,
        firstName: data.firstName,
        lastName: data.lastName,
        address: data.address ?? null,
        phoneNumber: data.phoneNumber ?? null,
      },
    }),
  });
}
