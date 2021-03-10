namespace BookLover.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookshelf", "OwnerId", c => c.Guid(nullable: false));
            DropColumn("dbo.Author", "UserId");
            DropColumn("dbo.Book", "AverageRating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "AverageRating", c => c.Double(nullable: false));
            AddColumn("dbo.Author", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.Bookshelf", "OwnerId");
        }
    }
}
