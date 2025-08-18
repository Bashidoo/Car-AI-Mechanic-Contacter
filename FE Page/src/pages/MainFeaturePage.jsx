import { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import useCars from '../data/useCars';
import FormComponent from '../components/FeatureFormComponent';
import EmailPreviewComponent from '../components/EmailPreviewComponent';
import SuccessMessageComponent from '../components/SuccessMessageComponent';
import Logo from '../images/Vector1.svg';
import BigWheel from '../images/Vector.svg';
import './MainFeaturePage.css';
import './coversAll.css';
import LogoutButton from '../components/LogoutButton';

function FeatureFormPage() {
  const { cars } = useCars();
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    selectedCar: '',
    diagnosisReport: '',
    city: '',
  });
  const [originalDiagnosisReport, setOriginalDiagnosisReport] = useState('');
  const [errors, setErrors] = useState({});
  const [user, setUser] = useState(null);
  const [showEmail, setShowEmail] = useState(false);
  const [emailContent, setEmailContent] = useState('');
  const [success, setSuccess] = useState('');
  const [isEditing, setIsEditing] = useState(false);
  const [currentSubmissionId, setCurrentSubmissionId] = useState(null);
  const [showSuccessOnly, setShowSuccessOnly] = useState(false);
  const [isGenerating, setIsGenerating] = useState(false);

  const API = import.meta.env.VITE_API_URL;

  // Helper function to safely decode JWT token
  const decodeToken = (token) => {
    try {
      if (!token) return null;
      
      // Check if it's a JWT token (has 3 parts separated by dots)
      if (token.includes('.')) {
        const parts = token.split('.');
        if (parts.length === 3) {
          return JSON.parse(atob(parts[1]));
        }
      }
      
      // If it's not a JWT, try to parse as JSON directly
      return JSON.parse(token);
    } catch (err) {
      console.error('Token decode error:', err);
      return null;
    }
  };

  // Fetch user's details and city from localStorage on component mount
  useEffect(() => {
    const token = localStorage.getItem('userToken');
    if (!token) {
      navigate('/login');
      return;
    }
    
    const decoded = decodeToken(token);
    if (!decoded) {
      console.error('Failed to decode token');
      setErrors({ api: 'Ogiltig token. Logga in igen.' });
      localStorage.removeItem('userToken');
      navigate('/login');
      return;
    }
    
    try {
      const users = JSON.parse(localStorage.getItem('users') || '[]');
      const currentUser = users.find((u) => u.email === decoded.email);
      if (currentUser) {
        setUser(currentUser);
        setFormData((prev) => ({ ...prev, city: currentUser.city || '' }));
      } else {
        // Create user if doesn't exist (fallback)
        const newUser = {
          email: decoded.email,
          fullName: decoded.fullName || 'Unknown User',
          firstName: decoded.fullName?.split(' ')[0] || 'Unknown',
          lastName: decoded.fullName?.split(' ').slice(1).join(' ') || 'User',
          city: '',
          cars: []
        };
        const users = JSON.parse(localStorage.getItem('users') || '[]');
        users.push(newUser);
        localStorage.setItem('users', JSON.stringify(users));
        setUser(newUser);
      }
    } catch (err) {
      console.error('Error loading user data:', err);
      setErrors({ api: 'Kunde inte ladda användardata.' });
    }
  }, [navigate]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
    if (name === 'diagnosisReport') {
      setOriginalDiagnosisReport(value);
    }
  };

  const generateAIEmail = async (prompt) => {
    try {
      const token = localStorage.getItem('userToken');
      if (!token) {
        throw new Error('No authentication token');
      }

      const response = await fetch(`${API}/AIGeneratedReport/generate`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` // Add auth header
        },
        body: JSON.stringify({
          prompt: prompt
        })
      });

      const data = await response.json();
      console.log('AI API Response:', data);

      if (!response.ok) {
        throw new Error(data.message || 'Failed to generate AI response');
      }

      if (data.success && data.data) {
        return data.data;
      } else {
        throw new Error(data.message || 'AI generation failed');
      }
    } catch (err) {
      console.error('AI Generation Error:', err);
      throw err;
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrors({});
    setIsGenerating(true);

    // Basic validation
    const newErrors = {};
    if (!formData.selectedCar) {
      newErrors.selectedCar = 'Välj en bil';
    }
    if (!formData.diagnosisReport) {
      newErrors.diagnosisReport = 'Diagnosrapport krävs';
    }
    if (!formData.city) {
      newErrors.city = 'Stad krävs';
    }

    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      setIsGenerating(false);
      return;
    }

    try {
      // Create the prompt for AI
      const prompt = `Bil: ${formData.selectedCar}\nDiagnosrapport: ${formData.diagnosisReport}\nStad: ${formData.city}\nAnvändare: ${user?.fullName || user?.firstName + ' ' + user?.lastName}`;
      
      console.log('Calling AI API with prompt:', prompt);
      
      // Call your AI API
      const aiGeneratedEmail = await generateAIEmail(prompt);
      
      console.log('AI Generated Email:', aiGeneratedEmail);
      
      // Save to localStorage
      const submission = {
        id: currentSubmissionId || Date.now().toString(),
        ...formData,
        userEmail: user.email,
        userName: user?.fullName || `${user?.firstName} ${user?.lastName}`,
        aiGeneratedEmail: aiGeneratedEmail, // Store the AI generated content
        createdAt: new Date().toISOString(),
      };
      
      const submissions = JSON.parse(localStorage.getItem('serviceRequests') || '[]');

      if (isEditing && currentSubmissionId) {
        // Update existing submission
        const submissionIndex = submissions.findIndex((s) => s.id === currentSubmissionId);
        if (submissionIndex !== -1) {
          submissions[submissionIndex] = submission;
        }
        setSuccess('Ändringar sparade!');
      } else {
        // Add new submission
        submissions.push(submission);
        setCurrentSubmissionId(submission.id);
        setSuccess('Förfrågan genererad!');
      }

      localStorage.setItem('serviceRequests', JSON.stringify(submissions));

      // Use the AI-generated email content
      setEmailContent(aiGeneratedEmail);
      setShowEmail(true);
      setIsEditing(false);
      
    } catch (err) {
      console.error('Error in handleSubmit:', err);
      setErrors({ 
        api: `Kunde inte generera e-post: ${err.message}` 
      });
    } finally {
      setIsGenerating(false);
    }
  };

  const handleReset = () => {
    setFormData({
      selectedCar: '',
      diagnosisReport: '',
      city: user?.city || '',
    });
    setOriginalDiagnosisReport('');
    setErrors({});
    setShowEmail(false);
    setEmailContent('');
    setSuccess('');
    setIsEditing(false);
    setCurrentSubmissionId(null);
    setShowSuccessOnly(false);
  };

  const handleEditEmail = () => {
    setFormData((prev) => ({ ...prev, diagnosisReport: originalDiagnosisReport }));
    setShowEmail(false);
    setSuccess('');
    setIsEditing(true);
  };

  const handleCancelEdit = () => {
    setShowEmail(true);
    setFormData({
      selectedCar: formData.selectedCar,
      diagnosisReport: originalDiagnosisReport,
      city: formData.city,
    });
    setErrors({});
    setSuccess('');
    setIsEditing(false);
  };

  const handleSendEmail = () => {
    console.log('Sending email to repair stations in', formData.city, ':', emailContent);
    setShowSuccessOnly(true);
    setShowEmail(false);
    setSuccess('E-post har skickats och vi meddelar dig så snart vi får svar.');
  };

  return (
    <div className="page-container">
      <div className="login-wrapper">
        <div className="logo-container">
          <Link to="/home">
            <img src={Logo} alt="Verkstadium logotyp" className="logo" />
          </Link>
          <div className="text-container">
            <h1>Verkstadium</h1>
            <p>Vi har koll på verkstäder nära dig!</p>
            <Link to="/how-it-works" className="how-it-works">
              Hur funkar det?
            </Link>
            <LogoutButton />
          </div>
        </div>
      </div>

      {showSuccessOnly ? (
        <SuccessMessageComponent message={success} />
      ) : (
        <>
          <img src={BigWheel} alt="bakgrunds-dekoration" className="background-wheel" />
          {showEmail ? (
            <EmailPreviewComponent
              user={user}
              formData={formData}
              emailContent={emailContent}
              errors={errors}
              success={success}
              handleReset={handleReset}
              handleEditEmail={handleEditEmail}
              handleSendEmail={handleSendEmail}
            />
          ) : (
            <FormComponent
              cars={cars}
              formData={formData}
              errors={errors}
              success={success}
              isEditing={isEditing}
              isGenerating={isGenerating} // Pass loading state to show spinner
              handleChange={handleChange}
              handleSubmit={handleSubmit}
              handleCancelEdit={handleCancelEdit}
            />
          )}
        </>
      )}
    </div>
  );
}

export default FeatureFormPage;
 /*import { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import useCars from '../data/useCars';
import FormComponent from '../components/FeatureFormComponent';
import EmailPreviewComponent from '../components/EmailPreviewComponent';
import SuccessMessageComponent from '../components/SuccessMessageComponent';
import Logo from '../images/Vector1.svg';
import BigWheel from '../images/Vector.svg';
import './MainFeaturePage.css';
import './coversAll.css';
import LogoutButton from '../components/LogoutButton';

function FeatureFormPage() {
  const { cars } = useCars();
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    selectedCar: '',
    diagnosisReport: '',
    city: '',
  });
  const [originalDiagnosisReport, setOriginalDiagnosisReport] = useState('');
  const [errors, setErrors] = useState({});
  const [user, setUser] = useState(null);
  const [showEmail, setShowEmail] = useState(false);
  const [emailContent, setEmailContent] = useState('');
  const [success, setSuccess] = useState('');
  const [isEditing, setIsEditing] = useState(false);
  const [currentSubmissionId, setCurrentSubmissionId] = useState(null);
  const [showSuccessOnly, setShowSuccessOnly] = useState(false);

  // Fetch user's details and city from localStorage on component mount
  useEffect(() => {
    const token = localStorage.getItem('userToken');
    if (!token) {
      navigate('/login');
      return;
    }
    try {
      const decoded = JSON.parse(atob(token));
      const users = JSON.parse(localStorage.getItem('users') || '[]');
      const currentUser = users.find((u) => u.email === decoded.email);
      if (currentUser) {
        setUser(currentUser);
        setFormData((prev) => ({ ...prev, city: currentUser.city }));
      } else {
        setErrors({ api: 'Användaren hittades inte.' });
      }
    } catch {
      setErrors({ api: 'Kunde inte ladda användardata.' });
    }
  }, [navigate]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
    if (name === 'diagnosisReport') {
      setOriginalDiagnosisReport(value);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setErrors({});

    // Basic validation
    const newErrors = {};
    if (!formData.selectedCar) {
      newErrors.selectedCar = 'Välj en bil';
    }
    if (!formData.diagnosisReport) {
      newErrors.diagnosisReport = 'Diagnosrapport krävs';
    }
    if (!formData.city) {
      newErrors.city = 'Stad krävs';
    }

    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      return;
    }

    // Save to localStorage
    try {
      const submission = {
        id: currentSubmissionId || Date.now().toString(),
        ...formData,
        userEmail: user.email,
        userName: `${user.firstName} ${user.lastName}`,
        createdAt: new Date().toISOString(),
      };
      const submissions = JSON.parse(localStorage.getItem('serviceRequests') || '[]');

      if (isEditing && currentSubmissionId) {
        // Update existing submission
        const submissionIndex = submissions.findIndex((s) => s.id === currentSubmissionId);
        if (submissionIndex !== -1) {
          submissions[submissionIndex] = submission;
        }
        setSuccess('Ändringar sparade!');
      } else {
        // Add new submission
        submissions.push(submission);
        setCurrentSubmissionId(submission.id);
        setSuccess('Förfrågan genererad!');
      }

      localStorage.setItem('serviceRequests', JSON.stringify(submissions));

      // Generate AI-reformed email content
      const aiReformedReport = `Hej,\n\nJag söker en offert för reparation av min bil (${formData.selectedCar}) baserat på följande diagnosrapport:\n\n${formData.diagnosisReport}\n\nVänligen ange kostnad och tid för reparation.\n\nVänliga hälsningar,\n${user.firstName} ${user.lastName}`;
      setEmailContent(aiReformedReport);
      setShowEmail(true);
      setIsEditing(false);
    } catch (err) {
      setErrors({ api: 'Kunde inte spara förfrågan. Försök igen.' });
    }
  };

  const handleReset = () => {
    setFormData({
      selectedCar: '',
      diagnosisReport: '',
      city: user?.city || '',
    });
    setOriginalDiagnosisReport('');
    setErrors({});
    setShowEmail(false);
    setEmailContent('');
    setSuccess('');
    setIsEditing(false);
    setCurrentSubmissionId(null);
    setShowSuccessOnly(false);
  };

  const handleEditEmail = () => {
    setFormData((prev) => ({ ...prev, diagnosisReport: originalDiagnosisReport }));
    setShowEmail(false);
    setSuccess('');
    setIsEditing(true);
  };

  const handleCancelEdit = () => {
    setShowEmail(true);
    setFormData({
      selectedCar: formData.selectedCar,
      diagnosisReport: originalDiagnosisReport,
      city: formData.city,
    });
    setErrors({});
    setSuccess('');
    setIsEditing(false);
  };

  const handleSendEmail = () => {
    console.log('Sending email to repair stations in', formData.city, ':', emailContent);
    setShowSuccessOnly(true);
    setShowEmail(false);
    setSuccess('E-post har skickats och vi meddelar dig så snart vi får svar.');
  };

  return (
    <div className="page-container">
      <div className="login-wrapper">
        <div className="logo-container">
          <Link to="/home">
            <img src={Logo} alt="Verkstadium logotyp" className="logo" />
          </Link>
          <div className="text-container">
            <h1>Verkstadium</h1>
            <p>Vi har koll på verkstäder nära dig!</p>
            <Link to="/how-it-works" className="how-it-works">
              Hur funkar det?
            </Link>
            <LogoutButton />
          </div>
        </div>
      </div>

      {showSuccessOnly ? (
        <SuccessMessageComponent message={success} />
      ) : (
        <>
          <img src={BigWheel} alt="bakgrunds-dekoration" className="background-wheel" />
          {/* <h1 className="page-title">Funktionsformulär</h1> }
          {showEmail ? (
            <EmailPreviewComponent
              user={user}
              formData={formData}
              emailContent={emailContent}
              errors={errors}
              success={success}
              handleReset={handleReset}
              handleEditEmail={handleEditEmail}
              handleSendEmail={handleSendEmail}
            />
          ) : (
            <FormComponent
              cars={cars}
              formData={formData}
              errors={errors}
              success={success}
              isEditing={isEditing}
              handleChange={handleChange}
              handleSubmit={handleSubmit}
              handleCancelEdit={handleCancelEdit}
            />
          )}
          
        </>
      )}
    </div>
  );
}

export default FeatureFormPage; }*/