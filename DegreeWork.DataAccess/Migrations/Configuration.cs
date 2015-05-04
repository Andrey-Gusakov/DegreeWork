namespace DegreeWork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DegreeWork.EnglishTutorDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DegreeWork.EnglishTutorDbContext context)
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

            context.Trainings.Add(new Common.Entities.Training() {
                WidgetName = "word-tranlation",
                Title = "Word-Translation",
                Config = @"{""wordsInfo"":{""attributes"":[0,1,2,3,5],""toTake"":10,""checkByField"":""id""}}"
            });
        }
    }
}
