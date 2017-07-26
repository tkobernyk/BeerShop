using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Description;

using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Repository;
using System.Linq;

namespace BeerShop.Api.Controllers
{
    public class BeersController : EntityControllerBase<Beer>
    {
        public BeersController(Repository<Beer> repository) : base(repository) {}

        // GET: api/Beers
        public IEnumerable<Beer> GetBeers()
        {
            return GetAll();
        }
        // GET: api/Beers?pageIndex=1&pageSize=1
        public IEnumerable<Beer> GetBeers(int pageIndex, int pageSize)
        {
            return GetEntities(pageIndex, pageSize);
        }

        // GET: api/Beers/5
        [ResponseType(typeof(Beer))]
        public IHttpActionResult GetBeer(int id)
        {
            return GetById(id);
        }

        // GET: api/Beers/GetBeersByName/Beer1
        [ResponseType(typeof(IEnumerable<Beer>))]
        public IHttpActionResult GetBeersByName(string name)
        {
            return GetByName(name);
        }

        // PUT: api/Beers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBeer(int id, Beer beer)
        {
            return Put(id, beer);
        }

        // POST: api/Beers
        [ResponseType(typeof(Beer))]
        public IHttpActionResult PostBeer(Beer beer)
        {
            return Post(beer);
        }

        // DELETE: api/Beers/5
        [ResponseType(typeof(Beer))]
        public IHttpActionResult DeleteBeer(int id)
        {
            return Delete(id);
        }
    }
}