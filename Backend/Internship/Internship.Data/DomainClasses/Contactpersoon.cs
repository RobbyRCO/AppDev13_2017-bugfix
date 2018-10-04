using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Data.DomainClasses
{
    public class Contactpersoon : BedrijfsPersoon
    {
        public int BedrijfInDienstId { get; set; }
        public Bedrijf BedrijfInDienst { get; set; }
        public List<Stageopdracht> Stageopdrachten { get; set; }
    }
}
