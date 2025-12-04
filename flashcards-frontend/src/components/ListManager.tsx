import { useState } from "react";
import { createFlashcardList, deleteFlashcardList } from "../api/flashcardListsApi";

interface ListManagerProps {
  selectedListId: string | null;
  onUpdate: () => void; // Kallas när listorna ändras
}

export default function ListManager({ selectedListId, onUpdate }: ListManagerProps) {
  const [title, setTitle] = useState("");

  async function handleCreate(e: React.FormEvent) {
    e.preventDefault();
    if (!title.trim()) return;

    await createFlashcardList({ title });
    setTitle("");
    onUpdate();
  }

  async function handleDelete() {
    if (!selectedListId) return;
    if (!confirm("Är du säker på att du vill ta bort den här listan?")) return;

    await deleteFlashcardList(selectedListId);
    onUpdate();
  }

  return (
    <div className="mt-4">
      {/* Skapa ny lista */}
      <form onSubmit={handleCreate} className="flex flex-col mb-4">
        <h3 className="text-xl font-semibold mb-2">Ny lista</h3>
        <input
          placeholder="Titel"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          className="w-full p-2 border rounded mb-2 focus:outline-none focus:ring-2 focus:ring-blue-400"
        />
        <button
          type="submit"
          className="w-full px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 transition"
        >
          Skapa
        </button>
      </form>

      {/* Delete-knapp för vald lista */}
      {selectedListId && (
        <button
          className="w-full px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600 transition"
          onClick={handleDelete}
        >
          Ta bort vald lista
        </button>
      )}
    </div>
  );
}
