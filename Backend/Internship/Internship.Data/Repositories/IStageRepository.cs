using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;

namespace Internship.Data.Repositories
{
    public interface IStageRepository
    {
        IEnumerable<Stage> GetAll();
        Stage Get(int id);
        Stage Post(Stage stage);
        Stage Update(Stage stage);
        void Delete(int id);
    }
}
