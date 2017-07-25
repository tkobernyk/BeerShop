using System;
using BeerShop.DataStore.Infrastructure.Context;


namespace BeerShop.DataStore.Infrastructure.Repository
{
    public class RepositoryBase : IDisposable
    {
        protected readonly IBeerShopContext _dbContext;

        protected RepositoryBase(IBeerShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
