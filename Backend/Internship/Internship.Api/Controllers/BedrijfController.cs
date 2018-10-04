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
    public class BedrijfController : ApiController
    {
        private IBedrijfRepository _repo;

        public BedrijfController(IBedrijfRepository repo)
        {
            _repo = repo;
        }

        // GET: api/bedrijf
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        // GET: api/bedrijf/5
        public IHttpActionResult Get(int id)
        {
            Bedrijf bedrijf = _repo.Get(id);
            if (bedrijf != null)
            {
                return Ok(bedrijf);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Bedrijf/209d8635-7ecd-4ca2-8b0d-67f196498547
        [Route("useraccountBedrijf/{id}")]
        public IHttpActionResult Get(string id)
        {
            Bedrijf bedrijf = _repo.GetBedrijfWithUserAccount(id);
            if (bedrijf != null)
            {
                return Ok(bedrijf);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/bedrijf/home/5
        [Route("bedrijf/home/{id}")]
        public IHttpActionResult GetHomeData(int id)
        {
            Bedrijf bedrijf = _repo.GetHomePageData(id);
            if (bedrijf != null)
            {
                return Ok(bedrijf);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/bedrijf/contactpersoonAndBedrijfspromotors/5
        [Route("bedrijf/contactpersoonAndBedrijfspromotors/{id}")]
        public IHttpActionResult GetBedrijfWithBedrijfspromotorsAndContactpersonen(int id)
        {
            Bedrijf bedrijf = _repo.GetBedrijfWithBedrijfspromotorsAndContactpersonen(id);
            if (bedrijf != null)
            {
                return Ok(bedrijf);
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        // POST: api/bedrijf
        public IHttpActionResult Post(Bedrijf bedrijf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdBedrijf = _repo.Post(bedrijf);
            
            return CreatedAtRoute("DefaultApi", new { controller = "Bedrijf", bedrijf.Id }, createdBedrijf);
        }

        // PUT: api/bedrijf/5
        public IHttpActionResult Put(int id, Bedrijf bedrijf)
        {
            if (!ModelState.IsValid || bedrijf.Id != id)
            {
                return BadRequest();
            }

            var rest = _repo.Get(id);
            if (rest != null)
            {
                _repo.Update(bedrijf);
                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/bedrijf/5
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
