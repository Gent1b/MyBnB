import React from 'react';

function StayList({ stays })  
{
  return(
  <div className="hotels-list-container">
    {stays.map((stay, index) => (
      <div className="property-container" key={index}>
        <div className="property-photo">
          <img src={stay.imageUrl} alt={`Property ${index}`} />
        </div>
        <h3 className="property-name">{stay.name}</h3>
        <p className="property-details">City: {stay.city}</p>
        <div className="property-bottom">
          <p className="property-details">Country: {stay.country}</p>
          <p className="see-more">See More</p>
        </div>
      </div>
    ))}
  </div>
  );
};

export default StayList;
