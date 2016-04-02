namespace Zanotuj.To.WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHashtag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HashTags",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        NoteId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .Index(t => t.NoteId);
            
            AddColumn("dbo.Notes", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HashTags", "NoteId", "dbo.Notes");
            DropIndex("dbo.HashTags", new[] { "NoteId" });
            DropColumn("dbo.Notes", "Title");
            DropTable("dbo.HashTags");
        }
    }
}
