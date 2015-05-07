namespace Veidibokin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intitial : DbMigration
    {
        public override void Up()
        {
         
            CreateTable(
                "dbo.ProfilePhotoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        profilePhoto = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);         

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZoneFollowers", "zoneID", "dbo.Zones");
            DropForeignKey("dbo.ZoneFollowers", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Worms", "statusID", "dbo.UserStatus");
            DropForeignKey("dbo.Worms", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserFollowers", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.StatusComments", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.StatusComments", "statusID", "dbo.UserStatus");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.GroupStatus", "statusID", "dbo.UserStatus");
            DropForeignKey("dbo.UserStatus", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserStatus", "photoId", "dbo.Photos");
            DropForeignKey("dbo.GroupStatus", "groupID", "dbo.Groups");
            DropForeignKey("dbo.GroupMembers", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupMembers", "groupID", "dbo.Groups");
            DropForeignKey("dbo.GroupCatches", "groupID", "dbo.Groups");
            DropForeignKey("dbo.GroupCatches", "catchID", "dbo.Catches");
            DropForeignKey("dbo.Catches", "zoneId", "dbo.Zones");
            DropForeignKey("dbo.Catches", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Catches", "photoId", "dbo.Photos");
            DropForeignKey("dbo.Catches", "fishTypeId", "dbo.FishTypes");
            DropForeignKey("dbo.Catches", "baitTypeID", "dbo.BaitTypes");
            DropIndex("dbo.ZoneFollowers", new[] { "userID" });
            DropIndex("dbo.ZoneFollowers", new[] { "zoneID" });
            DropIndex("dbo.Worms", new[] { "statusID" });
            DropIndex("dbo.Worms", new[] { "userID" });
            DropIndex("dbo.UserFollowers", new[] { "userID" });
            DropIndex("dbo.StatusComments", new[] { "statusID" });
            DropIndex("dbo.StatusComments", new[] { "userID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserStatus", new[] { "photoId" });
            DropIndex("dbo.UserStatus", new[] { "userId" });
            DropIndex("dbo.GroupStatus", new[] { "statusID" });
            DropIndex("dbo.GroupStatus", new[] { "groupID" });
            DropIndex("dbo.GroupMembers", new[] { "userID" });
            DropIndex("dbo.GroupMembers", new[] { "groupID" });
            DropIndex("dbo.GroupCatches", new[] { "catchID" });
            DropIndex("dbo.GroupCatches", new[] { "groupID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Catches", new[] { "baitTypeID" });
            DropIndex("dbo.Catches", new[] { "fishTypeId" });
            DropIndex("dbo.Catches", new[] { "photoId" });
            DropIndex("dbo.Catches", new[] { "zoneId" });
            DropIndex("dbo.Catches", new[] { "userId" });
            DropTable("dbo.ZoneFollowers");
            DropTable("dbo.Worms");
            DropTable("dbo.UserFollowers");
            DropTable("dbo.StatusComments");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProfilePhotoes");
            DropTable("dbo.UserStatus");
            DropTable("dbo.GroupStatus");
            DropTable("dbo.GroupMembers");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupCatches");
            DropTable("dbo.Zones");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Photos");
            DropTable("dbo.FishTypes");
            DropTable("dbo.Catches");
            DropTable("dbo.BaitTypes");
        }
    }
}
