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

            var training = context.Trainings.FirstOrDefault(t => t.WidgetName == "word-translation");
            if(training != null)
                context.Trainings.Remove(training);

            context.Trainings.AddOrUpdate(t => t.Name, new Common.Entities.Training() {
                Name = "word-translation",
                WidgetName = "steps-training",
                Title = "Word-Translation",
                Config = @"{""wordsInfo"":{""attributes"":[0,1,2,3,5],""toTake"":6,""checkByField"":""translation""},""steps-training"":{""trainingLogic"":""word-translation"",""toTake"":12,""options-training"":{""sampleProperty"":""translation"",""representationProperty"":""representation""}}}"
            },
            new Common.Entities.Training() {
                Name = "translation-word",
                WidgetName = "steps-training",
                Title = "Translation-Word",
                Config = @"{""wordsInfo"":{""attributes"":[0,1,2,3,5],""toTake"":6,""checkByField"":""representation""},""steps-training"":{""trainingLogic"":""translation-word"",""toTake"":12,""options-training"":{""sampleProperty"":""representation"",""representationProperty"":""translation""}}}"
            });
        }
    }
}
