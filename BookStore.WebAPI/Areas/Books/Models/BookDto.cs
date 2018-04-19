using BookStore.WebAPI.Areas.Authors.Models;
using BookStore.WebAPI.Areas.Publishers.Models;
using System.Collections.Generic;

namespace BookStore.WebAPI.Areas.Books.Models
{
    public class BookDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public PublisherDto Publisher { get; set; }

        public IEnumerable<AuthorDto> Authors { get; set; }
    }
}