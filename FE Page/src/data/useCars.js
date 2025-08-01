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
    
    // Load user cars on mount
    useEffect(() => {
        const token = localStorage.getItem('userToken');
        console.log('useEffect Token:', token);
        if (!token) {
            setErrors({ api: 'Ingen token hittades. Logga in.' });
            navigate('/login');
            return;
        }
        try {
            const decoded = JSON.parse(atob(token));
            console.log('useEffect Decoded token:', decoded);
            const users = JSON.parse(localStorage.getItem('users') || '[]');
            const user = users.find((u) => u.email === decoded.email);
            if (user) {
                setCars(user.cars || []);
            } else {
                setErrors({ api: 'Användaren hittades inte.' });
            }
        } catch (err) {
            console.error('useEffect decode error:', err.message);
            setErrors({ api: 'Kunde inte ladda användardata.' });
            navigate('/login');
        }
    }, [navigate]);
    
    const saveCar = ({ formData, errors }) => {
        console.log('saveCar called with:', { formData, errors });
        if (errors) {
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
            
            let decoded;
            try {
                decoded = JSON.parse(atob(token));
                console.log('Decoded token:', decoded);
                if (!decoded.email) {
                    console.error('Token missing email field:', decoded);
                    setErrors({ api: 'Ogiltig token. Inget email-fält.' });
                    navigate('/login');
                    return;
                }
            } catch (err) {
                console.error('Token decode error:', err.message);
                setErrors({ api: 'Ogiltig token. Logga in igen.' });
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
                updatedCars = updatedCars.map((car) =>
                    car.id === editId ? { ...car, ...formData } : car
            );
            setSuccess('Bil uppdaterad!');
            setEditId(null);
        } else {
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
        
        let decoded;
        try {
            decoded = JSON.parse(atob(token));
            console.log('deleteCar Decoded token:', decoded);
        } catch (err) {
            console.error('deleteCar decode error:', err.message);
            setErrors({ api: 'Ogiltig token. Logga in igen.' });
            navigate('/login');
            return;
        }
        
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