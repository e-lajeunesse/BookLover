namespace BookLover.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comment", "CreatedUtc");
        }
    }
}
