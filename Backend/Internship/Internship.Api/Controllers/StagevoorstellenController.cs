using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Internship.Data.DomainClasses;
using Internship.Data.Repositories;
using Microsoft.AspNet.Identity;

namespace Internship.Api.Controllers
{
    //[Authorize]
    public class StagevoorstellenController : ApiController
    {
        private IStagevoorstelRepository _repo;

        public StagevoorstellenController(IStagevoorstelRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Stagevoorstel
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        [Route("stagevoorstellen/teBehandelenStagecoordinator")]
        public IHttpActionResult GetTeBehandeleStagevoorstellen()
        {
            return Ok(_repo.GetTeBehandeleStagevoorstellen());
        }

        // GET: api/Stagevoorstel/5
        public IHttpActionResult Get(int id)
        {
            Stagevoorstel stagevoorstel = _repo.Get(id);
            if (stagevoorstel != null)
            {
                return Ok(stagevoorstel);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("stagevoorstellen/detail/{id}")]
        public IHttpActionResult GetDetailInformation(int id)
        {
            Stagevoorstel stagevoorstel = _repo.GetDetail(id);
            if (stagevoorstel != null)
            {
                return Ok(stagevoorstel);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/StageVoorstel
        public IHttpActionResult Post(Stagevoorstel stagevoorstel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdStagevoorstel = _repo.Post(stagevoorstel);
            
            return CreatedAtRoute("DefaultApi", new { controller = "Stagevoorstellen", id = createdStagevoorstel.Id }, createdStagevoorstel);
        }

        // PUT: api/StageVoorstellen/5
        public IHttpActionResult Put(int id, Stagevoorstel stagevoorstel)
        {
            if (!ModelState.IsValid || stagevoorstel.Id != id)
            {
                return BadRequest();
            }

            var rest = _repo.Get(id);
            if (rest != null)
            {
                _repo.Update(stagevoorstel);
                return Ok();
            }
            return NotFound();
        }

        // PUT: api/StageVoorstel/5/review
        [Route("stagevoorstellen/{id}/review")]
        public IHttpActionResult PutReview(int id, Stagevoorstel stagevoorstel)
        {
            if (!ModelState.IsValid || stagevoorstel.Id != id)
            {
                return BadRequest();
            }

            var rest = _repo.Get(id);
            if (rest != null)
            {
                _repo.UpdateReview(stagevoorstel);
                return Ok();
            }
            return NotFound();
        }

        // PUT: api/StageVoorstel/5/toekennenLector
        [Route("stagevoorstellen/{id}/toekennenLector")]
        public IHttpActionResult PutToekennenLector(int id, Stagevoorstel stagevoorstel)
        {
            if (!ModelState.IsValid || stagevoorstel.Id != id)
            {
                return BadRequest();
            }

            var rest = _repo.Get(id);
            if (rest != null)
            {
                _repo.UpdateToekennenLector(stagevoorstel);
                return Ok();
            }
            return NotFound();
        }

        // PUT: api/StageVoorstel/5/toekennenLector
        [Route("stagevoorstellen/{id}/status")]
        public IHttpActionResult PutUpdateStatus(int id, Stagevoorstel stagevoorstel)
        {
            if (!ModelState.IsValid || stagevoorstel.Id != id)
            {
                return BadRequest();
            }

            var rest = _repo.Get(id);
            if (rest != null)
            {
                _repo.UpdateStatus(stagevoorstel);
                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/StageVoorstel/5
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
