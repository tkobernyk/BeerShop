using System.Net;
using System.Linq;
using System.Web.Http.Results;
using System.Collections.Generic;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BeerShop.Api.Controllers.v2;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Models.v2;
using BeerShop.Api.Tests.DIController;
using BeerShop.DataStore.Infrastructure.Repository;


namespace BeerShop.Api.Tests
{
    [TestClass]
    public class BeersControllerV2Test
    {
        private readonly Repository<DraftBeer> _repository;
        private readonly BeersController _controller;

        public BeersControllerV2Test() : this(Unity.Register().Resolve<Repository<DraftBeer>>())
        { }

        public BeersControllerV2Test(Repository<DraftBeer> repository)
        {
            _repository = repository;
            _controller = new BeersController(_repository);
        }

        [TestMethod]
        [TestCategory("Api.GetData")]
        public void GetDraftBeers()
        {
            var beers = _controller.GetBeers();
            Assert.IsNotNull(beers);
            Assert.AreEqual(beers, _repository.GetAll());
        }

        [TestMethod]
        [TestCategory("Api.GetData")]
        public void GetDraftBeerById()
        {
            var id = 1;
            var result = _controller.GetBeer(id) as OkNegotiatedContentResult<DraftBeer>;
            Assert.IsNotNull(result);
            var beer = result.Content;
            Assert.IsNotNull(beer);
            Assert.AreEqual(beer, _repository.GetById(id));
        }

        [TestMethod]
        [TestCategory("Api.GetData.Validation")]
        public void FaliedGetDraftBeerById()
        {
            var id = 10;
            var result = _controller.GetBeer(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        [TestCategory("Api.GetData")]
        public void GetDraftBeerByName()
        {
            var name = "Beer1";
            var testBeers = _repository.GetByName(name);

            var result = _controller.GetBeersByName(name)
                as OkNegotiatedContentResult<IEnumerable<DraftBeer>>;

            Assert.IsNotNull(result);
            var beers = result.Content as IEnumerable<Beer>;
            Assert.IsNotNull(beers);
            Assert.AreEqual(beers.Count(), testBeers.Count());
        }

        [TestMethod]
        [TestCategory("Api.GetData.Validation")]
        public void FaliedGetDraftBeerByName()
        {
            var name = "test";
            var result = _controller.GetBeersByName(name);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        [TestCategory("Api.ModifyData")]
        public void UpdateDraftBeer()
        {
            var id = 2;
            var beer = _repository.GetById(id);
            beer.Name = "Test2";
            var result = _controller.PutBeer(id, beer) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        [TestCategory("Api.ModifyData.Validation")]
        public void FailedUpdateDraftBeer()
        {
            var beer = new DraftBeer
            {
                Id = 0,
                Name = "Test0"
            };
            var result = _controller.PutBeer(beer.Id, beer);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        [TestCategory("Api.ModifyData.Validation")]
        public void FailedUpdateDraftBeerWithInValidModel()
        {
            var beer = new DraftBeer
            {
                Id = 0,
                Name = "Test0"
            };
            _controller.ModelState.AddModelError("test", "test");
            var result = _controller.PutBeer(beer.Id, beer);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        [TestCategory("Api.ModifyData")]
        public void AddDraftBeer()
        {
            var beer = new DraftBeer
            {
                Name = "Beer3",
                Volume = 0.5,
                Country = "USA",
                Price = 17.0
            };
            var result = _controller.PostBeer(beer) as CreatedAtRouteNegotiatedContentResult<DraftBeer>;
            Assert.IsNotNull(result);
            var addedBeer = result.Content;
            Assert.AreNotEqual(addedBeer.Id, 0);
            Assert.AreEqual(addedBeer.Name, beer.Name);
        }

        [TestMethod]
        [TestCategory("Api.ModifyData")]
        public void DeleteDraftBeer()
        {
            int id = 1;
            var result = _controller.DeleteBeer(id) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        [TestCategory("Api.ModifyData.Validation")]
        public void FaliedDeleteDraftBeer()
        {
            int id = 10;
            var result = _controller.DeleteBeer(id) as NotFoundResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        [TestCategory("Api.GetData")]
        public void GetDraftBeersWithPaging()
        {
            var beers = _controller.GetBeers(1,2);
            Assert.IsNotNull(beers);
            Assert.AreEqual(beers.Count(), 2);
        }

        [TestMethod]
        [TestCategory("Api.GetData.Validation")]
        public void FaliedGetDraftBeersWithPaging()
        {
            var beers = _controller.GetBeers(10, 10);
            Assert.IsNotNull(beers);
            Assert.AreEqual(beers.Count(), 0);
        }
    }
}
