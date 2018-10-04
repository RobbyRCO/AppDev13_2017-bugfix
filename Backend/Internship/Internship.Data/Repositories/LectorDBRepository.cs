using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Internship.Data.Repositories
{
    public class LectorDBRepository : ILectorRepository
    {
        private ApplicationDbContext _context;

        public LectorDBRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lector> GetAll()
        {
            return _context.Lectoren.ToList();
        }

        public Lector Get(int id)
        {
            return _context.Lectoren.FirstOrDefault(l => l.Id == id);
        }

        public Lector GetByEmail(string email)
        {
            return _context.Lectoren.FirstOrDefault(s => s.SchoolMail.ToLower().Equals(email.ToLower()));
        }

        public Lector GetHomePageData(int id)
        {
            return
                _context.Lectoren.Include(l => l.Stagevoorstellen)
                    .FirstOrDefault(s => s.Id == id);
        }

        public Lector Post(Lector lector)
        {
            _context.Lectoren.Add(lector);
            _context.SaveChanges();
            return lector;
        }

        public void Update(Lector lector)
        {
            _context.Lectoren.AddOrUpdate(lector);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Lector lector = _context.Lectoren.FirstOrDefault(l => l.Id == id);
            _context.Lectoren.Remove(lector);
            _context.SaveChanges();
        }

        public Lector GetLectorWithUserAccount(string id)
        {
            return _context.Lectoren.Include(l => l.UserAccount).FirstOrDefault(l => l.UserAccount.Id == id);
        }
    }
}
