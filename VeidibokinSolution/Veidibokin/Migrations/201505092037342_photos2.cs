namespace Veidibokin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photos2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Photos");
        }
        
        public override void Down()
        {
        }
    }
}
