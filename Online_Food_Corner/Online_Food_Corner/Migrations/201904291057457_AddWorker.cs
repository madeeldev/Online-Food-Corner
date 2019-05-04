namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorker : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        worker_name = c.String(nullable: false),
                        salary = c.Int(nullable: false),
                        worker_status = c.String(nullable: false),
                        OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "OrderId", "dbo.Orders");
            DropIndex("dbo.Workers", new[] { "OrderId" });
            DropTable("dbo.Workers");
        }
    }
}
