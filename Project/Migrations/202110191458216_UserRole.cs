namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRole : DbMigration
    {
        public override void Up()
        {
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
                        UserPhone = c.String(nullable: false, maxLength: 11),
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
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropTable("dbo.Users");
            DropTable("dbo.Role");
            DropTable("dbo.Logins");
        }
    }
}
