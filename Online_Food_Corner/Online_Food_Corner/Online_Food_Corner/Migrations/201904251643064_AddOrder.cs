namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        total_price = c.Int(nullable: false),
                        timing = c.DateTime(nullable: false),
                        delivery_mode = c.String(nullable: false),
                        order_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Customers", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "OrderId");
            AddForeignKey("dbo.Customers", "OrderId", "dbo.Orders", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "OrderId", "dbo.Orders");
            DropIndex("dbo.Customers", new[] { "OrderId" });
            DropColumn("dbo.Customers", "OrderId");
            DropTable("dbo.Orders");
        }
    }
}
