using BookStore.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BookStore.Core.DataProvider.Maps
{
    internal class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Description).IsOptional().HasMaxLength(1000);
            Property(x => x.Price).IsRequired();
            Property(x => x.Quantity).IsRequired();
            Property(x => x.Title).IsRequired().HasMaxLength(200);

            HasRequired(x => x.Publisher)
                .WithMany(x => x.Books)
                .WillCascadeOnDelete(false);

            HasMany(x => x.Authors)
                .WithMany(x => x.Books)
                .Map(x =>
                {
                    x.ToTable("AuthorBooks");
                    x.MapLeftKey("BookId");
                    x.MapRightKey("AuthorId");
                });
        }
    }
}