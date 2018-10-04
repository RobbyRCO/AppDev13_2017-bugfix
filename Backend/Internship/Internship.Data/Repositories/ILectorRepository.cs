using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;

namespace Internship.Data.Repositories
{
    public interface ILectorRepository
    {
        IEnumerable<Lector> GetAll();
        Lector Get(int id);
        Lector GetByEmail(string email);
        Lector GetHomePageData(int id);
        Lector Post(Lector stagevoorstel);
        void Update(Lector lector);
        void Delete(int id);
        Lector GetLectorWithUserAccount(string id);
    }
}
