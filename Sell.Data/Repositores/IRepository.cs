
using Sell.Core;
using Sell.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell.Data.Repositores
{
    public partial interface IRepository<TEntity> where TEntity : Entity
    {

        TEntity GetById(params object[] ids);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }

        TEntity GetByIdAsNoTracking(params object[] ids);
        System.Threading.Tasks.Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task InsertAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(params object[] ids);
        Task<TEntity> GetByIdAsNoTrackingAsync(params object[] ids);
        List<T> RunFunctionDb<T>(string functionName, List<DbParamter> paramters) where T : new();
        //void DeleteAsync(Task<Category> category);
        //List<T> RunFunctionDb<T>(string functionName, List<DbParamter> paramters) where T : new();

    }
}