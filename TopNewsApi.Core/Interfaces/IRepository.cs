﻿using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNewsApi.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task Save();
        Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetByID(object id);
        Task Insert(TEntity entity);
        Task Delete(object id);
        Task Delete(TEntity entityToDelete);
        Task Update(TEntity ententityToUpdate);
    }
}
