using System.Data.Entity;
using BeerShop.DataStore.Models;
using BeerShop.DataStore;
using System;
using System.Data.Entity.Infrastructure;

namespace BeerShop.Api.Tests.Stubs
{
    class FakeBeerShopContext : DbContext, IBeerShopContext
    {
        public FakeBeerShopContext()
        {
            Breweries = new FakeBreweryDbSet();
            Beers = new FakeBeerDbSet();
        }

        public IDbSet<Brewery> Breweries { get; set; }
        public IDbSet<Beer> Beers { get; set; }
    }
}
