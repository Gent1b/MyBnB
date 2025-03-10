import React from 'react';
import { Link } from 'react-router-dom';
import './WelcomePage.css';
import Illustration from '../assets/svgs/hotel&app.svg';


function WelcomePage() {
  const isAuthenticated = () => {
    return !!localStorage.getItem('jwtToken');
  };
  return (
    <div className="welcome-container">
      <div className="welcome-content">
        <div className="welcome-illustration">
          <img src={Illustration} alt="Welcome Illustration" />
        </div>
        <div className="welcome-info">
          <div className="welcome-text">
            <h2>Find your next home</h2>
            <p>Explore apartments, meet hosts, and find your perfect match.</p>
          </div>
          {!isAuthenticated() ? (
          <div className="welcome-actions">
            <Link to="/login" className="action-link login-btn">Login</Link>
            <Link to="/signup" className="action-link signup-btn">Sign Up</Link>
          </div>
          ):
          <>
            <Link to="/hotels-apartaments" className="action-link login-btn">Explore Stays</Link>

          </>
          }
        </div>
      </div>
    </div>
  );
}

export default WelcomePage;
