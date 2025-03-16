import React from "react";
import { Modal, Button } from "react-bootstrap";
import "../Styles/PopupWindowStyle.css";

const PopupWindow = ({ show, onClose, report }) => {
  if (!report) return null;

  return (
    <Modal show={show} onHide={onClose} centered>
      <Modal.Header closeButton>
        <Modal.Title>{report.title}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <p>
          <strong>Customer:</strong> {report.customer}
        </p>
        <p>
          <strong>Occurrence Date:</strong>{" "}
          {new Date(report.occurrenceDate).toLocaleDateString()}
        </p>
        <p>
          <strong>Description:</strong> {report.description}
        </p>
        <p>
          <strong>Status:</strong>{" "}
          <span
            className={report.resolved ? "text-resolved" : "text-unresolved"}
          >
            {report.resolved ? "Resolved" : "Unresolved"}
          </span>
        </p>
      </Modal.Body>
    </Modal>
  );
};

export default PopupWindow;