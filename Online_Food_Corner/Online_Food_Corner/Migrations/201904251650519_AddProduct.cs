namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProduct : DbMigration
    {
        public override void Up()
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
            
            AddColumn("dbo.Customers", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "ProductId");
            AddForeignKey("dbo.Customers", "ProductId", "dbo.Products", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "ProductId", "dbo.Products");
            DropIndex("dbo.Customers", new[] { "ProductId" });
            DropColumn("dbo.Customers", "ProductId");
            DropTable("dbo.Products");
        }
    }
}
