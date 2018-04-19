using BookStore.WebAPI.Areas.Books.Models;
using System.Collections.Generic;

namespace BookStore.WebAPI.Areas.Publishers.Models
{
    public class PublisherDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<BookDto> Books { get; set; }
    }
}