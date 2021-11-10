namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 128, fixedLength: true),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 1000),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Stock = c.Int(nullable: false),
                        IsNewProduct = c.Boolean(),
                        Date = c.DateTime(nullable: false),
                        Image = c.String(),
                        Category = c.Int(nullable: false),
                        Tag = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Category", t => t.Category, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag, cascadeDelete: true)
                .Index(t => t.Category)
                .Index(t => t.Tag);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 128, fixedLength: true),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        UserMail = c.String(nullable: false, maxLength: 128),
                        UserPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserMail);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        UserEmail = c.String(nullable: false, maxLength: 50),
                        UserPhone = c.String(nullable: false, maxLength: 10),
                        UserPassword = c.String(nullable: false, maxLength: 10, unicode: false),
                        RoleID = c.Int(),
                        UserAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Role", t => t.RoleID)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Products", "Tag", "dbo.Tags");
            DropForeignKey("dbo.Products", "Category", "dbo.Category");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Products", new[] { "Tag" });
            DropIndex("dbo.Products", new[] { "Category" });
            DropTable("dbo.Users");
            DropTable("dbo.Role");
            DropTable("dbo.Logins");
            DropTable("dbo.Tags");
            DropTable("dbo.Products");
            DropTable("dbo.Category");
        }
    }
}
