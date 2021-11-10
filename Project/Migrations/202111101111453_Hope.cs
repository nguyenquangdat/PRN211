namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hope : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Test");
        }
    }
}
