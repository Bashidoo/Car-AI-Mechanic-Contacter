import { useLocation, Link } from 'react-router-dom';
import Logo from '../images/Vector1.svg';
import BigWheel from '../images/Vector.svg';
import './MyPages.css';
import './coversAll.css';
import LogoutButton from '../components/LogoutButton';

function MyPagesPage() {
  const { state } = useLocation();

  return (
    <div className="page-container">
      {/* Header / Branding */}
	  <div className='background-wrapper'>
				<img src={BigWheel} alt="bakgrunds-dekoration" className="background-wheel"/>
			</div>
      <div className="login-wrapper">
        <div className="logo-container">
          <Link to="/home">
            <img src={Logo} alt="Verkstadium logotyp" className="logo" />
          </Link>
          <div className="text-container">
            <h1>Verkstadium</h1>
            <p>Vi har koll på verkstäder nära dig!</p>
            <Link to="/how-it-works" className="how-it-works">
              Hur funkar det?
            </Link>
          </div>
        </div>
        
      </div>

      {/* Main Content */}
      <h1 className="page-title">Mina sidor</h1>
      {state?.success && <p className="success">{state.success}</p>}
      <p className="page-text">Se ditt personliga innehåll och inställningar här.</p>

      <div className="button-group">
        <Link to="/my-pages/settings" className="settings-btn">
          Inställningar
        </Link>
        <Link to="/my-pages/cars" className="myCar-btn">
          Mina bilar
        </Link>
        
		</div>
		<Link to="/home" className="back-home-btn">
  Tillbaka till hem
</Link>
<div className="logout-btn">
  <LogoutButton />
</div>
      </div>
    
  );
}

export default MyPagesPage;
