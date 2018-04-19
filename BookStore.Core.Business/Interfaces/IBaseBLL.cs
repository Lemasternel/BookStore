using System.Collections.Generic;

namespace BookStore.Core.Business.Interfaces
{
    public interface IBaseBLL<T>
    {
        void Add(T t);

        void Edit(T t);

        void Remove(long id);

        T Get(long id);

        IEnumerable<T> List();

        IEnumerable<T> List(long[] Ids);
    }
}
