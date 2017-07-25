using BeerShop.DataStore.Infrastructure.Context;
using System;

namespace BeerShop.Api.Tests.Stubs.Infrastructure.Repository
{
    abstract class FakeRepositoryBase : IDisposable
    {
        protected readonly IBeerShopContext _dbContext;

        protected FakeRepositoryBase(IBeerShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
