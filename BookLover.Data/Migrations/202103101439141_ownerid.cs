namespace BookLover.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ownerid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookshelf", "OwnerId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookshelf", "OwnerId");
        }
    }
}
