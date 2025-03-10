import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './Navbar.css';
import Logo from '../assets/images/mylogo2.png';
import { useUser } from '../UserContext';

function Navigationbar() {
  const navigate = useNavigate();
  const { userDetails } = useUser(); 

  const isAuthenticated = () => {
    return !!localStorage.getItem('jwtToken');
  };

  const logout = () => {
    localStorage.removeItem('jwtToken');
    navigate("/login");  
  };

  return (
    <div className="navbar">
      <div className="logo-container">
        <img src={Logo} alt="Your Logo" className="logo"/>
      </div>
      <div className="nav-links">
        <Link to="/" className="nav-link">Home</Link>
        {!isAuthenticated() && <Link to="/login" className="nav-link">Login</Link>}
        {!isAuthenticated() && <Link to="/signup" className="nav-link">Sign Up</Link>}
        {isAuthenticated() && <Link to="/hotels-apartaments" className="nav-link">Stays</Link>}
        {isAuthenticated() && <Link to="/profile-page" className="nav-link">Profile</Link>}
        {userDetails.role === "Admin"&&isAuthenticated() && <Link to = "/admin-dashboard" className="nav-link">Dashboard</Link>}
        {isAuthenticated() && <button onClick={logout} className="logout-btn">Logout</button>}

        
      </div>
    </div>
  );
}

export default Navigationbar;
