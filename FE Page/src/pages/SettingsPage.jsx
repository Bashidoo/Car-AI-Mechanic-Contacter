import { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { registerSchema } from '../data/registerSchema';
import Logo from '../images/Vector1.svg';
import BigWheel from '../images/Vector.svg';
import './SettingsPage.css';
import './coversAll.css';

function SettingsPage() {
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
    
    // Load user data on mount
    useEffect(() => {
        const token = localStorage.getItem('userToken');
        if (!token) {
            navigate('/login');
            return;
        }
        try {
            const decoded = JSON.parse(atob(token));
            const users = JSON.parse(localStorage.getItem('users') || '[]');
            const user = users.find((u) => u.email === decoded.email);
            if (user) {
                setFormData({
                    firstName: user.firstName || '',
                    lastName: user.lastName || '',
                    email: user.email || '',
                    mobile: user.mobile || '',
                    address: user.address || '',
                    postcode: user.postcode || '',
                    city: user.city || '',
                    password: '',
                    confirmPassword: '',
                });
            } else {
                setErrors({ api: 'Användaren hittades inte.' });
            }
        } catch {
            setErrors({ api: 'Kunde inte ladda användardata.' });
        }
    }, [navigate]);
    
    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        setErrors({});
        
        // Validate form data with Joi schema
        const { error } = registerSchema.validate(formData, { abortEarly: false });
        if (error) {
            const newErrors = {};
            error.details.forEach((err) => {
                newErrors[err.path[0]] = err.message;
            });
            setErrors(newErrors);
            return;
        }
        
        // *** START: Local Storage Implementation ***
        try {
            const users = JSON.parse(localStorage.getItem('users') || '[]');
            const userIndex = users.findIndex((u) => u.email === formData.email);
            if (userIndex === -1) {
                setErrors({ api: 'Användaren hittades inte.' });
                return;
            }
            
            // Update user data (exclude empty password if unchanged)
            const updatedUser = {
                ...users[userIndex],
                firstName: formData.firstName,
                lastName: formData.lastName,
                mobile: formData.mobile,
                address: formData.address,
                postcode: formData.postcode,
                city: formData.city,
                password: formData.password || users[userIndex].password, // Keep old password if empty
                updatedAt: new Date().toISOString(),
            };
            
            users[userIndex] = updatedUser;
            localStorage.setItem('users', JSON.stringify(users));
            
            navigate('/my-pages', { state: { success: 'Uppgifterna har sparats!' } });
        } catch (err) {
            setErrors({ api: 'Kunde inte spara ändringar. Försök igen.' });
        }
        // *** END: Local Storage Implementation ***
        
        // *** REPLACE WITH API CALL IN FUTURE ***
        /*
        try {
        const response = await fetch('/api/update-user', {
        method: 'PUT',
        headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('userToken')}`,
        },
        body: JSON.stringify({
        firstName: formData.firstName,
        lastName: formData.lastName,
        mobile: formData.mobile,
        address: formData.address,
        postcode: formData.postcode,
        city: formData.city,
        password: formData.password || undefined,
        }),
        });
        const data = await response.json();
        if (!response.ok) {
        throw new Error(JSON.stringify(data.error || { api: 'Kunde inte spara ändringar.' }));
        }
        navigate('/my-pages', { state: { success: 'Uppgifterna har sparats!' } });
        } catch (err) {
        const errorObj = JSON.parse(err.message);
        setErrors(errorObj);
        }
        */
    };
    
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
            
          </div>
        </div>
		</div>
        <h1 className="page-title">Inställningar</h1>
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
        disabled
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
        placeholder="Nytt lösenord (lämna tomt för att behålla)"
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
        placeholder="Bekräfta nytt lösenord"
        className="input"
        value={formData.confirmPassword}
        onChange={handleChange}
        />
        <p className="error">{errors.confirmPassword || '\u00A0'}</p>
        </div>
		</div>
        <div className="button-group">
        <button type="submit" className="button button-primary">
        Spara
        </button>
        <Link to="/my-pages" className="button button-secondary">
        Avbryt
        </Link>
        </div>
        </form>
        </div>
    );
}

export default SettingsPage;