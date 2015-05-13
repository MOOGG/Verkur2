namespace Veidibokin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goldmember : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.UserFollowers", "userID");
            AddForeignKey("dbo.UserFollowers", "userID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFollowers", "userID", "dbo.AspNetUsers");
            DropIndex("dbo.UserFollowers", new[] { "userID" });
        }
    }
}
