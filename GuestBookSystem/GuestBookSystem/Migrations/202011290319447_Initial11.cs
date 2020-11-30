namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Guestbooks", "AuthorEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guestbooks", "AuthorEmail", c => c.String(nullable: false));
        }
    }
}
