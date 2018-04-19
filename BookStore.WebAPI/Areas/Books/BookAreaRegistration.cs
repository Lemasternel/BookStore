using System.Web.Mvc;

namespace BookStore.WebAPI.Areas.Books
{
    public class BookAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Books";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Books_default",
                "Books/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}