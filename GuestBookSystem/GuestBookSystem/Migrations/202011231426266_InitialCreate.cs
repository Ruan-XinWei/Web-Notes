namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guestbooks",
                c => new
                    {
                        GuestbookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        Content = c.String(nullable: false),
                        AuthorEmail = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GuestbookId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Guestbooks");
        }
    }
}
