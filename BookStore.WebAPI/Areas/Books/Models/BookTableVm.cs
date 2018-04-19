namespace BookStore.WebAPI.Areas.Books.Models
{
    public class BookTableVm
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}