import { BrowserRouter, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import PassengerDashboard from './pages/PassengerDashboard';
import StaffDashboard from './pages/StaffDashboard';
import TicketSearch from './pages/TicketSearch';
import BookingDetails from './pages/BookingDetails';
import MyReservations from './pages/MyReservations';
import LoyaltyProgram from './pages/LoyaltyProgram';
import Profile from './pages/Profile';
import OperationsDashboard from './pages/OperationsDashboard';
import MySchedules from './pages/MySchedules';
import Reports from './pages/Reports';
import StaffAssignment from './pages/StaffAssignment';
import Customs from './pages/Customs';
import Maintenance from './pages/Maintenance';
import DataManagement from './pages/DataManagement';
function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/passenger-dashboard" element={<PassengerDashboard />} />
        <Route path="/staff-dashboard" element={<StaffDashboard />} />
        <Route path="/search" element={<TicketSearch />} />
        <Route path="/booking-details/:scheduleId" element={<BookingDetails />} />
        <Route path="/my-reservations" element={<MyReservations />} />
        <Route path="/loyalty-program" element={<LoyaltyProgram />} />
        <Route path="/profile" element={<Profile />} />
        <Route path="/operations-dashboard" element={<OperationsDashboard />} />
        <Route path="/my-schedules" element={<MySchedules />} />
        <Route path="/reports" element={<Reports />} />
        <Route path="/staff-assignment" element={<StaffAssignment />} />
        <Route path="/customs" element={<Customs />} />
        <Route path="/maintenance" element={<Maintenance />} />
        <Route path="/data-management" element={<DataManagement />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;