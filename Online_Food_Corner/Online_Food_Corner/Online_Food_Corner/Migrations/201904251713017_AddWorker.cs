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
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Workers");
        }
    }
}
