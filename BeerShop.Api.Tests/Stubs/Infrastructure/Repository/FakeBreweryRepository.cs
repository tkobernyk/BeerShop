using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.DataStore.Infrastructure.Repository;

namespace BeerShop.Api.Tests.Stubs.Infrastructure.Repository
{
    class FakeBreweryRepository : Repository<Brewery>
    {
        public FakeBreweryRepository(IBeerShopContext dbContext) : base (dbContext) { }
        public override IEnumerable<Brewery> GetAll()
        {
            return _dbContext.Breweries;
        }

        public override IEnumerable<Brewery> GetEntities(int pageIndex, int pageSize)
        {
            return _dbContext.Breweries.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public override Brewery GetById(int id)
        {
            return _dbContext.Breweries.Find(id);
        }

        public override IEnumerable<Brewery> GetByName(string name)
        {
            return _dbContext.Breweries.Where(br => br.Name == name);
        }
        public override Brewery Add(Brewery entity)
        {
            int maxId = _dbContext.Breweries.Max(br => br.Id);
            entity.Id = maxId + 1;
            _dbContext.Breweries.Add(entity);
            return entity;
        }
        public override Brewery Update(int id, Brewery entity)
        {
            var oldentity = _dbContext.Breweries.Find(id);
            if (oldentity == null)
                throw new DbUpdateConcurrencyException();
            _dbContext.Breweries.Remove(oldentity);
            _dbContext.Breweries.Add(entity);
            return entity;
        }
        public override Brewery Delete(Brewery entity)
        {
            return _dbContext.Breweries.Remove(entity);
        }
    }
}
