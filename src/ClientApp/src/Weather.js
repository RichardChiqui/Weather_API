
import ReactDOM from 'react-dom/client';

import React, { useState } from 'react';
import './custom.css';

export default function Weather() {

    const [Latitude, setLatitude] = useState('');
    const [Longitude, setLongitude] = useState('');
    const [temp, setTemp] = useState('');
    const [showTemp, setShowTemp] = useState(false)

    const handleSubmit = (event) => {
        event.preventDefault();
        console.log('Latitude:', Latitude);
        console.log('Longitude:', Longitude);
        console.log("test api: " + `/weather?latitude=${Latitude}&longitude=${Longitude}`);
        
        fetch(`/weather?latitude=${Latitude}&longitude=${Longitude}`, {
            method: 'GET'
        })
            .then(response => response.text())
            .then(data => {
                console.log('Response:', data);
                const jsonRes = JSON.parse(data);
                console.log("current weather: " + jsonRes.current_weather.temperature)
                setTemp(jsonRes.current_weather.temperature)
                setShowTemp(true)
            })
            .catch(error => {
                console.error('Error:', error);
                setShowTemp(false)
                // Handle any errors here
            });
    }
    return (


        <form className='weather-section' onSubmit={handleSubmit}>

            <div className="input-params">
                 <div className='weather-textboxes'>
                       <div class="input-groups">
                             <label for="Latitude">Latitude:</label>
                             <input type="text" id="Latitude" onChange={(event) => setLatitude(event.target.value)} name="Latitude"></input>
                      </div>
                      <div class="input-groups">
                            <label for="Longitude">Longitude:</label>
                           <input type="text" id="Longitude" onChange={(event) => setLongitude(event.target.value)} name="Longitude"></input>
                      </div>

                </div>
                <button className='get-temp-btn' type="submit">Go</button>
            </div>
           
          
            <div>
             {showTemp &&  <h2>
                    The current tempature is: {temp} F
                        </h2>}
            </div>
           
            
            

        </form>







    )
}