using BookStore.WebAPI.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookStore.WebAPI.Areas.Books.Models
{
    public class BookVm
    {
        public BookVm()
        {
            Publishers = new List<SelectListItem>();
            Authors = new List<SelectListItem>();
        }

        public long Id { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Labels))]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Labels))]
        public string Description { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Labels))]
        public decimal Price { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(Labels))]
        public int Quantity { get; set; }

        [Display(Name = "Publisher", ResourceType = typeof(Labels))]
        public long PublisherId { get; set; }

        public ICollection<SelectListItem> Publishers { get; set; }

        [Display(Name = "Authors", ResourceType = typeof(Labels))]
        public long[] AuthorsIds { get; set; }

        public ICollection<SelectListItem> Authors { get; set; }
    }
}