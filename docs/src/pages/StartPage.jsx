import { useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Logo from '../images/Vector1.svg';
import BigWheel from '../images/Vector.svg';
import './StartPage.css';
import './coversAll.css';

function StartPage() {
  const navigate = useNavigate();

  // Redirect to /home if user is logged in
  useEffect(() => {
    const token = localStorage.getItem('userToken');
    if (token) {
      navigate('/home');
    }
  }, [navigate]);

  return (
    <div className="page-container">
		<div className='background-wrapper'>
			<img src={BigWheel} alt="bakgrunds-dekoration" className="background-wheel"/>
		</div>
      <div className="start-login-wrapper">
        <div className="logo-container">
          <img src={Logo} alt="Verkstadium logotyp" className="logo" />
          <div className="text-container">
            <h1>Verkstadium</h1>
            <p>Vi har koll på verkstäder nära dig!</p>
            <Link to="/how-it-works" className="how-it-works">Hur funkar det?</Link>
          </div>
        </div>
       
        <div className="register-container">
          <p className="welcome-text">Ny användare?</p>
          <Link to="/register" className="welcome-text">Registrera dig!</Link>
        </div>
        <div className="login-container">
          <p className="welcome-text">Redan användare?</p>
          <Link to="/login" className="welcome-text">Logga in!</Link>
        </div>
      </div>
    </div>
  );
}

export default StartPage;