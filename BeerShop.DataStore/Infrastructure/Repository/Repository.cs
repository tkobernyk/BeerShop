using System;
using System.Collections.Generic;
using BeerShop.DataStore.Models.Interfaces;
using BeerShop.DataStore.Infrastructure.Context;

namespace BeerShop.DataStore.Infrastructure.Repository
{
    public abstract class Repository<T> : IDisposable where T : IEntity 
    {
        protected readonly IBeerShopContext _dbContext;

        protected Repository(IBeerShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }
        public virtual IEnumerable<T> GetEntities(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
        public virtual T GetById(int id)
        {
            throw new NotImplementedException();
        }
        public virtual IEnumerable<T> GetByName(string name)
        {
            throw new NotImplementedException();
        }
        public virtual T Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
        public virtual T Add(T entity)
        {
            throw new NotImplementedException();
        }
        public virtual T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
