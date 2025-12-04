import type { FlashcardResponseDto } from "./flashcardsApi";
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

async function apiFetch<T>(url: string, options: RequestInit = {}): Promise<T> {
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

  if (res.status === 204) return null as unknown as T;

  return res.json();
}

// ---------------------
// TYPES
// ---------------------
export interface FlashcardListResponseDto {
  flashcardListId: string;
  title: string;
  flashcardCount: number;
  createdAt: string;
}

export interface CreateFlashcardListDto {
  title: string;
}

export interface CreateFlashcardListResponseDto {
  flashcardListId: string;
  title: string;
}

export interface UpdateFlashcardListDto {
  flashcardListId: string;
  title: string;
}

// ---------------------
// API FUNCTIONS
// ---------------------

// GET all flashcard lists
export async function getFlashcardLists(): Promise<FlashcardListResponseDto[]> {
  return apiFetch(`/api/FlashcardLists/get-flashcard-lists`);
}

// CREATE new flashcard list
export async function createFlashcardList(
  dto: CreateFlashcardListDto
): Promise<CreateFlashcardListResponseDto> {
  return apiFetch(`/api/FlashcardLists/create-flashcard-list`, {
    method: "POST",
    body: JSON.stringify(dto),
  });
}

// GET flashcards for a specific list
export async function getFlashcardsForList(
  listId: string
): Promise<FlashcardResponseDto[]> {
  return apiFetch(`/api/FlashcardLists/${listId}/flashcards`);
}

// UPDATE flashcard list
export async function updateFlashcardList(
  id: string,
  dto: UpdateFlashcardListDto
): Promise<UpdateFlashcardListDto> {
  return apiFetch(`/api/FlashcardLists/${id}`, {
    method: "PATCH",
    body: JSON.stringify(dto),
  });
}

// DELETE flashcard list
export async function deleteFlashcardList(id: string): Promise<boolean> {
  return apiFetch(`/api/FlashcardLists/${id}`, {
    method: "DELETE",
  });
}
