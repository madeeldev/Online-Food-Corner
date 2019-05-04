namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCheckedInProduct1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsChecked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsChecked");
        }
    }
}
