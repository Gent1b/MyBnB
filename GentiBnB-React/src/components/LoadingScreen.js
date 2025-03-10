import React from 'react';
import { GridLoader } from 'react-spinners';
import './LoadingScreen.css';

function LoadingScreen(){
  return (
    <div className="loading-screen">
      <GridLoader color="#3498db" size={15} />
    </div>
  );
};

export default LoadingScreen;
