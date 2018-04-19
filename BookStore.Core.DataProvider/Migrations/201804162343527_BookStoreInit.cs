namespace BookStore.Core.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookStoreInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 1000),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        PublisherId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthorBooks",
                c => new
                    {
                        BookId = c.Long(nullable: false),
                        AuthorId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.AuthorId })
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.AuthorId);

            Sql("INSERT INTO Publishers (Name) VALUES('LerMais'), ('Leitura Divertida')");
            Sql("INSERT INTO Authors (FirstName, LastName) VALUES('João', 'Silva'), ('Maria', 'Cecília')");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.AuthorBooks", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.AuthorBooks", "BookId", "dbo.Books");
            DropIndex("dbo.AuthorBooks", new[] { "AuthorId" });
            DropIndex("dbo.AuthorBooks", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "PublisherId" });
            DropTable("dbo.AuthorBooks");
            DropTable("dbo.Publishers");
            DropTable("dbo.Authors");
            DropTable("dbo.Books");
        }
    }
}
