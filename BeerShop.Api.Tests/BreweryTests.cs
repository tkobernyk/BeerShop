using BeerShop.DataStore;
using BeerShop.Api.Tests.Stubs;
using BeerShop.DataStore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.ObjectModel;

namespace BeerShop.Api.Tests
{
    [TestClass]
    public class BreweryTests
    {
        private IBeerShopContext _dbContext;
        public BreweryTests()
        {
            Init();
        }
        [TestMethod]
        public void AddBreweryTest()
        {
        }

        private void Init()
        {
            _dbContext = new FakeBeerShopContext
            {
                Breweries = {
                    new Brewery {
                        Id = 1,
                        Name = "Brewery1"
                    },
                    new Brewery
                    {
                        Id = 2,
                        Name = "Brewery2"
                    }
                }
            };
            _dbContext.Beers = new FakeBeerDbSet{
                new Beer
                {
                    Id = 1,
                    Name = "Beer1",
                    Volume = 0.5M,
                    Country = "Ukraine",
                    Breweries = new Collection<Brewery> { _dbContext.Breweries.Find(1) }
                },
                new Beer
                {
                    Id = 2,
                    Name = "Beer2",
                    Volume = 0.5M,
                    Country = "Ukraine",
                    Breweries = new Collection<Brewery> { _dbContext.Breweries.Find(2) }
                },
                new Beer
                {
                    Id = 3,
                    Name = "Beer3",
                    Volume = 0.33M,
                    Country = "Ukraine",
                    Breweries = new Collection<Brewery> { _dbContext.Breweries.Find(1), _dbContext.Breweries.Find(2) }
                }
            };
        }
    }
}
