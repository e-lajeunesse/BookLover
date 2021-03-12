namespace BookLover.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookshelf", "UserProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookshelf", "UserProfileId");
            AddForeignKey("dbo.Bookshelf", "UserProfileId", "dbo.UserProfile", "UserProfileId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookshelf", "UserProfileId", "dbo.UserProfile");
            DropIndex("dbo.Bookshelf", new[] { "UserProfileId" });
            DropColumn("dbo.Bookshelf", "UserProfileId");
        }
    }
}
