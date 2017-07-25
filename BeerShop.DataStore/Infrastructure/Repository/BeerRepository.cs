using System;
using System.Linq;
using System.Collections.Generic;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Context;

namespace BeerShop.DataStore.Infrastructure.Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        private readonly IBeerShopContext _dbContext;

        public BeerRepository(IBeerShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Beer Add(Beer entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Beer Delete(Beer entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IEnumerable<Beer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Beer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Beer> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Beer Update(int id, Beer entity)
        {
            throw new NotImplementedException();
        }
    }
}
