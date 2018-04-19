using BookStore.Core.DataProvider.Maps;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
namespace BookStore.Core.DataProvider
{
    [DbConfigurationType(typeof(BookStoreConfiguration))]
    public class BookStoreContext : DbContext
    {
        public BookStoreContext() : base("BookStoreDB")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new PublisherMap());
            modelBuilder.Configurations.Add(new AuthorMap());
        }
    }

    internal class BookStoreConfiguration : DbConfiguration
    {
        public BookStoreConfiguration()
        {
            SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
        }
    }
}