import React, { useState, useEffect } from 'react';
import axios from 'axios';
import UserList from '../components/UserList';
import './AdminDashboard.css';

function AdminDashboard() {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    async function fetchUsers() {
      try {
        const response = await axios.get('https://localhost:7266/api/Admin/users');
        setUsers(response.data.data);
      } catch (error) {
        console.error("Error fetching users:", error);
      }
    }

    fetchUsers();
  }, []);

  const handleRemove = async (userId) => {
    try {
      await axios.delete(`https://localhost:7266/api/Admin/delete-user/${userId}`);
      setUsers(prevUsers => prevUsers.filter(user => user.id !== userId));
    } catch (error) {
      console.error("Error deleting user:", error);
    }
  };

  return (
    <div className="admin-dashboard">
      <h2>Admin Dashboard</h2>
      <UserList users={users} onRemove={handleRemove} />
    </div>
  );
}

export default AdminDashboard;
