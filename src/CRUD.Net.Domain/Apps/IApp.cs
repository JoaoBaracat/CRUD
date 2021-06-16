using CRUD.Net.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CRUD.Net.Domain.Apps
{
    public interface IApp<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(Guid id);
        void Create(TEntity entity);
        void Update(Guid id, TEntity entity);
        void CreateOrUpdate(TEntity entity);
        void Delete(Guid id);
    }
}
