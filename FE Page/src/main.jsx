import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { createHashRouter, RouterProvider } from 'react-router-dom';
import App from './App.jsx';
import StartPage from './pages/StartPage.jsx';
import HowItWorksPage from './pages/HowItWorksPage.jsx';
import RegisterPage from './pages/RegisterPage.jsx';
import LoginPage from './pages/LoginPage.jsx';
import HomePage from './pages/HomePage.jsx';
import FeatureFormPage from './pages/MainFeaturePage.jsx';
import ProtectedRoute from './components/ProtectedRoute.jsx';
import MyPagesPage from './pages/MyPages.jsx';
import SettingsPage from './pages/SettingsPage.jsx';
import CarsPage from './pages/CarsPage.jsx';


const router = createHashRouter([
  {
    path: '/',
    element: <App />,
    children: [
      {
        index: true,
        element: <StartPage />,
      },
      {
        path: '/how-it-works',
        element: <HowItWorksPage />,
      },
      {
        path: '/register',
        element: <RegisterPage />,
      },
      {
        path: '/login',
        element: <LoginPage />,
      },
      {
        element: <ProtectedRoute />,
        children: [
          {
            path: '/home',
            element: <HomePage />,
          },
          {
            path: '/my-pages',
            element: <MyPagesPage />,
          },
          {
            path: '/my-pages/settings',
            element: <SettingsPage />,
          },
          {
            path: '/my-pages/cars',
            element: <CarsPage />,
          },
          {
            path: '/feature-form',
            element: <FeatureFormPage />,
          },
        ],
      },
    ],
  },
]);

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
);