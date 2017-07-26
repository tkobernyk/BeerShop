using System;
using System.Collections.Generic;
using BeerShop.Logging;
using BeerShop.DataStore.Models.Interfaces;
using BeerShop.DataStore.Infrastructure.Context;


namespace BeerShop.DataStore.Infrastructure.Repository
{
    public abstract class Repository<T> : IDisposable where T : IEntity 
    {
        private readonly ILogger _log;
        protected readonly IBeerShopContext _dbContext;

        protected Repository(IBeerShopContext dbContext)
        {
            _dbContext = dbContext;
            _log = GlobalLogger.DefaultLogger;
        }
        public virtual IEnumerable<T> GetAll()
        {
            _log.Error("Not Implemented", new NotImplementedException());
            throw new NotImplementedException();
        }
        public virtual IEnumerable<T> GetEntities(int pageIndex, int pageSize)
        {
            _log.Error("Not Implemented", new NotImplementedException());
            throw new NotImplementedException();
        }
        public virtual T GetById(int id)
        {
            _log.Error("Not Implemented", new NotImplementedException());
            throw new NotImplementedException();
        }
        public virtual IEnumerable<T> GetByName(string name)
        {
            _log.Error("Not Implemented", new NotImplementedException());
            throw new NotImplementedException();
        }
        public virtual T Update(int id, T entity)
        {
            _log.Error("Not Implemented", new NotImplementedException());
            throw new NotImplementedException();
        }
        public virtual T Add(T entity)
        {
            _log.Error("Not Implemented", new NotImplementedException());
            throw new NotImplementedException();
        }
        public virtual T Delete(T entity)
        {
            _log.Error("Not Implemented", new NotImplementedException());
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
