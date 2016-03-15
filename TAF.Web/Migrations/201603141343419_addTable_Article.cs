namespace TAF.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable_Article : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        PublishDate = c.DateTime(nullable: false),
                        Content = c.String(),
                        CategoryId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        Version = c.Binary(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SystemDictionaries", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "CategoryId", "dbo.SystemDictionaries");
            DropIndex("dbo.Articles", new[] { "CategoryId" });
            DropTable("dbo.Articles");
        }
    }
}
