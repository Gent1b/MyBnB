import React from 'react';
import ReactDOM  from 'react-dom/client';
import { BrowserRouter as Router } from 'react-router-dom';
import App from './App'
import './axiosSetup';  // Import the setup file here
import { UserProvider } from './UserContext';


const el = document.getElementById('root');
const root = ReactDOM.createRoot(el);

root.render(
  <UserProvider>
  <Router>
<App />
</Router>
  </UserProvider>

);
