import { useNavigate } from 'react-router-dom';

function LogoutButton() {
    const navigate = useNavigate();
    
    const handleLogout = () => {
        localStorage.removeItem('userToken');
        navigate('/login');
    };
    
    const handleKeyDown = (e) => {
        if (e.key === 'Enter' || e.key === ' ') {
            handleLogout();
        }
    };
    
    return (
        <span
        className="my-pages"
        onClick={handleLogout}
        style={{ cursor: 'pointer' }}
        role="button"
        tabIndex={0}
        onKeyDown={handleKeyDown}
        >
        Logga ut
        </span>
    );
}

export default LogoutButton;