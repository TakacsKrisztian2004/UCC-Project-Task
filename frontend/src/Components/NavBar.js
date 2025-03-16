import React from "react";
import { Link, useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import "../Styles/NavBarStyle.css";

const NavBar = () => {
  const navigate = useNavigate();
  const token = Cookies.get("token");
  const isAdmin = Cookies.get("isAdmin") === "true";

  const handleSignOut = () => {
    Cookies.remove("token");
    Cookies.remove("username");
    Cookies.remove("isAdmin");
    Cookies.remove("userId");
    navigate("/");
    window.location.reload();
  };

  return (
    <nav>
      {token ? (
        <>
          <button id="signout" onClick={handleSignOut}>
            Sign Out
          </button>
          <Link to="/reports">
            Report List
          </Link>
          <Link to="/add-report">
            New Report
          </Link>
          {isAdmin && (
            <Link to="/admin" className="admin-link">
              Register
            </Link>
          )}
        </>
      ) : (
        <Link to="/">Home</Link>
      )}
    </nav>
  );
};

export default NavBar;