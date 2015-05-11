namespace Veidibokin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotosToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zones", "photo", c => c.String());
            AlterColumn("dbo.Groups", "photo", c => c.String());
            AlterColumn("dbo.AspNetUsers", "photo", c => c.String());
            AlterColumn("dbo.UserStatus", "photo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserStatus", "photo", c => c.Binary());
            AlterColumn("dbo.AspNetUsers", "photo", c => c.Binary());
            AlterColumn("dbo.Groups", "photo", c => c.Binary());
            AlterColumn("dbo.Zones", "photo", c => c.Binary());
        }
    }
}
