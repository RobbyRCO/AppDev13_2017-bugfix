using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Api.Models;

namespace Internship.Data.DomainClasses
{
   public class Bedrijf
    {
        public int Id { get; set; }
        public string Bedrijfsnaam { get; set; }
        public string Adres { get; set; }
        public int Postcode { get; set; }
        public string Gemeente { get; set; }
        public int Huisnummer { get; set; }
        public int Busnummer { get; set; }
        public string Telefoonnummer { get; set; }
        public string Email { get; set; }
        public bool AanwezigheidHandshake { get; set; }

        public List<Stagevoorstel> IngediendeStagevoorstellen { get; set; }
        public List<Bedrijfspromotor> Bedrijfspromotors { get; set; }
        public List<Contactpersoon> Contactpersonen { get; set; }

        public string UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }

    }
}
