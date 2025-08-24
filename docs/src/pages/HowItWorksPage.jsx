import { Link } from 'react-router-dom';
import Logo from '../images/Vector1.svg';
import BigWheel from '../images/Vector.svg';
import './HowItWorksPage.css';
import './coversAll.css';

function HowItWorksPage() {
  return (
    <div className="page-container">
		<div className='background-wrapper'>
											<img src={BigWheel} alt="bakgrunds-dekoration" className="background-wheel"/>
										</div>
      <div className="login-wrapper">
		 <div className="logo-container">
				<img src={Logo} alt="Verkstadium logotyp" className="logo"/>
					<div className='text-container'>
						<h1>Verkstadium</h1>
						<p>Vi har koll på verkstäder nära dig!</p>
						<p>Hur funkar det?</p>
	  
						
					</div>
					
				</div>
			

	<div className='text-box'>
						<h1>Hur funkar Verkstadium?</h1>
						<p className='description-text'>Verkstadium är tjänsten för dig som behöver hjälp med att fixa bilen.
							Vet du vad felet är eller har du en felkod? Skriv problemet till oss så hjälper vi dig att hitta närmaste verkstad till billigast pris.
						
						</p>
						<h2>Men hur gör man?</h2>

						<p className='description-text'>Det är väldigt enkelt! </p>
						<p className='description-text'>För att kunna få hjälp från oss behöver du ett konto. Så börja med att registrera dig som ny användare eller om du redan har ett konto, då är det bara att logga in. Sen behöver du registrera din bil eller de bilar du vill ha hjälp med. Efter detta går du vidare till vårat hjälp formulär. Där väljder du den bil du vill ha hjälp med. Skriver vilket problem bilen har som du vill ha hjälp med och där efter tar vi hand om resten. Innan du hunnit säga "Verkstadium" har vi hittat de närmsta verkstäderna med de billigaste priserna, du kommer få en notis på dina sidor där du kan kolla på och läsa mer om de verkstäder vi hittat åt dig! Självklart ser vi till att alla verkstäder vi hittar åt dig har riktiga kontaktuppgifter så det är lätt för dig att gå vidare och få ditt bil-problem löst.  </p>
						<p className='description-text'>Enkelt va? Registrera dig i dag och få hjälp med bilen på nolltid! </p>
						
					</div>
		
		 
		 </div> 

		 
      <Link to="/" className="go-back-btn">Tillbaka</Link>
    </div>
  );
}

export default HowItWorksPage;