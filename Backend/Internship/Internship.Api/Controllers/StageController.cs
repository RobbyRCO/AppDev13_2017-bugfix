using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Internship.Data.DomainClasses;
using Internship.Data.Repositories;

namespace Internship.Api.Controllers
{
    //[Authorize]
    public class StageController : ApiController
    {
        private IStageRepository _stageRepository;

        public StageController(IStageRepository stageRepository)
        {
            _stageRepository = stageRepository;
        }

        // GET: api/Stage
        public IHttpActionResult GetAll()
        {
            var stages = _stageRepository.GetAll();
            return Ok(stages);
        }

        // GET: api/Stage/5
        public IHttpActionResult Get(int id)
        {
            var stage = _stageRepository.Get(id);
            if (stage == null)
                return NotFound();
            return Ok(stage);
        }

        // POST: api/Stage
        public IHttpActionResult Post(Stage stage)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var addedStage = _stageRepository.Post(stage);
                return CreatedAtRoute("DefaultApi", new { controller = "Stage", addedStage.Id }, addedStage);
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Je hebt al een stage gekozen");
            }
            
        }

        // PUT: api/Stage/5
        public IHttpActionResult Put(int id, Stage stage)
        {
            if (!ModelState.IsValid || id != stage.Id)
                return BadRequest();
            if (_stageRepository.Get(id) == null)
                return NotFound(); 

            var updatedStage = _stageRepository.Update(stage);
            return Ok(updatedStage);

        }

        // DELETE: api/Stage/5
        public IHttpActionResult Delete(int id)
        {
            if (_stageRepository.Get(id) == null)
                return NotFound();
            _stageRepository.Delete(id);
            return Ok();
        }
    }
}
