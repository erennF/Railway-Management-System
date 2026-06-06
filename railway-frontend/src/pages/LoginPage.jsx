import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

function LoginPage() {
  const [email, setEmail] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      const response = await fetch('http://localhost:5277/api/Auth/Login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ Email: email })
      });

      if (response.ok) {
        const data = await response.json();
        
        localStorage.setItem('userId', data.id);
        localStorage.setItem('userRole', data.role);
        
        // Navigate based on role
        if (data.role === 'Passenger') {
          navigate('/passenger-dashboard');
        } else {
          navigate('/staff-dashboard');
        }
      } else {
        setError('User not found. Please check your email address.');
      }
    } catch (err) {
      setError('Failed to connect to the server. Is the backend running?');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-slate-200 flex items-center justify-center p-4">
      <div className="max-w-sm w-full bg-white rounded-2xl shadow-2xl overflow-hidden">
        
        <div className="relative h-48 flex flex-col items-center justify-center text-center">
          <div 
            className="absolute inset-0 bg-cover bg-center opacity-40"
            style={{ backgroundImage: "url('https://images.unsplash.com/photo-1474487548417-781cb71495f3?q=80&w=800&auto=format&fit=crop')" }}
          ></div>
          <div className="absolute inset-0 bg-gradient-to-t from-slate-900 to-transparent opacity-80"></div>
          <div className="relative z-10 p-4 mt-6">
            <h2 className="text-3xl font-extrabold text-white mb-1 drop-shadow-lg">Railway Portal</h2>
            <p className="text-slate-200 text-sm font-medium">Please enter your email to sign in</p>
          </div>
        </div>

        <div className="p-6">
          {error && (
            <div className="mb-4 p-3 bg-red-50 border border-red-200 rounded-lg">
              <p className="text-red-700 text-sm font-medium">{error}</p>
            </div>
          )}

          <form onSubmit={handleSubmit} className="space-y-4">
            <div>
              <label className="block text-xs font-bold text-gray-600 uppercase mb-1">Email Address</label>
              <input 
                type="email" 
                required 
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                disabled={loading}
                className="w-full px-3 py-2 bg-gray-50 border border-gray-200 rounded-lg focus:ring-2 focus:ring-slate-800 outline-none transition-all text-sm disabled:opacity-50"
                placeholder="example@mail.com"
              />
            </div>

            <button 
              type="submit" 
              disabled={loading}
              className="w-full bg-slate-800 hover:bg-slate-900 disabled:bg-slate-600 text-white font-bold py-2.5 px-4 rounded-lg transition-colors shadow-md text-sm mt-2"
            >
              {loading ? 'Signing in...' : 'Sign In'}
            </button>
          </form>

          <p className="text-xs text-gray-500 text-center mt-4">
            Demo: passenger@example.com or staff@example.com
          </p>
        </div>
      </div>
    </div>
  );
}

export default LoginPage;
