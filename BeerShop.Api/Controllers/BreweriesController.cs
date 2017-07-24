using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BeerShop.DataStore;
using BeerShop.DataStore.Models;

namespace BeerShop.Api.Controllers
{
    public class BreweriesController : ApiController
    {
        private readonly IBeerShopContext _dbContext;

        public BreweriesController(IBeerShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Breweries
        public IQueryable<Brewery> GetBreweries()
        {
            return _dbContext.Breweries;
        }

        // GET: api/Breweries/5
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult GetBrewery(int id)
        {
            Brewery brewery = _dbContext.Breweries.Find(id);
            if (brewery == null)
            {
                return NotFound();
            }

            return Ok(brewery);
        }

        // PUT: api/Breweries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrewery(int id, Brewery brewery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brewery.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(brewery).State = EntityState.Modified;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreweryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
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

            _dbContext.Breweries.Add(brewery);
            _dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = brewery.Id }, brewery);
        }

        // DELETE: api/Breweries/5
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult DeleteBrewery(int id)
        {
            Brewery brewery = _dbContext.Breweries.Find(id);
            if (brewery == null)
            {
                return NotFound();
            }

            _dbContext.Breweries.Remove(brewery);
            _dbContext.SaveChanges();

            return Ok(brewery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BreweryExists(int id)
        {
            return _dbContext.Breweries.Count(e => e.Id == id) > 0;
        }
    }
}