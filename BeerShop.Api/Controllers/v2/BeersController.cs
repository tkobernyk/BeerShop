using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Description;

using Microsoft.Web.Http;

using BeerShop.DataStore.Models;
using BeerShop.DataStore.Models.v2;
using BeerShop.DataStore.Infrastructure.Repository;


namespace BeerShop.Api.Controllers.v2
{
    [ApiVersion("2.0")]
    [RoutePrefix("api/v{version:apiVersion}/beers")]
    public class BeersController : EntityControllerBase<DraftBeer>
    {
        public BeersController(Repository<DraftBeer> repository) : base(repository) { }

        [HttpGet]
        [HttpHead]
        [Route("")]
        public IEnumerable<DraftBeer> GetBeers()
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
        [ResponseType(typeof(DraftBeer))]
        public IHttpActionResult GetBeer([FromUri]int id)
        {
            return GetById(id);
        }
        [HttpGet]
        [Route("{name}")]
        [Route("name/{name}")]
        [ResponseType(typeof(IEnumerable<DraftBeer>))]
        public IHttpActionResult GetBeersByName([FromUri]string name)
        {
            return GetByName(name);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBeer([FromUri]int id, [FromBody]DraftBeer beer)
        {
            return Put(id, beer);
        }


        [HttpPost]
        [Route("")]
        [ResponseType(typeof(DraftBeer))]
        public IHttpActionResult PostBeer([FromBody]DraftBeer beer)
        {
            return Post(beer);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteBeer([FromUri]int id)
        {
            return Delete(id);
        }
    }
}