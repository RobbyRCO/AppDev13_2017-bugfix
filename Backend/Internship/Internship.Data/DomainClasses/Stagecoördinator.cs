using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Api.Models;

namespace Internship.Data.DomainClasses
{
    public class Stagecoördinator : SchoolPersoon
    {
        public List<Stagevoorstel> ToeTeKennenStagevoorstellen { get; set; }
        public List<Stageopdracht> AanvragingenStageopdrachtenStudent { get; set; }

        public string UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
