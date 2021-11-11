namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepassword : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserPassword", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UserPassword", c => c.String(nullable: false, maxLength: 10, unicode: false));
        }
    }
}
