import React, { useEffect, useState } from "react";
import Cookies from "js-cookie";
import { useNavigate } from "react-router-dom";
import "../Styles/AdminStyle.css";

const AdminPage = () => {
  const navigate = useNavigate();

  useEffect(() => {
    if (!Cookies.get("token")) {
      navigate("/");
    } else if (Cookies.get("isAdmin") === "false") {
      navigate("/reports");
    }
  }, [navigate]); 
  
  const [formData, setFormData] = useState({
    username: "",
    password: "",
    email: "",
  });

  const [message, setMessage] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");

    try {
      const response = await fetch("https://localhost:7021/auth/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(formData),
      });

      if (response.ok) {
        setMessage("User registered successfully!");
        setFormData({ username: "", password: "", email: "" });
      } else {
        const errorData = await response.json();
        setMessage(errorData.message || "Failed to register user.");
      }
    } catch (error) {
      console.error("Error registering user:", error);
      setMessage("An error occurred while registering.");
    }
  };

  return (
    <div id="container">
      <div id="admin-page">
        <div id="admin-box">
          <h1>Admin - Register User</h1>

          {message && <p>{message}</p>}

          <form onSubmit={handleSubmit}>
            <label>Username:</label>
            <input
              type="text"
              name="username"
              value={formData.username}
              onChange={handleChange}
              required
            />

            <label>Password:</label>
            <input
              type="password"
              name="password"
              value={formData.password}
              onChange={handleChange}
              required
            />

            <label>Email:</label>
            <input
              type="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              required
            />

            <button type="submit">Register User</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default AdminPage;
