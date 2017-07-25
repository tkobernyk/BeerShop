using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.DataStore.Infrastructure.Repository;

namespace BeerShop.Api.Tests.Stubs.Infrastructure.Repository
{
    class FakeBeerRepository : FakeRepositoryBase, IRepository<Beer>
    {
        public FakeBeerRepository(IBeerShopContext dbContext) : base (dbContext) { }
        public IEnumerable<Beer> GetAll()
        {
            return _dbContext.Beers;
        }

        public Beer GetById(int id)
        {
            return _dbContext.Beers.Find(id);
        }

        public IEnumerable<Beer> GetByName(string name)
        {
            return _dbContext.Beers.Where(br => br.Name == name);
        }
        public Beer Add(Beer entity)
        {
            int maxId = _dbContext.Beers.Max(br => br.Id);
            entity.Id = maxId + 1;
            _dbContext.Beers.Add(entity);
            return entity;
        }
        public Beer Update(int id, Beer entity)
        {
            var oldentity = _dbContext.Beers.Find(id);
            if (oldentity == null)
                throw new DbUpdateConcurrencyException();
            _dbContext.Beers.Remove(oldentity);
            _dbContext.Beers.Add(entity);
            return entity;
        }
        public Beer Delete(Beer entity)
        {
            return _dbContext.Beers.Remove(entity);
        }
    }
}
