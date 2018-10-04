using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Internship.Data.Repositories
{
    public class BedrijfDBRepository : IBedrijfRepository
    {
        private ApplicationDbContext _context;

        public BedrijfDBRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Bedrijf> GetAll()
        {
            return _context.Bedrijven.ToList();
        }

        public Bedrijf Get(int id)
        {
            return _context.Bedrijven.FirstOrDefault(s => s.Id == id);
        }

        public Bedrijf GetByName(string name)
        {
            return _context.Bedrijven.FirstOrDefault(s => s.Bedrijfsnaam.Equals(name));
        }

        public Bedrijf GetHomePageData(int id)
        {
            return _context.Bedrijven.Include(s => s.IngediendeStagevoorstellen).FirstOrDefault(s => s.Id == id);
        }

        public Bedrijf Post(Bedrijf bedrijf)
        {
            _context.Bedrijven.Add(bedrijf);
            _context.SaveChanges();
            return bedrijf;
        }

        public void Update(Bedrijf bedrijf)
        {
            _context.Bedrijven.AddOrUpdate(bedrijf);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Bedrijf bedrijf = _context.Bedrijven.FirstOrDefault(s => s.Id == id);
            _context.Bedrijven.Remove(bedrijf);
            _context.SaveChanges();
        }

        public Bedrijf GetBedrijfWithUserAccount(string id)
        {
            return _context.Bedrijven.Include(b => b.UserAccount).FirstOrDefault(b => b.UserAccount.Id == id);
        }

        public Bedrijf GetBedrijfWithBedrijfspromotorsAndContactpersonen(int id)
        {
            return _context.Bedrijven.Include(b => b.Bedrijfspromotors).Include(b => b.Contactpersonen).FirstOrDefault(b => b.Id == id);
        }
    }
}
