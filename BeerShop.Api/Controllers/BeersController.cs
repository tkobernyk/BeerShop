using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Description;
using System.Web.Http.Results;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Models.v2;
using BeerShop.DataStore.Infrastructure.Repository;
using Microsoft.Web.Http;

namespace BeerShop.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [RoutePrefix("api/beers")]
    public class BeersController : EntityControllerBase<DraftBeer>
    {
        public BeersController(Repository<DraftBeer> repository) : base(repository) {}

        [HttpGet]
        [HttpHead]
        [Route("")]
        public IEnumerable<Beer> GetBeers()
        {
            return GetAll();
        }

        [HttpGet]
        [Route("{pageIndex:int}/{pageSize:int}")]
        [Route("pageIndex/{pageIndex:int}/pageSize/{pageSize:int}")]
        public IEnumerable<Beer> GetBeers([FromUri]int pageIndex, [FromUri]int pageSize)
        {
            return GetEntities(pageIndex, pageSize);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Route("id/{id:int}")]
        [ResponseType(typeof(Beer))]
        public IHttpActionResult GetBeer([FromUri]int id)
        {
            return GetById(id);
        }
        [HttpGet]
        [Route("{name}")]
        [Route("name/{name}")]
        [ResponseType(typeof(IEnumerable<Beer>))]
        public IHttpActionResult GetBeersByName([FromUri]string name)
        {
            return GetByName(name);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBeer([FromUri]int id, [FromBody]Beer beer)
        {
            return Put(id, DraftBeer.FromBeer(beer));
        }


        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Beer))]
        public IHttpActionResult PostBeer([FromBody]Beer beer)
        {
            return Post(DraftBeer.FromBeer(beer));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Beer))]
        public IHttpActionResult DeleteBeer([FromUri]int id)
        {
            return Delete(id);
        }
    }
}