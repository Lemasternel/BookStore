using BookStore.Core.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookStore.Core.DataProvider.Repository
{
    public interface IRepository
    {
        void AddOrEdit<T>(T t) where T : BaseData;

        void Delete<T>(long t) where T : BaseData;

        T Get<T>(long id) where T : BaseData;

        IQueryable<T> GetAll<T>() where T : BaseData;

        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression) where T : BaseData;

        void SaveChanges();
    }
}
