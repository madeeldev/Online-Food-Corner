namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerInDb1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Customers", "ProductId", "dbo.Products");
            DropIndex("dbo.Customers", new[] { "OrderId" });
            DropIndex("dbo.Customers", new[] { "ProductId" });
            DropColumn("dbo.Customers", "OrderId");
            DropColumn("dbo.Customers", "ProductId");
            DropTable("dbo.Orders");
            DropTable("dbo.Products");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_name = c.String(nullable: false),
                        product_type = c.String(nullable: false),
                        product_Price = c.Int(nullable: false),
                        product_picture = c.Binary(nullable: false),
                        product_quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
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
            
            AddColumn("dbo.Customers", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "ProductId");
            CreateIndex("dbo.Customers", "OrderId");
            AddForeignKey("dbo.Customers", "ProductId", "dbo.Products", "id", cascadeDelete: true);
            AddForeignKey("dbo.Customers", "OrderId", "dbo.Orders", "id", cascadeDelete: true);
        }
    }
}
