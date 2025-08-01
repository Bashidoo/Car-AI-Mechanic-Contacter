import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

function FormComponent({
  cars,
  formData,
  errors,
  success,
  isEditing,
  handleChange,
  handleSubmit,
  handleCancelEdit,
}) {
  return (
    <>
      <h1 className="page-title">Funktionsformulär</h1>
      {errors.api && <p className="error api-error">{errors.api}</p>}
      {success && <p className="success">{success}</p>}
      <form className="form" onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="selectedCar">Välj bil</label>
          <select
            id="selectedCar"
            name="selectedCar"
            className="input"
            value={formData.selectedCar}
            onChange={handleChange}
          >
            <option value="">Välj bil</option>
            {cars.map((car) => (
              <option key={car.id} value={`${car.brand} ${car.regNumber}`}>
                {car.brand} ({car.regNumber})
              </option>
            ))}
          </select>
          <p className="error">{errors.selectedCar || '\u00A0'}</p>
        </div>
        <div className="form-group">
          <label htmlFor="diagnosisReport">Diagnosrapport (Här ska du skriva exakt som står i felsökningsrapport)</label>
          <textarea
            id="diagnosisReport"
            name="diagnosisReport"
            placeholder="Diagnosrapport (Här ska du skriva exakt som står i felsökningsrapport)"
            className="textarea"
            value={formData.diagnosisReport}
            onChange={handleChange}
            rows="5"
            style={{ resize: 'vertical' }}
          />
          <p className="error">{errors.diagnosisReport || '\u00A0'}</p>
        </div>
        <div className="form-group">
          <label htmlFor="city">Jag söker verkstäder i</label>
          <input
            id="city"
            type="text"
            name="city"
            placeholder="Jag söker verkstäder i"
            className="input"
            value={formData.city}
            onChange={handleChange}
          />
          <p className="error">{errors.city || '\u00A0'}</p>
        </div>
        <div className="button-group">
          <button type="submit" className="button button-tertiary">
            {isEditing ? 'Spara' : 'Generera'}
          </button>
          {isEditing && (
            <button
              type="button"
              onClick={handleCancelEdit}
              className="button button-secondary"
            >
              Avbryt
            </button>
          )}
        </div>
      </form>
      <Link to="/home" className="back-home-btn">Tillbaka till hem</Link>
    </>
  );
}

FormComponent.propTypes = {
  cars: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.string,
      brand: PropTypes.string,
      regNumber: PropTypes.string,
    })
  ).isRequired,
  formData: PropTypes.shape({
    selectedCar: PropTypes.string,
    diagnosisReport: PropTypes.string,
    city: PropTypes.string,
  }).isRequired,
  errors: PropTypes.object.isRequired,
  success: PropTypes.string.isRequired,
  isEditing: PropTypes.bool.isRequired,
  handleChange: PropTypes.func.isRequired,
  handleSubmit: PropTypes.func.isRequired,
  handleCancelEdit: PropTypes.func.isRequired,
};

export default FormComponent;