namespace EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hashtag",
                c => new
                    {
                        HashTagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        UserId = c.String(maxLength: 128),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HashTagId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        EmailId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        CountryOfOrigin = c.String(nullable: false),
                        Image = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmailId);
            
            CreateTable(
                "dbo.TweetLikeDislike",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        TweetId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        IsLiked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LikeId)
                .ForeignKey("dbo.Tweet", t => t.TweetId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.TweetId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tweet",
                c => new
                    {
                        TweetId = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        UserId = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TweetId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FollowingUser",
                c => new
                    {
                        UserLinkId = c.Int(nullable: false, identity: true),
                        FolloweeId = c.String(maxLength: 128),
                        FollowerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserLinkId)
                .ForeignKey("dbo.User", t => t.FolloweeId)
                .ForeignKey("dbo.User", t => t.FollowerId)
                .Index(t => t.FolloweeId)
                .Index(t => t.FollowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowingUser", "FollowerId", "dbo.User");
            DropForeignKey("dbo.FollowingUser", "FolloweeId", "dbo.User");
            DropForeignKey("dbo.TweetLikeDislike", "UserId", "dbo.User");
            DropForeignKey("dbo.TweetLikeDislike", "TweetId", "dbo.Tweet");
            DropForeignKey("dbo.Tweet", "UserId", "dbo.User");
            DropForeignKey("dbo.Hashtag", "UserId", "dbo.User");
            DropIndex("dbo.FollowingUser", new[] { "FollowerId" });
            DropIndex("dbo.FollowingUser", new[] { "FolloweeId" });
            DropIndex("dbo.Tweet", new[] { "UserId" });
            DropIndex("dbo.TweetLikeDislike", new[] { "UserId" });
            DropIndex("dbo.TweetLikeDislike", new[] { "TweetId" });
            DropIndex("dbo.Hashtag", new[] { "UserId" });
            DropTable("dbo.FollowingUser");
            DropTable("dbo.Tweet");
            DropTable("dbo.TweetLikeDislike");
            DropTable("dbo.User");
            DropTable("dbo.Hashtag");
        }
    }
}
