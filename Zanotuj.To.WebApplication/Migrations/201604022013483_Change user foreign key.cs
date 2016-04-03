namespace Zanotuj.To.WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeuserforeignkey : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Notes", new[] { "User_Id" });
            DropColumn("dbo.Notes", "UserId");
            RenameColumn(table: "dbo.Notes", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Notes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Notes", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Notes", new[] { "UserId" });
            AlterColumn("dbo.Notes", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Notes", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Notes", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notes", "User_Id");
        }
    }
}
