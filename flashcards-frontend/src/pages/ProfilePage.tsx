import { useEffect, useState } from "react";
import { getCurrentUser, updateCurrentUser, deleteCurrentUser } from "../api/usersApi";
import { useNavigate } from "react-router-dom";
import Header from "../components/Header";

export default function ProfilePage() {
  const [userData, setUserData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    address: "",
    phoneNumber: "",
  });
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const navigate = useNavigate();

  async function loadUser() {
    setLoading(true);
    try {
      const data = await getCurrentUser();
      setUserData({
        firstName: data.firstName,
        lastName: data.lastName,
        email: data.email,
        address: data.address || "",
        phoneNumber: data.phoneNumber || "",
      });
    } catch (err) {
      console.error("Kunde inte ladda användare:", err);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    loadUser();
  }, []);

  const handleSave = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);
    try {
      await updateCurrentUser({
        firstName: userData.firstName,
        lastName: userData.lastName,
        address: userData.address,
        phoneNumber: userData.phoneNumber,
      });
      alert("Profil uppdaterad!");
    } catch (err) {
      console.error("Kunde inte spara:", err);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async () => {
    if (!confirm("Är du säker på att du vill radera ditt konto?")) return;
    try {
      await deleteCurrentUser();
      sessionStorage.removeItem("userData");
      navigate("/");
    } catch (err) {
      console.error("Kunde inte radera användare:", err);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <Header />

      <h2 className="text-2xl font-bold mb-6 text-center">Profil</h2>

      {loading ? (
        <p className="text-center text-gray-500">Laddar...</p>
      ) : (
        <form
          onSubmit={handleSave}
          className="max-w-xl mx-auto bg-white p-6 rounded shadow space-y-4"
        >
          <div>
            <label className="block font-semibold mb-1">Förnamn</label>
            <input
              type="text"
              value={userData.firstName}
              onChange={(e) =>
                setUserData({ ...userData, firstName: e.target.value })
              }
              className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-400"
            />
          </div>

          <div>
            <label className="block font-semibold mb-1">Efternamn</label>
            <input
              type="text"
              value={userData.lastName}
              onChange={(e) =>
                setUserData({ ...userData, lastName: e.target.value })
              }
              className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-400"
            />
          </div>

          <div>
            <label className="block font-semibold mb-1">Email</label>
            <input
              type="email"
              value={userData.email}
              readOnly
              className="w-full p-2 border rounded bg-gray-100 cursor-not-allowed"
            />
          </div>

          <div>
            <label className="block font-semibold mb-1">Adress</label>
            <input
              type="text"
              value={userData.address}
              onChange={(e) =>
                setUserData({ ...userData, address: e.target.value })
              }
              className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-400"
            />
          </div>

          <div>
            <label className="block font-semibold mb-1">Telefonnummer</label>
            <input
              type="text"
              value={userData.phoneNumber}
              onChange={(e) =>
                setUserData({ ...userData, phoneNumber: e.target.value })
              }
              className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-400"
            />
          </div>

          <div className="flex gap-4">
            <button
              type="submit"
              disabled={saving}
              className="flex-1 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 transition"
            >
              {saving ? "Sparar..." : "Spara"}
            </button>
            <button
              type="button"
              onClick={handleDelete}
              className="flex-1 px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600 transition"
            >
              Radera konto
            </button>
          </div>
        </form>
      )}
    </div>
  );
}
