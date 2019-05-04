namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workers", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Workers", "ApplicationUserId");
            AddForeignKey("dbo.Workers", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Workers", new[] { "ApplicationUserId" });
            DropColumn("dbo.Workers", "ApplicationUserId");
        }
    }
}
