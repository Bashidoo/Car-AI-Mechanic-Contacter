import { useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { loginSchema } from '../data/loginSchema';
import Logo from '../images/Vector1.svg';
import BigWheel from '../images/Vector.svg';
import './LoginPage.css';
import './coversAll.css';

function LoginPage() {
  const [formData, setFormData] = useState({
    email: '',
    password: '',
  });
  const [errors, setErrors] = useState({});
  const navigate = useNavigate();
  const { state } = useLocation();
  const API = import.meta.env.VITE_API_URL;
  

  // Debug
  if (import.meta.env.DEV) {
  console.log("API URL:", import.meta.env.VITE_API_URL);
}
  // Debug
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrors({});

    // 1) client-side validation
    const { error } = loginSchema.validate(formData, { abortEarly: false });
    if (error) {
      const fieldErrors = {};
      error.details.forEach(err => {
        fieldErrors[err.path[0]] = err.message;
      });
      setErrors(fieldErrors);
      return;
    }

    // 2) call your API
    try {
      const resp = await fetch(`${API}/Auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(formData),
      });
      const data = await resp.json();
      if (!resp.ok) {
        setErrors({ api: data.error || 'Fel e-post eller lösenord.' });
        return;
      }

      // 3) store the JWT
      localStorage.setItem('userToken', data.token);
      // 4) redirect to home
      navigate('/home');
    } catch {
      setErrors({ api: 'Kunde inte logga in. Försök igen senare.' });
    }
  };

  return (
    <div className="page-container">
		<div className="login-wrapper">
			<div className="login-logo-container">
				
                  <img src={Logo} alt="Verkstadium logotyp" className="logo" />
                
				<div className='text-container'>
					<h1>Verkstadium</h1>
					<p>Vi har koll på verkstäder nära dig!</p>

					
				</div>
				</div>
			</div>
			<img src={BigWheel} alt="bakgrunds-dekoration" className="background-wheel"/>
		
      
      {state?.success && <p className="success">{state.success}</p>}
      {errors.api && <p className="error api-error">{errors.api}</p>}
      <form className="form" onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="email">E-post:</label>
          <input
            type="email"
            name="email"
            id="email"
            placeholder="E-post"
            className="input"
            value={formData.email}
            onChange={handleChange}
          />
          <p className="error">{errors.email || '\u00A0'}</p>
        </div>
        <div className="form-group">
          <label htmlFor="password">Lösenord:</label>
          <input
            type="password"
            name="password"
            id="password"
            placeholder="Lösenord"
            className="input"
            value={formData.password}
            onChange={handleChange}
          />
          <p className="error">{errors.password || '\u00A0'}</p>
        </div>

       


	  


      <button type="submit" className="login-btn">
          Logga in
        </button>
      </form> 
	  
      <Link to="/register" className="link">Har du inget konto? Registrera dig</Link>
    </div>
  );
}

export default LoginPage;