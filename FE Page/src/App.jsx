import { Outlet, Link, useLocation } from 'react-router-dom';
import { isAuthenticated } from './components/ProtectedRoute';
import DropdownMenu from './components/DropdownMenu';
import LogoutButton from './components/LogoutButton';

function App() {
  const location = useLocation();
  
  return (
    <div className="app-container">
    {/* <nav className="nav-bar" role="navigation" aria-label="Huvudnavigering">
  <ul className="nav-list">
    {!isAuthenticated() && (
      <>
        <li>
          <Link
            to="/"
            className="nav-link"
            aria-current={location.pathname === '/' ? 'page' : undefined}
          >
            Startsida
          </Link>
        </li>
        <li>
          <Link
            to="/how-it-works"
            className="nav-link"
            aria-current={location.pathname === '/how-it-works' ? 'page' : undefined}
          >
            Hur funkar det?
          </Link>
        </li>
      </>
    )}
  </ul>
</nav> */}
    <main className="main-content">
    <Outlet />
    </main>
    </div>
  );
}

export default App;