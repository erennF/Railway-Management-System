import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function Reports() {
  const navigate = useNavigate();
  const role = localStorage.getItem('userRole');

  useEffect(() => {
    if (role !== 'Station Manager' && role !== 'Admin') {
      alert("Bu sayfaya erişim yetkiniz yok!");
      navigate('/staff-dashboard');
    }
  }, [role, navigate]);

  const [selectedReport, setSelectedReport] = useState('');
  const [param, setParam] = useState('');
  const [reportData, setReportData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  // Dropdown 
  const [trains, setTrains] = useState([]);
  const [schedules, setSchedules] = useState([]);
  const [routes, setRoutes] = useState([]);
  const [passengers, setPassengers] = useState([]);

  useEffect(() => {
    const fetchDropdowns = async () => {
      try {
        const [trainRes, schedRes, routeRes, passRes] = await Promise.all([
          fetch('http://localhost:5277/api/Staff/Dropdown/Trains'),
          fetch('http://localhost:5277/api/Staff/Dropdown/Schedules'),
          fetch('http://localhost:5277/api/Staff/Dropdown/Routes'),
          fetch('http://localhost:5277/api/Staff/Dropdown/Passengers')
        ]);
        if (trainRes.ok) setTrains(await trainRes.json());
        if (schedRes.ok) setSchedules(await schedRes.json());
        if (routeRes.ok) setRoutes(await routeRes.json());
        if (passRes.ok) setPassengers(await passRes.json());
      } catch (err) {
        console.error("Listeler yüklenemedi:", err);
      }
    };
    fetchDropdowns();
  }, []);

  const reportList = [
    { id: 'ActiveTrains', name: '1. Active Trains', type: 'date', endpoint: 'ActiveTrains/' },
    { id: 'RouteStations', name: '2. Route Stations', type: 'route', endpoint: 'RouteStations/' },
    { id: 'PassengerReservations', name: '3. Passenger Reservations', type: 'passenger', endpoint: 'PassengerReservations/' },
    { id: 'Waitlist', name: '4. Waitlist', type: 'schedule', endpoint: 'Waitlist/' },
    { id: 'LoadFactor', name: '5. Load Factor', type: 'schedule', endpoint: 'LoadFactor/' },
    { id: 'TopLoyaltyCustomers', name: '6. Top Loyalty Customers', type: 'none', endpoint: 'TopLoyaltyCustomers' },
    { id: 'DependentsOnDate', name: '7. Traveling Family Members', type: 'date', endpoint: 'DependentsOnDate/' },
    { id: 'MaintenanceHistory', name: '8. Maintenance History', type: 'train', endpoint: 'MaintenanceHistory/' },
    { id: 'FreightByCountry', name: '9. Customs/Shipping Operations', type: 'country', endpoint: 'FreightByCountry/' },
    { id: 'StaffAssignments', name: '10. Staff Assignments', type: 'date', endpoint: 'StaffAssignments/' },
  ];

  const handleFetchReport = async () => {
    if (!selectedReport) return;
    const reportDef = reportList.find(r => r.id === selectedReport);
    if (reportDef.type !== 'none' && !param) {
      setError('Please make the required selection for the report.');
      return;
    }

    setLoading(true); setError(''); setReportData(null);

    try {
      const url = `http://localhost:5277/api/Reports/${reportDef.endpoint}${reportDef.type !== 'none' ? param : ''}`;
      const response = await fetch(url);
      
      if (!response.ok) {
        const errText = await response.text();
        throw new Error(errText || "Report not found or no data available.");
      }

      setReportData(await response.json());
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const renderTable = (data) => {
    if (!data) return null;
    if (!Array.isArray(data)) {
      return (
        <div className="grid grid-cols-2 gap-4 bg-slate-50 p-6 rounded-lg border border-slate-200">
          {Object.entries(data).map(([key, value]) => (
            <div key={key} className="bg-white p-4 shadow-sm rounded border-l-4 border-indigo-500">
              <p className="text-xs text-slate-500 font-bold uppercase">{key}</p>
              <p className="text-lg font-bold text-slate-800">{value}</p>
            </div>
          ))}
        </div>
      );
    }
    if (data.length === 0) return <p className="text-slate-500">No data found.</p>;

    const columns = Object.keys(data[0]);
    return (
      <div className="overflow-x-auto bg-white rounded-lg shadow border border-slate-200">
        <table className="w-full text-left border-collapse">
          <thead>
            <tr className="bg-indigo-900 text-white">
              {columns.map(col => <th key={col} className="p-3 text-sm font-semibold">{col}</th>)}
            </tr>
          </thead>
          <tbody>
            {data.map((row, index) => (
              <tr key={index} className="border-b hover:bg-slate-50">
                {columns.map(col => (
                  <td key={col} className="p-3 text-sm text-slate-700">
                    {typeof row[col] === 'object' ? JSON.stringify(row[col]) : row[col]}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  };

  const currentReportDef = reportList.find(r => r.id === selectedReport);

  return (
    <div className="min-h-screen bg-slate-100 font-sans flex">
      
      <div className="w-1/4 bg-slate-900 text-white p-6 shadow-xl z-10 min-h-screen">
        <h2 className="text-2xl font-bold mb-6 flex items-center gap-2">📊 Admin Reports</h2>
        <div className="flex flex-col gap-2">
          {reportList.map(report => (
            <button
              key={report.id}
              onClick={() => { setSelectedReport(report.id); setParam(''); setReportData(null); setError(''); }}
              className={`text-left p-3 rounded transition-colors ${selectedReport === report.id ? 'bg-indigo-600 font-bold' : 'hover:bg-slate-800 text-slate-300'}`}
            >
              {report.name}
            </button>
          ))}
        </div>
        <button onClick={() => navigate('/staff-dashboard')} className="mt-8 w-full bg-slate-700 hover:bg-slate-600 text-white px-4 py-2 rounded">↩ Back to Dashboard</button>
      </div>

      <div className="w-3/4 p-10 max-h-screen overflow-y-auto">
        {!selectedReport ? (
          <div className="h-full flex flex-col items-center justify-center text-slate-400">
            <span className="text-6xl mb-4">📈</span>
            <h3 className="text-xl">Select a report from the left menu to view it</h3>
          </div>
        ) : (
          <div>
            <div className="bg-white p-6 rounded-xl shadow-md border-t-4 border-indigo-600 mb-6">
              <h3 className="text-xl font-bold text-slate-800 mb-4">{currentReportDef.name}</h3>
              
              <div className="flex gap-4 items-end">
                
                {currentReportDef.type === 'date' && (
                  <div className="flex-1">
                    <label className="block text-sm font-bold text-slate-600 mb-1">Select Date:</label>
                    <input type="date" value={param} onChange={(e) => setParam(e.target.value)} className="w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-indigo-500 outline-none" />
                  </div>
                )}
                
                {currentReportDef.type === 'train' && (
                  <div className="flex-1">
                    <label className="block text-sm font-bold text-slate-600 mb-1">Select Train:</label>
                    <select value={param} onChange={(e) => setParam(e.target.value)} className="w-full px-4 py-2 border rounded-lg outline-none">
                      <option value="">Select Train from List...</option>
                      {trains.map(t => <option key={t.trainId} value={t.trainId}>{t.name}</option>)}
                    </select>
                  </div>
                )}

                {currentReportDef.type === 'schedule' && (
                  <div className="flex-1">
                    <label className="block text-sm font-bold text-slate-600 mb-1">Select Schedule:</label>
                    <select value={param} onChange={(e) => setParam(e.target.value)} className="w-full px-4 py-2 border rounded-lg outline-none">
                      <option value="">Select Schedule from List...</option>
                      {schedules.map(s => <option key={s.scheduleId} value={s.scheduleId}>{s.displayText}</option>)}
                    </select>
                  </div>
                )}

                {currentReportDef.type === 'route' && (
                  <div className="flex-1">
                    <label className="block text-sm font-bold text-slate-600 mb-1">Select Route:</label>
                    <select value={param} onChange={(e) => setParam(e.target.value)} className="w-full px-4 py-2 border rounded-lg outline-none">
                      <option value="">Select Route from List...</option>
                      {routes.map(r => <option key={r.routeId} value={r.routeId}>{r.routeName}</option>)}
                    </select>
                  </div>
                )}

                {currentReportDef.type === 'passenger' && (
                  <div className="flex-1">
                    <label className="block text-sm font-bold text-slate-600 mb-1">Select Registered Passenger:</label>
                    <select value={param} onChange={(e) => setParam(e.target.value)} className="w-full px-4 py-2 border rounded-lg outline-none">
                      <option value="">Select Passenger from List...</option>
                      {passengers.map(p => <option key={p.passengerId} value={p.passengerId}>{p.name}</option>)}
                    </select>
                  </div>
                )}

                {currentReportDef.type === 'country' && (
                  <div className="flex-1">
                    <label className="block text-sm font-bold text-slate-600 mb-1">Select Country:</label>
                    <select value={param} onChange={(e) => setParam(e.target.value)} className="w-full px-4 py-2 border rounded-lg outline-none">
                      <option value="">Select Country from List...</option>
                      <option value="1">Türkiye (TR)</option>
                      <option value="2">Suudi Arabistan (KSA)</option>
                      <option value="3">Suriye (SY)</option>
                      <option value="4">Ürdün (JO)</option>
                    </select>
                  </div>
                )}

                <button onClick={handleFetchReport} disabled={loading} className="bg-indigo-600 hover:bg-indigo-700 text-white font-bold py-2 px-6 rounded-lg transition-colors h-10">
                  {loading ? 'Loading...' : 'Fetch Report'}
                </button>
              </div>
            </div>

            {error && <div className="bg-red-100 text-red-700 p-4 rounded-lg font-medium mb-6">{error}</div>}
            
            {reportData && (
              <div className="mt-6">
                <h4 className="text-lg font-bold text-slate-700 mb-3">Results</h4>
                {renderTable(reportData)}
              </div>
            )}
          </div>
        )}
      </div>
      
    </div>
  );
}

export default Reports;