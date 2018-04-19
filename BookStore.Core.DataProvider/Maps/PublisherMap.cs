using System.Data.Entity.ModelConfiguration;
using BookStore.Core.Domain;

namespace BookStore.Core.DataProvider.Maps
{
    internal class PublisherMap : EntityTypeConfiguration<Publisher>
    {
        public PublisherMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired().HasMaxLength(200);
        }
    }
}