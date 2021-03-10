namespace BookLover.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookshelf",
                c => new
                    {
                        BookshelfId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BookshelfId);
            
            CreateTable(
                "dbo.BookshelfBook",
                c => new
                    {
                        Bookshelf_BookshelfId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bookshelf_BookshelfId, t.Book_BookId })
                .ForeignKey("dbo.Bookshelf", t => t.Bookshelf_BookshelfId, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.Book_BookId, cascadeDelete: true)
                .Index(t => t.Bookshelf_BookshelfId)
                .Index(t => t.Book_BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookshelfBook", "Book_BookId", "dbo.Book");
            DropForeignKey("dbo.BookshelfBook", "Bookshelf_BookshelfId", "dbo.Bookshelf");
            DropIndex("dbo.BookshelfBook", new[] { "Book_BookId" });
            DropIndex("dbo.BookshelfBook", new[] { "Bookshelf_BookshelfId" });
            DropTable("dbo.BookshelfBook");
            DropTable("dbo.Bookshelf");
        }
    }
}
