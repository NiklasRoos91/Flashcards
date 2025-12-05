import { NavLink, useNavigate } from "react-router-dom";

export default function Header() {
  const navigate = useNavigate();

  

  function handleLogout() {
    if (confirm("Vill du logga ut?")) {
      sessionStorage.removeItem("userData");
      navigate("/");
    }
  }

  return (
    <header className="bg-white shadow p-4">
      <div className="max-w-7xl mx-auto flex flex-col items-center">

        {/* Titel */}
        <h1 className="text-3xl font-bold mb-2">Flashcards</h1>

        {/* Nav-länkar */}
        <nav className="flex gap-6">
          <NavLink
            to="/flashcards"
            className={({ isActive }) =>
              isActive
                ? "text-blue-500 font-semibold border-b-2 border-blue-500 pb-1"
                : "text-gray-700 hover:text-blue-500 transition"
            }
          >
            Hantera listor
          </NavLink>

          <NavLink
            to="/random"
            className={({ isActive }) =>
              isActive
                ? "text-blue-500 font-semibold border-b-2 border-blue-500 pb-1"
                : "text-gray-700 hover:text-blue-500 transition"
            }
          >
            Random kort
          </NavLink>

          <NavLink
            to="/profile"
            className={({ isActive }) =>
              isActive
                ? "text-blue-500 font-semibold border-b-2 border-blue-500 pb-1"
                : "text-gray-700 hover:text-blue-500 transition"
            }
          >
            Profil
          </NavLink>

          <button
            onClick={handleLogout}
            className="text-red-500 font-semibold hover:text-red-600 transition"
          >
            Logga ut
          </button>
        </nav>
      </div>
    </header>
  );
}
