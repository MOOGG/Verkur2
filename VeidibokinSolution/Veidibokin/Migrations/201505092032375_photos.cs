namespace Veidibokin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Catches", "Photo_ID", "dbo.Photos");
            DropForeignKey("dbo.UserStatus", "photoId", "dbo.Photos");
            DropIndex("dbo.Catches", new[] { "Photo_ID" });
            DropIndex("dbo.UserStatus", new[] { "photoId" });
            DropColumn("dbo.UserStatus", "photoId");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        photo = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.UserStatus", "photoId", c => c.Int());
            AddColumn("dbo.Catches", "Photo_ID", c => c.Int());
            CreateIndex("dbo.UserStatus", "photoId");
            CreateIndex("dbo.Catches", "Photo_ID");
            AddForeignKey("dbo.UserStatus", "photoId", "dbo.Photos", "ID");
            AddForeignKey("dbo.Catches", "Photo_ID", "dbo.Photos", "ID");
        }
    }
}
