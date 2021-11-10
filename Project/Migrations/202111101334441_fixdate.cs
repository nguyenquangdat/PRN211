namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contact", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contact", "CreateDate", c => c.String());
        }
    }
}
