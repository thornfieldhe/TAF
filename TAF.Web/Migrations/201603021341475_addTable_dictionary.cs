namespace TAF.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable_dictionary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemDictionaries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                        Value1 = c.String(),
                        Value2 = c.String(),
                        Value3 = c.String(),
                        Value4 = c.String(),
                        Value5 = c.String(),
                        Value6 = c.String(),
                        Value7 = c.String(),
                        Value8 = c.String(),
                        Value9 = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        Version = c.Binary(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SystemDictionaries");
        }
    }
}
