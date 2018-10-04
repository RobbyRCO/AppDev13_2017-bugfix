using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;

namespace Internship.Data.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student Get(int id);
        Student GetByName(string voornaam, string achternaam);
        Student Post(Student student);
        void Update(Student student);
        void Delete(int id);
        Student GetHomePageData(int id);
        Student GetStudentWithUserAccount(string id);
    }
}
