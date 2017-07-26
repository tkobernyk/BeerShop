using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Description;

using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Repository;

namespace BeerShop.Api.Controllers
{
    [Route("api/beers")]
    public class BeersController : EntityControllerBase<Beer>
    {
        public BeersController(Repository<Beer> repository) : base(repository) {}

        [AcceptVerbs("GET", "HEAD")]
        [Route("beers")]
        public IEnumerable<Beer> GetBeers()
        {
            return GetAll();
        }

        [HttpGet]
        [Route("beers/{pageIndex:int}/{pageSize:int}")]
        public IEnumerable<Beer> GetBeers([FromUri]int pageIndex, [FromUri]int pageSize)
        {
            return GetEntities(pageIndex, pageSize);
        }

        [HttpGet]
        [Route("beers/{id:int}")]
        [ResponseType(typeof(Beer))]
        public IHttpActionResult GetBeer([FromUri]int id)
        {
            return GetById(id);
        }
        [HttpGet]
        [Route("beers/{name}")]
        [ResponseType(typeof(IEnumerable<Beer>))]
        public IHttpActionResult GetBeersByName([FromUri]string name)
        {
            return GetByName(name);
        }

        [HttpPut]
        [Route("beers/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBeer([FromUri]int id, [FromBody]Beer beer)
        {
            return Put(id, beer);
        }


        [HttpPost]
        [Route("beers")]
        [ResponseType(typeof(Beer))]
        public IHttpActionResult PostBeer([FromBody]Beer beer)
        {
            return Post(beer);
        }

        [HttpDelete]
        [Route("beers/{id:int}")]
        [ResponseType(typeof(Beer))]
        public IHttpActionResult DeleteBeer([FromUri]int id)
        {
            return Delete(id);
        }
    }
}