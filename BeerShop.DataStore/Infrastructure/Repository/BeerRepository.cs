using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Context;

namespace BeerShop.DataStore.Infrastructure.Repository
{
    public class BeerRepository : Repository<Beer>
    {
        public BeerRepository(IBeerShopContext dbContext) : base(dbContext) {}
    }
}
