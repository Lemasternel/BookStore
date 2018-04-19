using BookStore.WebAPI.Areas.Books.Models;
using System.Collections.Generic;

namespace BookStore.WebAPI.Areas.Authors.Models
{
    public class AuthorDto
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<BookDto> Books { get; set; }
    }
}