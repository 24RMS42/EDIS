using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;

namespace EDIS.Data.Database.Repositories.Interfaces.Base
{
    public interface IRepository<T>
    {
        //Task<T> GetById(int id);

        Task<T> FindByQuery(Expression<Func<T, bool>> expression);

        Task Add(T entity);
        Task AddOrReplace(T entity);
        Task AddMany(IEnumerable<T> entities);
        Task AddManyOrReplace(IEnumerable<T> entities);
        Task Update(T entity);
        //Task Delete(int id);
        Task DeleteAll();
        Task Delete(T item);
        Task DeleteByQuery(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> expression);

        Task RunInTransaction(Action<SQLiteConnection> action);
    }
}