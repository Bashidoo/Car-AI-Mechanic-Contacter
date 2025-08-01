import { Navigate, Outlet } from 'react-router-dom';

export const isAuthenticated = () => {
  const token = localStorage.getItem('userToken');
  if (!token) return false;
  try {
    // Current: Decode fake token (base64-encoded JSON)
    const decoded = JSON.parse(atob(token));
    return decoded.exp > Date.now();
  } catch {
    return false;
  }

  // Future: Uncomment for JWT when backend provides JWT tokens
  /*
  try {
    import jwtDecode from 'jwt-decode';
    const decoded = jwtDecode(token);
    return decoded.exp * 1000 > Date.now();
  } catch {
    return false;
  }
  */
};

function ProtectedRoute() {
  return isAuthenticated() ? <Outlet /> : <Navigate to="/login" replace />;
}

export default ProtectedRoute;