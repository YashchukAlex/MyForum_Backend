namespace MyForum_Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.CategoryID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.CommentRating",
                c => new
                    {
                        CommentRatingID = c.Int(nullable: false, identity: true),
                        Rating = c.Boolean(nullable: false),
                        UserRefID = c.String(maxLength: 128),
                        CommentRefID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentRatingID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserRefID)
                .ForeignKey("dbo.Comment", t => t.CommentRefID, cascadeDelete: true)
                .Index(t => new { t.UserRefID, t.CommentRefID }, unique: true, name: "IX_UserIdAndComment");
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        NameImage = c.String(),
                        LastOnline = c.DateTime(),
                        CreateTimeAccount = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 500),
                        CreateTime = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        TopicRefID = c.Int(nullable: false),
                        UserRefID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserRefID)
                .ForeignKey("dbo.Topic", t => t.TopicRefID, cascadeDelete: true)
                .Index(t => t.TopicRefID)
                .Index(t => t.UserRefID);
            
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        TopicID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Text = c.String(nullable: false, maxLength: 1000),
                        CreateTime = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        LastActive = c.DateTime(nullable: false),
                        CategoryRefID = c.Int(nullable: false),
                        UserRefID = c.String(maxLength: 128),
                        StatusRefID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopicID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserRefID)
                .ForeignKey("dbo.Category", t => t.CategoryRefID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusRefID, cascadeDelete: true)
                .Index(t => t.CategoryRefID)
                .Index(t => t.UserRefID)
                .Index(t => t.StatusRefID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.StatusID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CommentRating", "CommentRefID", "dbo.Comment");
            DropForeignKey("dbo.Comment", "TopicRefID", "dbo.Topic");
            DropForeignKey("dbo.Topic", "StatusRefID", "dbo.Status");
            DropForeignKey("dbo.Topic", "CategoryRefID", "dbo.Category");
            DropForeignKey("dbo.Topic", "UserRefID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comment", "UserRefID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentRating", "UserRefID", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Status", new[] { "Name" });
            DropIndex("dbo.Topic", new[] { "StatusRefID" });
            DropIndex("dbo.Topic", new[] { "UserRefID" });
            DropIndex("dbo.Topic", new[] { "CategoryRefID" });
            DropIndex("dbo.Comment", new[] { "UserRefID" });
            DropIndex("dbo.Comment", new[] { "TopicRefID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CommentRating", "IX_UserIdAndComment");
            DropIndex("dbo.Category", new[] { "Name" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Status");
            DropTable("dbo.Topic");
            DropTable("dbo.Comment");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CommentRating");
            DropTable("dbo.Category");
        }
    }
}
