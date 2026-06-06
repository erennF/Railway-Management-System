import React, { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

function BookingDetails() {
  const { scheduleId } = useParams(); 
  const navigate = useNavigate();
  const passengerId = localStorage.getItem('userId');

  const [seatNumber, setSeatNumber] = useState('');
  const [coachType, setCoachType] = useState('Economy');
  const [hasDependent, setHasDependent] = useState(false);
  const [luggageWeight, setLuggageWeight] = useState(0); 

  const [step, setStep] = useState(1); 
  const [reservationId, setReservationId] = useState(null);
  const [priceDetails, setPriceDetails] = useState(null);
  const [message, setMessage] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  
  // The state monitors whether the train is full or not.
  const [isFull, setIsFull] = useState(false);

  const handleReserve = async (e) => {
    e.preventDefault();
    setError('');
    setMessage('');
    setLoading(true);
    setIsFull(false); 

    try {
      const resResponse = await fetch('http://localhost:5277/api/Booking/CreateReservation', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          passengerId: parseInt(passengerId),
          scheduleId: parseInt(scheduleId),
          seatNumber: seatNumber,
          coachType: coachType,
          hasDependent: hasDependent, 
          totalLuggageWeight: luggageWeight 
        })
      });

      if (!resResponse.ok) {
        const errText = await resResponse.text();
        
        // If the error message from the backend contains "full", "capacity", etc., trigger the Waitlist.
        if (errText.toLowerCase().includes('full') || errText.toLowerCase().includes('capacity') || errText.toLowerCase().includes('none')) {
           setIsFull(true);
           throw new Error("Unfortunately, there are no empty seats in the carriage you selected.");
        }
        
        throw new Error(errText); 
      }

      const resData = await resResponse.json();
      setReservationId(resData.reservationId);

      const basePrice = coachType === 'Business' ? 2000 : 1000;
      const priceResponse = await fetch(`http://localhost:5277/api/Booking/CalculatePrice?passengerId=${passengerId}&basePrice=${basePrice}&hasDependent=${hasDependent}`);

      if (priceResponse.ok) {
        const priceData = await priceResponse.json();
        setPriceDetails(priceData);
      }

      setStep(2); 
      setMessage("The seat has been temporarily reserved for you (Pending).");
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  //  Waiting List Join Function
  const handleJoinWaitlist = async () => {
    try {
      setLoading(true);
      const response = await fetch('http://localhost:5277/api/Booking/JoinWaitingList', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          PassengerId: parseInt(passengerId),
          ScheduleId: parseInt(scheduleId)
        })
      });

      if (!response.ok) throw new Error("Error occurred while joining the waiting list.");
      
      setMessage("✅ You have been successfully added to the waiting list! You will be notified if a seat becomes available.");
      setIsFull(false);
      setError('');
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const handlePayment = async () => {
    try {
      const response = await fetch(`http://localhost:5277/api/Booking/PayReservation/${reservationId}`, { method: 'PUT' });
      if (!response.ok) throw new Error("Payment failed.");

      setMessage("✅ Payment successful! Your ticket has been confirmed. You are being redirected...");
      setTimeout(() => navigate('/my-reservations'), 2500); 
    } catch (err) {
      setError(err.message);
    }
  };

  const handleCancel = async () => {
    try {
      const response = await fetch(`http://localhost:5277/api/Booking/CancelReservation/${reservationId}`, { method: 'PUT' });
      if (!response.ok) throw new Error("Cancellation failed.");

      setMessage("❌ Your reservation has been cancelled. The seat is now available.");
      setTimeout(() => navigate('/search'), 2000);
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <div style={{ maxWidth: '600px', margin: '40px auto', padding: '20px', fontFamily: 'Arial' }}>
      <h2 style={{ color: '#2c3e50' }}>🎫 Bilet Rezervasyonu</h2>
      
      {error && <div style={{ color: 'white', backgroundColor: '#e74c3c', padding: '10px', borderRadius: '5px', marginBottom: '15px' }}>{error}</div>}
      {isFull && (
        <div className="bg-amber-100 border-l-4 border-amber-500 p-4 mb-6 rounded shadow-sm">
          <h3 className="text-amber-800 font-bold mb-2">Waiting List</h3>
          <p className="text-amber-700 text-sm mb-3">Reservations were not possible as the train was at full capacity. You can join the waiting list if you wish.</p>
          <button 
            onClick={handleJoinWaitlist}
            disabled={loading}
            className="bg-amber-600 hover:bg-amber-700 text-white font-bold py-2 px-4 rounded transition-colors"
          >
            {loading ? 'Adding...' : 'Join Waiting List'}
          </button>
        </div>
      )}

      {message && <div style={{ color: '#27ae60', fontWeight: 'bold', marginBottom: '15px' }}>{message}</div>}

      {step === 1 && !isFull && (
        <form onSubmit={handleReserve} style={{ display: 'flex', flexDirection: 'column', gap: '15px' }}>
          
          <div style={{ backgroundColor: '#f8fafc', padding: '15px', borderRadius: '8px', border: '1px solid #e2e8f0' }}>
            <div style={{ marginBottom: '15px' }}>
              <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold', color: '#334155' }}>Seat Number (e.g., A1-12):</label>
              <input 
                type="text" 
                value={seatNumber} 
                onChange={(e) => setSeatNumber(e.target.value.toUpperCase())} 
                required
                style={{ width: '100%', padding: '10px', borderRadius: '5px', border: '1px solid #cbd5e1' }}
              />
            </div>

            <div style={{ marginBottom: '15px' }}>
              <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold', color: '#334155' }}>Coach Type:</label>
              <select 
                value={coachType} 
                onChange={(e) => setCoachType(e.target.value)}
                style={{ width: '100%', padding: '10px', borderRadius: '5px', border: '1px solid #cbd5e1' }}
              >
                <option value="Economy">Economy Class</option>
                <option value="Business">Business Class</option>
              </select>
            </div>
          </div>

          <div className="bg-white p-6 rounded-xl shadow-md border border-slate-200">
            <h3 className="text-lg font-bold text-slate-800 mb-3 flex items-center gap-2">
              🧳 Luggage Information
            </h3>
            <p className="text-sm text-slate-500 mb-4">
              Please enter the total weight of your luggage in kilograms (kg).
            </p>
            <div className="flex items-center gap-3 mb-4">
              <input 
                type="number" 
                min="0"
                max="100"
                value={luggageWeight}
                onChange={(e) => setLuggageWeight(parseInt(e.target.value) || 0)}
                className="w-32 px-4 py-2 border border-slate-300 rounded-lg text-center text-lg font-semibold text-slate-800 focus:ring-2 focus:ring-blue-500 focus:outline-none"
              />
              <span className="text-lg font-medium text-slate-600">kg</span>
            </div>

            <div style={{ display: 'flex', alignItems: 'center', gap: '10px', marginTop: '15px', borderTop: '1px solid #eee', paddingTop: '15px' }}>
              <input 
                type="checkbox" 
                id="dependent"
                checked={hasDependent} 
                onChange={(e) => setHasDependent(e.target.checked)} 
                style={{ width: '18px', height: '18px' }}
              />
              <label htmlFor="dependent" style={{ color: '#475569', fontWeight: '500' }}>I have a dependent traveling with me (25% discount)</label>
            </div>
          </div>

          <button 
            type="submit" 
            disabled={loading}
            style={{ padding: '12px', backgroundColor: '#3b82f6', color: 'white', border: 'none', borderRadius: '8px', fontSize: '16px', fontWeight: 'bold', cursor: 'pointer', marginTop: '10px' }}
          >
            {loading ? 'Processing...' : 'Start Reservation'}
          </button>
        </form>
      )}

      {step === 2 && priceDetails && (
        <div style={{ backgroundColor: '#f8fafc', padding: '25px', border: '1px solid #e2e8f0', borderRadius: '12px', boxShadow: '0 4px 6px -1px rgb(0 0 0 / 0.1)' }}>
          <h3 style={{ margin: '0 0 15px 0', color: '#1e293b' }}>💰 Payment Summary</h3>
          <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '10px' }}>
            <span style={{ color: '#64748b' }}>Base Price:</span>
            <span style={{ fontWeight: 'bold' }}>{priceDetails.originalPrice} TL</span>
          </div>
          <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '15px' }}>
            <span style={{ color: '#64748b' }}>Applied Discounts:</span>
            <span style={{ color: '#16a34a', fontWeight: 'bold' }}>{priceDetails.appliedDiscounts}</span>
          </div>
          <hr style={{ borderColor: '#e2e8f0', margin: '15px 0' }} />
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <span style={{ fontSize: '18px', fontWeight: 'bold', color: '#334155' }}>Amount to Pay:</span>
            <span style={{ fontSize: '24px', fontWeight: 'bold', color: '#ea580c' }}>{priceDetails.finalPrice} TL</span>
          </div>

          <div style={{ display: 'flex', gap: '15px', marginTop: '25px' }}>
            <button 
              onClick={handlePayment} 
              style={{ flex: 1, padding: '14px', backgroundColor: '#22c55e', color: 'white', border: 'none', borderRadius: '8px', cursor: 'pointer', fontWeight: 'bold', fontSize: '16px' }}
            >
              Pay with Credit Card
            </button>
            <button 
              onClick={handleCancel} 
              style={{ padding: '14px 24px', backgroundColor: '#ef4444', color: 'white', border: 'none', borderRadius: '8px', cursor: 'pointer', fontWeight: 'bold' }}
            >
              Cancel
            </button>
          </div>
        </div>
      )}
    </div>
  );
}

export default BookingDetails;