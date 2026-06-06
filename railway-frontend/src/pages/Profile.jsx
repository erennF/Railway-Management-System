import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function Profile() {
  const navigate = useNavigate();
  const passengerId = localStorage.getItem('userId');

  const [passengerData, setPassengerData] = useState({ name: '', email: '' });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    if (!passengerId) {
      navigate('/');
      return;
    }

    const fetchProfile = async () => {
      try {
        const response = await fetch(`http://localhost:5277/api/Passengers/${passengerId}`);
        if (!response.ok) throw new Error('Profil bilgileri alınamadı.');
        
        const data = await response.json();
        
        setPassengerData({ 
          name: data.name || data.Name || 'Bilinmiyor', 
          email: data.email || data.Email || 'Bilinmiyor' 
        });

      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchProfile();
  }, [passengerId, navigate]);

  if (loading) return <div className="min-h-screen bg-slate-100 flex items-center justify-center text-xl text-slate-600">Loading...</div>;

  return (
    <div className="min-h-screen bg-slate-100 p-8">
      <div className="max-w-2xl mx-auto">
        
       
        <div className="flex justify-between items-center mb-8 border-b border-slate-300 pb-4">
          <h2 className="text-3xl font-bold text-slate-800 m-0">👤 Profile Information</h2>
          <button 
            onClick={() => navigate('/passenger-dashboard')}
            className="bg-slate-600 hover:bg-slate-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
          >
            ↩ Back to Dashboard
          </button>
        </div>

        {error && <div className="bg-red-100 text-red-700 p-4 rounded-lg mb-6 border border-red-200">{error}</div>}

        <div className="bg-white p-8 rounded-xl shadow-lg border-t-4 border-orange-500">
          <div className="flex flex-col space-y-6">
            
            <div>
              <label className="block text-sm font-bold text-slate-500 mb-2 uppercase tracking-wider">Passenger ID</label>
              <div className="w-full px-4 py-3 rounded-lg border border-slate-200 bg-slate-50 text-slate-800 font-semibold text-lg">
                #{passengerId}
              </div>
            </div>

       
            <div>
              <label className="block text-sm font-bold text-slate-500 mb-2 uppercase tracking-wider">Name</label>
              <div className="w-full px-4 py-3 rounded-lg border border-slate-200 bg-slate-50 text-slate-800 font-semibold text-lg">
                {passengerData.name}
              </div>
            </div>

            
            <div>
              <label className="block text-sm font-bold text-slate-500 mb-2 uppercase tracking-wider">Email</label>
              <div className="w-full px-4 py-3 rounded-lg border border-slate-200 bg-slate-50 text-slate-800 font-semibold text-lg">
                {passengerData.email}
              </div>
            </div>

          </div>
        </div>
      </div>
    </div>
  );
}

export default Profile;