using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;
using Internship.Data.Repositories;
using Internship.Desktop.Data;
using Moq;
using NUnit.Framework;

namespace Internship.Desktop.Tests
{
    [TestFixture]
    class ParseServiceTests
    {
        private TestableParseService _parseService;

        [SetUp]
        public void SetUp()
        {
            _parseService = TestableParseService.CreateInstance();
        }

        [Test]
        public void ParseOudeStage__ValidInputString_ReturnsListOfOudeStage()
        {
            //Arrange
            string line = CsvStringBuilder.ParseToCsv(new StageBuilder().Build());

            //Act
            var resultData = ParseService.ParseOudeStage(line.Split(ParseService.DELIMITER)) as OudeStage;

            //Assert
            Assert.That(resultData, Is.Not.Null);
            Assert.That(resultData.EmailPromotor, Is.Not.Null);
            Assert.That(resultData.VoornaamContactpersoon, Is.Not.Null);
            Assert.That(resultData.Bemerkingen, Is.Not.Null);
        }

        [Test]
        public void ParseBedrijf_ValidInputParameters_ReturnsBedrijf()
        {
            //Arrange
            var name = ParameterBuilder.String();
            var adress = ParameterBuilder.String();
            var postalCode = ParameterBuilder.Int();
            var city = ParameterBuilder.String();
            var phoneNumber = ParameterBuilder.String();
            var email = ParameterBuilder.String();
            var handShake = ParameterBuilder.Bool();

            //Act
            var result = _parseService.ParseBedrijf
                (name, adress, postalCode, city, phoneNumber, email, handShake) as Bedrijf;

            //Assert
            Assert.That(result.Bedrijfsnaam, Is.EqualTo(name));
            Assert.That(result.Adres, Is.EqualTo(adress));
            Assert.That(result.Postcode, Is.EqualTo(postalCode));
            Assert.That(result.Gemeente, Is.EqualTo(city));
            Assert.That(result.Telefoonnummer, Is.EqualTo(phoneNumber));
            Assert.That(result.Email, Is.EqualTo(email));
            Assert.That(result.AanwezigheidHandshake, Is.EqualTo(handShake));
        }

        [Test]
        public void ParseStageOpdracht_ValidInputParameters_ReturnsStageopdracht()
        {
            //Arrange
            List<object> parameters = new List<object>();
            parameters.Add(ParameterBuilder.String()); //locatie
            var locatie = ParameterBuilder.String();
            var omschrijving = ParameterBuilder.String();
            var voorwaarden = ParameterBuilder.String();
            var thema = ParameterBuilder.String();
            var omschOmgeving = ParameterBuilder.String();
            var omgeving = ParameterBuilder.String();
            var aantalMederwerkersIt = ParameterBuilder.Int();
            var aantalMedewerkers = ParameterBuilder.Int();
            var aantalStagiairs = ParameterBuilder.Int();
            var afstudeerrichting = ParameterBuilder.Afstudeerrichting();
            var inleidendeActiviteit = ParameterBuilder.InleidendeActiviteit();
            var opdrachtgeverId = ParameterBuilder.Int();

            //Act
            var result = _parseService.ParseStageOpdracht(locatie, omschrijving, voorwaarden, thema, omschOmgeving,
                omgeving, aantalMederwerkersIt, aantalMedewerkers, aantalStagiairs, afstudeerrichting,
                inleidendeActiviteit, opdrachtgeverId) as Stageopdracht;

            //Assert
            Assert.That(result.Locatie,Is.EqualTo(locatie));
            Assert.That(result.Omschrijving, Is.EqualTo(omschrijving));
            Assert.That(result.Randvoorwaarden, Is.EqualTo(voorwaarden));
            Assert.That(result.Onderzoeksthema, Is.EqualTo(thema));
            Assert.That(result.BeschrijvingTechnischeOmgeving, Is.EqualTo(omschOmgeving));
            Assert.That(result.Omgeving, Is.EqualTo(omgeving));
            Assert.That(result.AantalITWerknemers, Is.EqualTo(aantalMederwerkersIt));
            Assert.That(result.AantalWerknemers, Is.EqualTo(aantalMedewerkers));
            Assert.That(result.AantalGewensteStagiairs, Is.EqualTo(aantalStagiairs));
            Assert.That(result.VoorkeurAfstudeerrichting, Is.EqualTo(afstudeerrichting));
            Assert.That(result.InleidendeActiviteiten, Is.EqualTo(inleidendeActiviteit));
            Assert.That(result.OpdrachtgeverId, Is.EqualTo(opdrachtgeverId));
        }

        [Test]
        public void ParseContactpersoon_ValidInputParameters_ReturnsContactpersoon()
        {
            //Arrange
            var naam = ParameterBuilder.String();
            var voornaam = ParameterBuilder.String();
            var email = ParameterBuilder.String();
            var telefoon = ParameterBuilder.String();
            var titel = ParameterBuilder.String();

            //Act
            var result = _parseService.ParseContactpersoon(naam, voornaam, email, telefoon, titel);

            //Assert
            Assert.That(result.Achternaam,Is.EqualTo(naam));
            Assert.That(result.Voornaam, Is.EqualTo(voornaam));
            Assert.That(result.Email, Is.EqualTo(email));
            Assert.That(result.Telefoonnummer, Is.EqualTo(telefoon));
            Assert.That(result.Title, Is.EqualTo(titel));
        }

        [Test]
        public void ParseBedrijfsPromotor_ValidInputParameters_ReturnsBedrijfsPromotor()
        {
            //Arrange
            var naam = ParameterBuilder.String();
            var voornaam = ParameterBuilder.String();
            var email = ParameterBuilder.String();
            var telefoon = ParameterBuilder.String();
            var titel = ParameterBuilder.String();
            var bedrijfsId = ParameterBuilder.Int();

            //Act
            var result =
                _parseService.ParseBedrijfsPromotor(naam, voornaam, email, telefoon, titel, bedrijfsId) as
                    Bedrijfspromotor;

            //Assert
            Assert.That(result.Achternaam, Is.EqualTo(naam));
            Assert.That(result.Voornaam, Is.EqualTo(voornaam));
            Assert.That(result.Email, Is.EqualTo(email));
            Assert.That(result.Telefoonnummer, Is.EqualTo(telefoon));
            Assert.That(result.Title, Is.EqualTo(titel));
            Assert.That(result.BedrijfInDienstId, Is.EqualTo(bedrijfsId));
        }

        [Test]
        public void ParseStageVoorstel_ValidInputParameters_ReturnsStageVoorstel()
        {
            //Arrange
            var timeStamp = ParameterBuilder.Date();
            var bemerkingen = ParameterBuilder.String();
            var stageopdracht = new Stageopdracht();
            var bedrijfId = ParameterBuilder.Int();
            var review = new Review();

            //Act
            var result =
                _parseService.ParseStageVoorstel(timeStamp, bemerkingen, stageopdracht, bedrijfId,
                    review) as Stagevoorstel;

            //Assert
            Assert.That(result.TimeStamp, Is.EqualTo(timeStamp));
            Assert.That(result.Bemerkingen, Is.EqualTo(bemerkingen));
            Assert.That(result.Stageopdracht, Is.EqualTo(stageopdracht));
            Assert.That(result.OpdrachtgeverId, Is.EqualTo(bedrijfId));
            Assert.That(result.Review, Is.EqualTo(review));
        }

        [Test]
        public void ParseReview_ValidInputParameters_ReturnsReview()
        {
            //Arrange
            var feedback = ParameterBuilder.String();
            var reviewer = new Lector();

            //Act
            var result = _parseService.ParseReview(feedback, reviewer) as Review;

            //Assert
            Assert.That(result.Feedback, Is.EqualTo(feedback));
            Assert.That(result.Reviewer, Is.EqualTo(reviewer));
        }

        [Test]
        public void ParseStudent_ValidInputParamaters_ReturnsStudent()
        {
            //Arrange
            var naam = ParameterBuilder.String();
            var voornaam = ParameterBuilder.String();
            var email = ParameterBuilder.String();
            var gsm = ParameterBuilder.String();

            //Act
            var result = _parseService.ParseStudent(naam, voornaam, email, gsm);

            //Assert
            Assert.That(result.Achternaam, Is.EqualTo(naam));
            Assert.That(result.Voornaam, Is.EqualTo(voornaam));
            Assert.That(result.SchoolMail, Is.EqualTo(email));
            Assert.That(result.Telefoonnummer, Is.EqualTo(gsm));
        }

        [TestCase("Neen",false)]
        [TestCase("nee", false)]
        [TestCase("ja", true)]
        [TestCase("jA", true)]
        public void ConvertAanwezigheidHandshake_MultipleInputs_ReturnsAppropriateBoolean(string text, bool? result)
        {
            //Arrange

            //Act
            var convertResult = _parseService.ConvertAanwezigheidHandShake(text);

            //Assert
            Assert.That(convertResult, Is.EqualTo(result));
        }

        [TestCase("5 studenten", 5)]
        [TestCase("qsdkfjsd", 0)]
        public void ConvertAantalStudenten_MultipleInputs_ReturnsAppropriateInteger(string text, int result)
        {
            //Arrange

            //Act
            var convertResult = _parseService.ConvertAantalStagiairs(text);

            //Assert
            Assert.That(convertResult, Is.EqualTo(result));
        }

        [TestCase("applicatieontwikkeling", Afstudeerrichting.ApplicatieOntwikkeling)]
        [TestCase("systemen en netwerkbeheer", Afstudeerrichting.SysteemEnNetwerkbeheer)]
        [TestCase("software management", Afstudeerrichting.Softwaremanagement)]
        [TestCase("GibberISh", null)]
        public void ConvertToAfstudeerrichting(string text, Afstudeerrichting? result)
        {
            //Arrange

            //Act
            var convertResult = _parseService.ConvertToAfstudeerrichting(text);

            //Assert
            Assert.That(convertResult, Is.EqualTo(result));
        }

        [TestCase("cv", InleidendeActiviteit.CV)]
        [TestCase("sollicitatiegesprek", InleidendeActiviteit.Solicitatiegesprek)]
        [TestCase("vergoeding / tegemoetkoming in verplaatsingskosten", InleidendeActiviteit.Vergoeding)]
        [TestCase("GibberISh", null)]
        public void ConvertToInleidendeActiviteit_MultipleInput_ReturnsAppropriateInleidendeActiviteit(string text,
            InleidendeActiviteit? result)
        {
            //Arrange

            //Act
            var convertResult = _parseService.ConvertToInleidendeActiviteit(text);

            //Assert
            Assert.That(convertResult, Is.EqualTo(result));
        }

        class StagevoorstelBuilder
        {
            private OudStagevoorstel voorstel;
            private Random random;

            public StagevoorstelBuilder()
            {
                random = new Random();
                voorstel = new OudStagevoorstel();
                foreach (var p in voorstel.GetType().GetProperties().Where(p => p.GetGetMethod().GetParameters().Count() == 0))
                {
                    p.SetValue(voorstel, Guid.NewGuid().ToString());
                }
            }

            public OudStagevoorstel Build()
            {
                return voorstel;
            }
        }

        class StageBuilder
        {
            private OudeStage stage;
            private Random random;

            public StageBuilder()
            {
                stage = new OudeStage();
                random = new Random();
                foreach (var p in stage.GetType().GetProperties().Where(p => p.GetGetMethod().GetParameters().Count() == 0))
                {
                    p.SetValue(stage, Guid.NewGuid().ToString());
                }
            }

            public OudeStage Build()
            {
                return stage;
            }
        }

        class CsvStringBuilder
        {
            public static string ParseToCsv(object obj)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var p in obj.GetType().GetProperties().Where(p => p.GetGetMethod().GetParameters().Count() == 0))
                {
                    stringBuilder.Append(p.GetValue(obj, null) + "\t");
                }
                return stringBuilder.ToString();
            }
        }

        class TestableParseService : ParseService
        {
            public Mock<IStagevoorstelRepository> _stagevoorstelRepoMock { get; }
            public Mock<IBedrijfRepository> _bedrijfRepoMock { get; }
            public Mock<IStageopdrachtenRepository> _stageOpdrachRepoMock { get; }
            public Mock<ILectorRepository> _lectorRepoMock { get; }
            public Mock<IStudentRepository> _studentRepoMock { get; }


            private TestableParseService(Mock<IStagevoorstelRepository> _stagevoorstelRepoMock,
                Mock<IBedrijfRepository> _bedrijfRepoMock,
                Mock<IStageopdrachtenRepository> _stageOpdrachRepoMock,
                Mock<ILectorRepository> _lectorRepoMock,
                Mock<IStudentRepository> _studentRepoMock) :
                base(_stagevoorstelRepoMock.Object,
                    _bedrijfRepoMock.Object,
                    _stageOpdrachRepoMock.Object,
                    _lectorRepoMock.Object,
                    _studentRepoMock.Object)

            {
                this._bedrijfRepoMock = _bedrijfRepoMock;
                this._lectorRepoMock = _lectorRepoMock;
                this._stageOpdrachRepoMock = _stageOpdrachRepoMock;
                this._stagevoorstelRepoMock = _stagevoorstelRepoMock;
                this._studentRepoMock = _studentRepoMock;
            }

            public static TestableParseService CreateInstance()
            {
                return new TestableParseService(new Mock<IStagevoorstelRepository>(), 
                    new Mock<IBedrijfRepository>(), new Mock<IStageopdrachtenRepository>(),
                    new Mock<ILectorRepository>(), new Mock<IStudentRepository>() );
            }
        }

        class GuidBuilder
        {
            public static string Build()
            {
                return Guid.NewGuid().ToString();
            }
        }

        class ParameterBuilder
        {
            public static string String()
            {
                return GuidBuilder.Build();
            }

            public static int Int()
            {
                return new Random().Next(1,int.MaxValue);
            }

            public static bool Bool()
            {
                return Convert.ToBoolean(Int()%2);
            }

            public static Afstudeerrichting Afstudeerrichting()
            {
                int number = Int()%3;
                if (number == 0)
                    return Internship.Data.DomainClasses.Afstudeerrichting.ApplicatieOntwikkeling;
                if (number == 1)
                    return Internship.Data.DomainClasses.Afstudeerrichting.Softwaremanagement;
                return Internship.Data.DomainClasses.Afstudeerrichting.SysteemEnNetwerkbeheer;
            }

            public static InleidendeActiviteit InleidendeActiviteit()
            {
                int number = Int() % 3;
                if (number == 0)
                    return Internship.Data.DomainClasses.InleidendeActiviteit.CV;
                if (number == 1)
                    return Internship.Data.DomainClasses.InleidendeActiviteit.Solicitatiegesprek;
                return Internship.Data.DomainClasses.InleidendeActiviteit.Vergoeding;
            }

            public static DateTime Date()
            {
                DateTime start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                return start.AddDays(new Random().Next(range));
            }
        }
    }
}
