using System.Collections.Generic;
using System.Linq;
using BookStore.Core.Business;
using BookStore.Core.Business.Exceptions;
using BookStore.Core.Business.Interfaces;
using BookStore.Core.DataProvider.Repository;
using BookStore.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookStore.Test.Business
{
    [TestClass]
    public class BookBLLTest
    {
        IPublisherBLL publisherBLL;
        IAuthorBLL authorBLL;
        IBookBLL bookBLL;

        Author author;
        Publisher publisher;

        public BookBLLTest()
        {
            var repo = new Repository();

            publisherBLL = new PublisherBLL(repo);
            authorBLL = new AuthorBLL(repo);
            bookBLL = new BookBLL(repo);

            author = authorBLL.List().FirstOrDefault();
            publisher = publisherBLL.List().FirstOrDefault();
        }

        public Book CreateBook()
        {
            var book = new Book();
            book.Authors.Add(author);
            book.Publisher = publisher;

            book.Description = "Test";
            book.Price = 10;
            book.Quantity = 2;
            book.Title = "Book Test";

            return book;
        }

        [TestMethod]
        public void ValidateTitle()
        {
            var book = CreateBook();
            book.Title = "  ";

            try
            {
                bookBLL.Add(book);
                Assert.Fail("Title check not working for books!");
            }
            catch (BusinessLogicException ex) { }

            book.Title = "Book Title Test";
            book.Quantity = -5;

            try
            {
                bookBLL.Add(book);
                Assert.Fail("Quantity check not working for books!");
            }
            catch (BusinessLogicException ex) { }

            book.Quantity = 5;
            book.Price = -5;

            try
            {
                bookBLL.Add(book);
                Assert.Fail("Price check not working for books!");
            }
            catch (BusinessLogicException ex) { }

            book.Price = 10;
            book.Publisher = null;

            try
            {
                bookBLL.Add(book);
                Assert.Fail("Publisher check not working for books!");
            }
            catch (BusinessLogicException ex) { }

            book.Publisher = publisher;
            book.Authors = new List<Author>();

            try
            {
                bookBLL.Add(book);
                Assert.Fail("Author check not working for books!");
            }
            catch (BusinessLogicException ex) { }
        }
    }
}
