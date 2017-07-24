using System.Linq;
using BeerShop.DataStore;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeerShop.Api.Controllers;
using System.Web.Http.Results;
using BeerShop.DataStore.Models;
using BeerShop.Api.Tests.DIController;

namespace BeerShop.Api.Tests
{
    [TestClass]
    public class BreweryTests
    {
        private readonly IBeerShopContext _dbContext;

        public BreweryTests() : this(Unity.Register().Resolve<IBeerShopContext>())
        {}

        public BreweryTests(IBeerShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        [TestMethod]
        public void GetBreweriesTest()
        {
            var controller = new BreweriesController(_dbContext);
            var breweries = controller.GetBreweries();
            Assert.IsNotNull(breweries);
            Assert.AreEqual(breweries, _dbContext.Breweries.AsQueryable());
        }

        [TestMethod]
        public void GetBreweryById()
        {
            var id = 1;
            var controller = new BreweriesController(_dbContext);
            var result = controller.GetBrewery(id) as OkNegotiatedContentResult<Brewery>;
            Assert.IsNotNull(result);
            var brewery = result.Content;
            Assert.IsNotNull(brewery);
            Assert.AreEqual(brewery, _dbContext.Breweries.Find(id));
        }
        [TestMethod]
        public void GetNotFoundResult()
        {
            var id = 10;
            var controller = new BreweriesController(_dbContext);
            var result = controller.GetBrewery(id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
