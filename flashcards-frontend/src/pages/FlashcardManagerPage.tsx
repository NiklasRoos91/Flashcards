import { useEffect, useState } from "react";
import ListSelector from "../components/ListSelector";
import ListManager from "../components/ListManager";
import FlashcardViewer from "../components/FlashcardViewer";
import Header from "../components/Header";
import { getFlashcardLists } from "../api/flashcardListsApi";
import type { FlashcardListResponseDto } from "../api/flashcardListsApi";

export default function FlashcardsPage() {
  const [lists, setLists] = useState<FlashcardListResponseDto[]>([]);
  const [selectedListId, setSelectedListId] = useState<string | null>(null);
  const [loading, setLoading] = useState(false); 
  const [flashcardsLoading, setFlashcardsLoading] = useState(false);

  async function loadLists() {
    setLoading(true);
    try {
      const data = await getFlashcardLists();
      setLists(data);
      if (data.length > 0 && !selectedListId) {
        setSelectedListId(data[0].flashcardListId);
      }
    } catch (err) {
      console.error("Kunde inte ladda listor:", err);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    loadLists();
  }, []);

    return (
    <div className="min-h-screen bg-gray-50">
      <Header />

      <div className="flex gap-8 p-6">
        <div className="w-80 bg-white p-4 rounded shadow flex flex-col">
          {loading? (
            <p className="text-gray-500 text-center mt-4">Laddar listor...</p>
          ) : (
            <>
              <ListSelector
                lists={lists}
                selectedListId={selectedListId}
                onSelect={setSelectedListId}
              />
              <ListManager
                selectedListId={selectedListId}
                onUpdate={loadLists}
              />
            </>
          )}
        </div>

        <div className="flex-1 bg-white p-6 rounded shadow">
          {flashcardsLoading  ? (
            <p className="text-gray-500 text-center mt-4">Laddar flashcards...</p>
          ) : selectedListId ? (
            lists.length === 0 ? (
              <p className="text-gray-500 text-center mt-4">
                Det finns inga flashcards i den här listan.
              </p>
            ) : (
              <FlashcardViewer
                listId={selectedListId}
              />
            )
          ) : (
            <p className="text-gray-500 text-lg text-center">
              Välj en lista för att visa flashcards.
            </p>          )}
        </div>
      </div>
    </div>
  );
}
