using CRUD.Net.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CRUD.Net.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(Guid id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
