using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Internship.Data.DomainClasses;
using Internship.Data.Repositories;

namespace Internship.Api.Controllers
{
    //[Authorize]
    public class StudentController : ApiController
    {
        private IStudentRepository _repo;

        public StudentController(IStudentRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Student
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        // GET: api/Student/5
        public IHttpActionResult Get(int id)
        {
            Student student = _repo.Get(id);
            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Student/home/5
        [Route("student/home/{id}")]
        public IHttpActionResult GetHomeData(int id)
        {
            Student student = _repo.GetHomePageData(id);
            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Student/209d8635-7ecd-4ca2-8b0d-67f196498547
        [Route("useraccountStudent/{id}")]
        public IHttpActionResult Get(string id)
        {
            Student student = _repo.GetStudentWithUserAccount(id);
            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        // POST: api/Student
        public IHttpActionResult Post(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdStudent = _repo.Post(student);



            return CreatedAtRoute("DefaultApi", new { controller = "Student", id = createdStudent.Id }, createdStudent);
        }

        // PUT: api/Student/5
        public IHttpActionResult Put(int id, Student student)
        {
            if (!ModelState.IsValid || student.Id != id)
            {
                return BadRequest();
            }

            var rest = _repo.Get(id);
            if (rest != null)
            {
                _repo.Update(student);
                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/Student 
        public IHttpActionResult Delete(int id)
        {
            var rest = _repo.Get(id);
            if (rest != null)
            {
                _repo.Delete(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
