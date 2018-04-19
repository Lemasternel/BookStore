using BookStore.Core.Business;
using BookStore.Core.Business.Interfaces;
using BookStore.Core.DataProvider.Repository;
using BookStore.WebAPI.Areas.Books.Model;
using BookStore.WebAPI.Areas.Books.Models;
using BookStore.WebAPI.Areas.Publishers.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BookStore.WebAPI.Areas.Publishers.Controllers
{
    public class PublisherController : ApiController
    {
        IPublisherBLL _publisherBLL;

        public PublisherController()
        {
            _publisherBLL = new PublisherBLL(new Repository());
        }

        [HttpGet]
        [Route("publisher/all")]
        public IHttpActionResult All()
        {
            try
            {
                var publishersDto = _publisherBLL.List().Select(x =>  new PublisherDto {
                    Id = x.Id,
                    Name = x.Name,
                    Books = x.Books.Select(y => new BookDto {
                        Id = y.Id,
                        Title = y.Title,
                        Description = y.Description
                    })
                }).ToList();

                return Ok(publishersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
