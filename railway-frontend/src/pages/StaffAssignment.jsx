import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function StaffAssignment() {
  const navigate = useNavigate();
  const role = localStorage.getItem('userRole');

  const [staffList, setStaffList] = useState([]);
  const [schedules, setSchedules] = useState([]);
  const [formData, setFormData] = useState({ staffId: '', scheduleId: '', date: '' });
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');
  const [error, setError] = useState('');

  useEffect(() => {
    if (role !== 'Station Manager' && role !== 'Admin') {
      navigate('/staff-dashboard');
      return;
    }

    const fetchDropdownData = async () => {
      try {
        const staffRes = await fetch('http://localhost:5277/api/Staff/Dropdown/Staffs');
        const scheduleRes = await fetch('http://localhost:5277/api/Staff/Dropdown/Schedules');
        
        if (staffRes.ok) setStaffList(await staffRes.json());
        if (scheduleRes.ok) setSchedules(await scheduleRes.json());
      } catch (err) {
        console.error("Dropdown verileri yüklenemedi", err);
      }
    };

    fetchDropdownData();
  }, [role, navigate]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage('');
    setError('');

    try {
      const response = await fetch('http://localhost:5277/api/Staff/AssignStaff', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          staffId: parseInt(formData.staffId),
          scheduleId: parseInt(formData.scheduleId),
          date: formData.date
        })
      });

      if (!response.ok) {
        const errText = await response.text();
        throw new Error(errText || "Assignment Error.");
      }

      const data = await response.json();
      setMessage(data.message || "Staff assigned successfully!");
      setFormData({ staffId: '', scheduleId: '', date: '' });
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-slate-100 p-8 font-sans">
      <div className="max-w-3xl mx-auto">
        <div className="flex justify-between items-center mb-8 border-b pb-4">
          <div>
            <h2 className="text-3xl font-bold text-slate-800">👥 Staff Assignment</h2>
            <p className="text-slate-500 mt-1">Assign drivers and staff to schedules.</p>
          </div>
          <button onClick={() => navigate('/staff-dashboard')} className="bg-slate-600 text-white px-4 py-2 rounded-lg">↩ Return Dashboard </button>
        </div>

        <div className="bg-white p-8 rounded-xl shadow-md border-t-4 border-teal-500">
          {error && <div className="mb-6 p-4 bg-red-50 text-red-700 rounded border-l-4 border-red-500">{error}</div>}
          {message && <div className="mb-6 p-4 bg-green-50 text-green-700 rounded border-l-4 border-green-500 font-bold">{message}</div>}

          <form onSubmit={handleSubmit} className="space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              
              <div>
                <label className="block text-sm font-bold text-slate-700 mb-2">Staff to Assign</label>
                <select 
                  required
                  value={formData.staffId}
                  onChange={(e) => setFormData({...formData, staffId: e.target.value})}
                  className="w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-teal-500 outline-none"
                >
                  <option value="">Select Staff...</option>
                  {staffList.map(s => (
                    <option key={s.staffId} value={s.staffId}>{s.name} ({s.role})</option>
                  ))}
                </select>
              </div>

              <div>
                <label className="block text-sm font-bold text-slate-700 mb-2">Target Train Schedule</label>
                <select 
                  required
                  value={formData.scheduleId}
                  onChange={(e) => setFormData({...formData, scheduleId: e.target.value})}
                  className="w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-teal-500 outline-none"
                >
                  <option value="">Select Schedule...</option>
                  {schedules.map(s => (
                    <option key={s.scheduleId} value={s.scheduleId}>{s.displayText}</option>
                  ))}
                </select>
              </div>

            </div>

            <div>
              <label className="block text-sm font-bold text-slate-700 mb-2">Assignment Date</label>
              <input type="date" required value={formData.date} onChange={(e) => setFormData({...formData, date: e.target.value})} className="w-full md:w-1/2 px-4 py-2 border rounded-lg" />
            </div>

            <button type="submit" disabled={loading} className="w-full bg-teal-600 text-white font-bold py-3 rounded-lg text-lg">
              {loading ? 'Processing...' : 'Confirm and Assign'}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}

export default StaffAssignment;