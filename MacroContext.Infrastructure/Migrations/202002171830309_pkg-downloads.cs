namespace MacroContext.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pkgdownloads : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Downloads", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "Downloads");
        }
    }
}
