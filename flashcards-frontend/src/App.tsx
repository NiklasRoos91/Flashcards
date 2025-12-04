import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import FlashcardsPage from "./pages/FlashcardsPage";
import RandomFlashcardsPage from "./pages/RandomFlashcardsPage";
import ProfilePage from "./pages/ProfilePage";
import Login from "./pages/Login";
import Register from "./pages/Register";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/flashcards" element={<FlashcardsPage />} />
        <Route path="/random" element={<RandomFlashcardsPage />} />
        <Route path="/profile" element={<ProfilePage />} />
      </Routes>
    </Router>
  );
}

export default App;
