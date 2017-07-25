using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using BeerShop.DataStore.Infrastructure.Repository;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Exception;

namespace BeerShop.Api.Controllers
{
    public class BreweriesController : ApiController
    {
        private readonly IRepository<Brewery> _repository;

        public BreweriesController(IRepository<Brewery> repository)
        {
            _repository = repository;
        }

        // GET: api/Breweries
        public IEnumerable<Brewery> GetBreweries()
        {
            return _repository.GetAll();
        }

        // GET: api/Breweries/5
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult GetBrewery(int id)
        {
            Brewery brewery = _repository.GetById(id);
            if (brewery == null)
            {
                return NotFound();
            }
            return Ok(brewery);
        }

        // GET: api/Breweries/Brewery1
        [ResponseType(typeof(IEnumerable<Brewery>))]
        public IHttpActionResult GetBreweryByName(string name)
        {
            var breweries = _repository.GetByName(name);
            if (breweries == null || breweries.Count() == 0)
            {
                return NotFound();
            }
            return Ok(breweries);
        }

        // PUT: api/Breweries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrewery(int id, Brewery brewery)
        {
            Brewery updatedBrewery = new Brewery { Id = id };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brewery.Id)
            {
                return BadRequest();
            }
            try
            {
                updatedBrewery = _repository.Update(id, brewery);
            }
            catch (EntityNotFoundException)
            { 
                if (!BreweryExists(updatedBrewery.Id))
                {
                    return NotFound();
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Breweries
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult PostBrewery(Brewery brewery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newBrewery = _repository.Add(brewery);
            return CreatedAtRoute("DefaultApi", new { id = newBrewery.Id }, newBrewery);
        }

        // DELETE: api/Breweries/5
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult DeleteBrewery(int id)
        {
            Brewery brewery = _repository.GetById(id);
            if (brewery == null)
            {
                return NotFound();
            }
            return Ok(_repository.Delete(brewery));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BreweryExists(int id)
        {
            return _repository.GetById(id) != null;
        }
    }
}