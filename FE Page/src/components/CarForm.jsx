import { carSchema } from '../data/carSchema';

function CarForm({ formData, setFormData, errors, onSubmit, isEditing }) {
  const handleChange = (e) => {
    const { name, value } = e.target;
    // Normalize regNumber: trim spaces, convert to uppercase
    const normalizedValue = name === 'regNumber' ? value.trim().toUpperCase() : value;
    setFormData({ ...formData, [name]: normalizedValue });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const { error } = carSchema.validate(formData, { abortEarly: false });
    if (error) {
      const newErrors = {};
      error.details.forEach((err) => {
        newErrors[err.path[0]] = err.message;
      });
      onSubmit({ errors: newErrors });
      return;
    }
    onSubmit({ formData });
  };

  return (
    <form className="form" onSubmit={handleSubmit}>
      <div className="form-group">
        <input
          type="text"
          name="brand"
          placeholder="Märke"
          className="input"
          value={formData.brand}
          onChange={handleChange}
        />
        <p className="error">{errors.brand || '\u00A0'}</p>
      </div>
      <div className="form-group">
        <input
          type="text"
          name="regNumber"
          placeholder="Registreringsnummer (t.ex. ABC123 eller ABC 77A)"
          className="input"
          value={formData.regNumber}
          onChange={handleChange}
        />
        <p className="error">{errors.regNumber || '\u00A0'}</p>
      </div>
      <button type="submit" className="button button-primary">
        {isEditing ? 'Uppdatera bil' : 'Lägg till bil'}
      </button>
    </form>
  );
}

export default CarForm;