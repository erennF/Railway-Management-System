import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

function OperationsDashboard() {
  const navigate = useNavigate();
  const [operatorId, setOperatorId] = useState(null);

  useEffect(() => {
    const id = localStorage.getItem('userId');
    const role = localStorage.getItem('userRole');

    if (!id || role !== 'Operations') {
      navigate('/');
    } else {
      setOperatorId(id);
    }
  }, [navigate]);

  const handleLogout = () => {
    localStorage.removeItem('userId');
    localStorage.removeItem('userRole');
    navigate('/');
  };

  return (
    <div className="min-h-screen bg-slate-900 text-slate-100 font-sans">
    
      <nav className="bg-slate-950 p-4 border-b border-slate-800 shadow-md flex justify-between items-center">
        <div className="flex items-center gap-2">
          <span className="text-2xl">⚙️</span>
          <h1 className="text-xl font-bold tracking-wide">Railway Operations Portal</h1>
        </div>
        <button 
          onClick={handleLogout}
          className="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
        >
        Log Out
        </button>
      </nav>

      
      <div className="max-w-6xl mx-auto p-8">
        <div className="bg-slate-800 rounded-xl p-8 border border-slate-700 shadow-xl mb-8">
          <h2 className="text-3xl font-extrabold text-white mb-2">System and Operations Management</h2>
          <p className="text-slate-400">Operator ID: <span className="font-mono text-cyan-400">#{operatorId}</span></p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          
        
          <div className="bg-slate-800 border border-slate-700 rounded-xl p-6 hover:border-cyan-500 transition-all shadow-md">
            <div className="text-3xl mb-3">🛤️</div>
            <h3 className="text-xl font-bold text-white mb-2">Track & Infrastructure Management</h3>
            <p className="text-slate-400 text-sm mb-4">Condition of tracks, signaling systems, and station infrastructure on the Saudi-Turkey route.</p>
            <button className="w-full bg-cyan-600 hover:bg-cyan-700 text-white py-2 rounded-lg font-medium transition-colors">
              Inspect Infrastructure
            </button>
          </div>

         
          <div className="bg-slate-800 border border-slate-700 rounded-xl p-6 hover:border-emerald-500 transition-all shadow-md">
            <div className="text-3xl mb-3">📅</div>
            <h3 className="text-xl font-bold text-white mb-2">Schedules</h3>
            <p className="text-slate-400 text-sm mb-4">Management of international train schedules, handling delays, and creating new timetables.</p>
            <button className="w-full bg-emerald-600 hover:bg-emerald-700 text-white py-2 rounded-lg font-medium transition-colors">
             Timetables
            </button>
          </div>

          <div className="bg-slate-800 border border-slate-700 rounded-xl p-6 hover:border-amber-500 transition-all shadow-md">
            <div className="text-3xl mb-3">📦</div>
            <h3 className="text-xl font-bold text-white mb-2">Cargo & Logistics</h3>
            <p className="text-slate-400 text-sm mb-4">Management of international cargo trains, including weight checks, cargo types, and border crossing logistics planning.</p>
            <button className="w-full bg-amber-600 hover:bg-amber-700 text-white py-2 rounded-lg font-medium transition-colors">
              Logistics
            </button>
          </div>

        </div>
      </div>
    </div>
  );
}

export default OperationsDashboard;