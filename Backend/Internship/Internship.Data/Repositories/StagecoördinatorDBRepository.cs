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
    public class StagecoördinatorDBRepository : IStagecoördinatorRepository
    {
        private ApplicationDbContext _context;

        public StagecoördinatorDBRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Stagecoördinator Get(int id)
        {
            return _context.Stagecoördinators.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Stagecoördinator> GetAll()
        {
            return _context.Stagecoördinators.ToList();
        }

        public Stagecoördinator GetHomePageData(int id)
        {
            return
                _context.Stagecoördinators.Include(s => s.AanvragingenStageopdrachtenStudent)
                    .Include(s => s.ToeTeKennenStagevoorstellen)
                    .FirstOrDefault(s => s.Id == id);
        }

        public Stagecoördinator Post(Stagecoördinator stagecoördinator)
        {
            _context.Stagecoördinators.Add(stagecoördinator);
            _context.SaveChanges();
            return stagecoördinator;
        }

        public void Update(Stagecoördinator stagecoördinator)
        {
            _context.Stagecoördinators.AddOrUpdate(stagecoördinator);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Stagecoördinator stagecoördinator = _context.Stagecoördinators.FirstOrDefault(o => o.Id == id);
            _context.Stagecoördinators.Remove(stagecoördinator);
            _context.SaveChanges();
        }

        public Stagecoördinator GetStagecoördinatorWithUserAccount(string id)
        {
            return _context.Stagecoördinators.Include(s => s.UserAccount).FirstOrDefault(s => s.UserAccount.Id == id);
        }
    }
}
