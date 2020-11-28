namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Name = c.String(),
                        Password = c.String(),
                        SRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.Guestbooks", "User_UserId", c => c.Int());
            CreateIndex("dbo.Guestbooks", "User_UserId");
            AddForeignKey("dbo.Guestbooks", "User_UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guestbooks", "User_UserId", "dbo.Users");
            DropIndex("dbo.Guestbooks", new[] { "User_UserId" });
            DropColumn("dbo.Guestbooks", "User_UserId");
            DropTable("dbo.Users");
        }
    }
}
