using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Data.DomainClasses
{
    public class Stage
    {
  //      public StageDocumenten StageDocumenten { get; set; }
        public int Id { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }

        public int StageOpdrachtId { get; set; }
        public virtual Stageopdracht StageOpdracht { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public StageStatus StageStatus { get; set; }
    }

    public enum StageStatus
    {
        InBehandeling,
        Goedgekeurd,
        Afgekeurd
    }
    
}
