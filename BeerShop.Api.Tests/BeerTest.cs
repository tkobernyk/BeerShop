using System.Net;
using System.Linq;
using System.Web.Http.Results;
using System.Collections.Generic;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BeerShop.Api.Controllers;
using BeerShop.DataStore.Models;
using BeerShop.Api.Tests.DIController;
using BeerShop.DataStore.Infrastructure.Repository;

namespace BeerShop.Api.Tests
{
    [TestClass]
    public class BeerTest
    {
        private readonly IRepository<Beer> _repository;
        private readonly BeersController _controller;

        public BeerTest() : this(Unity.Register().Resolve<IRepository<Beer>>())
        { }

        public BeerTest(IRepository<Beer> repository)
        {
            _repository = repository;
            _controller = new BeersController(_repository);
        }

        [TestMethod]
        public void GetBeersTest()
        {
            var beers = _controller.GetBeers();
            Assert.IsNotNull(beers);
            Assert.AreEqual(beers, _repository.GetAll());
        }

        [TestMethod]
        public void GetBeerById()
        {
            var id = 1;
            var result = _controller.GetBeer(id) as OkNegotiatedContentResult<Beer>;
            Assert.IsNotNull(result);
            var beer = result.Content;
            Assert.IsNotNull(beer);
            Assert.AreEqual(beer, _repository.GetById(id));
        }

        [TestMethod]
        public void FaliedGetResultById()
        {
            var id = 10;
            var result = _controller.GetBeer(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetBeerByName()
        {
            var name = "Beer1";
            var testBeers = _repository.GetByName(name);

            var result = _controller.GetBeersByName(name)
                as OkNegotiatedContentResult<IEnumerable<Beer>>;

            Assert.IsNotNull(result);
            var beers = result.Content;
            Assert.IsNotNull(beers);
            Assert.AreEqual(beers.Count(), testBeers.Count());
        }

        [TestMethod]
        public void FaliedGetResultByName()
        {
            var name = "test";
            var result = _controller.GetBeersByName(name);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateBeer()
        {
            var id = 2;
            var beer = _repository.GetById(id);
            beer.Name = "Test2";
            var result = _controller.PutBeer(id, beer) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailedUpdateBeer()
        {
            var beer = new Beer
            {
                Id = 0,
                Name = "Test0"
            };
            var result = _controller.PutBeer(beer.Id, beer);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void AddBeer()
        {
            var beer = new Beer
            {
                Name = "Beer3",
                Volume = 0.5M,
                Country = "USA",
                Price = 17.0M
            };
            var result = _controller.PostBeer(beer) as CreatedAtRouteNegotiatedContentResult<Beer>;
            Assert.IsNotNull(result);
            var addedBeer = result.Content;
            Assert.AreNotEqual(addedBeer.Id, 0);
            Assert.AreEqual(addedBeer.Name, beer.Name);
        }

        [TestMethod]
        public void DeleteBeer()
        {
            int id = 1;
            var result = _controller.DeleteBeer(id) as OkNegotiatedContentResult<Beer>;
            Assert.IsNotNull(result);
            var addedBeer = result.Content;
            Assert.IsTrue(_repository.GetById(addedBeer.Id) == null);
        }

        [TestMethod]
        public void FaliedDeleteBeer()
        {
            int id = 10;
            var result = _controller.DeleteBeer(id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetBeersWithPagingTest()
        {
            var beers = _controller.GetBeers(1,2);
            Assert.IsNotNull(beers);
            Assert.AreEqual(beers.Count(), 2);
        }

        [TestMethod]
        public void FaliedGetBeersWithPagingTest()
        {
            var beers = _controller.GetBeers(10, 10);
            Assert.IsNotNull(beers);
            Assert.AreEqual(beers.Count(), 0);
        }
    }
}
