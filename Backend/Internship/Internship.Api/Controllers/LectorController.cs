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
    public class LectorController : ApiController
    {
        private ILectorRepository _repo;

        public LectorController(ILectorRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Lector
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        // GET: api/Lector/5
        public IHttpActionResult Get(int id)
        {
            Lector lector = _repo.Get(id);
            if (lector != null)
            {
                return Ok(lector);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Lector/209d8635-7ecd-4ca2-8b0d-67f196498547
        [Route("useraccountLector/{id}")]
        public IHttpActionResult Get(string id)
        {
            Lector lector = _repo.GetLectorWithUserAccount(id);
            if (lector != null)
            {
                return Ok(lector);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Lector/home/5
        [Route("lector/home/{id}")]
        public IHttpActionResult GetHomeData(int id)
        {
            Lector lector = _repo.GetHomePageData(id);
            if (lector != null)
            {
                return Ok(lector);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Lector
        public IHttpActionResult Post(Lector lector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdLector = _repo.Post(lector);
            
            return CreatedAtRoute("DefaultApi", new { controller = "Lector", lector.Id }, createdLector);
        }

        // PUT: api/Lector/5
        public IHttpActionResult Put(int id, Lector lector)
        {
            if (!ModelState.IsValid || lector.Id != id)
            {
                return BadRequest();
            }

            var rest = _repo.Get(id);
            if (rest != null)
            {
                _repo.Update(lector);
                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/Lector/5
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
