namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Guestbooks", "Title", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Guestbooks", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Guestbooks", "AuthorEmail", c => c.String(nullable: false));
            AlterColumn("dbo.Guestbooks", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guestbooks", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Guestbooks", "AuthorEmail", c => c.String());
            AlterColumn("dbo.Guestbooks", "Content", c => c.String());
            AlterColumn("dbo.Guestbooks", "Title", c => c.String());
        }
    }
}
