// UserList.jsx
import React from 'react';
import UserCard from './UserCard';
import './UserList.css';

function UserList({ users, onRemove }) {
  return (
    <div className="user-list">
      {users.map(user => (
        <UserCard key={user.id} user={user} onRemove={onRemove} />
      ))}
    </div>
  );
}

export default UserList;
