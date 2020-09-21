namespace MacroContext.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MacroContext.Infrastructure.Abstractions.Orm.MacroContextDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MacroContext.Infrastructure.Abstractions.Orm.MacroContextDb";
        }

        protected override void Seed(MacroContext.Infrastructure.Abstractions.Orm.MacroContextDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
