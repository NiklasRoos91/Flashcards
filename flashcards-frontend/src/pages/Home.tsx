import { useEffect, useState } from "react";

interface FlashcardList {
  id: string;
  name: string;
}

export default function Home() {
  const [lists, setLists] = useState<FlashcardList[]>([]);
  const [newListName, setNewListName] = useState("");
  const [loading, setLoading] = useState(true);
  const [username, setUsername] = useState("");

  useEffect(() => {
    const userData = sessionStorage.getItem("userData");
      console.log("userData from sessionStorage:", userData); // <--- debug
    if (userData) {
      const parsed = JSON.parse(userData);
      console.log("parsed userData:", parsed); // <--- debug
      setUsername(parsed.username);
    }
  }, []);

  const fetchLists = async () => {
    try {
      const res = await fetch("/api/FlashcardLists/get-flashcard-lists");
      if (!res.ok) throw new Error("Failed to fetch flashcard lists");
      const data = await res.json();
      setLists(data);
    } catch (err) {
      console.error(err);
      setLists([]);
    } finally {
      setLoading(false);
    }
  };

  const createList = async () => {
    if (!newListName) return;
    try {
      const res = await fetch("/api/FlashcardLists/create-flashcard-list", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name: newListName }),
      });
      if (!res.ok) throw new Error("Failed to create list");
      setNewListName("");
      fetchLists(); // uppdatera listan
    } catch (err) {
      console.error(err);
      alert("Could not create list");
    }
  };

  useEffect(() => {
    fetchLists();
  }, []);

  return (
    <div className="p-8 max-w-2xl mx-auto">
      <h1 className="text-3xl font-bold mb-6">Welcome, {username}!</h1>
      <h2 className="text-3xl font-bold mb-6">Flashcard Lists</h2>

      <div className="mb-6">
        <input
          type="text"
          placeholder="New list name"
          value={newListName}
          onChange={(e) => setNewListName(e.target.value)}
          className="border p-2 rounded mr-2"
        />
        <button
          onClick={createList}
          className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
        >
          Create
        </button>
      </div>

      {loading ? (
        <p>Loading...</p>
      ) : lists.length === 0 ? (
        <p>No flashcard lists found.</p>
      ) : (
        <ul className="space-y-2">
          {lists.map((list) => (
            <li
              key={list.id}
              className="border p-4 rounded hover:bg-gray-100 cursor-pointer"
            >
              {list.name}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
