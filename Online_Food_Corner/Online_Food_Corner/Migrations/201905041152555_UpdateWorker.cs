namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWorker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workers", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Workers", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workers", "Password");
            DropColumn("dbo.Workers", "Email");
        }
    }
}
