import { useEffect, useState } from "react";
import ListSelector from "../components/ListSelector";
import ListManager from "../components/ListManager";
import FlashcardViewer from "../components/FlashcardManager";
import Header from "../components/Header";
import { getFlashcardLists } from "../api/flashcardListsApi";
import type { FlashcardListResponseDto } from "../api/flashcardListsApi";

export default function FlashcardsPage() {
  const [lists, setLists] = useState<FlashcardListResponseDto[]>([]);
  const [selectedListId, setSelectedListId] = useState<string | null>(null);

  async function loadLists() {
    try {
      const data = await getFlashcardLists();
      setLists(data);
    } catch (err) {
      console.error("Kunde inte ladda listor:", err);
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
          <ListSelector
            lists={lists}
            selectedListId={selectedListId}
            onSelect={setSelectedListId}
          />
          <ListManager
            selectedListId={selectedListId}
            onUpdate={loadLists}
          />
        </div>

        <div className="flex-1 bg-white p-6 rounded shadow">
          {selectedListId ? (
            <FlashcardViewer listId={selectedListId} />
          ) : (
            <p className="text-gray-500 text-lg">Välj en lista för att visa flashcards.</p>
          )}
        </div>
      </div>
    </div>
  );
}
