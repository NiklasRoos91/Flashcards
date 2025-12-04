import { useEffect, useState } from "react";
import { getCurrentUser } from "../api/usersApi"; // använd api-filen

interface UserInfo {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  address?: string | null;
  phoneNumber? : string | null;
}

export default function Home() {
  const [userInfo, setUserInfo] = useState<UserInfo | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const data = await getCurrentUser(); // anropar usersApi som lägger på token
        setUserInfo(data);
      } catch (err: any) {
        console.error(err);
        setError("Could not load user info.");
      } finally {
        setLoading(false);
      }
    };

    fetchUser();
  }, []);

  if (loading) return <p>Loading user info...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div className="p-8 max-w-2xl mx-auto">
      <h1 className="text-3xl font-bold mb-4">
        Welcome, {userInfo?.username}!
      </h1>
      <div className="border p-4 rounded w-full bg-white shadow-md">
        <p><strong>Email:</strong> {userInfo?.email}</p>
        <p><strong>First Name:</strong> {userInfo?.firstName}</p>
        <p><strong>Last Name:</strong> {userInfo?.lastName}</p>
        <p><strong>Address:</strong> {userInfo?.address ?? "-"}</p>
        <p><strong>Phone Number:</strong> {userInfo?.phoneNumber ?? "-"}</p>
      </div>
    </div>
  );
}
