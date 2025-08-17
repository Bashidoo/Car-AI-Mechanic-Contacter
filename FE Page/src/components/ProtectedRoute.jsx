// ProtectedRoute.jsx
import { Navigate, Outlet } from 'react-router-dom';

function ProtectedRoute() {
  const token = localStorage.getItem('userToken'); // Check for token
  console.log("Token in ProtectedRoute:", token);  // Debug

  if (!token) {
    // Redirect to login if no token
    return <Navigate to="/login" replace />;
  }

  // Render child routes
  return <Outlet />;
}

export default ProtectedRoute;
