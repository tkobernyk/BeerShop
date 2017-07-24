using System.Data.Entity;
using BeerShop.DataStore.Models;
using System.Data.Entity.Infrastructure;

namespace BeerShop.DataStore
{
    public interface IBeerShopContext
    {
        IDbSet<Brewery> Breweries { get; set; }
        IDbSet<Beer> Beers { get; set; }

        int SaveChanges();
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();
    }
}
