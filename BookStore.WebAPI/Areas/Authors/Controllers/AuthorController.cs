using BookStore.Core.Business;
using BookStore.Core.Business.Interfaces;
using BookStore.Core.DataProvider.Repository;
using BookStore.WebAPI.Areas.Authors.Models;
using BookStore.WebAPI.Areas.Books.Model;
using BookStore.WebAPI.Areas.Books.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BookStore.WebAPI.Areas.Authors.Controllers
{
    public class AuthorController : ApiController
    {
        IAuthorBLL _authorBLL;

        public AuthorController()
        {
            _authorBLL = new AuthorBLL(new Repository());
        }

        [HttpGet]
        [Route("author/all")]
        public IHttpActionResult All()
        {
            try
            {
                var authorsDto = _authorBLL.List().Select(x => new AuthorDto() {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Books = x.Books.Select(y => new BookDto {
                        Id = y.Id,
                        Title = y.Title,
                        Description = y.Description
                    })
                }).ToList();

                return Ok(authorsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
