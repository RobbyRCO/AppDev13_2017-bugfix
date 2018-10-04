using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;

namespace Internship.Desktop.Data
{
    public class OudStagevoorstel : IOldType
    {
        public string TimeStamp { get; set; } = ""; //0
        public string NaamBedrijfOpdrachtgever { get; set; } = ""; //1
        public string StraatEnNummer { get; set; } = ""; //2
        public string Postcode { get; set; } = ""; //3
        public string Gemeente { get; set; } = ""; //4
        public string Locatie { get; set; } = ""; //5
        public string AantalMedewerkers { get; set; } = ""; //6
        public string AantalMedewerkersIT { get; set; } = ""; //7
        public string AantalMedewerkersIT2 { get; set; } = ""; //8
        public string TitelContactpersoon { get; set; } = ""; //9
        public string NaamContactpersoon { get; set; } = ""; //10
        public string VoornaamContactpersoon { get; set; } = ""; //11
        public string TelefoonnummerContactpersoon { get; set; } = ""; //12
        public string EmailContactpersoon { get; set; } = ""; //13
        public string TitelPromotor { get; set; } = ""; //14
        public string NaamPromotor { get; set; } = ""; //15
        public string VoornaamPromotor { get; set; } = ""; //16
        public string TelefoonnummerPromotor { get; set; } = ""; //17
        public string EmailPromotor { get; set; } = ""; //18
        public string VoorkeurAfstudeerrichting { get; set; } = ""; //19
        public string OmschrijvingOpdracht { get; set; } = ""; //20
        public string Omgeving { get; set; } = ""; //21
        public string Tools { get; set; } = ""; //22
        public string Randvoorwaarden { get; set; } = ""; //23
        public string Onderzoeksthema { get; set; } = ""; //24
        public string InleidendeActiviteit { get; set; } = ""; //25
        public string AantalGewensteStagiars { get; set; } = ""; //26
        public string AanwezigHandshake { get; set; } = ""; //27
        public string ContacterendeStudenten { get; set; } = ""; //28
        public string Bemerkingen { get; set; } = ""; //29
        public string Onderzoeksthema2 { get; set; } = ""; //30
        public string Info { get; set; } = ""; //31
        public string Reviewer { get; set; } = ""; //32
        public string Verstuurd { get; set; } = ""; //33
        public string FeedbackReviewer { get; set; } = ""; //34
    }
}
