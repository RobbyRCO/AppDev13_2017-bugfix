using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Api.Models;

namespace Internship.Data.DomainClasses
{
    public class Student : SchoolPersoon
    {
        public Afstudeerrichting Afstudeerrichting { get; set; }

        public int? GekozenStageOpdrachtId { get; set; }
        public Stage GekozenStageOpdracht { get; set; } //staat niet in UML
        public List<Stageopdracht> Favorieten { get; set; }

        public string UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }

        public Student()
        {
            Favorieten = new List<Stageopdracht>();
        }
    }
}
