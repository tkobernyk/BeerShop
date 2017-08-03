using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.DataStore.Infrastructure.Repository;
using BeerShop.DataStore.Models.v2;

namespace BeerShop.Api.Tests.Stubs.Infrastructure.Repository
{
    internal class FakeDraftBeerRepository : Repository<DraftBeer>
    {
        public FakeDraftBeerRepository(IBeerShopContext dbContext) : base (dbContext) { }
        public override IEnumerable<DraftBeer> GetAll()
        {
            return _dbContext.Beers;
        }

        public override IEnumerable<DraftBeer> GetEntities(int pageIndex, int pageSize)
        {
            return _dbContext.Beers.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public override DraftBeer GetById(int id)
        {
            return _dbContext.Beers.Find(id);
        }

        public override IEnumerable<DraftBeer> GetByName(string name)
        {
            return _dbContext.Beers.Where(br => br.Name == name);
        }
        public override DraftBeer Add(DraftBeer entity)
        {
            int maxId = _dbContext.Beers.Max(br => br.Id);
            entity.Id = maxId + 1;
            _dbContext.Beers.Add(entity);
            return entity;
        }
        public override DraftBeer Update(int id, DraftBeer entity)
        {
            var oldentity = _dbContext.Beers.Find(id);
            if (oldentity == null)
                throw new DbUpdateConcurrencyException();
            _dbContext.Beers.Remove(oldentity);
            _dbContext.Beers.Add(entity);
            return entity;
        }
        public override DraftBeer Delete(DraftBeer entity)
        {
            return _dbContext.Beers.Remove(entity);
        }
    }
}
