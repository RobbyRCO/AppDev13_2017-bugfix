using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Data.DomainClasses
{
   public class Review
    {
        public int Id { get; set; }
        public string Feedback { get; set; }

        public int ReviewerId { get; set; }
        public Lector Reviewer { get; set; }
        public Stagevoorstel Stagevoorstel { get; set; }
    }
}
