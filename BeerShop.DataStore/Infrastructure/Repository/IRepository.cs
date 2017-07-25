using BeerShop.DataStore.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace BeerShop.DataStore.Infrastructure.Repository
{
    public interface IRepository<T> : IDisposable where T : IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> GetByName(string name);
        T Update(int id, T entity);
        T Add(T entity);
        T Delete(T entity);
    }
}
