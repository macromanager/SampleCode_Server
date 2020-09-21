namespace MacroContext.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MacroProfiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PackageId = c.Guid(nullable: false),
                        MacroPosition = c.Int(nullable: false),
                        ComponentType = c.Int(nullable: false),
                        ComponentName = c.String(),
                        RowVersion = c.Binary(),
                        Macro_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Macros", t => t.Macro_Id, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId)
                .Index(t => t.Macro_Id);
            
            CreateTable(
                "dbo.Macros",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        RowVersion = c.Binary(),
                        Code = c.String(),
                        Description = c.String(),
                        MacroType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Name = c.String(),
                        RowVersion = c.Binary(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReferenceProfiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PackageId = c.Guid(nullable: false),
                        Name = c.String(),
                        ReferenceId = c.Guid(nullable: false),
                        ReferenceVersion_Major = c.Int(nullable: false),
                        ReferenceVersion_Minor = c.Int(nullable: false),
                        RowVersion = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        RowVersion = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReferenceProfiles", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.MacroProfiles", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.MacroProfiles", "Macro_Id", "dbo.Macros");
            DropIndex("dbo.ReferenceProfiles", new[] { "PackageId" });
            DropIndex("dbo.MacroProfiles", new[] { "Macro_Id" });
            DropIndex("dbo.MacroProfiles", new[] { "PackageId" });
            DropTable("dbo.Users");
            DropTable("dbo.ReferenceProfiles");
            DropTable("dbo.Packages");
            DropTable("dbo.Macros");
            DropTable("dbo.MacroProfiles");
        }
    }
}
