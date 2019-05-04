namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCellNumberToApplicationUserClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "cellNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "cellNumber");
        }
    }
}
