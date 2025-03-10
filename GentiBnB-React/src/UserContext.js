import React, { createContext, useContext, useState, useEffect } from 'react';
import {jwtDecode} from 'jwt-decode'; // Make sure the import is correct

const UserContext = createContext(null);

export const useUser = () => useContext(UserContext);

export const UserProvider = ({ children }) => {
  const [userDetails, setUserDetails] = useState({
    emailVerified: false,
    userName: '',
    userId:'',
    role: '',
  });

  const updateUserDetailsFromToken = (token) => {
    if (token) {
      try {
        const decoded = jwtDecode(token);
        setUserDetails({
          emailVerified: decoded.EmailVerified === "True",
          userName: decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
          role: decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
          userId: decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'], 

        });
      } catch (error) {
        console.error('Error decoding JWT', error);
        localStorage.removeItem('jwtToken'); // Remove invalid token
        setUserDetails({
          emailVerified: false,
          userName: '',
          role: '',
        });
      }
    } else {
      setUserDetails({
        emailVerified: false,
        userName: '',
        role: '',
      });
    }
  };

  useEffect(() => {
    const token = localStorage.getItem('jwtToken');
    updateUserDetailsFromToken(token);

    const handleStorageChange = (e) => {
      if (e.key === 'jwtToken') {
        updateUserDetailsFromToken(e.newValue);
      }
    };
    window.addEventListener('storage', handleStorageChange);

    return () => {
      window.removeEventListener('storage', handleStorageChange);
    };
  }, []);

  return (
    <UserContext.Provider value={{ userDetails, setUserDetails }}>
      {children}
    </UserContext.Provider>
  );
};
