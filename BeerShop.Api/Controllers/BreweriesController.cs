﻿using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Repository;

namespace BeerShop.Api.Controllers
{
    public class BreweriesController : EntityControllerBase<Brewery>
    {
        public BreweriesController(IRepository<Brewery> repository) : base(repository) {}

        // GET: api/Breweries
        public IEnumerable<Brewery> GetBreweries()
        {
            return GetAll();
        }

        // GET: api/Breweries/5
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult GetBrewery(int id)
        {
            return GetById(id);
        }

        // GET: api/Breweries/Brewery1
        [ResponseType(typeof(IEnumerable<Brewery>))]
        public IHttpActionResult GetBreweryByName(string name)
        {
            return GetByName(name);
        }

        // PUT: api/Breweries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrewery(int id, Brewery brewery)
        {
            return Put(id, brewery);
        }

        // POST: api/Breweries
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult PostBrewery(Brewery brewery)
        {
            return Post(brewery);
        }

        // DELETE: api/Breweries/5
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult DeleteBrewery(int id)
        {
            return Delete(id);
        }
    }
}