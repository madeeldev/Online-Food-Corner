namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressToIU : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "address");
        }
    }
}
