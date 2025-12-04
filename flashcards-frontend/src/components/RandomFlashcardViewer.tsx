import { useEffect, useState } from "react";
import { getRandomFlashcard } from "../api/flashcardsApi";
import type { FlashcardResponseDto } from "../api/flashcardsApi";

interface RandomFlashcardViewerProps {
  listId: string;
}

export default function RandomFlashcardViewer({ listId }: RandomFlashcardViewerProps) {
  const [flashcard, setFlashcard] = useState<FlashcardResponseDto | null>(null);
  const [showAnswer, setShowAnswer] = useState(false);

  async function loadRandomFlashcard() {
    try {
      const data = await getRandomFlashcard(listId);
      setFlashcard(data);
      setShowAnswer(false); // Dölj svaret när ett nytt kort laddas
    } catch (err) {
      console.error("Kunde inte ladda kort:", err);
      setFlashcard(null);
    }
  }

  useEffect(() => {
    loadRandomFlashcard();
  }, [listId]);

  if (!flashcard) {
    return <p className="text-gray-500">Inga kort att visa.</p>;
  }

  return (
    <div className="p-6 bg-gray-50 rounded shadow max-w-lg mx-auto">
      <div className="border rounded p-4 bg-white shadow mb-4">
        <p className="text-lg font-semibold mb-2">Q: {flashcard.question}</p>
        {showAnswer && (
          <p className="text-gray-700 mt-2">
            <strong>A:</strong> {flashcard.answer}
          </p>
        )}
      </div>

      <div className="flex gap-4 justify-center">
        {!showAnswer && (
          <button
            onClick={() => setShowAnswer(true)}
            className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 transition"
          >
            Visa svar
          </button>
        )}
        <button
          onClick={loadRandomFlashcard}
          className="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600 transition"
        >
          Nästa kort
        </button>
      </div>
    </div>
  );
}
