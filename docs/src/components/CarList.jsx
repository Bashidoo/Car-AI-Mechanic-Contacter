function CarList({ cars, onEdit, onDelete }) {
    if (!cars.length) {
        return <p className="page-text">Inga bilar registrerade.</p>;
    }
    
    return (
        <div className="car-list">
        <h2 className="page-text">Dina bilar</h2>
        <ul>
        {cars.map((car) => (
            <li key={car.id} className="car-item">
            <span>{car.brand} - {car.regNumber}</span>
            <div className="button-group">
            <button
            className="button button-secondary"
            onClick={() => onEdit(car)}
            >
            Redigera
            </button>
            <button
            className="button button-tertiary"
            onClick={() => window.confirm('Vill du radera denna bil?') && onDelete(car.id)}
            >
            Radera
            </button>
            </div>
            </li>
        ))}
        </ul>
		
        </div>
    );
}

export default CarList;