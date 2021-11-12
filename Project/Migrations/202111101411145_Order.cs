namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        uid = c.Int(nullable: false),
                        total = c.Double(nullable: false),
                        time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Products", "Order_id", c => c.Int());
            CreateIndex("dbo.Products", "Order_id");
            AddForeignKey("dbo.Products", "Order_id", "dbo.Orders", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Order_id", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Order_id" });
            DropColumn("dbo.Products", "Order_id");
            DropTable("dbo.Orders");
        }
    }
}
