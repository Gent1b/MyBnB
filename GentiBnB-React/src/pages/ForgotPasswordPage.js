
import React, { useState } from 'react';
import axios from 'axios';
import "./FormStyles.css"
import "./ForgotPassword.css"
function ForgotPasswordPage() {
  const [email, setEmail] = useState('');
  const [message, setMessage] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(`https://localhost:7266/api/Authentication/forgot-password?email=${email}`);
      setMessage(response.data.message);
    } catch (error) {
      console.log(error);
      setMessage('Failed to send reset password email. Please try again.');
    }
  };
  

  return (
    <div className="forgot-password-container">
      <h2>Forgot Password</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Email:</label>
          <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
        </div>
        <button type="submit">Send Reset Password Email</button>
      </form>
      {message && <p>{message}</p>}
    </div>
  );
}

export default ForgotPasswordPage;
