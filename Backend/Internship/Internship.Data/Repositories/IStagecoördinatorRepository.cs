using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;

namespace Internship.Data.Repositories
{
    public interface IStagecoördinatorRepository
    {
        Stagecoördinator GetHomePageData(int id);
        IEnumerable<Stagecoördinator> GetAll();
        Stagecoördinator Get(int id);
        Stagecoördinator Post(Stagecoördinator stagecoördinator);
        void Update(Stagecoördinator stagecoördinator);
        void Delete(int id);
        Stagecoördinator GetStagecoördinatorWithUserAccount(string id);
    }
}
