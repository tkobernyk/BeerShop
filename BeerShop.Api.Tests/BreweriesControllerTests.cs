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
    public class BreweriesControllerTests
    {
        private readonly Repository<Brewery> _repository;
        private readonly BreweriesController _controller;

        public BreweriesControllerTests() : this(Unity.Register().Resolve<Repository<Brewery>>())
        {}

        public BreweriesControllerTests(Repository<Brewery> repository)
        {
            _repository = repository;
            _controller = new BreweriesController(_repository);
        }

        [TestMethod]
        [TestCategory("Api.GetData")]
        public void GetBreweries()
        {
            var breweries = _controller.GetBreweries();
            Assert.IsNotNull(breweries);
            Assert.AreEqual(breweries, _repository.GetAll());
        }

        [TestMethod]
        [TestCategory("Api.GetData")]
        public void GetBreweryById()
        {
            var id = 1;
            var result = _controller.GetBrewery(id) as OkNegotiatedContentResult<Brewery>;
            Assert.IsNotNull(result);
            var brewery = result.Content;
            Assert.IsNotNull(brewery);
            Assert.AreEqual(brewery, _repository.GetById(id));
        }

        [TestMethod]
        [TestCategory("Api.GetData.Validation")]
        public void FaliedGetResultById()
        {
            var id = 10;
            var result = _controller.GetBrewery(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        [TestCategory("Api.GetData")]
        public void GetBreweryByName()
        {
            var name = "Brewery1";
            var testBreweries = _repository.GetByName(name);

            var result = _controller.GetBreweriesByName(name) 
                as OkNegotiatedContentResult<IEnumerable<Brewery>>;

            Assert.IsNotNull(result);
            var breweries = result.Content;
            Assert.IsNotNull(breweries);
            Assert.AreEqual(breweries.Count(), testBreweries.Count());
        }

        [TestMethod]
        [TestCategory("Api.GetData.Validation")]
        public void FaliedGetResultByName()
        {
            var name = "test";
            var result = _controller.GetBreweriesByName(name);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        [TestCategory("Api.ModifyData")]
        public void UpdateBrewery()
        {
            var id = 2;
            var brewery = _repository.GetById(id);
            brewery.Name = "Test2";
            var result = _controller.PutBrewery(id, brewery) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        [TestCategory("Api.ModifyData.Validation")]
        public void FailedUpdateBrewery()
        {
            var brewery = new Brewery
            {
                Id = 0,
                Name = "Test0"
            };
            var result = _controller.PutBrewery(brewery.Id, brewery);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        [TestCategory("Api.ModifyData.Validation")]
        public void FailedUpdateBreweryWithInValidModel()
        {
            var brewery = new Brewery
            {
                Id = 0,
                Name = "Test0"
            };
            _controller.ModelState.AddModelError("test", "test");
            var result = _controller.PutBrewery(brewery.Id, brewery);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        [TestCategory("Api.ModifyData")]
        public void AddBrewery()
        {
            var brewery = new Brewery
            {
                Name = "Brewery3"
            };
            var result = _controller.PostBrewery(brewery) as CreatedAtRouteNegotiatedContentResult<Brewery>;
            Assert.IsNotNull(result);
            var addedBrewery = result.Content;
            Assert.AreNotEqual(addedBrewery.Id, 0);
            Assert.AreEqual(addedBrewery.Name, brewery.Name);
        }

        [TestMethod]
        [TestCategory("Api.ModifyData")]
        public void DeleteBrewery()
        {
            int id = 1;
            var result = _controller.DeleteBrewery(id) as OkNegotiatedContentResult<Brewery>;
            Assert.IsNotNull(result);
            var addedBrewery = result.Content;
            Assert.IsTrue(_repository.GetById(addedBrewery.Id) == null);
        }

        [TestMethod]
        [TestCategory("Api.ModifyData.Validation")]
        public void FaliedDeleteBrewery()
        {
            int id = 10;
            var result = _controller.DeleteBrewery(id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        [TestCategory("Api.GetData")]
        public void GetBreweriesWithPaging()
        {
            var breweries = _controller.GetBreweries(1, 2);
            Assert.IsNotNull(breweries);
            Assert.AreEqual(breweries.Count(), 2);
        }

        [TestMethod]
        [TestCategory("Api.GetData.Validation")]
        public void FaliedGetBeersWithPaging()
        {
            var breweries = _controller.GetBreweries(10, 10);
            Assert.IsNotNull(breweries);
            Assert.AreEqual(breweries.Count(), 0);
        }
    }
}
