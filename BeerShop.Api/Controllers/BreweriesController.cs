using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;

using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Repository;

namespace BeerShop.Api.Controllers
{
    [Route("api/breweries")]
    public class BreweriesController : EntityControllerBase<Brewery>
    {
        public BreweriesController(Repository<Brewery> repository) : base(repository) {}

        [AcceptVerbs("GET", "HEAD")]
        [Route("breweries")]
        public IEnumerable<Brewery> GetBreweries()
        {
            return GetAll();
        }

        [HttpGet]
        [Route("breweries/{pageIndex:int}/{pageSize:int}")]
        public IEnumerable<Brewery> GetBreweries([FromUri]int pageIndex, [FromUri]int pageSize)
        {
            return GetEntities(pageIndex, pageSize);
        }

        [HttpGet]
        [Route("breweries/{id:int}")]
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult GetBrewery([FromUri]int id)
        {
            return GetById(id);
        }

        [HttpGet]
        [Route("breweries/{name}")]
        [ResponseType(typeof(IEnumerable<Brewery>))]
        public IHttpActionResult GetBreweriesByName([FromUri]string name)
        {
            return GetByName(name);
        }

        [Route("breweries/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrewery([FromUri]int id, [FromBody]Brewery brewery)
        {
            return Put(id, brewery);
        }

 
        [HttpPost]
        [Route("breweries")]
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult PostBrewery([FromBody]Brewery brewery)
        {
            return Post(brewery);
        }

        [HttpDelete]
        [Route("breweries/{id:int}")]
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult DeleteBrewery([FromUri]int id)
        {
            return Delete(id);
        }
    }
}