using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Data.Database.Repositories.Interfaces.Base;
using EDIS.Domain.Base;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using Xamarin.Forms;

namespace EDIS.Data.Database.Repositories.BaseRepository
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected SQLiteAsyncConnection Connection;

        public Repository()
        {
            CreateTable();
        }

        public void CreateTable()
        {
            Connection = new SQLiteAsyncConnection(DependencyService.Get<IDbPath>().GetLocalDbPath());
            Connection.CreateTableAsync<T>().Wait();
        }

        //public async Task<T> GetById(int id)
        //{
        //    return await Connection.Table<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
        //}

        public async Task<T> FindByQuery(Expression<Func<T, bool>> expression)
        {
            return await Connection.Table<T>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task Add(T entity)
        {
            await Connection.InsertAsync(entity);
        }

        public async Task AddOrReplace(T entity)
        {
            await Connection.InsertOrReplaceAsync(entity);
        }

        public async Task AddMany(IEnumerable<T> entities)
        {
            await Connection.InsertAllAsync(entities);
        }
        
        public async Task AddManyOrReplace(IEnumerable<T> entities)
        {
            await Connection.InsertOrReplaceAllWithChildrenAsync(entities);
        }

        public async Task Update(T entity)
        {
            await Connection.UpdateAsync(entity);
        }

        //public async Task Delete(int id)
        //{
        //    var item = GetById(id);
        //    await Connection.DeleteAsync(item);
        //}

        public async Task Delete(T item)
        {
            await Connection.DeleteAsync(item);
        }

        public async Task DeleteAll()
        {
            try
            {
                var items = await Connection.Table<T>().ToListAsync();
                if (items.Any())
                    await Connection.DeleteAllAsync(items);
            }
            catch (Exception e)
            {
                
            }
        }

        public async Task DeleteByQuery(Expression<Func<T, bool>> expression)
        {
            try
            {
                var items = await Connection.Table<T>().Where(expression).ToListAsync();
                if (items.Any())
                    await Connection.DeleteAllAsync(items);
            }
            catch (Exception e)
            {

            }
        }

        public async Task<List<T>> GetAll()
        {
            return await Connection.Table<T>().ToListAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            return await Connection.GetAllWithChildrenAsync(expression);//Table<T>().Where(expression).ToListAsync();
        }

        public async Task RunInTransaction(Action<SQLiteConnection> action)
        {
            await Connection.RunInTransactionAsync(action);
        }
    }
}