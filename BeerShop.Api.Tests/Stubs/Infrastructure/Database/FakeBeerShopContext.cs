using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.ObjectModel;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.Api.Tests.Stubs.Infrastructure.Database.DBSet;


namespace BeerShop.Api.Tests.Stubs.Infrastructure.Database
{
    class FakeBeerShopContext : IBeerShopContext
    {
        public FakeBeerShopContext()
        {
            Init();
        }

        public IDbSet<Brewery> Breweries { get; set; }
        public IDbSet<Beer> Beers { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return 1;
        }

        private void Init()
        {
           Breweries = new FakeBreweryDbSet {
                new Brewery
                {
                    Id = 1,
                    Name = "Brewery1",
                    Beers = new Collection<Beer>()
        },
                new Brewery
                {
                    Id = 2,
                    Name = "Brewery2",
                    Beers = new Collection<Beer>()
                }
            };
            Beers = new FakeBeerDbSet{
                new Beer
                {
                    Id = 1,
                    Name = "Beer1",
                    Volume = 0.5M,
                    Country = "Ukraine",
                    Price = 15.0M,
                    Breweries = new Collection<Brewery> { Breweries.Find(1) }
                },
                new Beer
                {
                    Id = 2,
                    Name = "Beer2",
                    Volume = 0.5M,
                    Country = "Ukraine",
                    Price = 25.0M,
                    Breweries = new Collection<Brewery> { Breweries.Find(2) }
                },
                new Beer
                {
                    Id = 3,
                    Name = "Beer3",
                    Volume = 0.33M,
                    Country = "Ukraine",
                    Price = 20.50M,
                    Breweries = new Collection<Brewery> { Breweries.Find(1), Breweries.Find(2) }
                }
            };
            foreach (var brewery in Breweries)
            {
                var beers = Beers.Where(b => b.Breweries.Where(br => br.Id == brewery.Id).Count() > 0);
                foreach (var beer in beers)
                {
                    brewery.Beers.Add(beer);
                }                    
            }
        }
    }
}
