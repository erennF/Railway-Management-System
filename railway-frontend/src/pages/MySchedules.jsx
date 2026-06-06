import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function MySchedules() {
  const navigate = useNavigate();
  const staffId = localStorage.getItem('userId');
  const role = localStorage.getItem('userRole');

  const [schedules, setSchedules] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    // Only Driver or Admin can access it.
    if (role !== 'Driver' && role !== 'Admin') {
      navigate('/staff-dashboard');
      return;
    }

    const fetchMySchedules = async () => {
      try {
        const response = await fetch(`http://localhost:5277/api/Staff/Assignments/${staffId}`);
        if (!response.ok) throw new Error('Failed to fetch schedule information.');
        
        const data = await response.json();
        setSchedules(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchMySchedules();
  }, [staffId, role, navigate]);

  return (
    <div className="min-h-screen bg-slate-100 p-8 font-sans">
      <div className="max-w-5xl mx-auto">
        
        <div className="flex justify-between items-center mb-8 border-b border-slate-300 pb-4">
          <div>
            <h2 className="text-3xl font-bold text-slate-800 m-0">🚂 Upcoming Trips</h2>
            <p className="text-slate-500 mt-1">Your active train trips assigned as a driver are listed below.</p>
          </div>
          <button 
            onClick={() => navigate('/staff-dashboard')}
            className="bg-slate-600 hover:bg-slate-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
          >
            ↩ Return to Dashboard
          </button>
        </div>

        {loading && <div className="text-slate-500 font-medium">Fetching your trips...</div>}
        {error && <div className="bg-red-100 text-red-700 p-4 rounded-lg">{error}</div>}

        {!loading && !error && schedules.length === 0 && (
          <div className="text-center py-12 bg-white rounded-xl shadow-sm border border-dashed border-slate-300">
            <span className="text-4xl mb-3 block">🛤️</span>
            <h3 className="text-lg font-bold text-slate-700">No Assigned Trips</h3>
            <p className="text-slate-500">You don't have any scheduled trips at the moment.</p>
          </div>
        )}

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          {schedules.map((schedule, index) => (
            <div key={index} className="bg-white rounded-xl shadow-md p-6 border-l-4 border-blue-600 hover:shadow-lg transition-shadow">
              <div className="flex justify-between items-start mb-4">
                <div>
                  <span className="bg-blue-100 text-blue-800 text-xs font-bold px-2 py-1 rounded uppercase tracking-wider">
                    {schedule.trainName || 'Ekspres Tren'}
                  </span>
                  <h3 className="text-xl font-bold text-slate-800 mt-2">Trip #{schedule.scheduleId}</h3>
                </div>
                <div className="text-right">
                  <p className="text-sm text-slate-500 font-medium">Date</p>
                  <p className="text-lg font-bold text-slate-800">{new Date(schedule.departureDate).toLocaleDateString()}</p>
                </div>
              </div>

              <div className="flex items-center justify-between bg-slate-50 p-4 rounded-lg">
                <div className="text-center w-2/5">
                  <p className="text-xs text-slate-500 uppercase tracking-wider mb-1">Departure</p>
                  <p className="font-bold text-slate-800 truncate">{schedule.sourceStation}</p>
                  <p className="text-sm text-slate-600">{schedule.departureTime}</p>
                </div>
                
                <div className="w-1/5 flex justify-center text-slate-400">
                  ▶▶
                </div>

                <div className="text-center w-2/5">
                  <p className="text-xs text-slate-500 uppercase tracking-wider mb-1">Arrival</p>
                  <p className="font-bold text-slate-800 truncate">{schedule.destinationStation}</p>
                  <p className="text-sm text-slate-600">{schedule.arrivalTime}</p>
                </div>
              </div>
            </div>
          ))}
        </div>

      </div>
    </div>
  );
}

export default MySchedules;