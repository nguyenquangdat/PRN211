namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Order_id", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Order_id" });
            AddColumn("dbo.Orders", "pid", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Order_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Order_id", c => c.Int());
            DropColumn("dbo.Orders", "quantity");
            DropColumn("dbo.Orders", "price");
            DropColumn("dbo.Orders", "pid");
            CreateIndex("dbo.Products", "Order_id");
            AddForeignKey("dbo.Products", "Order_id", "dbo.Orders", "id");
        }
    }
}
