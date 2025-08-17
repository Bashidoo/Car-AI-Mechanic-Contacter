import { Navigate, Outlet } from 'react-router-dom';

function ProtectedRoute() {
  const token = localStorage.getItem('userToken'); // or your auth check

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  return <Outlet />; // Render child routes
}

export default ProtectedRoute;
