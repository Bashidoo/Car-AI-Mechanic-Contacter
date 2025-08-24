import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { carSchema } from '../data/carSchema';

function useCars() {
    const [formData, setFormData] = useState({ brand: '', regNumber: '' });
    const [cars, setCars] = useState([]);
    const [editId, setEditId] = useState(null);
    const [errors, setErrors] = useState({});
    const [success, setSuccess] = useState('');
    const navigate = useNavigate();
    
    // Helper function to safely decode token
    const decodeToken = (token) => {
        if (!token) return null;
        
        try {
            // Check if it's a JWT token (has 3 parts separated by dots)
            if (token.includes('.')) {
                const parts = token.split('.');
                if (parts.length === 3) {
                    // It's a JWT, decode the payload (middle part)
                    return JSON.parse(atob(parts[1]));
                }
            }
            
            // If it's not a JWT, try to parse it as JSON directly
            return JSON.parse(token);
        } catch (err) {
            console.error('Token decode error:', err.message);
            return null;
        }
    };
    
    // Load user cars on mount
    useEffect(() => {
        const token = localStorage.getItem('userToken');
        console.log('useEffect Token:', token);
        
        if (!token) {
            setErrors({ api: 'Ingen token hittades. Logga in.' });
            navigate('/login');
            return;
        }
        
        const decoded = decodeToken(token);
        if (!decoded) {
            console.error('Failed to decode token');
            setErrors({ api: 'Ogiltig token. Logga in igen.' });
            localStorage.removeItem('userToken'); // Clear invalid token
            navigate('/login');
            return;
        }
        
        console.log('useEffect Decoded token:', decoded);
        
        if (!decoded.email) {
            console.error('Token missing email field:', decoded);
            setErrors({ api: 'Ogiltig token. Inget email-fält.' });
            localStorage.removeItem('userToken');
            navigate('/login');
            return;
        }
        
        try {
            const users = JSON.parse(localStorage.getItem('users') || '[]');
            const user = users.find((u) => u.email === decoded.email);
            if (user) {
                setCars(user.cars || []);
            } else {
                setErrors({ api: 'Användaren hittades inte.' });
            }
        } catch (err) {
            console.error('Error loading user data:', err.message);
            setErrors({ api: 'Kunde inte ladda användardata.' });
        }
    }, [navigate]);
    
    const saveCar = ({ formData, errors }) => {
        console.log('saveCar called with:', { formData, errors });
        
        if (errors && Object.keys(errors).length > 0) {
            console.log('Validation errors:', errors);
            setErrors(errors);
            return;
        }
        
        try {
            const token = localStorage.getItem('userToken');
            console.log('Token:', token);
            
            if (!token) {
                setErrors({ api: 'Ingen giltig token hittades. Logga in igen.' });
                navigate('/login');
                return;
            }
            
            const decoded = decodeToken(token);
            if (!decoded) {
                setErrors({ api: 'Ogiltig token. Logga in igen.' });
                localStorage.removeItem('userToken');
                navigate('/login');
                return;
            }
            
            console.log('Decoded token:', decoded);
            
            if (!decoded.email) {
                console.error('Token missing email field:', decoded);
                setErrors({ api: 'Ogiltig token. Inget email-fält.' });
                localStorage.removeItem('userToken');
                navigate('/login');
                return;
            }
            
            const users = JSON.parse(localStorage.getItem('users') || '[]');
            console.log('Users:', users);
            const userIndex = users.findIndex((u) => u.email === decoded.email);
            
            if (userIndex === -1) {
                console.error('User not found:', decoded.email);
                setErrors({ api: 'Användaren hittades inte.' });
                return;
            }
            
            let updatedCars = users[userIndex].cars || [];
            console.log('Current cars:', updatedCars);
            
            if (editId) {
                // Update existing car
                updatedCars = updatedCars.map((car) =>
                    car.id === editId ? { ...car, ...formData } : car
                );
                setSuccess('Bil uppdaterad!');
                setEditId(null);
            } else {
                // Add new car
                if (updatedCars.some((car) => car.regNumber === formData.regNumber)) {
                    setErrors({ regNumber: 'Registreringsnummer finns redan.' });
                    return;
                }
                updatedCars.push({
                    id: Date.now().toString(),
                    brand: formData.brand,
                    regNumber: formData.regNumber,
                });
                setSuccess('Bil tillagd!');
            }
            
            users[userIndex] = { ...users[userIndex], cars: updatedCars };
            
            try {
                localStorage.setItem('users', JSON.stringify(users));
                console.log('Saved to localStorage:', users);
            } catch (err) {
                console.error('localStorage write error:', err.message);
                setErrors({ api: 'Kunde inte skriva till localStorage. Kontrollera lagringsutrymme.' });
                return;
            }
            
            setCars(updatedCars);
            setFormData({ brand: '', regNumber: '' });
            
        } catch (err) {
            console.error('Error in saveCar:', err.message);
            setErrors({ api: 'Kunde inte spara bilen. Försök igen.' });
        }
    };

    const editCar = (car) => {
        setFormData({ brand: car.brand, regNumber: car.regNumber });
        setEditId(car.id);
        setErrors({});
        setSuccess('');
    };

    const deleteCar = (id) => {
        try {
            const token = localStorage.getItem('userToken');
            console.log('deleteCar Token:', token);
            
            if (!token) {
                setErrors({ api: 'Ingen giltig token hittades. Logga in igen.' });
                navigate('/login');
                return;
            }
            
            const decoded = decodeToken(token);
            if (!decoded) {
                setErrors({ api: 'Ogiltig token. Logga in igen.' });
                localStorage.removeItem('userToken');
                navigate('/login');
                return;
            }
            
            console.log('deleteCar Decoded token:', decoded);
            
            const users = JSON.parse(localStorage.getItem('users') || '[]');
            const userIndex = users.findIndex((u) => u.email === decoded.email);
            
            if (userIndex === -1) {
                console.error('User not found:', decoded.email);
                setErrors({ api: 'Användaren hittades inte.' });
                return;
            }
            
            const updatedCars = users[userIndex].cars.filter((car) => car.id !== id);
            users[userIndex] = { ...users[userIndex], cars: updatedCars };
            
            try {
                localStorage.setItem('users', JSON.stringify(users));
                console.log('deleteCar Saved to localStorage:', users);
            } catch (err) {
                console.error('deleteCar localStorage write error:', err.message);
                setErrors({ api: 'Kunde inte radera bilen. Försök igen.' });
                return;
            }
            
            setCars(updatedCars);
            setSuccess('Bil raderad!');
            
        } catch (err) {
            console.error('Error in deleteCar:', err.message);
            setErrors({ api: 'Kunde inte radera bilen. Försök igen.' });
        }
    };

    return {
        formData,
        setFormData,
        cars,
        errors,
        success,
        isEditing: !!editId,
        saveCar,
        editCar,
        deleteCar,
    };
}

export default useCars;