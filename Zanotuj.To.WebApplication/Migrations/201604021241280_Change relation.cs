namespace Zanotuj.To.WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changerelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HashTags", "NoteId", "dbo.Notes");
            DropIndex("dbo.HashTags", new[] { "NoteId" });
            CreateTable(
                "dbo.NoteHashTags",
                c => new
                    {
                        Note_Id = c.Int(nullable: false),
                        HashTag_Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Note_Id, t.HashTag_Name })
                .ForeignKey("dbo.Notes", t => t.Note_Id, cascadeDelete: true)
                .ForeignKey("dbo.HashTags", t => t.HashTag_Name, cascadeDelete: true)
                .Index(t => t.Note_Id)
                .Index(t => t.HashTag_Name);
            
            DropColumn("dbo.HashTags", "NoteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HashTags", "NoteId", c => c.Int(nullable: false));
            DropForeignKey("dbo.NoteHashTags", "HashTag_Name", "dbo.HashTags");
            DropForeignKey("dbo.NoteHashTags", "Note_Id", "dbo.Notes");
            DropIndex("dbo.NoteHashTags", new[] { "HashTag_Name" });
            DropIndex("dbo.NoteHashTags", new[] { "Note_Id" });
            DropTable("dbo.NoteHashTags");
            CreateIndex("dbo.HashTags", "NoteId");
            AddForeignKey("dbo.HashTags", "NoteId", "dbo.Notes", "Id", cascadeDelete: true);
        }
    }
}
