import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import "../Styles/ResetPasswordStyle.css";

const ResetPasswordPage = () => {
  const { token } = useParams();
  const decodedToken = token ? decodeURIComponent(token) : null;
  const navigate = useNavigate();
  const [emailAddress, setEmailAddress] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [message, setMessage] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    if (!decodedToken) {
      setMessage("Invalid or expired token.");
    } else {
      console.log("Decoded Token:", decodedToken);
    }
  }, [decodedToken]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");

    if (!emailAddress) {
      setMessage("Please enter your email address.");
      return;
    }

    if (newPassword !== confirmPassword) {
      setMessage("Passwords do not match.");
      return;
    }

    if (!decodedToken) {
      setMessage("A reset token is required to reset the password.");
      return;
    }

    setIsLoading(true);

    try {
      const response = await fetch("https://localhost:7021/auth/reset-password", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          emailAddress: emailAddress,
          newPassword: newPassword,
          resetToken: decodedToken,
        }),
      });

      if (response.ok) {
        setMessage("Password reset successfully!");
        setTimeout(() => {
          navigate("/");
        }, 2000);
      } else if (response.status === 400) {
        setMessage("The reset token is invalid or expired. Please try again.");
      } else {
        setMessage("Failed to reset password. Please try again.");
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
      <div id="reset-password-page">
        <div id="reset-password-box">
          <h2>Reset Password</h2>
          {message && <p className="message">{message}</p>}

          {decodedToken ? (
            <form onSubmit={handleSubmit} className="reset-password-form">
              <label htmlFor="emailAddress">Email Address:</label>
              <input
                type="email"
                id="emailAddress"
                value={emailAddress}
                onChange={(e) => setEmailAddress(e.target.value)}
                required
                placeholder="Enter your email"
              />

              <label htmlFor="newPassword">New Password:</label>
              <input
                type="password"
                id="newPassword"
                value={newPassword}
                onChange={(e) => setNewPassword(e.target.value)}
                required
                placeholder="Enter new password"
              />

              <label htmlFor="confirmPassword">Confirm New Password:</label>
              <input
                type="password"
                id="confirmPassword"
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
                required
                placeholder="Confirm new password"
              />

              <button type="submit" disabled={isLoading}>
                {isLoading ? "Resetting..." : "Reset Password"}
              </button>
            </form>
          ) : (
            <p>Please use the reset link sent to your email to reset your password.</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default ResetPasswordPage;