
import React, { useState } from 'react';
import "./StayForm.css"
import { useUser } from '../UserContext';

function StayForm({ onSubmit}) {

  const { userDetails } = useUser();
  const { userId } = userDetails;
  const [formData, setFormData] = useState({
    applicationUserId: userId,
    name: '',
    country: '',
    city: '',
    imageUrl: '',
    maxGuests: ''
  });

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({ ...prevData, [name]: value }));
  };

  const handleSubmitForm = (event) => {
    event.preventDefault();
    onSubmit(formData);
  };

  return (
    <div className="form-overlay"> 
    <div className="stay-form-container"> 

      <form onSubmit={handleSubmitForm}>

          <div>
            <label htmlFor="name">Name</label>
            <input
              type="text"
              name="name"
              id="name"
              value={formData.name}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label htmlFor="country">Country</label>
            <input
              type="text"
              name="country"
              id="country"
              value={formData.country}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label htmlFor="city">City</label>
            <input
              type="text"
              name="city"
              id="city"
              value={formData.city}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label htmlFor="imageUrl">Image URL</label>
            <input
              type="text"
              name="imageUrl"
              id="imageUrl"
              value={formData.imageUrl}
              onChange={handleInputChange}
              required
            />
          </div>
          <div>
            <label htmlFor="maxGuests">Max Guests</label>
            <input
              type="number"
              name="maxGuests"
              id="maxGuests"
              value={formData.maxGuests}
              onChange={handleInputChange}
              required
            />
          </div>
      <button type="submit">Add Stay</button>
    </form>
    
    </div>
    </div>
  );
};

export default StayForm;
