namespace Veidibokin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photos3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Catches", "Photo_ID", "dbo.Photos");
            DropIndex("dbo.Catches", new[] { "Photo_ID" });
            DropIndex("dbo.UserStatus", new[] { "photoId" });
        }
        
        public override void Down()
        {
        }
    }
}
