namespace Veidibokin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photos4 : DbMigration
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
