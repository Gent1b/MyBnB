import React, { useState, useEffect, useCallback } from 'react';
import axios from 'axios';
import './HotelsApartamentsList.css';
import Dropdown from '../components/Dropdown'; 
import StayList from '../components/StayList';
import StayForm from '../components/StayForm';
import { useUser } from '../UserContext';

function HotelsApartamentsList() {
  const [stays, setStays] = useState([]);
  const [selectedCity, setSelectedCity] = useState('All Locations');
  const [guests, setGuests] = useState(0);
  const [isOpenForm, setIsOpenForm] = useState(false);
  const [cities, setCities] = useState([]);
  const [userMessage, setUserMessage] = useState(''); 
  const { userDetails } = useUser(); 

  const fetchStaysByCity = useCallback(async (city) => {
    const endpoint = city === 'All Locations' 
      ? 'https://localhost:7266/api/stays' 
      : `https://localhost:7266/api/stays/city/${city}`;
    try {
      const response = await axios.get(endpoint);
      setStays(response.data.data);
    } catch (error) {
      console.error('Error fetching stays:', error);
    }
  }, []);

  const fetchCities = useCallback(async () => {
    try {
      const response = await axios.get('https://localhost:7266/api/stays/cities');
      setCities(response.data.data);
    } catch (error) {
      console.error('Error fetching cities:', error);
    }
  }, []);

  useEffect(() => {
    fetchCities();
    fetchStaysByCity(selectedCity);
  }, [fetchCities, fetchStaysByCity, selectedCity]);

  const filteredStays = stays.filter(stay => stay.maxGuests >= guests);

  const toggleModal = () => {
 
    if (userDetails && userDetails.emailVerified) {
      setIsOpenForm(curr => !curr);
      setUserMessage(''); 
    } else {
      setUserMessage('Please verify your email to do this.');
    }
  };
const handleSubmit = async (stayData) => {
  try {
    const response = await axios.post('https://localhost:7266/api/stays', stayData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    if (response.data.success) {
      console.log("Stay created successfully:", response.data.data);
      setIsOpenForm(false); 
     
    } else {
      console.error("Error creating stay:", response.data.message);
    }
  } catch (error) {
    console.error("Error creating stay:", error);
  }
};


  return (
    <div className="hotels-page">
      {!isOpenForm ? (
        <>
          {userMessage && <p className="message">{userMessage}</p>}

          <div className="choose-location">
            <button className="add-hotel-button" onClick={toggleModal}>
              Add Venue
            </button>
            <Dropdown 
              cities={['All Locations', ...cities]} 
              selectedCity={selectedCity} 
              onSelectCity={setSelectedCity} 
              guests={guests} 
              onSetGuests={setGuests}
            />
          </div>
          <StayList stays={filteredStays} />
        </>
      ) : (
        <StayForm onSubmit={handleSubmit} onClose={toggleModal} />
      )}
    </div>
  );
    
}

export default HotelsApartamentsList;
