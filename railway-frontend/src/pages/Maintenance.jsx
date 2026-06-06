import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function Maintenance() {
  const navigate = useNavigate();
  const [trains, setTrains] = useState([]);
  const [formData, setFormData] = useState({ trainId: '', details: '' });
  const [sensorForm, setSensorForm] = useState({ sensorName: '', trainId: '' });
  const [sensors, setSensors] = useState([]);
  const [historyRecords, setHistoryRecords] = useState([]);
  const [searchTrainId, setSearchTrainId] = useState('');
  const [message, setMessage] = useState('');

  useEffect(() => {
    const fetchTrains = async () => {
      const res = await fetch('http://localhost:5277/api/Staff/Dropdown/Trains');
      if (res.ok) setTrains(await res.json());
    };
    fetchTrains();
  }, []);

  // MAINTENANCE RECORDING FUNCTION
  const handleSubmit = async (e) => {
    e.preventDefault();
    const res = await fetch('http://localhost:5277/api/Staff/AddMaintenance', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ trainId: parseInt(formData.trainId), details: formData.details })
    });
    if (res.ok) { setMessage("Maintenance record added successfully."); setFormData({ trainId: '', details: '' }); }
  };

  // FETCH MAINTENANCE HISTORY FUNCTION
  const handleFetchHistory = async () => {
    if (!searchTrainId) return;
    const res = await fetch(`http://localhost:5277/api/Reports/MaintenanceHistory/${searchTrainId}`);
    if (res.ok) setHistoryRecords(await res.json());
  };

  // ADD SENSOR FUNCTION
  const handleAddSensor = async (e) => {
    e.preventDefault();
    const res = await fetch('http://localhost:5277/api/Staff/AddSensor', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ sensorName: sensorForm.sensorName, trainId: parseInt(sensorForm.trainId) })
    });
    if (res.ok) { setMessage("Sensor added successfully."); setSensorForm({ sensorName: '', trainId: '' }); }
  };

  return (
    <div className="min-h-screen bg-slate-100 p-8">
      <div className="max-w-6xl mx-auto space-y-8">
        <div className="flex justify-between items-center border-b pb-4">
          <h2 className="text-3xl font-bold">🛠️ Maintenance and Sensor Management</h2>
          <button onClick={() => navigate('/staff-dashboard')} className="bg-slate-600 text-white px-4 py-2 rounded-lg">Dashboard</button>
        </div>

        {message && <div className="p-4 bg-green-100 text-green-800 rounded font-bold">{message}</div>}

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          
          <div className="space-y-8">
            <div className="bg-white p-6 rounded-xl shadow-md border-t-4 border-orange-500">
              <h3 className="font-bold mb-4">🔧 Maintenance Report</h3>
              <select onChange={(e) => setFormData({...formData, trainId: e.target.value})} className="w-full p-2 border rounded mb-2">
                <option value="">Select Train...</option>
                {trains.map(t => <option key={t.trainId} value={t.trainId}>{t.name}</option>)}
              </select>
              <textarea placeholder="Details..." onChange={(e) => setFormData({...formData, details: e.target.value})} className="w-full p-2 border rounded mb-2"></textarea>
              <button onClick={handleSubmit} className="w-full bg-orange-600 text-white py-2 rounded">Save</button>
            </div>

            <div className="bg-white p-6 rounded-xl shadow-md border-t-4 border-slate-700">
              <h3 className="font-bold mb-4">📜Past Maintenance Records</h3>
              <select onChange={(e) => setSearchTrainId(e.target.value)} className="w-full p-2 border rounded mb-2">
                <option value="">Select Train...</option>
                {trains.map(t => <option key={t.trainId} value={t.trainId}>{t.name}</option>)}
              </select>
              <button onClick={handleFetchHistory} className="w-full bg-slate-700 text-white py-2 rounded mb-4">Fetch History</button>
              <div className="space-y-2">
                {historyRecords.map((r, i) => <div key={i} className="p-2 border-b text-sm">{r.details} ({new Date(r.date).toLocaleDateString()})</div>)}
              </div>
            </div>
          </div>

          <div className="bg-white p-6 rounded-xl shadow-md border-t-4 border-teal-600">
            <h3 className="font-bold mb-4">📡 Sensor Management</h3>
            <select onChange={(e) => setSensorForm({...sensorForm, trainId: e.target.value})} className="w-full p-2 border rounded mb-2">
              <option value="">Select Train...</option>
              {trains.map(t => <option key={t.trainId} value={t.trainId}>{t.name}</option>)}
            </select>
            <input type="text" placeholder="Sensor Name..." onChange={(e) => setSensorForm({...sensorForm, sensorName: e.target.value})} className="w-full p-2 border rounded mb-2" />
            <button onClick={handleAddSensor} className="w-full bg-teal-600 text-white py-2 rounded">Add Sensor</button>
          </div>
        </div>
      </div>
    </div>
  );
}
export default Maintenance;