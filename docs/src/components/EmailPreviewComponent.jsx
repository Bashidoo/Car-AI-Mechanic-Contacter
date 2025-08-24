import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

function EmailPreviewComponent({
  user,
  formData,
  emailContent,
  errors,
  success,
  handleReset,
  handleEditEmail,
  handleSendEmail,
}) {
  return (
    <>
      <h1 className="page-title">E-postförhandsvisning</h1>
      {errors.api && <p className="error api-error">{errors.api}</p>}
      {success && <p className="success">{success}</p>}
      <div className="email-preview">
        <p className="page-text"><strong>Namn:</strong> {user?.firstName} {user?.lastName}</p>
        <p className="page-text"><strong>E-post:</strong> {user?.email}</p>
        <p className="page-text"><strong>Vald bil:</strong> {formData.selectedCar}</p>
        <hr />
        <p className="page-text"><strong>Förfrågan om service:</strong></p>
        <pre className="textarea" style={{ whiteSpace: 'pre-wrap', wordBreak: 'break-word' }}>{emailContent}</pre>
        <div className="button-group">
          <button onClick={handleReset} className="button button-secondary">
            Börja om
          </button>
          <button onClick={handleEditEmail} className="button button-secondary">
            Ändra
          </button>
          <button onClick={handleSendEmail} className="button button-primary">
            Skicka e-post
          </button>
        </div>
      </div>
      <Link to="/home" className="link">Tillbaka till hem</Link>
    </>
  );
}

EmailPreviewComponent.propTypes = {
  user: PropTypes.shape({
    firstName: PropTypes.string,
    lastName: PropTypes.string,
    email: PropTypes.string,
  }),
  formData: PropTypes.shape({
    selectedCar: PropTypes.string,
    diagnosisReport: PropTypes.string,
    city: PropTypes.string,
  }).isRequired,
  emailContent: PropTypes.string.isRequired,
  errors: PropTypes.object.isRequired,
  success: PropTypes.string.isRequired,
  handleReset: PropTypes.func.isRequired,
  handleEditEmail: PropTypes.func.isRequired,
  handleSendEmail: PropTypes.func.isRequired,
};

export default EmailPreviewComponent;