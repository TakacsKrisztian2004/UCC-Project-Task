import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import backgroundImage from "./Images/pexels-marek-piwnicki-3907296-8738455.jpg";

import HomePage from "./Pages/HomePage";
import AdminPage from "./Pages/AdminPage";
import ReportListPage from "./Pages/ReportListPage";
import ForgotPasswordPage from "./Pages/ForgotPasswordPage";
import AddReportPage from "./Pages/AddReportPage";
import ModifyReportPage from "./Pages/ModifyReportPage";
import ResetPasswordPage from "./Pages/ResetPasswordPage";

import NavBar from "./Components/NavBar";
import Footer from "./Components/Footer";

function App() {
  const backgroundStyle = {
    backgroundImage: `url(${backgroundImage})`,
    overflow: "hidden",
    backgroundSize: "100% 106.5vh",
    backgroundRepeat: "repeat-y",
  };

  return (
    <div style={backgroundStyle}>
    <Router>
      <NavBar />
      <div className="container mt-4">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/admin" element={<AdminPage />} />
          <Route path="/reports" element={<ReportListPage />} />
          <Route path="/forgot-password" element={<ForgotPasswordPage />} />
          <Route path="/reset-password/:token" element={<ResetPasswordPage />} />
          <Route path="/add-report" element={<AddReportPage />} />
          <Route path="/modify-report/:id" element={<ModifyReportPage />} />
        </Routes>
      </div>
      <Footer />
    </Router>
    </div>
  );
}

export default App;
