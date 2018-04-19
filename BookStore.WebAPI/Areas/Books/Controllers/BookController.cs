using BookStore.Core.Business;
using BookStore.Core.Business.Exceptions;
using BookStore.Core.Business.Interfaces;
using BookStore.WebAPI.Areas.Books.Model;
using System;
using System.Net;
using System.Web.Http;
using BookStore.Core.Domain;
using System.Linq;
using BookStore.WebAPI.Areas.Authors.Models;
using BookStore.WebAPI.Areas.Books.Models;
using BookStore.Core.DataProvider.Repository;
using BookStore.WebAPI.Areas.Publishers.Models;

namespace BookStore.WebAPI.Areas.Books.Controllers
{
    public class BookController : ApiController
    {
        IBookBLL _bookBLL;
        IAuthorBLL _authorBLL;
        IPublisherBLL _publisherBLL;

        public BookController()
        {
            var repo = new Repository();

            _bookBLL = new BookBLL(repo);
            _authorBLL = new AuthorBLL(repo);
            _publisherBLL = new PublisherBLL(repo);
        }

        [HttpPost]
        [Route("book/add")]
        public IHttpActionResult Add(BookVm bookVm)
        {
            try
            {
                var book = MapToBook(bookVm);
                _bookBLL.Add(book);

                bookVm.Id = book.Id;

                var bookDto = MapToBookDto(book);
                return Ok(bookDto);
            }
            catch (BusinessLogicException ex)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new ErrorResult(Resources.Messages.Status500));
            }
        }

        [HttpPost]
        [Route("book/edit")]
        public IHttpActionResult Edit(BookVm bookVm)
        {
            try
            {
                var book = _bookBLL.Get(bookVm.Id);
                book = MapToBook(bookVm, book);
                _bookBLL.Edit(book);

                var bookDto = MapToBookDto(book);
                return Ok(bookDto);
            }
            catch (BusinessLogicException ex)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new ErrorResult(Resources.Messages.Status500));
            }
        }

        [HttpPost]
        [Route("book/remove")]
        public IHttpActionResult Remove(BookVm bookVm)
        {
            try
            {
                _bookBLL.Remove(bookVm.Id);
                return Ok();
            }
            catch (BusinessLogicException ex)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new ErrorResult(Resources.Messages.Status500));
            }
        }

        [HttpGet]
        [Route("book/get")]
        public IHttpActionResult Get(long id)
        {
            var book = _bookBLL.Get(id);
            if (book == null)
            {
                return Content(HttpStatusCode.BadRequest, new ErrorResult(Resources.Messages.BookNotFound));
            }

            var bookDto = MapToBookDto(book);
            return Ok(bookDto);
        }

        [HttpGet]
        [Route("book/all")]
        public IHttpActionResult All()
        {
            var booksDto = _bookBLL.List().OrderBy(x => x.Title).Select(x => MapToBookDto(x)).ToList();

            return Ok(booksDto);
        }

        private static BookDto MapToBookDto(Book x)
        {
            return new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                Quantity = x.Quantity,
                Authors = x.Authors.Select(y => new AuthorDto
                {
                    Id = y.Id,
                    FirstName = y.FirstName,
                    LastName = y.LastName
                }),
                Publisher = new PublisherDto
                {
                    Id = x.PublisherId,
                    Name = x.Publisher.Name
                }

            };
        }

        private Book MapToBook(BookVm bookVm, Book bookEdit = null)
        {
            var book = bookEdit ?? new Book();
            var publisher = _publisherBLL.Get(bookVm.PublisherId);

            book.Authors.Clear();
            if (bookVm.AuthorsIds != null)
            {
                foreach (var autorId in bookVm.AuthorsIds)
                {
                    var autor = _authorBLL.Get(autorId);
                    book.Authors.Add(autor);
                }
            }

            book.Id = bookVm.Id;
            book.Description = bookVm.Description;
            book.Price = bookVm.Price;
            book.PublisherId = publisher?.Id ?? 0;
            book.Quantity = bookVm.Quantity;
            book.Title = bookVm.Title;

            return book;
        }
    }
}
