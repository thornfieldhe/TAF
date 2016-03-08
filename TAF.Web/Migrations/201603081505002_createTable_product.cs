namespace TAF.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTable_product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        ColorId = c.Guid(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductionDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        Version = c.Binary(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SystemDictionaries", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.SystemDictionaries", t => t.ColorId)
                .Index(t => t.CategoryId)
                .Index(t => t.ColorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ColorId", "dbo.SystemDictionaries");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.SystemDictionaries");
            DropIndex("dbo.Products", new[] { "ColorId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Products");
        }
    }
}
