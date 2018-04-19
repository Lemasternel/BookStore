using BookStore.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BookStore.Core.DataProvider.Maps
{
    internal class AuthorMap : EntityTypeConfiguration<Author>
    {
        public AuthorMap()
        {
            HasKey(x => x.Id);
            Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            Property(x => x.LastName).IsRequired().HasMaxLength(50);
        }
    }
}