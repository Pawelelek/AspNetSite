using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.Interfaces;
using Go1Bet.Core.Context;

namespace Go1Bet.Infrastructure.Repository
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        internal AppDbContext context;
        internal DbSet<TEntity> dbSet;
        public Repository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task Delete(object id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete != null)
            {
                await Delete(entityToDelete);
            }
        }

        public async Task Delete(TEntity entityToDelete)
        {
            await Task.Run(
                () =>
                {
                    if (context.Entry(entityToDelete).State == EntityState.Detached)
                    {
                        dbSet.Attach(entityToDelete);
                    }
                    dbSet.Remove(entityToDelete);
                });
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(dbSet, specification);
        }

        public async Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(TEntity ententityToUpdate)
        {
            await Task.Run
                (
                () =>
                {
                    dbSet.Attach(ententityToUpdate);
                    context.Entry(ententityToUpdate).State = EntityState.Modified;
                });
        }
    }
}
