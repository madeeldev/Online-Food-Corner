namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderToWorker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workers", "OrderId", c => c.Int());
            CreateIndex("dbo.Workers", "OrderId");
            AddForeignKey("dbo.Workers", "OrderId", "dbo.Orders", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "OrderId", "dbo.Orders");
            DropIndex("dbo.Workers", new[] { "OrderId" });
            DropColumn("dbo.Workers", "OrderId");
        }
    }
}
