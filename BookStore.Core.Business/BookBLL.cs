using BookStore.Core.Business.Exceptions;
using BookStore.Core.Business.Interfaces;
using BookStore.Core.DataProvider.Repository;
using BookStore.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Core.Business
{
    public class BookBLL : IBookBLL
    {
        private IRepository repo { get; set; }
        public BookBLL(IRepository repository)
        {
            repo = repository;
        }

        public void Add(Book t)
        {
            ValidateModel(t);

            repo.AddOrEdit(t);
            repo.SaveChanges();
        }


        public void Edit(Book t)
        {
            ValidateModel(t);

            repo.AddOrEdit(t);
            repo.SaveChanges();
        }

        public void Remove(long id)
        {
            repo.Delete<Book>(id);
            repo.SaveChanges();
        }

        public Book Get(long id)
        {
            return repo.Get<Book>(id);
        }

        public IEnumerable<Book> List()
        {
            return repo.GetAll<Book>();
        }

        public IEnumerable<Book> List(long[] Ids)
        {
            return repo.GetAll<Book>(x => Ids.Contains(x.Id));
        }

        private void ValidateModel(Book t)
        {
            if (t.Id != 0 && Get(t.Id) == null)
            {
                throw new BusinessLogicException(Resources.Messages.BookNotFound);
            }

            if (string.IsNullOrWhiteSpace(t.Title))
            {
                throw new BusinessLogicException(Resources.Messages.TitleMustBeInformed);
            }

            if (t.Title.Length > 200)
            {
                throw new BusinessLogicException(Resources.Messages.TitleToLong);
            }

            if (!string.IsNullOrWhiteSpace(t.Description) && t.Description.Length > 1000)
            {
                throw new BusinessLogicException(Resources.Messages.DescriptionToLong);
            }

            if (t.Price < 0)
            {
                throw new BusinessLogicException(Resources.Messages.PriceCantBeNegative);
            }

            if (t.Quantity < 0)
            {
                throw new BusinessLogicException(Resources.Messages.QuantityCantBeNegative);
            }

            var books = repo.GetAll<Book>();
            if (books.Any(x => x.Title == t.Title && x.Id != t.Id))
            {
                throw new BusinessLogicException(Resources.Messages.BookTitleAlreadyExists);
            }

            if ((t.PublisherId == 0 && t.Publisher == null))
            {
                throw new BusinessLogicException(Resources.Messages.PublisherMustBeInformed);
            }

            if (t.Authors == null || !t.Authors.Any())
            {
                throw new BusinessLogicException(Resources.Messages.AuthorMustBeInformed);
            }
        }

    }
}
