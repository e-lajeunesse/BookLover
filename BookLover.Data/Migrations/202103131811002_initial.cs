namespace BookLover.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Genre = c.String(nullable: false),
                        Description = c.String(maxLength: 500),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Author", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.BookReview",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ReviewTitle = c.String(),
                        ReviewText = c.String(nullable: false),
                        CreatedReview = c.DateTimeOffset(nullable: false, precision: 7),
                        BookRating = c.Double(nullable: false),
                        BookId = c.Int(nullable: false),
                        UserProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        CommentText = c.String(nullable: false, maxLength: 500),
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.BookReview", t => t.ReviewId, cascadeDelete: true)
                .Index(t => t.ReviewId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
            CreateTable(
                "dbo.Bookshelf",
                c => new
                    {
                        BookshelfId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        UserProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookshelfId)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
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
            
            CreateTable(
                "dbo.UserProfileBook",
                c => new
                    {
                        UserProfile_UserProfileId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserProfile_UserProfileId, t.Book_BookId })
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_UserProfileId, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.Book_BookId, cascadeDelete: true)
                .Index(t => t.UserProfile_UserProfileId)
                .Index(t => t.Book_BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.BookReview", "UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.UserProfileBook", "Book_BookId", "dbo.Book");
            DropForeignKey("dbo.UserProfileBook", "UserProfile_UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.Bookshelf", "UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.BookshelfBook", "Book_BookId", "dbo.Book");
            DropForeignKey("dbo.BookshelfBook", "Bookshelf_BookshelfId", "dbo.Bookshelf");
            DropForeignKey("dbo.Comment", "ReviewId", "dbo.BookReview");
            DropForeignKey("dbo.BookReview", "BookId", "dbo.Book");
            DropForeignKey("dbo.Book", "AuthorId", "dbo.Author");
            DropIndex("dbo.UserProfileBook", new[] { "Book_BookId" });
            DropIndex("dbo.UserProfileBook", new[] { "UserProfile_UserProfileId" });
            DropIndex("dbo.BookshelfBook", new[] { "Book_BookId" });
            DropIndex("dbo.BookshelfBook", new[] { "Bookshelf_BookshelfId" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Bookshelf", new[] { "UserProfileId" });
            DropIndex("dbo.Comment", new[] { "ReviewId" });
            DropIndex("dbo.BookReview", new[] { "UserProfileId" });
            DropIndex("dbo.BookReview", new[] { "BookId" });
            DropIndex("dbo.Book", new[] { "AuthorId" });
            DropTable("dbo.UserProfileBook");
            DropTable("dbo.BookshelfBook");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Bookshelf");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Comment");
            DropTable("dbo.BookReview");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}
