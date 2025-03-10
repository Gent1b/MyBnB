import React, { useState } from 'react';

function Dropdown  ({ cities, selectedCity, onSelectCity, guests, onSetGuests }) {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  const handleCitySelect = (city, event) => {
    event.stopPropagation(); // keeps dropdown open
    onSelectCity(city);
    setIsDropdownOpen(false); 
  };

  return (
    <div className="dropdown-guests">
      <div className={`custom-dropdown ${isDropdownOpen ? 'open' : ''}`} onClick={toggleDropdown}>
        <span>{selectedCity || "Location"}</span>
        <div className={`dropdown-options ${isDropdownOpen ? 'open' : ''}`}>
          {cities.map((city, index) => (
            <div key={index}
                 className={`dropdown-option ${city === selectedCity ? "active" : ""}`}
                 onClick={(e) => handleCitySelect(city, e)}>
              {city}
            </div>
          ))}
        </div>
      </div>
      <div className="guest-input-container">
        <div className="guests-input">
          <button className="guests-button minus-button" onClick={() => onSetGuests(Math.max(0, guests - 1))}>
            -
          </button>
          <input value={guests}
                 onChange={(e) => onSetGuests(Math.max(0, parseInt(e.target.value)))}
                 min="0"
                 className="guests-input-field"/>
          <button className="guests-button plus-button" onClick={() => onSetGuests(guests + 1)}>
            +
          </button>
        </div>
      </div>
    </div>
  );
};

export default Dropdown;
