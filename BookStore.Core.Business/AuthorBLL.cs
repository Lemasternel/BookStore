using BookStore.Core.Business.Interfaces;
using BookStore.Core.DataProvider.Repository;
using BookStore.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Core.Business
{
    public class AuthorBLL : IAuthorBLL
    {
        private IRepository repo { get; set; }

        public AuthorBLL(IRepository repository)
        {
            repo = repository;
        }

        public void Add(Author t)
        {
            repo.AddOrEdit(t);
            repo.SaveChanges();
        }

        public void Edit(Author t)
        {
            repo.AddOrEdit(t);
            repo.SaveChanges();
        }

        public Author Get(long id)
        {
            return repo.Get<Author>(id);
        }

        public IEnumerable<Author> List()
        {
            return repo.GetAll<Author>();
        }

        public IEnumerable<Author> List(long[] ids)
        {
            return repo.GetAll<Author>(x => ids.Contains(x.Id));
        }

        public void Remove(long id)
        {
            repo.Delete<Author>(id);
            repo.SaveChanges();
        }
    }
}
