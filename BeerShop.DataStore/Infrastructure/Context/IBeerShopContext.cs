using System.Data.Entity;
using BeerShop.DataStore.Models;
using System.Data.Entity.Infrastructure;

namespace BeerShop.DataStore.Infrastructure.Context
{
    public interface IBeerShopContext
    {
        IDbSet<Brewery> Breweries { get; set; }
        IDbSet<Beer> Beers { get; set; }

        int SaveChanges();

        void Dispose();
    }
}
