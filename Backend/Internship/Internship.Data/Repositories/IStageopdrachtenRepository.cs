using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;

namespace Internship.Data.Repositories
{
   public interface IStageopdrachtenRepository
    {
        IEnumerable<Stageopdracht> GetAll();
        Stageopdracht Get(int id);
        Stageopdracht Post(Stageopdracht stageOpdracht);
        void Update(Stageopdracht stageOpdracht);
        void Delete(int id);
        IEnumerable<Stageopdracht> GetAllGoedgekeurdeStageopdrachten();
        Stageopdracht GetWithFavorites(int id);
        void UpdateFavorites(Stageopdracht stageopdracht, int studentId);
        Stageopdracht GetDetailStageopdracht(int id);
    }
}
