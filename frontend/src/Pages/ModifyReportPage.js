import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import "../Styles/ModifyReportStyle.css";

const ModifyReportPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [report, setReport] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
      if (!Cookies.get("token")) {
        navigate("/");
      }
    }, []);

  useEffect(() => {
    const fetchReport = async () => {
      try {
        const response = await fetch(`https://localhost:7129/api/Report/${id}`);
        if (!response.ok) {
          throw new Error("Failed to fetch report data");
        }
        const data = await response.json();
        setReport(data);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };
    fetchReport();
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setReport((prevReport) => ({
      ...prevReport,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(`https://localhost:7129/api/Report/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(report),
      });

      if (!response.ok) {
        throw new Error("Failed to update the report");
      }
      alert("Report updated successfully");
      navigate("/");
    } catch (error) {
      setError(error.message);
    }
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p className="text-danger">Error: {error}</p>;

  return (
    <div className="container mt-5">
      <div id="modify-report-page">
        <div id="modify-report">
          <h1>Modify Report</h1>
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label className="form-label">Title</label>
              <input
                type="text"
                name="title"
                maxLength={35}
                placeholder="Max 35 characters."
                value={report.title}
                onChange={handleChange}
                className="form-control"
                required
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Occurrence Date</label>
              <input
                type="date"
                name="occurrence"
                value={report.occurrence?.split("T")[0] || ""}
                onChange={handleChange}
                className="form-control"
                required
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Description</label>
              <textarea
                name="description"
                value={report.description}
                onChange={handleChange}
                className="form-control"
                required
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Customer</label>
              <input
                type="text"
                name="customer"
                maxLength={45}
                placeholder="Max 45 characters."
                value={report.customer}
                onChange={handleChange}
                className="form-control"
                required
              />
            </div>
            <div id="modify-report-bottom">
              <div className="form-check">
                <input
                  type="checkbox"
                  name="resolved"
                  checked={report.resolved}
                  onChange={(e) =>
                    setReport((prevReport) => ({
                      ...prevReport,
                      resolved: e.target.checked,
                    }))
                  }
                  className="form-check-input"
                />
                <label className="form-check-label">Resolved</label>
              </div>
              <button type="submit" className="btn btn-primary">
                Update Report
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ModifyReportPage;
