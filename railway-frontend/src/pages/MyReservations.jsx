import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function MyReservations() {
  const [reservations, setReservations] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
  
    const passengerId = localStorage.getItem('userId');
    
    if (!passengerId) {
      navigate('/');
      return;
    }

    const fetchReservations = async () => {
      try {
        const response = await fetch(`http://localhost:5277/api/Reports/PassengerReservations/${passengerId}`);
        
        if (!response.ok) {
          if (response.status === 404) {
            setReservations([]); 
            return;
          }
          throw new Error('An error occurred while loading the reservations..');
        }
        
        const data = await response.json();
        setReservations(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchReservations();
  }, [navigate]);

  return (
    <div className="min-h-screen bg-slate-100 p-8">
      <div className="max-w-4xl mx-auto">
        <div className="flex justify-between items-center mb-8">
          <h2 className="text-3xl font-bold text-slate-800">📋 My Reservations</h2>
          <button 
            onClick={() => navigate('/passenger-dashboard')}
            className="bg-slate-600 hover:bg-slate-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
          >
            ↩ Return to Dashboard
          </button>
        </div>

        {loading && <p className="text-lg text-slate-600">Loading...</p>}
        {error && <div className="bg-red-100 text-red-700 p-4 rounded-lg mb-6">{error}</div>}

        {!loading && !error && reservations.length === 0 && (
          <div className="bg-white p-8 rounded-lg shadow text-center">
            <p className="text-xl text-slate-600 mb-4">You don't have any reservations yet.</p>
            <button 
              onClick={() => navigate('/search')}
              className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded-lg font-medium"
            >
              Buy Tickets Now
            </button>
          </div>
        )}

        <div className="grid gap-6">
          {reservations.map((res) => (
            <div key={res.reservationId} className="bg-white rounded-lg shadow-md p-6 border-l-4 border-blue-500">
              <div className="flex justify-between items-start border-b pb-4 mb-4">
                <div>
                  <h3 className="text-xl font-bold text-blue-900">{res.trainName}</h3>
                  <p className="text-slate-600">{res.routeName}</p>
                </div>
                <div className="text-right">
                  <span className={`px-3 py-1 rounded-full text-sm font-bold ${
                    res.status === 'Paid' ? 'bg-green-100 text-green-700' : 
                    res.status === 'Pending' ? 'bg-yellow-100 text-yellow-700' : 
                    'bg-red-100 text-red-700'
                  }`}>
                    {res.status === 'Paid' ? 'Approved (Paid)' : 
                     res.status === 'Pending' ? 'Pending' : 'Cancelled'}
                  </span>
                </div>
              </div>
              
              <div className="grid grid-cols-2 md:grid-cols-4 gap-4 text-sm">
                <div>
                  <p className="text-slate-500 font-semibold">Date</p>
                  <p className="text-slate-800">{res.travelDate}</p>
                </div>
                <div>
                  <p className="text-slate-500 font-semibold">Reservation ID</p>
                  <p className="text-slate-800">#{res.reservationId}</p>
                </div>
                <div>
                  <p className="text-slate-500 font-semibold">Seats</p>
                  <p className="text-slate-800">
                    {res.seats.length > 0 
                      ? res.seats.map(s => `${s.seatNumber} (${s.coachType})`).join(', ') 
                      : 'No seats selected'}
                  </p>
                </div>
                <div>
                  <p className="text-slate-500 font-semibold">Total Luggage</p>
                  <p className="text-slate-800">{res.totalLuggageWeight} kg</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default MyReservations;