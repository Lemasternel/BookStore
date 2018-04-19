using BookStore.Core.Business.Interfaces;
using BookStore.Core.DataProvider.Repository;
using BookStore.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Core.Business
{
    public class PublisherBLL : IPublisherBLL
    {
        private IRepository repo;

        public PublisherBLL(IRepository repository)
        {
            repo = repository;
        }

        public void Add(Publisher t)
        {
            repo.AddOrEdit(t);
            repo.SaveChanges();
        }

        public void Edit(Publisher t)
        {
            repo.AddOrEdit(t);
            repo.SaveChanges();
        }

        public Publisher Get(long id)
        {
            return repo.Get<Publisher>(id);
        }

        public IEnumerable<Publisher> List()
        {
            return repo.GetAll<Publisher>();
        }

        public IEnumerable<Publisher> List(long[] Ids)
        {
            return repo.GetAll<Publisher>(x => Ids.Contains(x.Id));
        }

        public void Remove(long id)
        {
            repo.Delete<Publisher>(id);
            repo.SaveChanges();
        }
    }
}
