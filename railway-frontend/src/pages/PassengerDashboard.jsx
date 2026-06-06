import { useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';

function PassengerDashboard() {
  const navigate = useNavigate();
  const [passengerId, setPassengerId] = useState(null);

  useEffect(() => {
    const id = localStorage.getItem('userId');
    if (!id) {
      navigate('/');
    } else {
      setPassengerId(id);
    }
  }, [navigate]);

  const handleLogout = () => {
    localStorage.removeItem('userId');
    localStorage.removeItem('userRole');
    navigate('/');
  };

  return (
    <div className="min-h-screen bg-slate-100">
      <nav className="bg-slate-800 text-white p-4 shadow-lg">
        <div className="max-w-6xl mx-auto flex justify-between items-center">
          <h1 className="text-2xl font-bold">Railway Portal</h1>
          <button
            onClick={handleLogout}
            className="bg-red-600 hover:bg-red-700 px-4 py-2 rounded-lg font-medium transition-colors"
          >
            Log Out
          </button>
        </div>
      </nav>

      <div className="max-w-6xl mx-auto p-6">
        <div className="bg-white rounded-lg shadow-lg p-8">
          <h2 className="text-3xl font-bold text-slate-800 mb-2">Welcome, Passenger!</h2>
          <p className="text-slate-600 text-lg mb-6">Passenger ID: {passengerId}</p>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
           
            <div className="bg-gradient-to-br from-blue-50 to-blue-100 rounded-lg p-6 border border-blue-200 hover:shadow-lg transition-shadow cursor-pointer">
                <h3 className="text-xl font-bold text-blue-900 mb-2">🎟️ Make a Reservation</h3>
                <p className="text-blue-700 mb-4">Book your train ticket with ease</p>
              
              <button 
                onClick={() => navigate('/search')} 
                className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
              >
                Search Tickets
              </button>
            </div>

        
            <div className="bg-gradient-to-br from-green-50 to-green-100 rounded-lg p-6 border border-green-200 hover:shadow-lg transition-shadow cursor-pointer">
              <h3 className="text-xl font-bold text-green-900 mb-2">📋 My Reservations</h3>
              <p className="text-green-700 mb-4">Your current and past reservations</p>
              
            
              <button 
                onClick={() => navigate('/my-reservations')}
                className="bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
              >
                View Reservations
              </button>
            </div>

            <div className="bg-gradient-to-br from-purple-50 to-purple-100 rounded-lg p-6 border border-purple-200 hover:shadow-lg transition-shadow cursor-pointer">
              <h3 className="text-xl font-bold text-purple-900 mb-2">⭐ Loyalty Program</h3>
              <p className="text-purple-700 mb-4">Your points and level information</p>
              
            
              <button 
                onClick={() => navigate('/loyalty-program')}
                className="bg-purple-600 hover:bg-purple-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
              >
                Details
              </button>
            </div>

          
            <div className="bg-gradient-to-br from-orange-50 to-orange-100 rounded-lg p-6 border border-orange-200 hover:shadow-lg transition-shadow cursor-pointer">
              <h3 className="text-xl font-bold text-orange-900 mb-2">👤 Profile</h3>
              <p className="text-orange-700 mb-4">Your personal information and settings</p>
              <button 
                onClick={() => navigate('/profile')}
                className="bg-orange-600 hover:bg-orange-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
              >
                Edit
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default PassengerDashboard;
