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
    public class StageopdrachtenController : ApiController
    {
        private IStageopdrachtenRepository _repo;

        public StageopdrachtenController(IStageopdrachtenRepository repo)
        {
            _repo = repo;
        }

        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        [Route("stageopdrachten/goedgekeurd")]
        public IHttpActionResult GetGoedgekeurdeStageopdrachten()
        {
            return Ok(_repo.GetAllGoedgekeurdeStageopdrachten());
        }

        public IHttpActionResult Get(int id)
        {
            Stageopdracht stageopdracht = _repo.Get(id);
            if (stageopdracht != null)
            {
                return Ok(stageopdracht);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("stageopdrachten/favorites/{id}")]
        public IHttpActionResult GetStageopdrachtenWithFavorites(int id)
        {
            Stageopdracht stageopdracht = _repo.GetWithFavorites(id);
            if (stageopdracht != null)
            {
                return Ok(stageopdracht);
            }
            else
            {
                return NotFound();
            }
        }
        

        [Route("stageopdrachten/detail/{id}")]
        public IHttpActionResult GetDetailStageopdrachtLectorAndStagecoordinator(int id)
        {
            var stageopdracht = _repo.GetDetailStageopdracht(id);
            if (stageopdracht != null)
                return Ok();
            return NotFound();
        }

        public IHttpActionResult Post(Stageopdracht stageOpdracht)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdStageopdracht = _repo.Post(stageOpdracht);

            return CreatedAtRoute("DefaultApi", new { controller = "Stageopdrachten", id = createdStageopdracht.Id }, createdStageopdracht);
        }

        public IHttpActionResult Put(int id, Stageopdracht stageOpdracht)
        {
            if (!ModelState.IsValid || stageOpdracht.Id != id)
            {
                return BadRequest();
            }

            var rest = _repo.Get(id);
            if (rest != null)
            {
                _repo.Update(stageOpdracht);
                return Ok();
            }
            return NotFound();
        }

        [Route("stageopdrachten/updateFavorite/{id}/{studentId}")]
        [HttpPut]
        public IHttpActionResult PutFavorites(int id, int studentId, Stageopdracht stageopdracht)
        {
            if (!ModelState.IsValid || stageopdracht.Id != id)
            {
                return BadRequest();
            }

            var rest = _repo.GetWithFavorites(id);
            if (rest != null)
            {
                _repo.UpdateFavorites(stageopdracht, studentId);
                return Ok();
            }
            return NotFound();
        }



        // DELETE: api/StageOpdrachten/5
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
