namespace Zanotuj.To.WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeuserforeignkeyv2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Notes", name: "UserId", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Notes", name: "IX_UserId", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Notes", name: "IX_ApplicationUserId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Notes", name: "ApplicationUserId", newName: "UserId");
        }
    }
}
