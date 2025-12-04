import type { FlashcardListResponseDto } from "../api/flashcardListsApi";

interface ListSelectorProps {
  lists: FlashcardListResponseDto[];
  selectedListId: string | null;
  onSelect: (id: string) => void;
}

export default function ListSelector({ lists, selectedListId, onSelect }: ListSelectorProps) {
  return (
    <div className="flex flex-col">
      <h2 className="text-2xl font-bold mb-4">Mina listor</h2>

      <ul className="space-y-2 flex-1 overflow-y-auto">
        {lists.map(list => (
          <li key={list.flashcardListId} className="flex items-center justify-between">
            <button
              className={`text-left px-3 py-2 rounded w-full transition ${
                selectedListId === list.flashcardListId
                  ? "bg-blue-200 hover:bg-blue-300"
                  : "bg-gray-100 hover:bg-gray-200"
              }`}
              onClick={() => onSelect(list.flashcardListId)}
            >
              {list.title} ({list.flashcardCount})
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
