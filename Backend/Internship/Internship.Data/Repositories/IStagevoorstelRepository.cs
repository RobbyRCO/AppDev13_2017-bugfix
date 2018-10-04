using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;

namespace Internship.Data.Repositories
{
    public interface IStagevoorstelRepository
    {
        Stagevoorstel Post(Stagevoorstel stagevoorstel);
        Stagevoorstel Get(int id);
        Stagevoorstel GetByTimeStamp(DateTime timeStamp);
        void UpdateReview(Stagevoorstel stagevoorstel);
        IEnumerable<Stagevoorstel> GetAll();
        void Delete(int id);
        Stagevoorstel GetDetail(int id);
        void Update(Stagevoorstel stagevoorstel);
        void UpdateToekennenLector(Stagevoorstel stagevoorstel);
        void UpdateStatus(Stagevoorstel stagevoorstel);
        IEnumerable<Stagevoorstel> GetTeBehandeleStagevoorstellen();
    }
}
