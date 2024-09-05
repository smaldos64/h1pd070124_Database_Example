namespace Database1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Database1.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Database1.Models.CodeModelDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Database1.Models.CodeModelDB";
        }

        protected override void Seed(Database1.Models.CodeModelDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            IList<Standard> defaultStandards = new List<Standard>();

            defaultStandards.Add(new Standard() { StandardName = "Standard 1" });
            defaultStandards.Add(new Standard() { StandardName = "Standard 2" });
            defaultStandards.Add(new Standard() { StandardName = "Standard 3" });

            foreach (Standard std in defaultStandards)
                context.Standards.Add(std);

            base.Seed(context);

        }
    }
}
