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
    public class StudentDBRepository : IStudentRepository
    {
        private ApplicationDbContext _context;

        public StudentDBRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Studenten.ToList();
        }

        public Student Get(int id)
        {
            return _context.Studenten.FirstOrDefault(s => s.Id == id);
        }

        public Student GetByName(string voornaam, string achternaam)
        {
            return _context.Studenten.FirstOrDefault(
                s => s.Voornaam.Equals(voornaam) && s.Achternaam.Equals(achternaam));
        }

        public Student GetHomePageData(int id)
        {
            return _context.Studenten.Include(s => s.GekozenStageOpdracht).Include(s => s.Favorieten).FirstOrDefault(s => s.Id == id);
        }

        public Student Post(Student student)
        {
            _context.Studenten.Add(student);
            _context.SaveChanges();
            return student;
        }

        public void Update(Student student)
        {
            _context.Studenten.AddOrUpdate(student);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Student student = _context.Studenten.FirstOrDefault(s => s.Id == id);
            _context.Studenten.Remove(student);
            _context.SaveChanges();
        }

        public Student GetStudentWithUserAccount(string id)
        {
            return _context.Studenten.Include(s => s.UserAccount).FirstOrDefault(s => s.UserAccount.Id == id);
        }
    }
}
