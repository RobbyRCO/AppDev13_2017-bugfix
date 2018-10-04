using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Internship.Api.Models;
using Internship.Data.DomainClasses;
using Internship.Desktop.Data;
using Internship.Data.Repositories;

namespace Internship.Desktop
{
    public class ParseService
    {
        private readonly IStagevoorstelRepository _voorstelRepo = null;
        private readonly IBedrijfRepository _bedrijfRepo = null;
        private readonly IStageopdrachtenRepository _opdrachtRepo = null;
        private readonly ILectorRepository _lectorRepo = null;
        private readonly IStudentRepository _studentRepo = null;
        public const char DELIMITER = '\t';

        public ParseService(IStagevoorstelRepository voorstelRepo, IBedrijfRepository bedrijfRepo,
            IStageopdrachtenRepository opdrachtRepo, ILectorRepository lectorRepo, IStudentRepository studentRepo)
        {
            _voorstelRepo = voorstelRepo;
            _bedrijfRepo = bedrijfRepo;
            _opdrachtRepo = opdrachtRepo;
            _lectorRepo = lectorRepo;
            _studentRepo = studentRepo;
        }

        public static OudStagevoorstel ParseOudStageVoorstel(string[] elements)
        {
            try
            {
                int i = 0;
                OudStagevoorstel voorstel = new OudStagevoorstel();
                foreach (var p in voorstel.GetType().GetProperties().Where(p => p.GetGetMethod().GetParameters().Count() == 0))
                {
                    p.SetValue(voorstel, elements[i]);
                    i++;
                }

                return voorstel;
            }
            catch (FormatException)
            {
                throw new FormatException("Waarschuwing => Een of meerdere rijen konden niet omgezet worden");
            }
        }

        public static OudeStage ParseOudeStage(string[] elements)
        {
            try
            {
                OudeStage stage = new OudeStage();
                int i = 0;
                foreach (var p in stage.GetType().GetProperties().Where(p => p.GetGetMethod().GetParameters().Count() == 0))
                {
                    p.SetValue(stage, elements[i]);
                    i++;
                }
                return stage;
            }
            catch (FormatException)
            {
                throw new FormatException("Waarschuwing => Een of meerdere rijen konden niet omgezet worden");
            }
        }

        public async Task<string> InsertOudStageVoorstel(OudStagevoorstel oudVoorstel)
        {
            Stageopdracht stageopdracht;
            Bedrijf bedrijf = null;
            Stagevoorstel stagevoorstel;

            //HttpResponseMessage response = await new HttpClient().GetAsync("/api/bedrijf/"+ oudVoorstel.NaamBedrijfOpdrachtgever);
            //response.EnsureSuccessStatusCode(); // Throw on error code. 
            //var controle = await response.Content.ReadAsAsync<Bedrijf>();

            Bedrijf controle = _bedrijfRepo.GetByName(oudVoorstel.NaamBedrijfOpdrachtgever);
            if (controle == null)
            {
                bedrijf = ParseBedrijf(oudVoorstel.NaamBedrijfOpdrachtgever, oudVoorstel.StraatEnNummer, Convert.ToInt32(oudVoorstel.Postcode),
                    oudVoorstel.Gemeente, oudVoorstel.TelefoonnummerContactpersoon, oudVoorstel.EmailContactpersoon,
                    (bool)ConvertAanwezigheidHandShake(oudVoorstel.AanwezigHandshake));
                _bedrijfRepo.Post(bedrijf);
            }
            if (_voorstelRepo.GetByTimeStamp(Convert.ToDateTime(oudVoorstel.TimeStamp)) == null)
            {
                stageopdracht = ParseStageOpdracht(oudVoorstel.Locatie, oudVoorstel.OmschrijvingOpdracht,
                    oudVoorstel.Randvoorwaarden,
                    oudVoorstel.Onderzoeksthema, oudVoorstel.Onderzoeksthema2, oudVoorstel.Omgeving,
                    Convert.ToInt32(oudVoorstel.AantalMedewerkersIT),
                    Convert.ToInt32(oudVoorstel.AantalMedewerkers), (int)ConvertAantalStagiairs(oudVoorstel.AantalGewensteStagiars),
                    (Afstudeerrichting)ConvertToAfstudeerrichting(oudVoorstel.VoorkeurAfstudeerrichting),
                    (InleidendeActiviteit)ConvertToInleidendeActiviteit(oudVoorstel.InleidendeActiviteit),
                    _bedrijfRepo.GetByName(oudVoorstel.NaamBedrijfOpdrachtgever).Id);

                stageopdracht.Contactpersoon = ParseContactpersoon(oudVoorstel.NaamContactpersoon,
                    oudVoorstel.VoornaamContactpersoon,
                    oudVoorstel.EmailContactpersoon, oudVoorstel.TelefoonnummerContactpersoon,
                    oudVoorstel.TitelContactpersoon);

                stageopdracht.Bedrijfspromotor = ParseBedrijfsPromotor(oudVoorstel.NaamPromotor,
                    oudVoorstel.VoornaamPromotor,
                    oudVoorstel.EmailPromotor, oudVoorstel.TelefoonnummerPromotor, oudVoorstel.TitelPromotor,
                    (int)stageopdracht.OpdrachtgeverId);


                stagevoorstel = ParseStageVoorstel(Convert.ToDateTime(oudVoorstel.TimeStamp), oudVoorstel.Bemerkingen,
                    stageopdracht, bedrijf.Id,
                    ParseReview(oudVoorstel.FeedbackReviewer, _lectorRepo.GetByEmail(oudVoorstel.Reviewer)));
            }
            return null;
        }

        public string InsertOudeStage(OudeStage oudeStage)
        {
            Student student = null;
            Stageopdracht stageopdracht;
            Bedrijf bedrijf = null;

            if (_studentRepo.GetByName(oudeStage.VoornaamStudent, oudeStage.NaamStudent) == null)
            {
                student = ParseStudent(oudeStage.NaamStudent, oudeStage.VoornaamStudent,
                   oudeStage.MailStudent, oudeStage.GsmStudent);
            }

            Bedrijf controle = _bedrijfRepo.GetByName(oudeStage.NaamBedijf);
            if (controle == null)
            {
                bedrijf = ParseBedrijf(oudeStage.NaamBedijf, oudeStage.StraatBedrijf, Convert.ToInt32(oudeStage.PostcodeBedrijf),
                    oudeStage.GemeenteBedrijf, oudeStage.TelefoonContactPersoon, oudeStage.EmailContactPersoon,
                    (bool)ConvertAanwezigheidHandShake(oudeStage.AanwezigHandshake));
                _bedrijfRepo.Post(bedrijf);
            }

            //ParseStage(Convert.ToDateTime(oudeStage.BeginBP), Convert.ToDateTime(oudeStage.EindBP), )
            return null;
        }

        public Bedrijf ParseBedrijf
            (string bedrijfsnaam, string adres, int postcode, string gemeente, string telefoonnummer, string email, bool aanwezigHandShake)
        {
            return new Bedrijf
            {
                Bedrijfsnaam = bedrijfsnaam,
                Adres = adres,
                Postcode = postcode,
                Gemeente = gemeente,
                Telefoonnummer = telefoonnummer,
                Email = email,
                AanwezigheidHandshake = aanwezigHandShake
            };
        }

        public Stageopdracht ParseStageOpdracht
            (string locatie, string omschrijvingOpdracht, string randvoorwaarden, string onderzoeksthema,
            string beschrijvingTechnischeOmgeving, string omgeving, int medewerkersIT, int medewerkers, int gewensteStagiairs, Afstudeerrichting gewensteAfstudeerrichting,
            InleidendeActiviteit inleidendeActiviteit, int opdrachtgeverId)
        {
            return new Stageopdracht()
            {
                Locatie = locatie,
                Omschrijving = omschrijvingOpdracht,
                Randvoorwaarden = randvoorwaarden,
                Onderzoeksthema = onderzoeksthema,
                BeschrijvingTechnischeOmgeving = beschrijvingTechnischeOmgeving,
                Omgeving = omgeving,
                AantalITWerknemers = medewerkersIT,
                AantalWerknemers = medewerkers,
                AantalGewensteStagiairs = gewensteStagiairs,
                VoorkeurAfstudeerrichting = gewensteAfstudeerrichting,
                InleidendeActiviteiten = inleidendeActiviteit,
                OpdrachtgeverId = opdrachtgeverId
            };
        }

        public Contactpersoon ParseContactpersoon
            (string naam, string voornaam,
            string email, string telefoonnummer, string titel)
        {
            return new Contactpersoon
            {
                Achternaam = naam,
                Voornaam = voornaam,
                Email = email,
                Telefoonnummer = telefoonnummer,
                Title = titel
            };
        }

        public Bedrijfspromotor ParseBedrijfsPromotor
            (string naam, string voornaam, string email, string telefoonNummer,
             string titel, int bedrijfId)
        {
            return new Bedrijfspromotor
            {
                Achternaam = naam,
                Voornaam = voornaam,
                Email = email,
                Telefoonnummer = telefoonNummer,
                Title = titel,
                BedrijfInDienstId = bedrijfId
            };
        }

        public Stagevoorstel ParseStageVoorstel(DateTime timeStamp, string bemerkingen, Stageopdracht stageopdracht, int bedrijfId, Review review)
        {
            return new Stagevoorstel
            {
                TimeStamp = timeStamp,
                Bemerkingen = bemerkingen,
                //Verstuurd = oudVoorstel.Verstuurd
                Stageopdracht = stageopdracht,
                OpdrachtgeverId = bedrijfId,
                Review = review
            };
        }

        public Review ParseReview(string feedback, Lector reviewer)
        {
            return new Review
            {
                Feedback = feedback,
                Reviewer = reviewer,
                ReviewerId = reviewer.Id
            };
        }

        public bool? ConvertAanwezigheidHandShake(string text)
        {
            if (text.ToLower().Equals("neen") || text.ToLower().Equals("nee"))
                return false;
            if (text.ToLower().Equals("ja"))
                return true;
            return null;
        }

        public int? ConvertAantalStagiairs(string aantalGewensteStagiars)
        {
            try
            {
                string[] elements = aantalGewensteStagiars.Split(' ');
                return Convert.ToInt32(elements[0]);
            }
            catch (FormatException ex)
            {
                return 0;
            }
            catch (ArgumentException ex)
            {
                return 0;
            }
        }

        public Student ParseStudent(string naam, string voornaam,
            string email, string gsm)
        {
            return new Student
            {
                Achternaam = naam,
                Voornaam = voornaam,
                SchoolMail = email,
                Telefoonnummer = gsm
            };
        }

        public Afstudeerrichting? ConvertToAfstudeerrichting(string text)
        {
            if (text.ToLower().Equals("applicatieontwikkeling"))
                return Afstudeerrichting.ApplicatieOntwikkeling;
            if (text.ToLower().Equals("systemen en netwerkbeheer"))
                return Afstudeerrichting.SysteemEnNetwerkbeheer;
            if (text.ToLower().Equals("software management"))
                return Afstudeerrichting.Softwaremanagement;
            return null;
        }

        //TODO: Method herwerken om de list van inleidende activiteiten op te vullen
        public InleidendeActiviteit? ConvertToInleidendeActiviteit(string text)
        {
            string[] elements = text.Split(',');

            string inleidendeActiviteit = elements[0].Trim().ToLower();
            if (inleidendeActiviteit.Equals("cv"))
                return InleidendeActiviteit.CV;
            if (inleidendeActiviteit.Equals("sollicitatiegesprek"))
                return InleidendeActiviteit.Solicitatiegesprek;
            if (inleidendeActiviteit.Equals("vergoeding / tegemoetkoming in verplaatsingskosten"))
                return InleidendeActiviteit.Vergoeding;
            return null;
        }

    }
}
