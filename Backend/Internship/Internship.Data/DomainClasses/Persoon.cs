using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Api.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Internship.Data.DomainClasses
{
    public class Persoon
    {
        public int Id { get; set; }
        public String Achternaam { get; set; }
        public String Voornaam { get; set; }
        public String Email { get; set; }
        public String Telefoonnummer { get; set; }
    }
}
