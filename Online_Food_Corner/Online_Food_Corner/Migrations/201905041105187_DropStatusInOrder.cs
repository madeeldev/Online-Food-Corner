namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropStatusInOrder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Status", c => c.DateTime(nullable: false));
        }
    }
}
