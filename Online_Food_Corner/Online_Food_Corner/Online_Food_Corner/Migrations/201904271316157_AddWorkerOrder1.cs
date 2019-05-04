namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkerOrder1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WorkerOrders", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkerOrders", "Status", c => c.String());
        }
    }
}
