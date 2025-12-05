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
export interface FlashcardResponseDto {
  flashcardId: string;
  question: string;
  answer: string;
  tags: string[];
}

export interface CreateFlashcardDto {
  question: string;
  answer: string;
  flashcardListId: string;
  tags?: string[];
}

export interface CreateFlashcardResponseDto extends FlashcardResponseDto {}

export interface UpdateFlashcardDto {
  question?: string;
  answer?: string;
  tags?: string[];
}


// GET random flashcard
export async function getRandomFlashcard(flashcardListId: string): Promise<FlashcardResponseDto> {
  return apiFetch(`/api/Flashcards/get-random-flashcard/${flashcardListId}`);
}

// GET flashcard by id
export async function getFlashcardById(flashcardId: string): Promise<FlashcardResponseDto> {
  return apiFetch(`/api/Flashcards/${flashcardId}`);
}

// CREATE flashcard
export async function createFlashcard(dto: CreateFlashcardDto): Promise<CreateFlashcardResponseDto> {
  return apiFetch(`/api/Flashcards/create-flashcard`, {
    method: "POST",
    body: JSON.stringify(dto),
  });
}

// UPDATE flashcard
export async function updateFlashcard(flashcardId: string, dto: UpdateFlashcardDto): Promise<FlashcardResponseDto> {
  return apiFetch(`/api/Flashcards/${flashcardId}`, {
    method: "PATCH",
    body: JSON.stringify(dto),
  });
}

// DELETE flashcard
export async function deleteFlashcard(flashcardId: string): Promise<boolean> {
  return apiFetch(`/api/Flashcards/${flashcardId}`, {
    method: "DELETE",
  });
}
