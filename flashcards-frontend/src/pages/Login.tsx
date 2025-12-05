import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../api/authApi";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    if (loading) return;
    setLoading(true);

    if (!email || !password) {
      alert("Please enter both email and password.");
      setLoading(false);
      return;
    }

    try {
      const data = await login(email, password);

      // Save user data to sessionStorage
      sessionStorage.setItem("userData", JSON.stringify(data));

      // Redirect to home page
      navigate("/flashcards");
    } catch (err) {
      console.error(err);
      alert("Login failed. Check your credentials.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-md mx-auto mt-20 p-6 border rounded shadow">
      <h1 className="text-2xl font-bold mb-6 text-center">Login</h1>

      <form onSubmit={handleLogin}>
        <label className="block mb-2">Email</label>
        <input
        type="email"
        autoComplete="username"
        className="border p-2 mb-4 w-full"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />

      <label className="block mb-2">Password</label>
      <input
        type="password"
        autoComplete="current-password"
        className="border p-2 mb-4 w-full"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />

      <button
        type= "submit"
        disabled={loading}
        className="bg-blue-500 text-white px-4 py-1 rounded w-1/2 mb-3 block mx-auto"
      >
        {loading ? "Logging in..." : "Login"}
      </button>

      <button
      type="button"
      onClick={() => navigate("/register")}
        className="bg-green-500 text-white px-4 py-1 rounded w-1/2 block mx-auto"
      >
        Register
      </button>
      </form>
    </div>
  );
}
