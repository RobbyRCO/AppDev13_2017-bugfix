using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;

namespace Internship.Data.Repositories
{
    public interface IBedrijfRepository
    {
        IEnumerable<Bedrijf> GetAll();
        Bedrijf Get(int id);
        Bedrijf GetByName(string name);
        Bedrijf GetHomePageData(int id);
        Bedrijf Post(Bedrijf bedrijf);
        void Update(Bedrijf bedrijf);
        void Delete(int id);
        Bedrijf GetBedrijfWithUserAccount(string id);
        Bedrijf GetBedrijfWithBedrijfspromotorsAndContactpersonen(int id);
    }
}
