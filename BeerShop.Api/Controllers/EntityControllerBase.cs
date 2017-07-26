using System.Net;
using System.Linq;
using System.Web.Http;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

using BeerShop.DataStore.Infrastructure.Repository;
using BeerShop.DataStore.Models.Interfaces;

namespace BeerShop.Api.Controllers
{
    public abstract class EntityControllerBase<T> : ApiController where T : IEntity
    {
        protected readonly Repository<T> _repository;

        protected EntityControllerBase(Repository<T> repository)
        {
            _repository = repository;
        }

        protected virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        protected virtual IEnumerable<T> GetEntities(int pageIndex, int pageSize)
        {
            return _repository.GetEntities(pageIndex, pageSize);
        }

        protected virtual IHttpActionResult GetById(int id)
        {
            T entity = _repository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        protected virtual IHttpActionResult GetByName(string name)
        {
            var entities = _repository.GetByName(name);
            if (entities == null || entities.Count() == 0)
            {
                return NotFound();
            }
            return Ok(entities);
        }

        protected virtual IHttpActionResult Put(int id, T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entity.Id)
            {
                return BadRequest();
            }
            //db.Entry(beer).State = EntityState.Modified;
            try
            {
                _repository.Update(id, entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected virtual IHttpActionResult Post(T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedEntity = _repository.Add(entity);

            return CreatedAtRoute("DefaultApi", new { id = addedEntity.Id }, addedEntity);
        }

        protected virtual IHttpActionResult Delete(int id)
        {
            T entity = _repository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(_repository.Delete(entity));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntityExists(int id)
        {
            return _repository.GetById(id) != null;
        }
    }
}