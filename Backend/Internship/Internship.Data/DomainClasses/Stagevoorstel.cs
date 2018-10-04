using Internship.Data.DomainClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace Internship.Data.DomainClasses
{
    public class Stagevoorstel
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public String Bemerkingen { get; set; }
        //  TODO: Verstuurd is een string
        public bool Verstuurd { get; set; }

        public virtual Stageopdracht Stageopdracht { get; set; }
        public int OpdrachtgeverId { get; set; }
        public virtual Bedrijf Opdrachtgever { get; set; }
        public Review Review { get; set; }
        public int? ReviewLectorId { get; set; }
        public Lector ReviewLector { get; set; }

        public int? StagecoördinatorBehandelingLectorId { get; set; }
        public Stagecoördinator StagecoördinatorBehandelingLector { get; set; }
    }
}
