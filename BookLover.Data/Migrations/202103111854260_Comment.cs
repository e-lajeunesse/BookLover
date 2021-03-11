namespace BookLover.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        CommentText = c.String(nullable: false, maxLength: 500),
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.BookReview", t => t.ReviewId, cascadeDelete: true)
                .Index(t => t.ReviewId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "ReviewId", "dbo.BookReview");
            DropIndex("dbo.Comment", new[] { "ReviewId" });
            DropTable("dbo.Comment");
        }
    }
}
