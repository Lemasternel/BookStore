using System.Collections.Generic;
namespace BookStore.Core.Domain
{
    public class Publisher : BaseData
    {
        public Publisher()
        {
            Books = new List<Book>();
        }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}