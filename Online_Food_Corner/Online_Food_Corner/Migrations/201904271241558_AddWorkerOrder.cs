namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkerOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkerOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkerId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: true)
                .Index(t => t.WorkerId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkerOrders", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.WorkerOrders", "OrderId", "dbo.Orders");
            DropIndex("dbo.WorkerOrders", new[] { "OrderId" });
            DropIndex("dbo.WorkerOrders", new[] { "WorkerId" });
            DropTable("dbo.WorkerOrders");
        }
    }
}
