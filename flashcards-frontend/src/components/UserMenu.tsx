import { useEffect, useState } from "react";

interface UserInfo {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  address?: string;
  phoneNumber?: string;
}

export default function UserMenu() {
  const [userInfo, setUserInfo] = useState<UserInfo | null>(null);
  const [formData, setFormData] = useState<UserInfo>({
    username: "",
    email: "",
    firstName: "",
    lastName: "",
    address: "",
    phoneNumber: "",
  });
  const [editMode, setEditMode] = useState(false);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const res = await fetch("/api/Users/current");
        if (!res.ok) throw new Error("Failed to fetch user data");
        const data: UserInfo = await res.json();
        setUserInfo(data);
        setFormData(data);
      } catch (err) {
        console.error(err);
      }
    };
    fetchUser();
  }, []);

  const handleUpdate = async () => {
    setLoading(true);
    try {
      const res = await fetch("/api/Users/current", {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });
      if (!res.ok) throw new Error("Update failed");
      const updated: UserInfo = await res.json();
      setUserInfo(updated);
      setEditMode(false);
    } catch (err) {
      console.error(err);
      alert("Could not update user info");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async () => {
    if (!confirm("Are you sure you want to delete your account?")) return;
    try {
      const res = await fetch("/api/Users/current", { method: "DELETE" });
      if (!res.ok) throw new Error("Delete failed");
      sessionStorage.removeItem("userData");
      window.location.href = "/"; // redirect till login
    } catch (err) {
      console.error(err);
      alert("Could not delete user");
    }
  };

  if (!userInfo) return <p>Loading user info...</p>;

  return (
    <div className="border p-4 rounded w-80 bg-white shadow-md">
      {!editMode ? (
        <div>
          <p className="font-bold mb-2">{userInfo.username}</p>
          <p>{userInfo.email}</p>
          <p>{userInfo.firstName} {userInfo.lastName}</p>
          <p>{userInfo.address}</p>
          <p>{userInfo.phoneNumber}</p>
          <div className="mt-4 flex justify-between">
            <button
              onClick={() => setEditMode(true)}
              className="bg-blue-500 text-white px-3 py-1 rounded"
            >
              Edit
            </button>
            <button
              onClick={handleDelete}
              className="bg-red-500 text-white px-3 py-1 rounded"
            >
              Delete
            </button>
          </div>
        </div>
      ) : (
        <form
          onSubmit={(e) => {
            e.preventDefault();
            handleUpdate();
          }}
          className="space-y-2"
        >
          <input
            type="text"
            value={formData.username}
            onChange={(e) => setFormData({ ...formData, username: e.target.value })}
            placeholder="Username"
            className="border p-2 w-full rounded"
          />
          <input
            type="email"
            value={formData.email}
            onChange={(e) => setFormData({ ...formData, email: e.target.value })}
            placeholder="Email"
            className="border p-2 w-full rounded"
          />
          <input
            type="text"
            value={formData.firstName}
            onChange={(e) => setFormData({ ...formData, firstName: e.target.value })}
            placeholder="First Name"
            className="border p-2 w-full rounded"
          />
          <input
            type="text"
            value={formData.lastName}
            onChange={(e) => setFormData({ ...formData, lastName: e.target.value })}
            placeholder="Last Name"
            className="border p-2 w-full rounded"
          />
          <input
            type="text"
            value={formData.address ?? ""}
            onChange={(e) => setFormData({ ...formData, address: e.target.value })}
            placeholder="Address"
            className="border p-2 w-full rounded"
          />
          <input
            type="text"
            value={formData.phoneNumber ?? ""}
            onChange={(e) => setFormData({ ...formData, phoneNumber: e.target.value })}
            placeholder="Phone Number"
            className="border p-2 w-full rounded"
          />
          <div className="flex justify-between mt-2">
            <button
              type="submit"
              disabled={loading}
              className="bg-blue-500 text-white px-3 py-1 rounded"
            >
              {loading ? "Saving..." : "Save"}
            </button>
            <button
              type="button"
              onClick={() => setEditMode(false)}
              className="bg-gray-300 px-3 py-1 rounded"
            >
              Cancel
            </button>
          </div>
        </form>
      )}
    </div>
  );
}
