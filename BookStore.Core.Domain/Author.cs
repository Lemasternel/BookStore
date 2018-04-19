using System.Collections.Generic;
namespace BookStore.Core.Domain
{
    public class Author : BaseData
    {
        public Author()
        {
            Books = new List<Book>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}