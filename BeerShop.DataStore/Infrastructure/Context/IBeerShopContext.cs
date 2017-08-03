using System.Data.Entity;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Models.v2;

namespace BeerShop.DataStore.Infrastructure.Context
{
    public interface IBeerShopContext
    {
        IDbSet<Brewery> Breweries { get; set; }
        IDbSet<DraftBeer> Beers { get; set; }

        int SaveChanges();

        void Dispose();
    }
}
