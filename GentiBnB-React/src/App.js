import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Navigationbar from './components/Navigationbar';
import WelcomePage from './pages/WelcomePage';
import './App.css';  
import Registration from './pages/Registration';
import LoginPage from './pages/LoginPage';
import AdminDashboard from './pages/AdminDashboard';
import ForgotPasswordPage from './pages/ForgotPasswordPage';
import ResetPasswordPage from './pages/ResetPasswordPage';
import HotelsApartamentsList from './pages/HotelsApartamentsList';
import ProfilePage from './pages/ProfilePage';
import { useUser } from './UserContext';

function App() {
  const { userDetails } = useUser(); 



  return (
      <div>
        <Navigationbar/>
        <Routes>
          <Route path="/" element={<WelcomePage />} />
          <Route path="/signup" element={<Registration />} />
          <Route path="/login" element={<LoginPage />} />
          {userDetails.role === "Admin" && <Route path="/admin-dashboard" element={<AdminDashboard />} />}
          <Route path="/forgot-password" element={<ForgotPasswordPage />} />
          <Route path="/reset-password" element={<ResetPasswordPage />} />
          <Route path="/hotels-apartaments" element={<HotelsApartamentsList />} />
          <Route path="/profile-page" element={<ProfilePage />} />
        </Routes>
      </div>
  );
}

export default App;
