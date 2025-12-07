import { useEffect, useState } from "react";
import { getFlashcardsForList } from "../api/flashcardListsApi";
import type { FlashcardResponseDto } from "../api/flashcardsApi";
import { createFlashcard, deleteFlashcard, updateFlashcard  } from "../api/flashcardsApi";

export default function FlashcardViewer({ listId }: { listId: string }) {
  const [flashcards, setFlashcards] = useState<FlashcardResponseDto[]>([]);
  const [question, setQuestion] = useState("");
  const [answer, setAnswer] = useState("");
  const [showAnswerId, setShowAnswerId] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [feedback, setFeedback] = useState("");
  const[search, setSearch] = useState("");
  const [editingId, setEditingId] = useState<string | null>(null);
  const [editQuestion, setEditQuestion] = useState("");
  const [editAnswer, setEditAnswer] = useState("");

  async function loadFlashcards() {
    setLoading(true);
    try {
      const data = await getFlashcardsForList(listId);
      setFlashcards(data);
    } catch (err) {
      console.error("Kunde inte ladda flashcards:", err);
  }   finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    loadFlashcards();
  }, [listId]);

  async function handleCreate() {
    if (!question.trim() || !answer.trim()) return; 

    setSaving(true);
    try {
      await createFlashcard({
        question,
        answer,
        flashcardListId: listId,
        tags: []
      });

      setQuestion("");
      setAnswer("");
      setFeedback("Flashcard lagt till!");
      setTimeout(() => setFeedback(""), 3000);

      await loadFlashcards();
    } catch (err) {
      console.error("Kunde inte skapa flashcard:", err);
      setFeedback("Kunde inte skapa flashcard.");
    } finally {
      setSaving(false);
    }
  }

  async function handleDelete(id: string) {
    await deleteFlashcard(id);
    loadFlashcards();
  }

  async function handleUpdate(id: string) {
    try {
      await updateFlashcard(id, { question: editQuestion, answer: editAnswer });
      setEditingId(null);
      await loadFlashcards();
      setFeedback("Flashcard uppdaterat!");
      setTimeout(() => setFeedback(""), 3000);
    } catch (err) {
      console.error("Kunde inte uppdatera flashcard:", err);
      setFeedback("Kunde inte uppdatera flashcard.");
    }
  }

  const filteredFlashcards = flashcards.filter(fc =>
    fc.question.toLowerCase().includes(search.toLowerCase()) ||
    fc.answer.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className="p-4">
      <h2 className="text-2xl font-bold mb-4">Flashcards</h2>

      {feedback && (
        <p className="text-green-500 font-semibold mb-2">{feedback}</p>
      )}

      <input
        type="text"
        placeholder="Sök flashcards..."
        value={search}
        onChange={e => setSearch(e.target.value)}
        className="w-full p-2 border rounded mb-4 focus:outline-none focus:ring-2 focus:ring-blue-400"
      />

      {loading ? (
        <p className="text-gray-500 text-center mb-4">Laddar flashcards...</p>
      ) : flashcards.length === 0 ? (
        <p className="text-gray-500 text-center mb-4">Inga flashcards i denna lista.</p>
      ) : (
        <div className="space-y-4">
          {filteredFlashcards.map(fc => (
            <div key={fc.flashcardId} className="border rounded shadow p-4 bg-gray-50">
              {editingId === fc.flashcardId ? (
                <>
                  <input
                    value={editQuestion}
                    onChange={(e) => setEditQuestion(e.target.value)}
                    className="w-full p-2 border rounded mb-1"
                  />
                  <input
                    value={editAnswer}
                    onChange={(e) => setEditAnswer(e.target.value)}
                    className="w-full p-2 border rounded mb-2"
                  />
                  <div className="flex gap-2">
                    <button
                      className="px-3 py-1 bg-green-500 text-white rounded hover:bg-green-600"
                      onClick={() => handleUpdate(fc.flashcardId)}
                    >
                      Spara
                    </button>
                    <button
                      className="px-3 py-1 bg-gray-300 rounded hover:bg-gray-400"
                      onClick={() => setEditingId(null)}
                    >
                      Avbryt
                    </button>
                  </div>
                </>
              ) : (
                <>
                  <p>
                    <strong>Q:</strong> {fc.question}
                  </p>
                  <p>
                    <strong>A:</strong> {fc.answer}
                  </p>
                  <div className="flex gap-2 mt-2">
                    <button
                      className="px-3 py-1 bg-yellow-500 text-white rounded hover:bg-yellow-600"
                      onClick={() => { 
                        setEditingId(fc.flashcardId);
                        setEditQuestion(fc.question);
                        setEditAnswer(fc.answer);
                      }}
                    >
                      Redigera
                    </button>
                    <button
                      className="px-3 py-1 bg-red-500 text-white rounded hover:bg-red-600"
                      onClick={() => handleDelete(fc.flashcardId)}
                    >
                      Ta bort
                    </button>
                  </div>
                </>
              )}  
            </div>
          ))}
        </div>
      )}

      <form
        onSubmit={(e) => {
          e.preventDefault();
          handleCreate();
        }}
        className="mt-6"
      >
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
          type="submit"
          disabled={saving}
          className={`px-4 py-2 text-white rounded transition ${
            saving ? "bg-gray-400 cursor-not-allowed" : "bg-blue-500 hover:bg-blue-600"
          }`}
        >
          {saving ? "Sparar..." : "Lägg till"}
        </button>
      </form>
    </div>
  );
}
