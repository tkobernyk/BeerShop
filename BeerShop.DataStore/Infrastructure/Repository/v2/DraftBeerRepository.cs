using BeerShop.DataStore.Models.v2;
using BeerShop.DataStore.Infrastructure.Context;

namespace BeerShop.DataStore.Infrastructure.Repository.v2
{
    public class DraftBeerRepository : Repository<DraftBeer>
    {
        public DraftBeerRepository(IBeerShopContext dbContext) : base(dbContext) {}
    }
}
