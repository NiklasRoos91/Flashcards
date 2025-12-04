import { useEffect, useState } from "react";
import ListSelector from "../components/ListSelector";
import RandomFlashcardViewer from "../components/RandomFlashcardViewer";
import Header from "../components/Header";
import { getFlashcardLists } from "../api/flashcardListsApi";
import type { FlashcardListResponseDto } from "../api/flashcardListsApi";

export default function RandomFlashcardsPage() {
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
      {/* Header högst upp */}
      <Header />

      <div className="max-w-7xl mx-auto flex gap-8 p-6">
        {/* Lista att välja från */}
        <div className="w-80 bg-white p-4 rounded shadow flex flex-col">
          <ListSelector
            lists={lists}
            selectedListId={selectedListId}
            onSelect={setSelectedListId}
          />
        </div>

        {/* Random kort */}
        <div className="flex-1">
          {selectedListId ? (
            <RandomFlashcardViewer listId={selectedListId} />
          ) : (
            <p className="text-gray-500 text-lg text-center mt-20">
              Välj en lista för att börja.
            </p>
          )}
        </div>
      </div>
    </div>
  );
}
