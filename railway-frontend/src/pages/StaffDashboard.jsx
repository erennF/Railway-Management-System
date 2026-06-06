import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

function StaffDashboard() {
  const navigate = useNavigate();
  const [staffId, setStaffId] = useState(null);
  const [role, setRole] = useState('');

  useEffect(() => {
    const id = localStorage.getItem('userId');
    const userRole = localStorage.getItem('userRole'); 

    if (!id || !userRole || userRole === 'Passenger') {
      navigate('/'); 
    } else {
      setStaffId(id);
      setRole(userRole);
    }
  }, [navigate]);

  const handleLogout = () => {
    localStorage.removeItem('userId');
    localStorage.removeItem('userRole');
    navigate('/');
  };

  return (
    <div className="min-h-screen bg-slate-50 font-sans">
      <nav className="bg-slate-900 text-white p-4 shadow-lg">
        <div className="max-w-6xl mx-auto flex justify-between items-center">
          <div className="flex items-center gap-2">
            <span className="text-2xl">🛡️</span>
            <h1 className="text-2xl font-bold">Railway Staff Portal</h1>
          </div>
          <div className="flex items-center gap-4">
            <span className="bg-slate-700 px-3 py-1 rounded-full text-sm font-medium text-slate-200">
              Role: {role}
            </span>
            <button
              onClick={handleLogout}
              className="bg-red-600 hover:bg-red-700 px-4 py-2 rounded-lg font-medium transition-colors"
            >
              Logout
            </button>
          </div>
        </div>
      </nav>

      <div className="max-w-6xl mx-auto p-6 mt-6">
        <div className="bg-white rounded-xl shadow-lg p-8 border-t-4 border-slate-800 mb-8">
          <h2 className="text-3xl font-bold text-slate-800 mb-2">Welcome to the Work Panel</h2>
          <p className="text-slate-600 text-lg">
            Staff ID: <span className="font-bold text-slate-900">#{staffId}</span> | 
            Role: <span className="font-bold text-blue-600 ml-1">{role}</span>
          </p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          
          {(role === 'Station Manager' || role === 'Admin') && (
            <>
              <div className="bg-gradient-to-br from-indigo-50 to-indigo-100 rounded-lg p-6 border border-indigo-200 hover:shadow-xl transition-all">
                <h3 className="text-xl font-bold text-indigo-900 mb-2">📊 System Reports</h3>
                <p className="text-indigo-700 mb-4">Active trains, occupancy rates, waiting lists, and statistics.</p>
                <button 
                  onClick={() => navigate('/reports')}
                  className="w-full bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
                >
                  View Reports
                </button>
              </div>

              <div className="bg-gradient-to-br from-teal-50 to-teal-100 rounded-lg p-6 border border-teal-200 hover:shadow-xl transition-all">
                <h3 className="text-xl font-bold text-teal-900 mb-2">👥 Staff Assignments</h3>
                <p className="text-teal-700 mb-4">Assign drivers, conductors, or security personnel to train schedules.</p>
                <button 
                  onClick={() => navigate('/staff-assignment')}
                  className="w-full bg-teal-600 hover:bg-teal-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
                >
                  Assign Staff
                </button>
              </div>
              <div className="bg-gradient-to-br from-blue-50 to-blue-100 rounded-lg p-6 border border-blue-200 hover:shadow-xl transition-all">
                <h3 className="text-xl font-bold text-blue-900 mb-2">🗃️ Data Management</h3>
                <p className="text-blue-700 mb-4">Add new trains to the system, define staff members, and plan new schedules.</p>
                <button 
                  onClick={() => navigate('/data-management')}
                  className="w-full bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
                >
                  Data Management Panel
                </button>
              </div>
            </>
            
          )}

          {(role === 'Maintenance Technician' || role === 'Admin') && (
            <div className="bg-gradient-to-br from-red-50 to-red-100 rounded-lg p-6 border border-red-200 hover:shadow-xl transition-all">
              <h3 className="text-xl font-bold text-red-900 mb-2">🛠️ Maintenance Management</h3>
              <p className="text-red-700 mb-4">Create train fault records, input sensor data, and list past maintenance activities.</p>
              <button 
                onClick={() => navigate('/maintenance')}
                className="w-full bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
              >
                Maintenance Panel
              </button>
            </div>
          )}

          {(role === 'Customs Coordinator' || role === 'Admin') && (
            <div className="bg-gradient-to-br from-amber-50 to-amber-100 rounded-lg p-6 border border-amber-200 hover:shadow-xl transition-all">
              <h3 className="text-xl font-bold text-amber-900 mb-2">📦 Customs Operations</h3>
              <p className="text-amber-700 mb-4">Manage cargo transit through border checkpoints and customs approvals.</p>
              <button 
                onClick={() => navigate('/customs')}
                className="w-full bg-amber-600 hover:bg-amber-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
              >
                Customs Panel
              </button>
            </div>
          )}
          {(role === 'Driver') && (
            <div className="bg-gradient-to-br from-blue-50 to-blue-100 rounded-lg p-6 border border-blue-200 hover:shadow-xl transition-all">
              <h3 className="text-xl font-bold text-blue-900 mb-2">🚂 My Schedules</h3>
              <p className="text-blue-700 mb-4">View your upcoming shifts and train routes.</p>
              <button 
                onClick={() => navigate('/my-schedules')}
                className="w-full bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
              >
                View Schedules
              </button>
            </div>
          )}

        </div>
      </div>
    </div>
  );
}

export default StaffDashboard;