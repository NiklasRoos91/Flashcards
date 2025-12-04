import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { register } from "../api/authApi";

export default function Register() {
  const [email, setEmail] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");

  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();

  if (loading) return;
  setLoading(true);

  const missingFields: string[] = [];
  if (!email) missingFields.push("Email");
  if (!username) missingFields.push("Username");
  if (!password) missingFields.push("Password");
  if (!firstName) missingFields.push("First Name");
  if (!lastName) missingFields.push("Last Name");

  if (missingFields.length > 0) {
    alert("Please fill in the following fields:\n" + missingFields.join("\n"));
    setLoading(false);
    return;
  }

    try {
      const data = await register({
        email,
        username,
        password,
        firstName,
        lastName,
      });

      alert("Registration successful!");
      navigate("/");
    } catch (err: any) {
      console.error(err);
    
      if (err?.errorsMessages && Array.isArray(err.errorsMessages)) {
        alert(err.errorsMessages.join("\n"));
      } else if (err?.message) {
        alert("Registration failed: " + err.message);
      } else {
        alert("Registration failed: " + err.message);
      }
      } finally {
        setLoading(false);
      }
  };

  return (
    <div className="max-w-md mx-auto mt-20 p-6 border rounded shadow">
      <h1 className="text-2xl font-bold mb-6 text-center">Register</h1>

      <form onSubmit={handleRegister}>
        <label className="block mb-2">Email</label>
        <input
            type="email"
            className="border p-2 mb-4 w-full"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
        />

        <label className="block mb-2">Username</label>
        <input
            type="text"
            className="border p-2 mb-4 w-full"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
        />

        <label className="block mb-2">Password</label>
        <input
            type="password"
            className="border p-2 mb-4 w-full"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
        />

        <label className="block mb-2">First Name</label>
        <input
            type="text"
            className="border p-2 mb-4 w-full"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
        />

        <label className="block mb-2">Last Name</label>
        <input
            type="text"
            className="border p-2 mb-4 w-full"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
        />
            <button
            type="button"
            onClick={handleRegister}
            disabled={loading}
        className="bg-green-500 text-white px-4 py-1 rounded w-1/2 block mx-auto"
            >
            {loading ? "Registering..." : "Register"}
            </button>
        </form>
    </div>
  );
}
