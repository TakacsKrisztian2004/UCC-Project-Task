import React, { useState } from "react";
import "../Styles/ForgotPasswordStyle.css";

const ForgotPasswordPage = () => {
  const [email, setEmail] = useState("");
  const [message, setMessage] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const handleChange = (e) => {
    setEmail(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");
    setIsLoading(true);
  
    try {
      const url = `https://localhost:7021/auth/send-reset-email?emailAddress=${encodeURIComponent(email)}`;
      
      const response = await fetch(url, {
        method: "POST",
      });
  
      if (response.ok) {
        setMessage("Password reset email sent successfully!");
      } else {
        setMessage("Failed to send reset email. Please try again.");
      }
    } catch (error) {
      setMessage("An error occurred. Please try again later.");
      console.error("Error:", error);
    } finally {
      setIsLoading(false);
    }
  };    

  return (
    <div className="container">
      <div id="forgot-password-page">
        <div id="forgot-password-box">
          <h2>Forgot Password</h2>
          <form onSubmit={handleSubmit} className="forgot-password-form">
            <label htmlFor="email">Enter your email:</label>
            <input
              type="email"
              id="email"
              value={email}
              onChange={handleChange}
              required
              placeholder="Enter your email"
            />
            <button type="submit" disabled={!email || isLoading}>
              {isLoading ? "Sending..." : "Send Reset Email"}
            </button>
          </form>
          {message && <p className="message">{message}</p>}
        </div>
      </div>
    </div>
  );
};

export default ForgotPasswordPage;
