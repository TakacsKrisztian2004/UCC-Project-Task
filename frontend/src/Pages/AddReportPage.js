import React, { useEffect, useState } from "react";
import Cookies from "js-cookie";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import "../Styles/AddReportStyle.css";

const AddReportPage = () => {
  const navigate = useNavigate();

  useEffect(() => {
    if (!Cookies.get("token")) {
      navigate("/");
    }
  }, []);

  const [formData, setFormData] = useState({
    title: "",
    description: "",
    customer: "",
  });

  const userId = Cookies.get("userId");

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!userId) {
      alert("User ID not found. Please log in again.");
      return;
    }

    const reportData = {
      ...formData,
      userID: userId,
    };

    try {
      const response = await axios.post(
        "https://localhost:7129/api/Report",
        reportData,
        {
          headers: { "Content-Type": "application/json" },
        }
      );

      if (response.status === 201) {
        alert("Report created successfully!");
        setFormData({ title: "", description: "", customer: "" });
      }
    } catch (error) {
      console.error("Error creating report:", error);
      alert("Failed to create report.");
    }
  };

  return (
    <div id="add-report-page">
      <div id="add-report-box">
        <h1>Add Report</h1>
        <form id="add-report-form" onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Title:</label>
            <input
              className="add-report-input"
              type="text"
              name="title"
              maxLength={35}
              placeholder="Max 35 characters."
              value={formData.title}
              onChange={handleChange}
              required
            />
          </div>
          <div className="form-group">
            <label>Description:</label>
            <textarea
              className="add-report-input"
              name="description"
              value={formData.description}
              onChange={handleChange}
              required
              rows="4"
            />
          </div>
          <div className="form-group">
            <label>Customer:</label>
            <input
              className="add-report-input"
              type="text"
              name="customer"
              maxLength={45}
              placeholder="Max 45 characters."
              value={formData.customer}
              onChange={handleChange}
              required
            />
          </div>
          <button id="add-report-button" type="submit">
            Submit Report
          </button>
        </form>
      </div>
    </div>
  );
};

export default AddReportPage;
