import { useState, useEffect, useRef } from 'react';
import { Link } from 'react-router-dom';

function DropdownMenu({ currentPath }) {
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);
    const dropdownRef = useRef(null);
    
    const toggleDropdown = () => {
        setIsDropdownOpen((prev) => !prev);
    };
    
    const handleKeyDown = (e) => {
        if (e.key === 'Enter' || e.key === ' ') {
            e.preventDefault();
            toggleDropdown();
        }
    };
    
    useEffect(() => {
        const handleClickOutside = (event) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
                setIsDropdownOpen(false);
            }
        };
        
        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);
    
    return (
        <div className="dropdown" ref={dropdownRef}>
        <span
        className="nav-link dropdown-toggle"
        onClick={toggleDropdown}
        onKeyDown={handleKeyDown}
        role="button"
        aria-haspopup="true"
        aria-expanded={isDropdownOpen}
        tabIndex={0}
        >
        Mina Sidor
        </span>
        {isDropdownOpen && (
            <ul className="dropdown-menu" aria-expanded={isDropdownOpen}>
            <li>
            <Link
            to="/my-pages/settings"
            className="nav-link"
            onClick={() => setIsDropdownOpen(false)}
            aria-current={currentPath === '/my-pages/settings' ? 'page' : undefined}
            >
            Inställningar
            </Link>
            </li>
            <li>
            <Link
            to="/my-pages/cars"
            className="nav-link"
            onClick={() => setIsDropdownOpen(false)}
            aria-current={currentPath === '/my-pages/cars' ? 'page' : undefined}
            >
            Mina bilar
            </Link>
            </li>
            </ul>
        )}
        </div>
    );
}

export default DropdownMenu;