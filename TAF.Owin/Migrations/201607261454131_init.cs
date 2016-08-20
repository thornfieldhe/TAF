namespace TAF.Owin.Migrations
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
                "dbo.Logs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Action = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        ModifyBy = c.Guid(nullable: false),
                        Version = c.Binary(),
                        Note = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Log_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        ModifyBy = c.Guid(nullable: false),
                        Version = c.Binary(),
                        Note = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UpdateMigrations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        ModifyBy = c.Guid(nullable: false),
                        Version = c.Binary(),
                        Note = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UpdateMigration_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        ModifyBy = c.Guid(nullable: false),
                        Version = c.Binary(),
                        Note = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserRole_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(),
                        UserName = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ChangedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        ModifyBy = c.Guid(nullable: false),
                        Version = c.Binary(),
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
            DropTable("dbo.UserRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserRole_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.UpdateMigrations",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UpdateMigration_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Roles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Logs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Log_Status", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
