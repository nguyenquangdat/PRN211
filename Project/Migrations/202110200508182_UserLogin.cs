namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserLogin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserPhone", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UserPhone", c => c.String(nullable: false, maxLength: 11));
        }
    }
}
