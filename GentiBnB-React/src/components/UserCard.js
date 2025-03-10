// UserCardComponent.jsx
import React from 'react';
import './UserCard.css';

function UserCard({ user, onRemove }) {
  return (
    <div className="user-card">
      <img src={user.profilePictureUrl} alt={`${user.userName}'s profile`} />
      <div className="user-details">
        <p>ID: {user.id}</p>
        <p>Username: {user.userName}</p>
        <p>Full Name: {user.fullName}</p>
        <p>Country: {user.country}</p>
        <button onClick={() => onRemove(user.id)}>Remove</button>
      </div>
    </div>
  );
}

export default UserCard;
