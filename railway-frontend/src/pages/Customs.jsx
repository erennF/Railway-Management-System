import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function Customs() {
  const navigate = useNavigate();
  const role = localStorage.getItem('userRole');

  // Only the Customs Coordinator and Admin can access it.
  useEffect(() => {
    if (role !== 'Customs Coordinator' && role !== 'Admin') {
      alert("Bu sayfaya erişim yetkiniz yok!");
      navigate('/staff-dashboard');
    }
  }, [role, navigate]);

  const [stationId, setStationId] = useState('');
  const [shipments, setShipments] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const handleFetchCustoms = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    setSuccess('');
    setShipments([]);

    try {
      const response = await fetch(`http://localhost:5277/api/Staff/CustomsClearance/${stationId}`);
      
      if (!response.ok) {
        throw new Error("No customs/cargo processing was found at this station.");
      }

      const data = await response.json();
      setShipments(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleApprove = (clearanceId) => {
    setShipments(shipments.map(s => 
      s.clearanceId === clearanceId ? { ...s, status: 'Cleared (Approved)' } : s
    ));
    setSuccess(`Cargo #${clearanceId} successfully cleared through customs!`);
    setTimeout(() => setSuccess(''), 3000);
  };

  return (
    <div className="min-h-screen bg-slate-100 p-8 font-sans">
      <div className="max-w-6xl mx-auto">
      
        <div className="flex justify-between items-center mb-8 border-b border-slate-300 pb-4">
          <div>
            <h2 className="text-3xl font-bold text-slate-800 m-0">📦 Customs and Logistics Management</h2>
            <p className="text-slate-500 mt-1">Manage cargo transit and customs (Clearance) approvals at border stations.</p>
          </div>
          <button 
            onClick={() => navigate('/staff-dashboard')}
            className="bg-slate-600 hover:bg-slate-700 text-white px-4 py-2 rounded-lg font-medium transition-colors"
          >
            ↩ Return to Dashboard
          </button>
        </div>

        <div className="bg-white p-6 rounded-xl shadow-md border-t-4 border-amber-500 mb-8">
          <form onSubmit={handleFetchCustoms} className="flex gap-4 items-end">
            <div className="flex-1">
              <label className="block text-sm font-bold text-slate-700 mb-2">Enter Border Station ID (e.g., 1, 2...):</label>
              <input 
                type="number" 
                required
                min="1"
                placeholder="Station ID..."
                value={stationId}
                onChange={(e) => setStationId(e.target.value)}
                className="w-full px-4 py-2 border border-slate-300 rounded-lg focus:ring-2 focus:ring-amber-500 outline-none"
              />
            </div>
            <button 
              type="submit"
              disabled={loading}
              className="bg-amber-600 hover:bg-amber-700 text-white font-bold py-2 px-8 rounded-lg transition-colors h-10"
            >
              {loading ? 'Searching...' : 'Fetch Shipments'}
            </button>
          </form>
        </div>

        {/* Warning Messages */}
        {error && <div className="mb-6 p-4 bg-red-50 border-l-4 border-red-500 text-red-700 font-medium rounded">{error}</div>}
        {success && <div className="mb-6 p-4 bg-green-50 border-l-4 border-green-500 text-green-700 font-bold rounded">{success}</div>}

        {/* Results Table */}
        {shipments.length > 0 && (
          <div className="bg-white rounded-xl shadow-md overflow-hidden border border-slate-200">
            <table className="w-full text-left border-collapse">
              <thead>
                <tr className="bg-slate-800 text-white">
                  <th className="p-4 font-semibold text-sm">Customs ID</th>
                  <th className="p-4 font-semibold text-sm">Date</th>
                  <th className="p-4 font-semibold text-sm">Shipper Company</th>
                  <th className="p-4 font-semibold text-sm">Weight (Ton)</th>
                  <th className="p-4 font-semibold text-sm">Status</th>
                  <th className="p-4 font-semibold text-sm text-center">Action</th>
                </tr>
              </thead>
              <tbody>
                {shipments.map((shipment, index) => (
                  <tr key={index} className="border-b hover:bg-slate-50 transition-colors">
                    <td className="p-4 font-bold text-slate-700">#{shipment.clearanceId}</td>
                    <td className="p-4 text-slate-600">{new Date(shipment.date).toLocaleDateString()}</td>
                    <td className="p-4 text-slate-800 font-medium">{shipment.shipper}</td>
                    <td className="p-4 text-slate-600">{shipment.weight} Ton</td>
                    <td className="p-4">
                      <span className={`px-3 py-1 rounded-full text-xs font-bold ${
                        shipment.status.includes('Cleared') ? 'bg-green-100 text-green-700' : 'bg-yellow-100 text-yellow-800'
                      }`}>
                        {shipment.status}
                      </span>
                    </td>
                    <td className="p-4 text-center">
                      {!shipment.status.includes('Cleared') ? (
                        <button 
                          onClick={() => handleApprove(shipment.clearanceId)}
                          className="bg-green-600 hover:bg-green-700 text-white px-3 py-1 rounded text-sm font-medium transition-colors"
                        >
                          Approve (Clear)
                        </button>
                      ) : (
                        <span className="text-green-600 font-bold text-xl">✓</span>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}

      </div>
    </div>
  );
}

export default Customs;