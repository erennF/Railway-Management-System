import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

function TicketSearch() {
  const [fromStation, setFromStation] = useState('');
  const [toStation, setToStation] = useState('');
  const [date, setDate] = useState('');
  const [results, setResults] = useState([]);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  
  const navigate = useNavigate();

  const stations = [
    { id: 1, name: 'Istanbul Haydarpasa (Turkiye)' },
    { id: 2, name: 'Gaziantep Border (Turkiye)' },
    { id: 3, name: 'Aleppo Central (Syria)' },
    { id: 4, name: 'Damascus Station (Syria)' },
    { id: 6, name: 'Amman Passenger (Jordan)' },
    { id: 7, name: 'Riyadh Main Station (Saudi Arabia)' }
  ];

  const handleSearch = async (e) => {
    e.preventDefault();
    setError('');
    setResults([]);
    setLoading(true);

    try {
  
      const response = await fetch(`http://localhost:5277/api/Booking/Search?fromStation=${fromStation}&toStation=${toStation}&date=${date}`);
      
      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText || "An error occurred during the search..");
      }

      const data = await response.json();
      setResults(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleBookClick = (scheduleId) => {

    navigate(`/booking-details/${scheduleId}`);
  };

  return (
   <div className="max-w-4xl mx-auto mt-10 p-6 font-sans">
     
      <div className="flex justify-between items-center mb-8 border-b pb-4">
        <h2 className="text-3xl font-bold text-slate-800 m-0">🚄 Train Schedule Search</h2>
        
        <button 
          onClick={() => navigate('/passenger-dashboard')}
          className="bg-slate-600 hover:bg-slate-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
        >
          ↩ Back to Dashboard
        </button>
      </div>
      <form onSubmit={handleSearch} style={{ display: 'flex', gap: '15px', marginBottom: '30px', flexWrap: 'wrap' }}>
        <select 
          value={fromStation} 
          onChange={(e) => setFromStation(e.target.value)} 
          required
          style={{ padding: '10px', borderRadius: '5px', flex: 1 }}
        >
          <option value="">Departure Station</option>
          {stations.map(s => <option key={s.id} value={s.id}>{s.name}</option>)}
        </select>

        <select 
          value={toStation} 
          onChange={(e) => setToStation(e.target.value)} 
          required
          style={{ padding: '10px', borderRadius: '5px', flex: 1 }}
        >
          <option value="">Arrival Station</option>
          {stations.map(s => <option key={s.id} value={s.id}>{s.name}</option>)}
        </select>

        <input 
          type="date" 
          value={date} 
          onChange={(e) => setDate(e.target.value)} 
          required
          style={{ padding: '10px', borderRadius: '5px' }}
        />

        <button type="submit" disabled={loading} style={{ padding: '10px 20px', backgroundColor: '#007BFF', color: 'white', border: 'none', borderRadius: '5px', cursor: 'pointer' }}>
          {loading ? 'Searching...' : 'Find Schedules'}
        </button>
      </form>

      {error && <div style={{ color: 'red', padding: '10px', backgroundColor: '#fee', borderRadius: '5px' }}>{error}</div>}

      {/* Search Results */}
      {results.length > 0 && (
        <div>
          <h3>Available Schedules</h3>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '15px' }}>
            {results.map((train) => (
              <div key={train.scheduleId} style={{ border: '1px solid #ddd', padding: '15px', borderRadius: '8px', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                  <h4 style={{ margin: '0 0 5px 0', color: '#333' }}>{train.trainName}</h4>
                  <p style={{ margin: '0', color: '#666', fontSize: '14px' }}>Rota: {train.routeName}</p>
                  <p style={{ margin: '5px 0 0 0', fontWeight: 'bold' }}>Kalkış: {train.departureTime} | Tarih: {train.availableDate}</p>
                </div>
                <button 
                  onClick={() => handleBookClick(train.scheduleId)}
                  style={{ padding: '10px 15px', backgroundColor: '#28A745', color: 'white', border: 'none', borderRadius: '5px', cursor: 'pointer' }}
                >
                  Buy Tickets
                </button>
              </div>
            ))}
          </div>
        </div>
      )}
    </div>
  );
}

export default TicketSearch;