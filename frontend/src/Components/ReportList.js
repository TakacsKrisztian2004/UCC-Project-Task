import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import PopupWindow from "./PopupWindow";
import "../Styles/ReportListStyle.css";

const ReportList = () => {
  const [reports, setReports] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error] = useState("");
  const [selectedReport, setSelectedReport] = useState(null);
  const [showPopup, setShowPopup] = useState(false);
  const [filter, setFilter] = useState("all");

  const navigate = useNavigate();

  useEffect(() => {
    const fetchReports = async () => {
      try {
        const response = await fetch(
          `https://localhost:7129/api/Connection/userdetailsbyusername/${Cookies.get(
            "username"
          )}`,
          {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
            },
          }
        );

        const data = await response.json();

        if (data.reports && Array.isArray(data.reports)) {
          const reportDetails = data.reports.map((report) => ({
            id: report.reportId,
            title: report.title,
            occurrenceDate: report.occurrence,
            description: report.description,
            customer: report.customer,
            resolved: report.resolved,
          }));

          setReports(reportDetails);
        } else {
          setReports([]);
        }
      } catch (error) {
        console.error("Error fetching reports:", error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchReports();
  }, []);

  const handleModify = (reportId) => {
    navigate(`/modify-report/${reportId}`);
  };

  const handleShowMore = (report) => {
    setSelectedReport(report);
    setShowPopup(true);
  };

  const filteredReports = reports.filter((report) => {
    if (filter === "all") return true;
    if (filter === "resolved") return report.resolved;
    if (filter === "unresolved") return !report.resolved;
    return true;
  });

  const deleteReport = async (reportId) => {
    if (!window.confirm("Are you sure you want to delete this report?")) {
      return;
    }

    try {
      const response = await fetch(
        `https://localhost:7129/api/Report/${reportId}`,
        {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      if (response.ok) {
        setReports((prevReports) =>
          prevReports.filter((report) => report.id !== reportId)
        );
      } else {
        console.error("Failed to delete the report");
      }
    } catch (error) {
      console.error("Error deleting report:", error);
    }
  };

  const toggleResolveReport = async (reportId, currentStatus) => {
    try {
      const response = await fetch(
        `https://localhost:7129/api/Report/toggleresolved/${reportId}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      if (response.ok) {
        setReports((prevReports) =>
          prevReports.map((report) =>
            report.id === reportId
              ? { ...report, resolved: !currentStatus }
              : report
          )
        );
      } else {
        console.error("Failed to toggle the report status");
      }
    } catch (error) {
      console.error("Error toggling report status:", error);
    }
  };

  return (
    <div id="report-list">
      <div className="container">
        <h2 id="report-list-title">Reports</h2>

        <div
          className="filter-buttons btn-group mb-4"
          role="group"
          aria-label="Filter"
        >
          <button
            className={`btn ${
              filter === "all" ? "btn-primary" : "btn-outline-primary"
            }`}
            onClick={() => setFilter("all")}
          >
            All
          </button>
          <button
            className={`btn ${
              filter === "unresolved" ? "btn-primary" : "btn-outline-primary"
            }`}
            onClick={() => setFilter("unresolved")}
          >
            Unresolved
          </button>
          <button
            className={`btn ${
              filter === "resolved" ? "btn-primary" : "btn-outline-primary"
            }`}
            onClick={() => setFilter("resolved")}
          >
            Resolved
          </button>
        </div>

        {isLoading && (
          <div className="d-flex justify-content-center">
            <div className="spinner-border" role="status"></div>
          </div>
        )}

        {error && <p className="text-danger text-center">{error}</p>}

        {!isLoading && filteredReports.length === 0 && (
          <div className="text-center">
            <h2 className="text-white">No reports could be found.</h2>
          </div>
        )}

        <div className="row">
          {filteredReports.map((report) => (
            <div key={report.id} className="col-12 col-md-6 col-lg-4 mb-4">
              <div className="card shadow-sm">
                <div className="card-body">
                  <h5 className="card-title">{report.title}</h5>
                  <h6 className="card-subtitle mb-2">
                    {new Date(report.occurrenceDate).toLocaleDateString()}
                  </h6>
                  <p className="card-text">
                    <strong>Customer:</strong> {report.customer}
                  </p>
                  <p className="card-text">
                    {report.description.length > 100
                      ? `${report.description.substring(0, 100)}...`
                      : report.description}
                  </p>
                  {report.description.length > 100 && (
                    <button
                      id="show-more"
                      onClick={() => handleShowMore(report)}
                    >
                      Show More
                    </button>
                  )}
                  <p className="card-text">
                    <strong>Status:</strong>{" "}
                    {report.resolved ? (
                      <span className="text-resolved">Resolved</span>
                    ) : (
                      <span className="text-unresolved">Unresolved</span>
                    )}
                  </p>

                  <button
                    className={`resolve-button ${
                      report.resolved ? "btn-orange" : "btn-green"
                    }`}
                    onClick={() =>
                      toggleResolveReport(report.id, report.resolved)
                    }
                  >
                    {report.resolved ? "Unresolve" : "Resolve"}
                  </button>

                  <div className="d-flex justify-content-between mt-3">
                    <button
                      className="modify-button"
                      onClick={() => handleModify(report.id)}
                    >
                      Modify
                    </button>
                    <button
                      className="delete-button"
                      onClick={() => deleteReport(report.id)}
                    >
                      Delete
                    </button>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>

      <PopupWindow
        show={showPopup}
        onClose={() => setShowPopup(false)}
        report={selectedReport}
      />
    </div>
  );
};

export default ReportList;
