import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import axios from "axios";
import "../Styles/HomeStyle.css";
import AuthService from "../Components/AuthService";

const HomePage = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    if (Cookies.get("token")) {
      navigate("/reports");
    }
  }, []);

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const token = await AuthService.login(username, password);
      if (!token) {
        setError("Username or password is incorrect");
        return;
      }

      const response = await axios.post(
        `https://localhost:7021/auth/is-admin?username=${username}`
      );

      if (token) {
        Cookies.set("token", token, { expires: 7 });
        Cookies.set("username", username, { expires: 7 });
        Cookies.set("isAdmin", response.data.isAdmin, { expires: 7 });

        fetch(`https://localhost:7129/api/User/getuserbyusername/${username}`)
          .then((res) => res.json())
          .then((data) => {
            if (data && data.id) {
              Cookies.set("userId", data.id, { expires: 7 });
              console.log("User ID stored:", data.id);
              navigate("/reports");
            } else {
              console.error("User ID not found in response:", data);
            }
          })
          .catch((error) => {
            console.error("Error fetching user ID:", error);
            setError("Error fetching user ID.");
          });
      }
    } catch (error) {
      setError("Login failed. Please try again.");
    }
  };

  return (
    <div id="home">
      <div id="home-content">
        <h1 id="title">
          Welcome to the customer report management system, please log in to
          continue...
        </h1>
        <form id="login-form" onSubmit={handleLogin} autoComplete="on">
          <div className="form-inputs">
            <div className="form-group">
              <label htmlFor="username">Username:</label>
              <input
                type="text"
                id="username"
                placeholder="Username"
                autoComplete="username"
                value={username}
                onChange={(e) => {
                  setUsername(e.target.value);
                  setError("");
                }}
              />
            </div>
            <div className="form-group">
              <label htmlFor="password">Password:</label>
              <input
                type="password"
                id="password"
                placeholder="Password"
                autoComplete="current-password"
                value={password}
                onChange={(e) => {
                  setPassword(e.target.value);
                  setError("");
                }}
              />
            </div>
          </div>
          {error && <p style={{ color: "red" }}>{error}</p>}
          <Link id="forgot-password-link" to="/forgot-password">
            Forgot password?
          </Link>
          <button id="login-button" type="submit">
            Login
          </button>
        </form>
      </div>
    </div>
  );
};

export default HomePage;
