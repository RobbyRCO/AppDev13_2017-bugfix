using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;

namespace Internship.Data.Repositories
{
    public class StageDBRepository : IStageRepository
    {
        private ApplicationDbContext _context;

        public StageDBRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Stage> GetAll()
        {
            return _context.Stages.ToList();
        }

        public Stage Get(int id)
        {
            return _context.Stages.FirstOrDefault(s => s.Id == id);
        }

        public Stage Post(Stage stage)
        {
            var student = _context.Studenten.FirstOrDefault(s => s.Id == stage.StudentId);
            stage.Student = student;
            _context.Stages.Add(stage);
            _context.SaveChanges();
            student.GekozenStageOpdrachtId = stage.Id;
            _context.SaveChanges();
            return stage;
        }

        public Stage Update(Stage stage)
        {
            var original = _context.Stages.First(s => s.Id == stage.Id);
            var entry = _context.Entry(original);
            entry.CurrentValues.SetValues(stage);
            _context.SaveChanges();
            return stage;
        }

        public void Delete(int id)
        {
            _context.Stages.Remove(this.Get(id));
            _context.SaveChanges();
        }
    }
}
