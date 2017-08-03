using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.ObjectModel;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Models.v2;
using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.Api.Tests.Stubs.Infrastructure.Database.DBSet;



namespace BeerShop.Api.Tests.Stubs.Infrastructure.Database
{
    internal class FakeBeerShopContext : IBeerShopContext
    {
        public FakeBeerShopContext()
        {
            Init();
        }

        public IDbSet<Brewery> Breweries { get; set; }
        public IDbSet<DraftBeer> Beers { get; set; }

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
            Beers = new FakeDraftBeerDbSet{
                new DraftBeer
                {
                    Id = 1,
                    Name = "Beer1",
                    Volume = 0.5,
                    Country = "Ukraine",
                    Price = 15.0,
                    IsDraft = true,
                    Breweries = new Collection<Brewery> { Breweries.Find(1) }
                },
                new DraftBeer
                {
                    Id = 2,
                    Name = "Beer2",
                    Volume = 0.5,
                    Country = "Ukraine",
                    Price = 25.0,
                    Breweries = new Collection<Brewery> { Breweries.Find(2) }
                },
                new DraftBeer
                {
                    Id = 3,
                    Name = "Beer3",
                    Volume = 0.33,
                    Country = "Ukraine",
                    Price = 20.50,
                    IsDraft = true,
                    Breweries = new Collection<Brewery> { Breweries.Find(1), Breweries.Find(2) }
                }
            };
            foreach (var brewery in Breweries)
            {
                var beers = Beers.Where(b => b.Breweries.Any(br => br.Id == brewery.Id));
                foreach (var beer in beers)
                {
                    brewery.Beers.Add(beer);
                }                    
            }
        }
    }
}
