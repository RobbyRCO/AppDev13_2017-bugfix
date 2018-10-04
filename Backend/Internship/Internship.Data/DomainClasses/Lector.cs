using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Api.Models;

namespace Internship.Data.DomainClasses
{
   public class Lector : SchoolPersoon
    {
        public Afstudeerrichting Afstudeerrichting { get; set; }
        public bool MagReviewen { get; set; }

        public List<Review> Reviews { get; set; }
        public List<Stagevoorstel> Stagevoorstellen { get; set; }

        public string UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
