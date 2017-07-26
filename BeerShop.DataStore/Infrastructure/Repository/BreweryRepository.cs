using System;
using System.Collections.Generic;
using BeerShop.DataStore.Models;
using BeerShop.DataStore.Infrastructure.Context;

namespace BeerShop.DataStore.Infrastructure.Repository
{
    public class BreweryRepository : Repository<Brewery>
    {
        public BreweryRepository(IBeerShopContext dbContext) : base(dbContext) {}
    }
}
