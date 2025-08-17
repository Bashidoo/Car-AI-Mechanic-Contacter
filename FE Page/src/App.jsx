import { Outlet } from 'react-router-dom';

function App() {
  return (
    <div>
      {/* Header, navbar, or other layout components */}
      <header>
        <h1></h1>
      </header>

      {/* Render child routes */}
      <main>
        <Outlet />
      </main>

      {/* Footer */}
      <footer>
        <p>&copy; 2025 CarDealership</p>
      </footer>
    </div>
  );
}

export default App;
