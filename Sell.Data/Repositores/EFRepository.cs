using Microsoft.EntityFrameworkCore;
using Sell.Core;
using Sell.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell.Data.Repositores
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region Fields
        private readonly IApplcationDbContext _context = null;

        private DbSet<TEntity> entities;

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (entities == null)
                    entities = this._context.Set<TEntity>();

                return entities;
            }
        }

        public EFRepository(IApplcationDbContext context)
        {
            this._context = context;
        }

       
        #endregion

        public IQueryable<TEntity> Table => Entities;


        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this._context.Set<TEntity>().Remove(entity);
            this._context.SaveChanges();
        }

        public async virtual Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this._context.Set<TEntity>().Remove(entity);

            await this._context.SaveChangesAsync();
        }


        public virtual TEntity GetById(params object[] ids)
        {
            return this._context.Set<TEntity>().Find(ids);
        }
        public virtual TEntity GetByIdAsNoTracking(params object[] ids)
        {
            var entity = this._context.Set<TEntity>().Find(ids);

            if (entity != null)
            {
                this._context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public async virtual Task<TEntity> GetByIdAsync(params object[] ids)
        {
            return await this._context.Set<TEntity>().FindAsync(ids);
        }
        public async virtual Task<TEntity> GetByIdAsNoTrackingAsync(params object[] ids)
        {
            var entity = await this._context.Set<TEntity>().FindAsync(ids);

            if (entity != null)
            {
                this._context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }
        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this._context.Set<TEntity>().Add(entity);
            this._context.SaveChanges();
        }


        public async virtual Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await this._context.Set<TEntity>().AddAsync(entity);
            await this._context.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this._context.Set<TEntity>().Update(entity);
            this._context.SaveChanges();
        }

        public async virtual Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this._context.Set<TEntity>().Update(entity);
            await this._context.SaveChangesAsync();
        }


        public List<T> RunFunctionDb<T>(string functionName, List<DbParamter> paramters) where T : new()
        {
            return _context.RunSp<T>(functionName, paramters);
        }
    }
}
