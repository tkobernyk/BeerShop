using System;
using System.Linq;
using System.Collections.Generic;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Context;
using BeerShop.DataStore.Infrastructure.Exception;

namespace BeerShop.DataStore.Infrastructure.Repository
{
    public class BreweryRepository : IRepository<Brewery>
    {
        private readonly IBeerShopContext _dbContext;
        public BreweryRepository(IBeerShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Brewery> GetAll()
        {
            return _dbContext.Breweries;
        }

        public Brewery GetById(int id)
        {
            return _dbContext.Breweries.Find(id);
        }

        public IEnumerable<Brewery> GetByName(string name)
        {
            return _dbContext.Breweries.Where(br => br.Name == name);
        }
        public Brewery Add(Brewery entity)
        {
            int maxId = _dbContext.Breweries.Max(br => br.Id);
            entity.Id = maxId + 1;
            _dbContext.Breweries.Add(entity);
            return entity;
        }
        public Brewery Update(int id, Brewery entity)
        {
            var oldentity = _dbContext.Breweries.Find(id);
            if (oldentity == null)
                throw new EntityNotFoundException();
            _dbContext.Breweries.Remove(oldentity);
            _dbContext.Breweries.Add(entity);
            return entity;
        }
        public Brewery Delete(Brewery entity)
        {
            return _dbContext.Breweries.Remove(entity);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
