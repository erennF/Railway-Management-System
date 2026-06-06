import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function LoyaltyProgram() {
  const [loyaltyData, setLoyaltyData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const passengerId = localStorage.getItem('userId');
    if (!passengerId) {
      navigate('/');
      return;
    }

    const fetchLoyalty = async () => {
      try {
        const response = await fetch(`http://localhost:5277/api/Passengers/${passengerId}/Loyalty`);
        if (!response.ok) throw new Error('Loyalty information could not be obtained.');
        
        const data = await response.json();
        setLoyaltyData(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchLoyalty();
  }, [navigate]);

  // Auxiliary functions for level calculations and visualization.
  const getLevelDetails = (miles) => {
    if (miles >= 100000) return { title: 'Gold Class', color: 'bg-yellow-500', text: 'text-yellow-600', nextTier: null, discount: '%25 Discount' };
    if (miles >= 50000) return { title: 'Silver Class', color: 'bg-slate-400', text: 'text-slate-600', nextTier: 100000, nextName: 'Gold', discount: '%10 Discount' };
    if (miles >= 10000) return { title: 'Green Class', color: 'bg-green-500', text: 'text-green-600', nextTier: 50000, nextName: 'Silver', discount: '%5 Discount' };
    return { title: 'Beginner (Bronze)', color: 'bg-orange-400', text: 'text-orange-600', nextTier: 10000, nextName: 'Green', discount: 'No Discount' };
  };

  if (loading) return <div className="p-8 text-center text-slate-600 text-xl">Loading...</div>;
  if (error) return <div className="p-8 text-center text-red-600">{error}</div>;

  const levelInfo = getLevelDetails(loyaltyData.totalMiles);
  const progressPercentage = levelInfo.nextTier 
    ? Math.min((loyaltyData.totalMiles / levelInfo.nextTier) * 100, 100) 
    : 100;

  return (
    <div className="min-h-screen bg-slate-100 p-8">
      <div className="max-w-3xl mx-auto">
       
        <div className="flex justify-between items-center mb-8 border-b pb-4">
          <h2 className="text-3xl font-bold text-slate-800 m-0">⭐ Loyalty Program</h2>
          <button 
            onClick={() => navigate('/passenger-dashboard')}
            className="bg-slate-600 hover:bg-slate-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
          >
            ↩ Return to Dashboard
          </button>
        </div>

        <div className="bg-white rounded-xl shadow-lg p-8 border-t-4 border-purple-500">
          <div className="flex flex-col items-center mb-8">
            <div className={`w-24 h-24 rounded-full flex items-center justify-center text-white text-4xl font-bold mb-4 shadow-md ${levelInfo.color}`}>
              {levelInfo.title.charAt(0)}
            </div>
            <h3 className={`text-3xl font-bold ${levelInfo.text}`}>{levelInfo.title}</h3>
            <p className="text-slate-500 mt-2 text-lg">Current Ticket Benefits: <span className="font-bold text-slate-800">{levelInfo.discount}</span></p>
          </div>

          <div className="bg-slate-50 p-6 rounded-lg mb-6 border border-slate-100">
            <div className="flex justify-between items-end mb-2">
              <div>
                <p className="text-sm text-slate-500 font-semibold uppercase tracking-wider">Total Miles</p>
                <p className="text-3xl font-bold text-slate-800">{loyaltyData.totalMiles.toLocaleString()} <span className="text-lg text-slate-500 font-normal">miles</span></p>
              </div>
              {levelInfo.nextTier && (
                <div className="text-right">
                  <p className="text-sm text-slate-500 font-semibold">Target: {levelInfo.nextName}</p>
                  <p className="text-lg font-bold text-slate-700">{levelInfo.nextTier.toLocaleString()} miles</p>
                </div>
              )}
            </div>

          
            <div className="w-full bg-slate-200 rounded-full h-4 mt-4 overflow-hidden">
              <div 
                className={`h-4 rounded-full transition-all duration-1000 ${levelInfo.color}`} 
                style={{ width: `${progressPercentage}%` }}
              ></div>
            </div>
            {levelInfo.nextTier && (
              <p className="text-sm text-slate-500 mt-3 text-center">
                To reach the next level, you need <span className="font-bold text-purple-600">{(levelInfo.nextTier - loyaltyData.totalMiles).toLocaleString()}</span> more miles.
              </p>
            )}
            {!levelInfo.nextTier && (
              <p className="text-sm text-slate-500 mt-3 text-center font-bold text-yellow-600">
                Congratulations! You are at the highest loyalty level.
              </p>
            )}
          </div>
        </div>

      </div>
    </div>
  );
}

export default LoyaltyProgram;