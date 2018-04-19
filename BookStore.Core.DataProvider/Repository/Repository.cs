using BookStore.Core.Domain;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BookStore.Core.DataProvider.Repository
{
    public class Repository : IRepository
    {
        private static BookStoreContext context;
        private static IRepository repo;

        //public static IRepository GetRepository()
        //{
        //    if (repo == null)
        //    {
        //        repo = new Repository();
        //    }

        //    return repo;
        //}

        public Repository()
        {
            context = new BookStoreContext();
            context.Database.Connection.ConnectionString = ConfigurationManager.AppSettings["BookStoreDB"];
        }

        public void AddOrEdit<T>(T t) where T : BaseData
        {
            var entry = context.Entry<T>(t);
            entry.State = t.Id == 0 ? entry.State = EntityState.Added : EntityState.Modified;
        }

        public void Delete<T>(long id) where T : BaseData
        {
            var entity = context.Set<T>().FirstOrDefault(x => x.Id == id);
            var entry = context.Entry<T>(entity);
            entry.State = EntityState.Deleted;
        }

        public T Get<T>(long id) where T : BaseData
        {
            var entity = context.Set<T>().Find(id);
            return entity;
        }

        public IQueryable<T> GetAll<T>() where T : BaseData
        {
            var entities = context.Set<T>();
            return context.Set<T>();
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression) where T : BaseData
        {
            return context.Set<T>().Where(expression);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
