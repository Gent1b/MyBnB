import React, { useState, useEffect } from 'react';
import axios from 'axios';
import defaultProfilePic from '../assets/images/gentibnblogo.png';
import { useUser } from '../UserContext';
import './ProfilePage.css';

function ProfilePage()  {
  const [user, setUser] = useState(null);
  const [editMode, setEditMode] = useState(false);
  const [editedUser, setEditedUser] = useState(null);
  const { userDetails } = useUser();
  const { userName } = userDetails;

  useEffect(() => {
    if (userName) {
      axios.get(`https://localhost:7266/api/users/by-username/${userName}`)
        .then(response => {
          setUser(response.data.data);
          setEditedUser(response.data.data); 
        })
        .catch(error => {
          console.error('Error fetching user details:', error);
        });
    }
  }, [userName]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setEditedUser({ ...editedUser, [name]: value });
  };

  const handleEditToggle = () => {
    setEditMode(prev => !prev);
    if (editMode) {
 
      setEditedUser(user);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (user && editedUser) {
      try {
        const response = await axios.put(`https://localhost:7266/api/users/${user.id}`, editedUser);
        setUser(response.data.data);
        setEditMode(false); 
      } catch (error) {
        console.error('Error updating user:', error);
      }
    }
  };

  if (!user) return <div>Loading...</div>;

  return (
    <div className="profile-container">
      {!editMode ? (
        <>
          <h1 className="profile-heading">Profile</h1>
          <img
            src={user.profilePictureUrl || defaultProfilePic}
            alt="Profile"
            className="profile-picture"
          />
          <div className="profile-details">
            <p className="detail-item"><strong>Username:</strong> {user.userName}</p>
            <p className="detail-item"><strong>Email:</strong> {user.email}</p>
            <p className="detail-item"><strong>Full Name:</strong> {user.fullName}</p>
            <p className="detail-item"><strong>Country:</strong> {user.country}</p>
          </div>
          <button onClick={handleEditToggle} className="edit-button">Edit Profile</button>
        </>
      ) : (
        <form className="edit-form" onSubmit={handleSubmit}>
          <div className="form-field">
            <label htmlFor="fullName" className="form-label">Full Name</label>
            <input 
              type="text"
              name="fullName"
              id="fullName"
              value={editedUser.fullName}
              onChange={handleInputChange}
              className="form-input"
            />
               <label htmlFor="profilePictureUrl" className="form-label">Profile PictureUrl</label>
            <input 
              type="text"
              name="profilePictureUrl"
              id="profilePictureUrl"
              value={editedUser.profilePictureUrl}
              onChange={handleInputChange}
              className="form-input"
            />

          </div>
          
          <button type="submit" className="save-button">Save Changes</button>
          <button type="button" onClick={handleEditToggle} className="cancel-button">Cancel</button>
        </form>
      )}
    </div>
  );
};

export default ProfilePage;
