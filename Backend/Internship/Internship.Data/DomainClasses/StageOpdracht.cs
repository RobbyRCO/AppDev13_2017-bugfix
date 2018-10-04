using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Data.DomainClasses
{
   public class Stageopdracht
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Locatie { get; set; }
        public string Omschrijving { get; set; }
        public string Omgeving { get; set; }
        public string Randvoorwaarden { get; set; } 
        public string Onderzoeksthema { get; set; } 
        public string BeschrijvingTechnischeOmgeving { get; set; }
        public int AantalITWerknemers { get; set; }
        public int AantalITBegeleiders { get; set; }
        public int AantalWerknemers { get; set; }
        public int AantalGewensteStagiairs { get; set; } 
        public Afstudeerrichting VoorkeurAfstudeerrichting { get; set; }
        public InleidendeActiviteit InleidendeActiviteiten { get; set; }
        public Periode Periode { get; set; }

        public int? OpdrachtgeverId { get; set; }
        public virtual Bedrijf Opdrachtgever { get; set; }
        public int? ContactpersoonId { get; set; }
        public Contactpersoon Contactpersoon { get; set; }
        public int? BedrijfspromotorId { get; set; }
        public Bedrijfspromotor Bedrijfspromotor { get; set; }
        public Stagecoördinator StagecoördinatorBehandelingStageopdrachtStudent { get; set; }
        public Stage Stage { get; set; }
        public virtual List<Student> StudentFavorieten { get; set; }
    }

    public enum InleidendeActiviteit
    {
        CV,
        Solicitatiegesprek,
        Vergoeding
    }

    public enum Status
    {
        Nieuw,
        ToegewezenLector,
        Goedgekeurd,
        Afgekeurd,
        GoedgekeurdDoorLector,
        AfgekeurdDoorLector

    }

    public enum Periode
    {
        Semester1,
        Semester2
    }
}
