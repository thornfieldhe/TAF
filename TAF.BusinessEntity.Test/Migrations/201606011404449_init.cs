namespace TAF.BusinessEntity.Test.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Age = c.Int(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifyBy = c.String(),
                        Version = c.Binary(),
                        Note = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Student_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifyBy = c.String(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Note = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Students",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Student_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
