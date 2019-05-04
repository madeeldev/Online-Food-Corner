namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWorker1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workers", "Address", c => c.String(nullable: false));
            AddColumn("dbo.Workers", "CellNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workers", "CellNumber");
            DropColumn("dbo.Workers", "Address");
        }
    }
}
