import { useEffect, useState } from "react";
import { getFlashcardsForList } from "../api/flashcardListsApi";
import type { FlashcardResponseDto } from "../api/flashcardsApi";
import { createFlashcard, deleteFlashcard } from "../api/flashcardsApi";

export default function FlashcardViewer({ listId }: { listId: string }) {
  const [flashcards, setFlashcards] = useState<FlashcardResponseDto[]>([]);
  const [question, setQuestion] = useState("");
  const [answer, setAnswer] = useState("");
  const [showAnswerId, setShowAnswerId] = useState<string | null>(null);

  async function loadFlashcards() {
    const data = await getFlashcardsForList(listId);
    setFlashcards(data);
  }

  useEffect(() => {
    loadFlashcards();
  }, [listId]);

  async function handleCreate() {
    if (!question.trim() || !answer.trim()) return;

    await createFlashcard({
      question,
      answer,
      flashcardListId: listId,
      tags: []
    });

    setQuestion("");
    setAnswer("");
    loadFlashcards();
  }

  async function handleDelete(id: string) {
    await deleteFlashcard(id);
    loadFlashcards();
  }

  return (
    <div className="p-4">
      <h2 className="text-2xl font-bold mb-4">Flashcards</h2>

      <div className="space-y-4">
        {flashcards.map(fc => (
          <div key={fc.flashcardId} className="border rounded shadow p-4 bg-gray-50">
            <div
              className="cursor-pointer p-2 bg-gray-200 rounded hover:bg-gray-300 transition"
              onClick={() =>
                setShowAnswerId(showAnswerId === fc.flashcardId ? null : fc.flashcardId)
              }
            >
              <strong>Q:</strong> {fc.question}
            </div>

            {showAnswerId === fc.flashcardId && (
              <div className="p-2 mt-2 bg-gray-100 rounded">
                <strong>A:</strong> {fc.answer}
              </div>
            )}

            <button
              className="mt-2 px-3 py-1 bg-red-500 text-white rounded hover:bg-red-600 transition"
              onClick={() => handleDelete(fc.flashcardId)}
            >
              Ta bort
            </button>
          </div>
        ))}
      </div>

      <div className="mt-6">
        <h3 className="text-xl font-semibold mb-2">Skapa nytt flashcard</h3>

        <input
          placeholder="Fråga"
          value={question}
          onChange={e => setQuestion(e.target.value)}
          className="w-full p-2 border rounded mb-2 focus:outline-none focus:ring-2 focus:ring-blue-400"
        />
        <input
          placeholder="Svar"
          value={answer}
          onChange={e => setAnswer(e.target.value)}
          className="w-full p-2 border rounded mb-2 focus:outline-none focus:ring-2 focus:ring-blue-400"
        />

        <button
          className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 transition"
          onClick={handleCreate}
        >
          Lägg till
        </button>
      </div>
    </div>
  );
}
