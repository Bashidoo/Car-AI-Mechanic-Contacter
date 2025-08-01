import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { registerSchema } from '../data/registerSchema';
import Logo from '../images/Vector1.svg';
import BigWheel from '../images/Vector.svg';
import './RegisterPage.css';
import './coversAll.css';

function RegisterPage() {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    mobile: '',
    address: '',
    postcode: '',
    city: '',
    password: '',
    confirmPassword: '',
  });
  const [errors, setErrors] = useState({});
  const navigate = useNavigate();
  const API = import.meta.env.VITE_API_URL;

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrors({});

    // 1) client-side Joi validation
    const { error } = registerSchema.validate(formData, { abortEarly: false });
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
      const resp = await fetch(`${API}/auth/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(formData),
      });
      const data = await resp.json();
      if (!resp.ok) {
        setErrors(data.errors || { api: data.error || 'Registrering misslyckades.' });
        return;
      }

      // 3) on success, redirect to login
      navigate('/login', {
        state: { success: 'Registreringen lyckades! Logga in för att fortsätta.' }
      });
    } catch {
      setErrors({ api: 'Kunde inte registrera. Försök igen senare.' });
    }
  };

  return (
    <div className="page-container">
		<div className='background-wrapper'>
					<img src={BigWheel} alt="bakgrunds-dekoration" className="background-wheel"/>
				</div>
		<div className="login-wrapper">
		<div className="register-logo-container">
						<img src={Logo} alt="Verkstadium logotyp" className="logo"/>
							<div className='text-container'>
								<h1>Verkstadium</h1>
								<p>Vi har koll på verkstäder nära dig!</p>
								
			  
								
							</div>
						</div>
						</div>
						
      <h1 className="page-title">Registrera ett konto!</h1>
      {errors.api && <p className="error api-error">{errors.api}</p>}
      <form className="form" onSubmit={handleSubmit}>
        <div className='form-grid'>
          <div className="form-group">
            <input
              type="text"
              name="firstName"
              placeholder="Namn"
              className="input"
              value={formData.firstName}
              onChange={handleChange}
            />
            <p className="error">{errors.firstName || '\u00A0'}</p>
          </div>
          <div className="form-group">
            <input
              type="text"
              name="lastName"
              placeholder="Efternamn"
              className="input"
              value={formData.lastName}
              onChange={handleChange}
            />
            <p className="error">{errors.lastName || '\u00A0'}</p>
          </div>
          <div className="form-group">
            <input
              type="email"
              name="email"
              placeholder="E-post"
              className="input"
              value={formData.email}
              onChange={handleChange}
            />
            <p className="error">{errors.email || '\u00A0'}</p>
          </div>
          <div className="form-group">
            <input
              type="tel"
              name="mobile"
              placeholder="Mobil"
              className="input"
              value={formData.mobile}
              onChange={handleChange}
            />
            <p className="error">{errors.mobile || '\u00A0'}</p>
          </div>
          <div className="form-group">
            <input
              type="text"
              name="address"
              placeholder="Adress"
              className="input"
              value={formData.address}
              onChange={handleChange}
            />
            <p className="error">{errors.address || '\u00A0'}</p>
          </div>
          <div className="form-group">
            <input
              type="text"
              name="postcode"
              placeholder="Postnummer"
              className="input"
              value={formData.postcode}
              onChange={handleChange}
            />
            <p className="error">{errors.postcode || '\u00A0'}</p>
          </div>
          <div className="form-group">
            <input
              type="text"
              name="city"
              placeholder="Postort"
              className="input"
              value={formData.city}
              onChange={handleChange}
            />
            <p className="error">{errors.city || '\u00A0'}</p>
          </div>
          <div className="form-group">
            <input
              type="password"
              name="password"
              placeholder="Lösenord"
              className="input"
              value={formData.password}
              onChange={handleChange}
            />
            <p className="error">{errors.password || '\u00A0'}</p>
          </div>
          <div className="form-group">
            <input
              type="password"
              name="confirmPassword"
              placeholder="Bekräfta lösenord"
              className="input"
              value={formData.confirmPassword}
              onChange={handleChange}
            />
            <p className="error">{errors.confirmPassword || '\u00A0'}</p>
          </div>
        </div>
        <div className="form-button-wrapper">
          <button type="submit" className="register-button">
            Registrera
          </button>
        </div>
      </form>
      <Link to="/login" className="link">Har du redan ett konto? Logga in</Link>
    </div>
  );
}

export default RegisterPage;