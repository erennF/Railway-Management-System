import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function DataManagement() {
  const navigate = useNavigate();
  const role = localStorage.getItem('userRole');

  useEffect(() => {
    if (role !== 'Station Manager' && role !== 'Admin') {
      navigate('/staff-dashboard');
    }
  }, [role, navigate]);

  const [trains, setTrains] = useState([]);
  const [routes, setRoutes] = useState([]);
  const [trainForm, setTrainForm] = useState({ name: '', trainType: 'Passenger' });
  const [staffForm, setStaffForm] = useState({ name: '', role: 'Driver', email: '' });
  const [scheduleForm, setScheduleForm] = useState({ date: '', departureTime: '', trainId: '', routeId: '' });

  const [message, setMessage] = useState('');
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchDropdowns = async () => {
      try {
        const [trainRes, routeRes] = await Promise.all([
          fetch('http://localhost:5277/api/Staff/Dropdown/Trains'),
          fetch('http://localhost:5277/api/Staff/Dropdown/Routes')
        ]);
        if (trainRes.ok) setTrains(await trainRes.json());
        if (routeRes.ok) setRoutes(await routeRes.json());
      } catch (err) { console.error("Listeler yüklenemedi."); }
    };
    fetchDropdowns();
  }, []);

  const handleAddTrain = async (e) => {
    e.preventDefault();
    try {
      const res = await fetch('http://localhost:5277/api/Staff/Management/AddTrain', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(trainForm)
      });
      if (res.ok) { setMessage("Train added."); setTrainForm({ name: '', trainType: 'Passenger' }); }
    } catch (err) { setError("Error occurred."); }
  };

  const handleAddStaff = async (e) => {
    e.preventDefault();
    try {
      const res = await fetch('http://localhost:5277/api/Staff/Management/AddStaff', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(staffForm)
      });
      if (res.ok) { setMessage("Staff member added."); setStaffForm({ name: '', role: 'Driver', email: '' }); }
    } catch (err) { setError("Error occurred."); }
  };

  const handleAddSchedule = async (e) => {
    e.preventDefault();
    try {
      const res = await fetch('http://localhost:5277/api/Staff/Management/AddSchedule', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          date: scheduleForm.date,
          departureTime: scheduleForm.departureTime,
          trainId: parseInt(scheduleForm.trainId),
          routeId: parseInt(scheduleForm.routeId)
        })
      });
      if (res.ok) { setMessage("Schedule added."); setScheduleForm({ date: '', departureTime: '', trainId: '', routeId: '' }); }
    } catch (err) { setError("Error occurred while adding schedule."); }
  };

  return (
    <div className="min-h-screen bg-slate-100 p-8 font-sans">
      <div className="max-w-7xl mx-auto">
        <div className="flex justify-between items-center mb-8 border-b pb-4">
          <h2 className="text-3xl font-bold">🗃️ System Data Management</h2>
          <button onClick={() => navigate('/staff-dashboard')} className="bg-slate-600 text-white px-4 py-2 rounded-lg">  ↩ Return to Dashboard</button>
        </div>

        {message && <div className="mb-6 p-4 bg-green-100 text-green-800 rounded font-bold">{message}</div>}

        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {/* Train Addition Form */}
          <div className="bg-white p-6 rounded-xl shadow-md border-t-4 border-blue-600">
            <h3 className="font-bold mb-4">🚄 New Train</h3>
            <form onSubmit={handleAddTrain} className="space-y-4">
              <input type="text" placeholder="Train Name" required value={trainForm.name} onChange={(e) => setTrainForm({...trainForm, name: e.target.value})} className="w-full p-2 border rounded" />
              <button type="submit" className="w-full bg-blue-600 text-white py-2 rounded">Add</button>
            </form>
          </div>

            {/* Staff Addition Form */}
          <div className="bg-white p-6 rounded-xl shadow-md border-t-4 border-purple-600">
            <h3 className="text-lg font-bold text-slate-800 mb-4">👤 New Staff Member</h3>
            <form onSubmit={handleAddStaff} className="space-y-4">
              <div>
                <label className="block text-xs font-bold text-slate-600 mb-1">Name</label>
                <input type="text" required value={staffForm.name} onChange={(e) => setStaffForm({...staffForm, name: e.target.value})} className="w-full px-3 py-2 border rounded" />
              </div>
              
              
              <div>
                <label className="block text-xs font-bold text-slate-600 mb-1">System Role</label>
                <select 
                  required 
                  value={staffForm.role} 
                  onChange={(e) => setStaffForm({...staffForm, role: e.target.value})} 
                  className="w-full px-3 py-2 border rounded"
                >
                  <option value="Driver">Driver</option>
                  <option value="Station Manager">Station Manager</option>
                  <option value="Maintenance Technician">Maintenance Technician </option>
                  <option value="Customs Coordinator">Customs Coordinator </option>
                </select>
              </div>

              <div>
                <label className="block text-xs font-bold text-slate-600 mb-1">Email</label>
                <input type="email" required value={staffForm.email} onChange={(e) => setStaffForm({...staffForm, email: e.target.value})} className="w-full px-3 py-2 border rounded" />
              </div>
              <button type="submit" className="w-full bg-purple-600 text-white font-bold py-2 rounded mt-2">Save Staff Member</button>
            </form>
          </div>

          <div className="bg-white p-6 rounded-xl shadow-md border-t-4 border-teal-600">
            <h3 className="font-bold mb-4">📅 New Schedule</h3>
            <form onSubmit={handleAddSchedule} className="space-y-4">
              <select required onChange={(e) => setScheduleForm({...scheduleForm, trainId: e.target.value})} className="w-full p-2 border rounded">
                <option value="">Select Train...</option>
                {trains.map(t => <option key={t.trainId} value={t.trainId}>{t.name}</option>)}
              </select>
              <select required onChange={(e) => setScheduleForm({...scheduleForm, routeId: e.target.value})} className="w-full p-2 border rounded">
                <option value="">Select Route...</option>
                {routes.map(r => <option key={r.routeId} value={r.routeId}>{r.routeName}</option>)}
              </select>
              <input type="date" required onChange={(e) => setScheduleForm({...scheduleForm, date: e.target.value})} className="w-full p-2 border rounded" />
              <input type="time" required onChange={(e) => setScheduleForm({...scheduleForm, departureTime: e.target.value})} className="w-full p-2 border rounded" />
              <button type="submit" className="w-full bg-teal-600 text-white py-2 rounded">Add Schedule</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}

export default DataManagement;