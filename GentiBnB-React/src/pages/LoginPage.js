import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import './LoginPage.css';
import './FormStyles.css';
import { useUser } from '../UserContext';

function LoginPage() {
  
  const [formData, setFormData] = useState({
    username: '',
    password: '',
  });
  const [message, setMessage] = useState('');
  const { userDetails,setUserDetails } = useUser(); 
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(
        'https://localhost:7266/api/Authentication/Login',
        formData,
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
      
      if (response.data.success) {
        localStorage.setItem('jwtToken', response.data.data.token);
        const decodedToken = jwtDecode(response.data.data.token);
        setUserDetails({
          emailVerified: decodedToken.EmailVerified === "True",
          userName: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
          role: decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
          userId: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'], 

        });
        
  
        if (decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === "Admin") {
          navigate('/admin-dashboard');
        } else {
          navigate('/hotels-apartaments');
        }
  
      } else {
        setMessage(response.data.message);
      }
    } catch (error) {
      if (error.response && error.response.data && error.response.data.message) {
        setMessage('Login failed: ' + error.response.data.message);
      } else {
        setMessage('An unexpected error occurred during login.');
      }
    }
  };
  
  

  return (
    <div className="login-container">
      <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="username">Username</label>
          <input type="text" name="username" id="username" onChange={handleChange} />
        </div>
        <div>
          <label htmlFor="password">Password</label>
          <input type="password" name="password" id="password" onChange={handleChange} />
        </div>
        <button type="submit">Login</button>
      </form>
      {message && <p className="message">{message}</p>}
      <a href="/forgot-password">Forgot password?</a>
    </div>
  );
}

export default LoginPage;
