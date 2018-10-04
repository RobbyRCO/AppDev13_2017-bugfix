using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Internship.Data;
using Internship.Data.DomainClasses;
using Internship.Data.Repositories;

namespace Internship.Api.Controllers
{
    //[Authorize]
    public class StagecoördinatorController : ApiController
    {
        private IStagecoördinatorRepository _repo;

        public StagecoördinatorController(IStagecoördinatorRepository repo)
        {
            _repo = repo;
        }


        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        public IHttpActionResult Get(int id)
        {
            Stagecoördinator stagecoördinator = _repo.Get(id);
            if (stagecoördinator != null)
            {
                return Ok(stagecoördinator);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/stagecoördinator/home/5
        [Route("stagecoördinator/home/{id}")]
        public IHttpActionResult GetHomeData(int id)
        {
            Stagecoördinator stagecoördinator = _repo.GetHomePageData(id);
            if (stagecoördinator != null)
            {
                return Ok(stagecoördinator);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/stagecoordinator/209d8635-7ecd-4ca2-8b0d-67f196498547
        [Route("useraccountStagecoördinator/{id}")]
        public IHttpActionResult Get(string id)
        {
            Stagecoördinator stagecoördinator = _repo.GetStagecoördinatorWithUserAccount(id);
            if (stagecoördinator != null)
            {
                return Ok(stagecoördinator);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Stagecoördinator
        public IHttpActionResult Post(Stagecoördinator stagecoördinator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdStagecoördinator = _repo.Post(stagecoördinator);

            return CreatedAtRoute("DefaultApi", new { controller = "Stagecoördinator", id = createdStagecoördinator.Id }, createdStagecoördinator);
        }

        // PUT: api/Stagecoördinator/5
        public IHttpActionResult Put(int id, Stagecoördinator stagecoördinator)
        {
            if (!ModelState.IsValid || stagecoördinator.Id != id)
            {
                return BadRequest();
            }

            var rest = _repo.Get(id);
            if (rest != null)
            {
                _repo.Update(stagecoördinator);
                return Ok();
            }
            return NotFound();

        }

        // DELETE: api/Stagecoördinator/5
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

