using CRUD.Net.Domain.Repositories;
using CRUD.Net.Domain.Entities;
using CRUD.Net.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUD.Net.Infra.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly DbSet<TEntity> _entities;

        protected Repository(CrudDbContext context)
        {
            _entities = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public TEntity GetById(Guid id)
        {
            return _entities.Find(id);
        }

        public void Create(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

    }
}
