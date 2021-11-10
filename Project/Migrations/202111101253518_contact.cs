namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        firtName = c.String(),
                        LastName = c.String(),
                        email = c.String(),
                        Subject = c.String(),
                        Message = c.String(),
                        Status = c.Int(nullable: false),
                        CreateDate = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contact");
        }
    }
}
