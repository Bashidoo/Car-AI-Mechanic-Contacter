import CarForm from '../components/CarForm';
import CarList from '../components/CarList';
import useCars from '../data/useCars';

function CarsPage() {
    const { formData, setFormData, cars, errors, success, isEditing, saveCar, editCar, deleteCar } = useCars();
    
    return (
        <div className="page-container">
        <h1 className="page-title">Mina bilar</h1>
        {errors.api && <p className="error api-error">{errors.api}</p>}
        {success && <p className="success">{success}</p>}
        <CarForm
        formData={formData}
        setFormData={setFormData}
        errors={errors}
        onSubmit={saveCar}
        isEditing={isEditing}
        />
        <CarList cars={cars} onEdit={editCar} onDelete={deleteCar} />
        </div>
    );
}

export default CarsPage;