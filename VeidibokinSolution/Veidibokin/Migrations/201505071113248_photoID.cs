namespace Veidibokin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photoID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserStatus", "photoId", "dbo.Photos");
            DropIndex("dbo.UserStatus", new[] { "photoId" });
            AlterColumn("dbo.UserStatus", "photoId", c => c.Int());
            CreateIndex("dbo.UserStatus", "photoId");
            AddForeignKey("dbo.UserStatus", "photoId", "dbo.Photos", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserStatus", "photoId", "dbo.Photos");
            DropIndex("dbo.UserStatus", new[] { "photoId" });
            AlterColumn("dbo.UserStatus", "photoId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserStatus", "photoId");
            AddForeignKey("dbo.UserStatus", "photoId", "dbo.Photos", "ID", cascadeDelete: true);
        }
    }
}
