using System.Collections.Generic;
namespace BookStore.Core.Domain
{
    public class Book : BaseData
    {
        public Book()
        {
            Authors = new List<Author>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public long PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}