import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate} from 'react-router-dom';
import './Registration.css';
import './FormStyles.css';


function Registration() {
  const [formData, setFormData] = useState({
    userName: '',
    email: '',
    password: '',
    fullName: '',
    country: '',
    profilePictureUrl: '',
    role: 'User', // Default role
  });
  const [message, setMessage] = useState('');
  const [showModal, setShowModal] = useState(false);
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
        `https://localhost:7266/api/Authentication?role=${formData.role}`, 
        formData,
        {
          headers: {
            'Content-Type': 'application/json'
          }
        }
      );
  
      if (response.data.success) {
        setMessage(response.data.message);
        setShowModal(true);
        setTimeout(() => {
          navigate('/login'); 
        }, 3000); 
      } else {
        switch(response.data.statusCode) {
          case 500:
            setMessage(response.data.error); 
            break;
          case 409:
            setMessage(response.data.message); 
            break;
          default:
            setMessage(response.data.message); 
            break;
        }
      }
    } catch (error) {
      console.log(error.response.data);  
  
      if (error.response && error.response.data && error.response.data.error) {
        setMessage('An error occurred: ' + error.response.data.error);
      } else {
        setMessage('An unexpected error occurred.');
      }
    }
  };
  


  return (
    <div className="register-container">
      <h2>Registration</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="userName">Username</label>
          <input type="text" name="userName" id="userName" onChange={handleChange} />
        </div>
        <div>
          <label htmlFor="email">Email</label>
          <input type="email" name="email" id="email" onChange={handleChange} />
        </div>
        <div>
          <label htmlFor="password">Password</label>
          <input type="password" name="password" id="password" onChange={handleChange} />
        </div>
        <div>
          <label htmlFor="fullName">Full Name</label>
          <input type="text" name="fullName" id="fullName" onChange={handleChange} />
        </div>
        <div>
          <label htmlFor="country">Country</label>
          <input type="text" name="country" id="country" onChange={handleChange} />
        </div>
        <div>
          <label htmlFor="profilePictureUrl">Profile Picture URL</label>
          <input type="text" name="profilePictureUrl" id="profilePictureUrl" onChange={handleChange} />
        </div>
        <div>
          <label htmlFor="role">Role</label>
          <select name="role" id="role" onChange={handleChange}>
            <option value="User">User</option>
            <option value="Admin">Admin</option>
            <option value="HR">HR</option>
          </select>
        </div>
        <button type="submit">Register</button>
      </form>
      {message && <p className="message">{message}</p>}
      {showModal && (
        <div className="modal">
          <div className="modal-content">
            <span className="close-button" onClick={() => setShowModal(false)}>&times;</span>
            <h2>Success!</h2>
            <p>User Created Successfully! Redirecting to login...</p>
          </div>
        </div>
      )}
    </div>
  );
}

export default Registration;
