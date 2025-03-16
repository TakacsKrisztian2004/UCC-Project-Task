import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";

const ModifyReportPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [report, setReport] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

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
      <h1>Modify Report</h1>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Title</label>
          <input
            type="text"
            name="title"
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
            value={report.customer}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>
        <div className="form-check mb-3">
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
        <button type="submit" className="btn btn-primary">Update Report</button>
      </form>
    </div>
  );
};

export default ModifyReportPage;