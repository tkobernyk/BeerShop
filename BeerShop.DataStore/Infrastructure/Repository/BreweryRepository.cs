using System;
using System.Collections.Generic;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Context;

namespace BeerShop.DataStore.Infrastructure.Repository
{
    public class BreweryRepository : RepositoryBase, IRepository<Brewery>
    {
        public BreweryRepository(IBeerShopContext dbContext) : base(dbContext) {}
        public IEnumerable<Brewery> GetAll()
        {
            throw new NotImplementedException();
        }

        public Brewery GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brewery> GetByName(string name)
        {
            throw new NotImplementedException();
        }
        public Brewery Add(Brewery entity)
        {
            throw new NotImplementedException();
        }
        public Brewery Update(int id, Brewery entity)
        {
            throw new NotImplementedException();
        }
        public Brewery Delete(Brewery entity)
        {
            throw new NotImplementedException();
        }
    }
}
